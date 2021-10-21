using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles all the ongoing/completed stories
// It is responsable of starting and finishing all the stories
public class StorySystem : MonoBehaviour
{
    public List<Story> OngoingStories => m_OngoingStories;
    public List<Story> CompletedStories => m_CompletedStories;

    private List<Story> m_OngoingStories = new List<Story>();
    private List<Story> m_CompletedStories = new List<Story>();

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
    public void CompleteStory(StoryData storyData, Quest quest)
    {
        Story s = m_OngoingStories.Find((s) => s.Data == storyData);
        m_OngoingStories.Remove(s);
        m_CompletedStories.Add(s);
        s.Complete(quest);
        Debug.Log(s.QuestResult);
    }
}
