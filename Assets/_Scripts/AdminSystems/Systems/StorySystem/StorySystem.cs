using System.Linq;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using CQM.Components;
using CQM.Databases;


// Handles all the ongoing/completed stories
// It is responsable of starting and finishing all the stories
public class StorySystem : ISystemEvents
{
    private ComponentsContainer<StoryInfoComponent> m_StoriesInfo;
    private List<ID> _ongoingStories;
    private List<ID> _storiesToStart;
    private List<ID> _completedStories;
    private List<ID> _finalizedStories;

    private QuestSystem _questSystem = new QuestSystem(); // This could be refactored

    private Event<ID> OnStoryStarted;
    private Event<ID> OnStoryCompleted;
    private Event<ID> OnStoryFinalized;
    private EventVoid OnAllStoriesCompleted;
    private EventVoid OnAllStoriesFinalized;


    public void Initialize(ComponentsContainer<StoryInfoComponent> storiesInfo,
                           List<ID> ongoingStories,
                           List<ID> storiesToStart,
                           List<ID> completedStories,
                           List<ID> finalizedStories)
    {
        m_StoriesInfo = storiesInfo;
        _ongoingStories = ongoingStories;
        _storiesToStart = storiesToStart;
        _completedStories = completedStories;
        _finalizedStories = finalizedStories;
    }

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("story_sys");

        OnStoryStarted = callbacks.AddEvent<ID>(new ID("story_started"));
        OnStoryCompleted = callbacks.AddEvent<ID>(new ID("story_completed"));
        OnStoryFinalized = callbacks.AddEvent<ID>(new ID("story_finalized"));
        OnAllStoriesCompleted = callbacks.AddEvent(new ID("all_stories_completed"));
        OnAllStoriesFinalized = callbacks.AddEvent(new ID("all_stories_finalized"));

        commands.AddEvent<ID>(new ID("start_story")).OnInvoked += StartStory;
        commands.AddEvent<StorySys_CompleteStoyEvtArgs>(new ID("complete_story")).OnInvoked +=
            (args) => CompleteStory(args.m_StoryId, args.m_QuestData);
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
        _storiesToStart.Remove(storyId);

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
        _ongoingStories.Remove(storyId);
        _completedStories.Add(storyId);

        _questSystem.GetOverallTag(questData.m_PiecesList, out QPTag.TagType tagType, out int value);

        ID targetId = questData.m_PiecesList.Find(a => a.m_Type == QuestPieceFunctionalComponent.PieceType.Target).m_ID;

        ProcessStoryData(story.m_StoryData, tagType, value, targetId, out BranchOption result, out StoryRepercusionComponent rep);
        story.m_State = StoryInfoComponent.State.Completed;
        story.m_QuestData = questData;
        story.m_QuestBranchResult = result;
        story.m_QuestRepercusion = rep;

        Debug.Assert(questData != null);
        Debug.Assert(result != null);
        Debug.Assert(rep != null);

        OnStoryCompleted.Invoke(storyId);

        if (_storiesToStart.Count == 0
            && _ongoingStories.Count == 0)
        {
            OnAllStoriesCompleted.OnInvoked += () => { Debug.Log("All Stories Completed"); };
            OnAllStoriesCompleted.Invoke();
        }
    }

    // This is called when the Dialogue System or the newspaper shows
    // the player the "Ending"(text) of the story
    private void FinalizeStory(ID storyId)
    {
        _completedStories.Remove(storyId);
        _finalizedStories.Add(storyId);

        OnStoryFinalized.Invoke(storyId);

        if (_storiesToStart.Count == 0
            && _ongoingStories.Count == 0
            && _completedStories.Count == 0)
        {
            OnAllStoriesFinalized.OnInvoked += () => { Debug.Log("All Stories Finalized"); };
            OnAllStoriesFinalized.Invoke();
        }
    }

    // Process a story with the given tag and value and get the result
    private void ProcessStoryData(StoryData data, QPTag.TagType tag, int value, ID targetId, out BranchOption result, out StoryRepercusionComponent rep)
    {
        result = null;
        rep = null;
        bool match = false;
        for (int i = 0; i < data.m_BranchOptions.Count; i++)
        {
            if (ProcessBranchOption(data.m_BranchOptions[i], tag, value, targetId, out result, out rep))
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
    private bool ProcessBranchOption(BranchOption branchOpt, QPTag.TagType tag, int value, ID targetId, out BranchOption result, out StoryRepercusionComponent rep)
    {
        bool match = CheckCondition(branchOpt.m_Condition, tag, value, targetId);
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
    private bool CheckCondition(BranchCondition bCondition, QPTag.TagType tag, int value, ID targetId)
    {
        if (tag == bCondition.m_Tag
            && value >= bCondition.m_Value
            && bCondition.m_Target == targetId)
            return true;
        else
            return false;
    }
}
