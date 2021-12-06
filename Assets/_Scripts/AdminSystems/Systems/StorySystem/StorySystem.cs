using System.Linq;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using CQM.Components;
using CQM.Databases;
using CQM.Systems;


// Handles all the ongoing/completed stories
// It is responsable of starting and finishing all the stories
public class StorySystem : ISystemEvents
{
    private ComponentsContainer<StoryInfoComponent> m_StoriesInfo;
    private Singleton_GameStoriesStateComponent _storiesStateComponent;
    private List<ID> _ongoingStories;
    private List<ID> _primaryStoriesToStart;
    private List<ID> _failedStories;
    private List<ID> _completedStories;
    private List<ID> _finalizedStories;

    private QuestSystem _questSystem = new QuestSystem(); // This could be refactored

    private Event<ID> OnStoryStarted;
    private Event<ID> OnStoryCompleted;
    private Event<ID> OnStoryFailed;
    private Event<ID> OnStoryFinalized;
    private EventVoid OnAllPrimaryStoriesCompleted;
    private EventVoid OnAllPrimaryStoriesFinalized;


    public void Initialize(ComponentsContainer<StoryInfoComponent> storiesInfo,
                           Singleton_GameStoriesStateComponent storiesState)
    {
        m_StoriesInfo = storiesInfo;
        _storiesStateComponent = storiesState;
        _ongoingStories = storiesState.m_OngoingStories;
        _primaryStoriesToStart = storiesState.m_MainStoriesToStartOrder;
        _failedStories = storiesState.m_FailedStories;
        _completedStories = storiesState.m_CompletedStories;
        _finalizedStories = storiesState.m_FinalizedPrimaryStories;
    }

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("story_sys");

        OnStoryStarted = callbacks.AddEvent<ID>(new ID("story_started"));
        OnStoryCompleted = callbacks.AddEvent<ID>(new ID("story_completed"));
        OnStoryFailed = callbacks.AddEvent<ID>(new ID("story_failed"));
        OnStoryFinalized = callbacks.AddEvent<ID>(new ID("story_finalized"));
        OnAllPrimaryStoriesCompleted = callbacks.AddEvent(new ID("all_primary_stories_completed"));
        OnAllPrimaryStoriesFinalized = callbacks.AddEvent(new ID("all_primary_stories_finalized"));

        commands.AddEvent<ID>(new ID("start_story")).OnInvoked += StartStory;
        commands.AddEvent<StorySys_CompleteStoyEvtArgs>(new ID("complete_story")).OnInvoked +=
            (args) => CompleteStory(args.m_StoryId, args.m_QuestData);
        commands.AddEvent<ID>(new ID("fail_story")).OnInvoked += FailStory;
        commands.AddEvent<ID>(new ID("finalize_story")).OnInvoked += FinalizeStory;
    }

    // Called by some in-game conversation that starts
    // a story with the given ID
    public void StartStory(ID storyId)
    {
        StoryInfoComponent story = m_StoriesInfo[storyId];

        if (story.m_State != StoryInfoComponent.State.NotStarted)
            Debug.LogError("Tried to start a story that is already in progress or completed");

        story.m_State = StoryInfoComponent.State.InProgress;
        _ongoingStories.Add(storyId);

        if (_primaryStoriesToStart.Contains(storyId))
            _primaryStoriesToStart.Remove(storyId);
        else
            _storiesStateComponent.m_AvailableSecondaryStoriesToStart.Remove(storyId);

        OnStoryStarted.Invoke(storyId);
    }

    // We assume that the quest passed here 
    // is the one that corresponds with the given story
    // Called by the QuestMaking system when the user
    // has definitely decided that the quest is final
    public void CompleteStory(ID storyId, QuestDataComponent questData)
    {
#if UNITY_EDITOR
        if (!_ongoingStories.Contains(storyId))
            throw new System.Exception("Trying to complete a story that isnt ongoing");
#endif

        StoryInfoComponent story = m_StoriesInfo[storyId];

        _questSystem.GetOverallTag(questData.m_PiecesList, out QPTag.TagType tagType, out int value);

        ID targetId = questData.m_PiecesList.Find(a => a.m_Type == QuestPieceFunctionalComponent.PieceType.Target).m_ID;
        
        ProcessStoryData(story.m_StoryData, tagType, value, targetId, out BranchOption result, out StoryRepercusionComponent rep, out int powerValue);
        story.m_State = StoryInfoComponent.State.Completed;
        story.m_QuestData = questData;
        story.m_QuestBranchResult = result;
        story.m_QuestRepercusion = rep;

        Debug.Assert(questData != null);
        Debug.Assert(result != null);
        Debug.Assert(rep != null);

        _ongoingStories.Remove(storyId);
        //if (value < powerValue && !_storiesStateComponent.m_AllSecondaryStories.Contains(storyId))
        if (value <= 1)
        {
            _failedStories.Add(storyId);
        }
        else
        {
            _completedStories.Add(storyId);
        }
        
        

        OnStoryCompleted.Invoke(storyId);

        if (_primaryStoriesToStart.Count == 0
            && _ongoingStories.Count == 0)
        {
            OnAllPrimaryStoriesCompleted.OnInvoked += () => {
                Debug.Log("All Stories Completed");
            };
            OnAllPrimaryStoriesCompleted.Invoke();
        }
    }

    private void FailStory(ID storyId)
    {
        Debug.Log("FAILSTORY EXECUTED");
        _failedStories.Remove(storyId);

        var s = _storiesStateComponent;
        if (!s.m_AllSecondaryStories.Contains(storyId) && !_primaryStoriesToStart.Contains(storyId))
        {
            //_primaryStoriesToStart.Add(storyId);
            _primaryStoriesToStart.Insert(_primaryStoriesToStart.Count-1, storyId);
            m_StoriesInfo[storyId].m_State = StoryInfoComponent.State.Finalized;

            //La siguiente misión que cargar ha de ser Vacía, no tiene que haber misiones empezadas ni pendientes de completar.
            Debug.Log("Ongoing: " + _ongoingStories.Count);
            Debug.Log("Completed: " + _completedStories.Count);
            if (_primaryStoriesToStart[0].NameID == "" && _ongoingStories.Count == 0)
            {
                Debug.Log("EL ACABOSE");
                Admin.Global.EventSystem.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state").Invoke(GameStateSystem.State.EndGame);
            }

            if (_primaryStoriesToStart.Count == 0
                && _ongoingStories.Count == 0
                && _completedStories.Count == 0)
            {
                OnAllPrimaryStoriesFinalized.OnInvoked += () => {
                    Debug.Log("All Primary Stories Finalized");
                    //Admin.Global.EventSystem.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state").Invoke(GameStateSystem.State.EndGame);
                };
                OnAllPrimaryStoriesFinalized.Invoke();
            }
        }
        else
        {
            s.m_AvailableSecondaryStoriesToStart.Add(storyId);
            m_StoriesInfo[storyId].m_State = StoryInfoComponent.State.NotStarted;
        }

        OnStoryFailed.Invoke(storyId);
    }

    // This is called when the Dialogue System
    // the player the "Ending"(text) of the story
    private void FinalizeStory(ID storyId)
    {
        Debug.Log("FINALIZESTORY EXECUTED");
        _completedStories.Remove(storyId);

        var s = _storiesStateComponent;
        if (!s.m_AllSecondaryStories.Contains(storyId))
        {
            _finalizedStories.Add(storyId);
            m_StoriesInfo[storyId].m_State = StoryInfoComponent.State.Finalized;

            //La siguiente misión que cargar ha de ser Vacía, no tiene que haber misiones empezadas ni pendientes de completar.
            Debug.Log("Ongoing: " + _ongoingStories.Count);
            Debug.Log("Completed: " + _completedStories.Count);
            if (_primaryStoriesToStart[0].NameID == "" && _ongoingStories.Count == 0)
            {
                Debug.Log("EL ACABOSE");
                Admin.Global.EventSystem.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state").Invoke(GameStateSystem.State.EndGame);
            }

            if (_primaryStoriesToStart.Count == 0
                && _ongoingStories.Count == 0
                && _completedStories.Count == 0)
            {
                OnAllPrimaryStoriesFinalized.OnInvoked += () => { Debug.Log("All Primary Stories Finalized");
                    //Admin.Global.EventSystem.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state").Invoke(GameStateSystem.State.EndGame);
                };
                OnAllPrimaryStoriesFinalized.Invoke();
            }
        }
        else
        {
            s.m_AvailableSecondaryStoriesToStart.Add(storyId);
            m_StoriesInfo[storyId].m_State = StoryInfoComponent.State.NotStarted;
        }

        OnStoryFinalized.Invoke(storyId);
    }

    // Process a story with the given tag and value and get the result
    private void ProcessStoryData(StoryData data, QPTag.TagType tag, int value, ID targetId, out BranchOption result, out StoryRepercusionComponent rep, out int powerValue)
    {
        result = null;
        rep = null;
        powerValue = 0;
        bool match = false;
        for (int i = 0; i < data.m_BranchOptions.Count; i++)
        {
            if (ProcessBranchOption(data.m_BranchOptions[i], tag, value, targetId, out result, out rep, out powerValue))
            {
                match = true;
                break;
            }
        }

        #region UNITY_EDITOR
#if UNITY_EDITOR
        if (!match)
        {
            throw new System.Exception("There is an error with the story response structure");
        }
#endif
        #endregion
    }

    // Process a Branch Option
    private bool ProcessBranchOption(BranchOption branchOpt, QPTag.TagType tag, int value, ID targetId, out BranchOption result, out StoryRepercusionComponent rep, out int powerValue)
    {
        bool match = CheckCondition(branchOpt.m_Condition, tag, value, targetId, out powerValue);
        //powerValue = branchOpt.m_Condition.m_Power;
        
        if (match)
        {
            result = branchOpt;
            rep = branchOpt.m_Repercusion;
        }
        else
        {
            result = null;
            rep = null;
        }
        return match;
    }

    // Check if a Branch Condition Is Met
    private bool CheckCondition(BranchCondition bCondition, QPTag.TagType tag, int value, ID targetId, out int powerValue)
    {
        powerValue = 0;
        if (tag == bCondition.m_Tag
            && value >= bCondition.m_Value
            && bCondition.m_Target == targetId)
        {
            powerValue = bCondition.m_Power;
            Debug.LogWarning("TARGET " + targetId);
            Debug.LogWarning("POWER VALUE " + powerValue);
            return true;
        }
        else
            return false;
    }
}
