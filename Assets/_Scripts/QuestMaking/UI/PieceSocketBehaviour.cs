using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSocketBehaviour : MonoBehaviour
{
    public bool Filled => m_Filled;
    public QuestPiece CurrentPiece => m_CurrentPiece;

    public event Action<QuestPiece> OnPieceAdded;
    public event Action<QuestPiece> OnPieceRemoved;

    [SerializeField]
    private bool m_Filled = false;
    [SerializeField]
    private QuestPiece m_CurrentPiece;
    [SerializeField]
    private QuestPiece.PieceType m_RequiredType = QuestPiece.PieceType.Action;

    public bool TryToSetPiece(QuestPiece piece)
    {
        if (!m_Filled && piece.Type == m_RequiredType)
        {
            m_CurrentPiece = piece;
            m_Filled = true;
            OnPieceAdded?.Invoke(piece);
            return true;
        }
        return false;
    }

    public void RemovePiece()
    {
        m_Filled = false;
        OnPieceRemoved?.Invoke(m_CurrentPiece);
        m_CurrentPiece = null;
    }
}
