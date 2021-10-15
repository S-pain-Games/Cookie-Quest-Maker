using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Handles all the ongoing/completed stories</para>
/// <para>It is responsable of starting and finishing all the stories</para>
/// </summary>
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
    public void StartStory(StoryData storyData)
    {
        Story story = new Story(storyData);
        m_OngoingStories.Add(story);
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
        Debug.Log(s.QuestResult);
    }
}

/// <summary>
/// <para>Runtime representation of a ongoing/completed story and its state</para>
/// <para>Currently its only created by the Story System when a new story its started</para>
/// <para>Unstarted stories dont have this representation</para>
/// </summary>
[System.Serializable]
public class Story
{
    public StoryData Data => m_StoryData;
    public string QuestResult => m_QuestResult;
    public bool Completed => m_Completed;

    [SerializeField]
    private StoryData m_StoryData;

    [SerializeField]
    private bool m_Completed = false;

    /// <summary>
    /// The quest that was created to complete the story
    /// </summary>
    [SerializeField]
    private Quest m_Quest;

    /// <summary>
    /// The final result of the story given the quest
    /// </summary>
    [SerializeField]
    private string m_QuestResult = "";

    public Story(StoryData storyData)
    {
        m_StoryData = storyData;
    }

    public void Complete(Quest quest)
    {
        quest.GetOverallTag(out QuestPieceTagType tagType, out int value);
        m_StoryData.Check(tagType, value, out string result);

        m_QuestResult = result;
        m_Completed = true;
        m_Quest = quest;
    }
}