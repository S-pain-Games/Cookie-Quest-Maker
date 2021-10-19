using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Entry point to the Quest Making Gameplay
public class QuestMakerSystem : MonoBehaviour
{
    private StorySystem _storySystem;
    private Quest m_CurrentQuest = new Quest();
    private Story _currentStory;

    public void Awake()
    {
        _storySystem = Admin.g_Instance.storySystem;
    }

    // These methods have to be called in a specific order
    // SelectStory -> StartBuildingQuest -> Add/Remove Pieces -> Finish Making Quest

    public void SelectStory(int index)
    {
        _currentStory = _storySystem.OngoingStories[index];
    }

    public void StartBuildingQuest()
    {
        if (_currentStory == null)
            throw new MissingReferenceException();
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
