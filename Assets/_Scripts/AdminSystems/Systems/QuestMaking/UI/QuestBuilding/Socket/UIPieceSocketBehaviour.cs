using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPieceSocketBehaviour : MonoBehaviour
{
    public bool Filled => m_Filled;
    public QuestPiece CurrentPiece => _currentPiece;
    public QuestPiece.PieceType RequiredType => m_RequiredType;

    public event Action<QuestPiece> OnPieceSocketed;
    public event Action<QuestPiece> OnPieceUnsocketed;

    [SerializeField]
    private bool m_Filled = false;
    [SerializeField]
    private QuestPiece.PieceType m_RequiredType = QuestPiece.PieceType.Action;
    [SerializeField]
    private QuestPiece _currentPiece;

    public bool TryToSetPiece(QuestPiece piece)
    {
        if (!m_Filled && piece.Type == m_RequiredType)
        {
            _currentPiece = piece;
            m_Filled = true;
            OnPieceSocketed?.Invoke(piece);
            return true;
        }
        return false;
    }

    public void RemovePiece()
    {
        m_Filled = false;
        OnPieceUnsocketed?.Invoke(_currentPiece);
        _currentPiece = null;
    }

    public void OnMatchingPieceSelectedHandle()
    {
        GetComponent<Image>().color = Color.blue;
    }

    public void OnMatchingPieceUnselectedHandle()
    {
        GetComponent<Image>().color = Color.red;
    }
}
