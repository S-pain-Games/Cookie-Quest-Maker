using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySystem
{
    public IReadOnlyList<Story> OngoingStories => m_OngoingStories;

    [SerializeField]
    private List<Story> m_OngoingStories;
    [SerializeField]
    private List<Story> m_CompletedStories;

    public StorySystem()
    {
        m_OngoingStories = new List<Story>();
        m_CompletedStories = new List<Story>();
    }

    // Called by some in-game conversation that starts
    // a story with the given data
    public Story StartStory(StoryData storyData)
    {
        Story story = new Story(storyData);
        m_OngoingStories.Add(story);
        return story;
    }

    // We assume that the quest passed here 
    // is the one that corresponds with the given story
    // Called by the QuestMaking system when the user
    // has definitely decided that the quest is final
    public void CompleteQuest(StoryData storyData, Quest quest)
    {
        Story s = m_OngoingStories.Find((s) => s.Data == storyData);
        m_OngoingStories.Remove(s);
        m_CompletedStories.Add(s);
        s.Complete(quest);
        Debug.Log(s.Result);
    }
}

/// <summary>
/// Runtime representation of a story and its state
/// </summary>
public class Story
{
    public StoryData Data => m_StoryData;
    public string Result => m_Result;

    [SerializeField]
    private StoryData m_StoryData;

    private bool m_Completed = false;

    /// <summary>
    /// The quest that was created to complete the story
    /// </summary>
    private Quest m_Quest;

    /// <summary>
    /// The final result of the story given the quest
    /// </summary>
    private string m_Result = "";

    public Story(StoryData storyData)
    {
        m_StoryData = storyData;
    }

    public void Complete(Quest quest)
    {
        quest.GetValue(out QuestPieceTagType tagType, out int value);
        m_StoryData.Check(tagType, value, out string result);

        m_Result = result;
        m_Completed = true;
        m_Quest = quest;
    }
}