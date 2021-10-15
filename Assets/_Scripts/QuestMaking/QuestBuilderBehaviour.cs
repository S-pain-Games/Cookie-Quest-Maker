using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class QuestBuilderBehaviour : MonoBehaviour
{
    public QuestBuilder Builder => m_QuestBuilder;

    [Header("Listening To")]
    [SerializeField]
    private VoidEventHandle _startMakingQuest;
    [SerializeField]
    private QuestPieceEventHandle _addPiece;
    [SerializeField]
    private QuestPieceEventHandle _removePiece;
    [SerializeField]
    private VoidEventHandle _finishMakingQuest;

    [SerializeField]
    private QuestBuilder m_QuestBuilder = new QuestBuilder();

    private void OnEnable()
    {
        _startMakingQuest.OnEvent += m_QuestBuilder.StartEmptyQuest;
        _addPiece.OnEvent += m_QuestBuilder.AddPiece;
        _removePiece.OnEvent += m_QuestBuilder.RemovePiece;
        _finishMakingQuest.OnEvent += m_QuestBuilder.FinishMakingQuest;
    }

    private void OnDisable()
    {
        _startMakingQuest.OnEvent -= m_QuestBuilder.StartEmptyQuest;
        _addPiece.OnEvent -= m_QuestBuilder.AddPiece;
        _removePiece.OnEvent -= m_QuestBuilder.RemovePiece;
        _finishMakingQuest.OnEvent -= m_QuestBuilder.FinishMakingQuest;
    }
}
