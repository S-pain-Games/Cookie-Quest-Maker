using UnityEngine;
//Runtime representation of a ongoing/completed story and its state
//Currently its only created by the Story System when a new story its started
//Unstarted stories dont have this representation

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
    [SerializeField]
    private Quest m_Quest; // The quest that was created to complete the story
    [SerializeField]
    private string m_QuestResult = ""; // The final result of the story given the quest

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