using System.Linq;
using System.Collections;
using UnityEngine;


// Handles all the ongoing/completed stories
// It is responsable of starting and finishing all the stories
public class StorySystem : MonoBehaviour
{
    // Initialized Through Admin
    [HideInInspector]
    public StoryDB storyDB;
    [HideInInspector]
    public QuestSystem questSystem = new QuestSystem(); // This could be refactored

    // Called by some in-game conversation that starts
    // a story with the given data
    public void StartStory(int storyId)
    {
        Story story = storyDB.m_StoriesDB[storyId];
        story.m_State = Story.State.InProgress;
        storyDB.m_OngoingStories.Add(story);
    }

    // We assume that the quest passed here 
    // is the one that corresponds with the given story
    // Called by the QuestMaking system when the user
    // has definitely decided that the quest is final
    public void CompleteStory(int storyId, QuestData questData)
    {
        Story story = storyDB.m_StoriesDB[storyId];
        storyDB.m_OngoingStories.Remove(story);
        storyDB.m_CompletedStories.Add(story);

        questSystem.GetOverallTag(questData.m_PiecesList, out QPTag.TagType tagType, out int value);
        ProcessStoryData(story.m_StoryData, tagType, value, out string result);
        story.m_QuestResult = result;
        story.m_State = Story.State.Completed;
        story.m_QuestData = questData;

        Debug.Log(story.m_QuestResult);
    }

    // Process a story with the given tag and value and get the result
    private void ProcessStoryData(StoryData data, QPTag.TagType tag, int value, out string result)
    {
        result = "";
        bool match = false;
        for (int i = 0; i < data.m_BranchOptions.Count; i++)
        {
            if (ProcessBranchOption(data.m_BranchOptions[i], tag, value, out result)) //data.m_BranchOptions[i].Check(tag, value, out result))
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
    private bool ProcessBranchOption(BranchOption branchOpt, QPTag.TagType tag, int value, out string result)
    {
        bool match = CheckCondition(branchOpt.m_Condition, tag, value);
        if (match)
            result = branchOpt.m_Result;
        else
            result = "";
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
