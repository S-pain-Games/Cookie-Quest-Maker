using System.Linq;
using System.Collections;
using UnityEngine;


// Handles all the ongoing/completed stories
// It is responsable of starting and finishing all the stories
public class StorySystem : MonoBehaviour
{
    // Initialized Through Admin
    private StoryDB _storyDB;
    private QuestSystem _questSystem = new QuestSystem(); // This could be refactored

    public void Initialize(StoryDB storyDb)
    {
        _storyDB = storyDb;
    }

    // Called by some in-game conversation that starts
    // a story with the given ID
    public void StartStory(int storyId)
    {
        Story story = _storyDB.m_StoriesDB[storyId];

        if (story.m_State != Story.State.NotStarted)
            Debug.LogError("Tried to start a story that is already in progress or completed");

        story.m_State = Story.State.InProgress;
        _storyDB.m_OngoingStories.Add(storyId);
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

        Story story = _storyDB.m_StoriesDB[storyId];
        _storyDB.m_OngoingStories.Remove(storyId);
        _storyDB.m_CompletedStories.Add(storyId);
        _storyDB.OnStoryCompleted?.Invoke(_storyDB.m_CompletedStories.Count);

        _questSystem.GetOverallTag(questData.m_PiecesList, out QPTag.TagType tagType, out int value);
        ProcessStoryData(story.m_StoryData, tagType, value, out string result, out StoryRepercusion rep);
        story.m_State = Story.State.Completed;
        story.m_QuestData = questData;
        story.m_QuestResult = result;
        story.m_QuestRepercusion = rep;

        Debug.Log(story.m_QuestResult);
    }

    // This is called when the Dialogue System or the newspaper shows
    // the player the "Ending"(text) of the story
    public void FinalizeStory(int storyId)
    {
        Story story = _storyDB.m_StoriesDB[storyId];
        _storyDB.m_CompletedStories.Remove(storyId);
        _storyDB.m_FinalizedStories.Add(storyId);
    }

    // Process a story with the given tag and value and get the result
    private void ProcessStoryData(StoryData data, QPTag.TagType tag, int value, out string result, out StoryRepercusion rep)
    {
        result = "";
        rep = null;
        bool match = false;
        for (int i = 0; i < data.m_BranchOptions.Count; i++)
        {
            if (ProcessBranchOption(data.m_BranchOptions[i], tag, value, out result, out rep))
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
    private bool ProcessBranchOption(BranchOption branchOpt, QPTag.TagType tag, int value, out string result, out StoryRepercusion rep)
    {
        bool match = CheckCondition(branchOpt.m_Condition, tag, value);
        if (match)
        {
            result = branchOpt.m_Result;
            rep = branchOpt.m_Repercusion;
        }
        else
        {
            result = "";
            rep = null;
        }
        return match;
    }

    // Check if a Branch Condition Is Met
    private bool CheckCondition(BranchCondition bCondition, QPTag.TagType tag, int value)
    {
        if (tag == bCondition.m_Tag)
        {
            if (value >= bCondition.m_Value)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }
}
