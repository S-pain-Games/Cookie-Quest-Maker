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
    // SelectStory -> Add/Remove Pieces -> Finish Making Quest
    public void SelectStory(int storyId)
    {
        // We generate garbage when selecting a story multiple times
        // but wachugonadu its not a priority right now
        m_Data = new QMGameplayData();
        m_Data.m_StoryID = storyId;
        m_Data.m_CurrentQuest = new QuestData();
    }

    public void AddPiece(QuestPiece piece)
    {
        m_Data.m_CurrentQuest.m_PiecesList.Add(piece);

        switch (piece.m_Type)
        {
            case QuestPiece.PieceType.Action:
                m_Data.m_ActionAdded = true;
                break;
            case QuestPiece.PieceType.Target:
                m_Data.m_TargetAdded = true;
                break;
            case QuestPiece.PieceType.Cookie:
                m_Data.m_CookieAdded = true;
                break;
            default:
                break;
        }
    }

    public void RemovePiece(QuestPiece piece)
    {
        m_Data.m_CurrentQuest.m_PiecesList.Remove(piece);

        switch (piece.m_Type)
        {
            case QuestPiece.PieceType.Action:
                m_Data.m_ActionAdded = false;
                break;
            case QuestPiece.PieceType.Target:
                m_Data.m_TargetAdded = false;
                break;
            case QuestPiece.PieceType.Cookie:
                m_Data.m_CookieAdded = false;
                break;
            default:
                break;
        }
    }

    public bool TryFinishMakingQuest()
    {
        if (m_Data.m_CookieAdded && m_Data.m_ActionAdded && m_Data.m_TargetAdded)
        {
            _storySystem.CompleteStory(m_Data.m_StoryID, m_Data.m_CurrentQuest);
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class QMGameplayData
{
    public QuestData m_CurrentQuest;
    public bool m_CookieAdded;
    public bool m_TargetAdded;
    public bool m_ActionAdded;
    public int m_StoryID;
}