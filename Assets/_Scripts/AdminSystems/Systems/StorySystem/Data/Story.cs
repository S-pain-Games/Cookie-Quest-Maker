using UnityEngine;
//Runtime representation of a ongoing/completed story and its state
//Currently its only created by the Story System when a new story its started
//Unstarted stories dont have this representation

[System.Serializable]
public class Story
{
    public StoryData m_StoryData; // Persistent Story Data
    public QuestData m_QuestData; // The quest that was created to complete the story
    public State m_State = State.NotStarted;
    public string m_QuestResult = ""; // The final result of the story given the quest

    public enum State
    {
        NotStarted,
        InProgress,
        Completed
    }
}