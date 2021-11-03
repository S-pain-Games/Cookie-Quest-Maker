using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Entry point to the Quest Making Gameplay System
public class QuestMakingSystem : ISystemEvents
{
    private QMGameplayData m_Data = new QMGameplayData();
    public Event<StorySys_CompleteStoyEvtArgs> _completeStoryCmd;

    public void Initialize()
    {
        var evtSys = Admin.Global.EventSystem;
        _completeStoryCmd = evtSys.GetCommandByName<Event<StorySys_CompleteStoyEvtArgs>>("story_sys", "complete_story");
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
            _completeStoryCmd.Invoke(new StorySys_CompleteStoyEvtArgs(m_Data.m_StoryID, m_Data.m_CurrentQuest));
            //_storySystem.CompleteStory(m_Data.m_StoryID, m_Data.m_CurrentQuest);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = "quest_making_sys".GetHashCode();

        commands.AddEvent<int>("select_story".GetHashCode()).OnInvoked += SelectStory;
        commands.AddEvent<QuestPiece>("add_piece".GetHashCode()).OnInvoked += AddPiece;
        commands.AddEvent<QuestPiece>("remove_piece".GetHashCode()).OnInvoked += RemovePiece;
    }
}

public struct StorySys_CompleteStoyEvtArgs
{
    public int m_StoryId;
    public QuestData m_QuestData;

    public StorySys_CompleteStoyEvtArgs(int storyId, QuestData questData)
    {
        m_StoryId = storyId;
        m_QuestData = questData;
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