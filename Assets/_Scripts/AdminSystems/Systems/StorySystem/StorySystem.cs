using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles all the ongoing/completed stories
// It is responsable of starting and finishing all the stories
public class StorySystem : MonoBehaviour
{
    // Initialized Through Admin
    [HideInInspector]
    public StoryDB storyDB;
    [HideInInspector]
    public QuestSystem questSystem = new QuestSystem();

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

public class StoryDB
{
    public Dictionary<int, Story> m_StoriesDB = new Dictionary<int, Story>();
    public List<Story> m_OngoingStories = new List<Story>();
    public List<Story> m_CompletedStories = new List<Story>();

    public Dictionary<int, StoryRepercusion> m_Repercusions = new Dictionary<int, StoryRepercusion>();

    public StoryDB()
    {
        LoadData();
    }

    public void LoadData()
    {
        LoadStoryRepercusions();

        Story s = new Story();
        StoryData sData = new StoryData
        {
            m_Title = "The Introductory Madness",
            m_IntroductionPhrase = "Test Story Introduction"
        };

        // Convince Condition
        BranchOption bOpt = new BranchOption
        {
            m_Repercusion = m_Repercusions["center_wolf_dead".GetHashCode()],
            m_Result = "Test Story Totally Completed Convincingly"
        };
        BranchCondition bCon = new BranchCondition
        {
            m_Tag = QPTag.TagType.Convince,
            m_Value = 1
        };
        bOpt.m_Condition = bCon;
        sData.m_BranchOptions.Add(bOpt);

        // Help Condition
        bOpt = new BranchOption
        {
            m_Repercusion = m_Repercusions["center_wolf_dead".GetHashCode()],
            m_Result = "Test Story Totally Completed Helpingly"
        };
        bCon = new BranchCondition
        {
            m_Tag = QPTag.TagType.Help,
            m_Value = 1
        };
        bOpt.m_Condition = bCon;
        sData.m_BranchOptions.Add(bOpt);

        // Harm Condition
        bOpt = new BranchOption
        {
            m_Repercusion = m_Repercusions["center_wolf_dead".GetHashCode()],
            m_Result = "Test Story Totally Completed Harmingly"
        };
        bCon = new BranchCondition
        {
            m_Tag = QPTag.TagType.Harm,
            m_Value = 1
        };
        bOpt.m_Condition = bCon;
        sData.m_BranchOptions.Add(bOpt);

        s.m_StoryData = sData;
        sData.Build();

        m_StoriesDB.Add("test".GetHashCode(), s);
    }

    private void LoadStoryRepercusions()
    {
        var rep = new StoryRepercusion();
        m_Repercusions.Add("center_wolf_dead".GetHashCode(), rep);
    }
}