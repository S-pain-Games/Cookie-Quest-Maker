using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Entry point to the Quest Making Gameplay
public class QuestMakerSystem
{
    [SerializeField]
    private StorySystem _storySystem;

    [SerializeField]
    private Quest m_CurrentQuest = new Quest();
    private Story _currentStory;

    public QuestMakerSystem()
    {
        _storySystem = Admin.g_Instance.storySystem;
    }

    public void SelectStory(int index)
    {
        _currentStory = _storySystem.OngoingStories[index];
    }

    public void StartBuildingQuest()
    {
        m_CurrentQuest = new Quest();
    }

    public void AddPiece(QuestPiece piece)
    {
        m_CurrentQuest.AddPiece(piece);
    }

    public void RemovePiece(QuestPiece piece)
    {
        m_CurrentQuest.RemovePiece(piece);
    }

    public void FinishMakingQuest()
    {
        _storySystem.CompleteStory(_currentStory.Data, m_CurrentQuest);
    }
}
