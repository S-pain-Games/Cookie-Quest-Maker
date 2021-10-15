using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entry point to the Quest Making Gameplay
/// </summary>
public class QuestMaker
{
    [SerializeField]
    private StorySystem m_StorySystem;

    [SerializeField]
    private QuestBuilder m_QuestBuilder;

    public IReadOnlyList<Story> GetAvailableStories()
    {
        return m_StorySystem.OngoingStories;
    }

    /// <param name="index">Index of the story in ongoing stories list</param>
    public void SelectStory(int index)
    {
    }

    public void OnQuestCompleted(Quest quest)
    {

    }
}
