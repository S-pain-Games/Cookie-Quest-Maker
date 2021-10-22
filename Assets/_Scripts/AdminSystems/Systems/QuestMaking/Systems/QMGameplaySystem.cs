using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Entry point to the Quest Making Gameplay System
public class QMGameplaySystem : MonoBehaviour
{
    // References to Systems and Dbs
    private StorySystem _storySystem;
    private StoryDB _storyDB;

    private QMGameplayData m_Data = new QMGameplayData();

    public void Initialize(StorySystem storySys, StoryDB storyDb)
    {
        _storySystem = storySys;
        _storyDB = storyDb;
    }

    // These methods have to be called in a specific order
    // SelectStory -> StartBuildingQuest -> Add/Remove Pieces -> Finish Making Quest

    public void SelectStory(int storyId)
    {
        m_Data.m_StoryID = storyId;
    }

    [MethodButton]
    public void StartBuildingQuest()
    {
        m_Data.m_CurrentQuest = new QuestData();
    }

    [MethodButton]
    public void StartStory()
    {
        SelectStory("test".GetHashCode());
    }

    public void AddPiece(QuestPiece piece)
    {
        m_Data.m_CurrentQuest.m_PiecesList.Add(piece);
    }

    public void RemovePiece(QuestPiece piece)
    {
        m_Data.m_CurrentQuest.m_PiecesList.Remove(piece);
    }

    [MethodButton]
    public void FinishMakingQuest()
    {
        _storySystem.CompleteStory(m_Data.m_StoryID, m_Data.m_CurrentQuest);
    }
}

public class QMGameplayData
{
    public QuestData m_CurrentQuest;
    public int m_StoryID;
}