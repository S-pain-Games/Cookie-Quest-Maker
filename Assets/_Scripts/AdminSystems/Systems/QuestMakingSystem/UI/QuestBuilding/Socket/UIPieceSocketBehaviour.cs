using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPieceSocketBehaviour : MonoBehaviour
{
    public event Action<QuestPiece> OnPieceSocketed;
    public event Action<QuestPiece> OnPieceUnsocketed;

    public bool m_Filled = false;
    public QuestPiece.PieceType RequiredType = QuestPiece.PieceType.Action;
    public QuestPiece m_CurrentPiece;

    private Color m_BaseColor;

    private void Start()
    {
        m_BaseColor = GetComponent<Image>().color;
    }

    public void Clear()
    {
        m_Filled = false;
        m_CurrentPiece = null;
        GetComponent<Image>().color = m_BaseColor;
    }

    public bool TryToSetPiece(QuestPiece piece)
    {
        if (!m_Filled && piece.m_Type == RequiredType)
        {
            m_CurrentPiece = piece;
            m_Filled = true;
            OnPieceSocketed?.Invoke(piece);
            return true;
        }
        return false;
    }

    public void RemovePiece()
    {
        m_Filled = false;
        OnPieceUnsocketed?.Invoke(m_CurrentPiece);
        m_CurrentPiece = null;
    }

    public void OnMatchingPieceSelectedHandle()
    {
        GetComponent<Image>().color = Color.blue;
    }

    public void OnMatchingPieceUnselectedHandle()
    {
        GetComponent<Image>().color = m_BaseColor;
    }
}
