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
    // Initialized Through Admin
    private StoryDB _storyDB;
    private QuestSystem _questSystem = new QuestSystem(); // This could be refactored

    private Event<int> OnStoryStarted;
    private Event<int> OnStoryCompleted;
    private Event<int> OnStoryFinalized;
    private EventVoid OnAllStoriesCompleted;
    private EventVoid OnAllStoriesFinalized;

    public void Initialize(StoryDB storyDb)
    {
        _storyDB = storyDb;
    }

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = "story_sys".GetHashCode();

        OnStoryStarted = callbacks.AddEvent<int>("story_started".GetHashCode());
        OnStoryCompleted = callbacks.AddEvent<int>("story_completed".GetHashCode());
        OnStoryFinalized = callbacks.AddEvent<int>("story_finalized".GetHashCode());
        OnAllStoriesCompleted = callbacks.AddEvent("all_stories_completed".GetHashCode());
        OnAllStoriesFinalized = callbacks.AddEvent("all_stories_finalized".GetHashCode());

        commands.AddEvent<int>("start_story".GetHashCode()).OnInvoked += StartStory;
        commands.AddEvent<StorySys_CompleteStoyEvtArgs>("complete_story".GetHashCode()).OnInvoked +=
            (args) => CompleteStory(args.m_StoryId, args.m_QuestData);
        commands.AddEvent<int>("finalize_story".GetHashCode()).OnInvoked += FinalizeStory;
    }

    // Called by some in-game conversation that starts
    // a story with the given ID
    public void StartStory(int storyId)
    {
        Story story = _storyDB.GetStoryComponent<Story>(storyId);

        if (story.m_State != Story.State.NotStarted)
            Debug.LogError("Tried to start a story that is already in progress or completed");

        story.m_State = Story.State.InProgress;
        _storyDB.m_OngoingStories.Add(storyId);
        _storyDB.m_StoriesToStart.Remove(storyId);

        OnStoryStarted.Invoke(storyId);
    }

    // We assume that the quest passed here 
    // is the one that corresponds with the given story
    // Called by the QuestMaking system when the user
    // has definitely decided that the quest is final
    public void CompleteStory(int storyId, QuestData questData)
    {
#if UNITY_EDITOR
        if (!_storyDB.m_OngoingStories.Contains(storyId))
            throw new System.Exception("Trying to complete a story that isnt ongoing");
#endif

        Story story = _storyDB.GetStoryComponent<Story>(storyId);
        _storyDB.m_OngoingStories.Remove(storyId);
        _storyDB.m_CompletedStories.Add(storyId);

        _questSystem.GetOverallTag(questData.m_PiecesList, out QPTag.TagType tagType, out int value);

        int targetId = questData.m_PiecesList.Find(a => a.m_Type == QuestPiece.PieceType.Target).m_ParentID;

        ProcessStoryData(story.m_StoryData, tagType, value, targetId, out List<string> result, out StoryRepercusion rep);
        story.m_State = Story.State.Completed;
        story.m_QuestData = questData;
        story.m_QuestResult = result;
        story.m_QuestRepercusion = rep;

        OnStoryCompleted.Invoke(storyId);

        if (_storyDB.m_StoriesToStart.Count == 0
            && _storyDB.m_OngoingStories.Count == 0)
        {
            OnAllStoriesCompleted.OnInvoked += () => { Debug.Log("All Stories Completed"); };
            OnAllStoriesCompleted.Invoke();
        }
    }

    // This is called when the Dialogue System or the newspaper shows
    // the player the "Ending"(text) of the story
    private void FinalizeStory(int storyId)
    {
        _storyDB.m_CompletedStories.Remove(storyId);
        _storyDB.m_FinalizedStories.Add(storyId);

        OnStoryFinalized.Invoke(storyId);

        if (_storyDB.m_StoriesToStart.Count == 0
            && _storyDB.m_OngoingStories.Count == 0
            && _storyDB.m_CompletedStories.Count == 0)
        {
            OnAllStoriesFinalized.OnInvoked += () => { Debug.Log("All Stories Finalized"); };
            OnAllStoriesFinalized.Invoke();
        }
    }

    // Process a story with the given tag and value and get the result
    private void ProcessStoryData(StoryData data, QPTag.TagType tag, int value, int targetId, out List<string> result, out StoryRepercusion rep)
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
    private bool ProcessBranchOption(BranchOption branchOpt, QPTag.TagType tag, int value, int targetId, out List<string> result, out StoryRepercusion rep)
    {
        bool match = CheckCondition(branchOpt.m_Condition, tag, value, targetId);
        if (match)
        {
            result = branchOpt.m_Result;
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
    private bool CheckCondition(BranchCondition bCondition, QPTag.TagType tag, int value, int targetId)
    {
        if (tag == bCondition.m_Tag
            && value >= bCondition.m_Value
            && bCondition.m_Target == targetId)
            return true;
        else
            return false;
    }
}
