using CQM.Components;
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
    public void SelectStory(ID storyId)
    {
        // We generate garbage when selecting a story multiple times
        // but wachugonadu its not a priority right now
        m_Data = new QMGameplayData();
        m_Data.m_StoryID = storyId;
        m_Data.m_CurrentQuest = new QuestDataComponent();
    }

    public void AddPiece(QuestPieceFunctionalComponent piece)
    {
        m_Data.m_CurrentQuest.m_PiecesList.Add(piece);

        switch (piece.m_Type)
        {
            case QuestPieceFunctionalComponent.PieceType.Action:
                m_Data.m_ActionAdded = true;
                break;
            case QuestPieceFunctionalComponent.PieceType.Target:
                m_Data.m_TargetAdded = true;
                break;
            case QuestPieceFunctionalComponent.PieceType.Cookie:
                m_Data.m_CookieAdded = true;
                break;
            default:
                break;
        }
    }

    public void RemovePiece(QuestPieceFunctionalComponent piece)
    {
        m_Data.m_CurrentQuest.m_PiecesList.Remove(piece);

        switch (piece.m_Type)
        {
            case QuestPieceFunctionalComponent.PieceType.Action:
                m_Data.m_ActionAdded = false;
                break;
            case QuestPieceFunctionalComponent.PieceType.Target:
                m_Data.m_TargetAdded = false;
                break;
            case QuestPieceFunctionalComponent.PieceType.Cookie:
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

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("quest_making_sys");

        commands.AddEvent<ID>(new ID("select_story")).OnInvoked += SelectStory;
        commands.AddEvent<QuestPieceFunctionalComponent>(new ID("add_piece")).OnInvoked += AddPiece;
        commands.AddEvent<QuestPieceFunctionalComponent>(new ID("remove_piece")).OnInvoked += RemovePiece;
    }
}

public struct StorySys_CompleteStoyEvtArgs
{
    public ID m_StoryId;
    public QuestDataComponent m_QuestData;

    public StorySys_CompleteStoyEvtArgs(ID storyId, QuestDataComponent questData)
    {
        m_StoryId = storyId;
        m_QuestData = questData;
    }
}

public class QMGameplayData
{
    public QuestDataComponent m_CurrentQuest;
    public bool m_CookieAdded;
    public bool m_TargetAdded;
    public bool m_ActionAdded;
    public ID m_StoryID;
}