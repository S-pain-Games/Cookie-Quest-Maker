using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestPiecesStorage
{
    public List<QuestPiece> AvailablePieces => m_AvaliablePieces;

    [SerializeField]
    private List<QuestPiece> m_AvaliablePieces = new List<QuestPiece>();

    public void AddPiece(QuestPiece piece)
    {
        if (!m_AvaliablePieces.Contains(piece))
            m_AvaliablePieces.Add(piece);
    }

    public void RemovePiece(QuestPiece piece)
    {
        m_AvaliablePieces.Remove(piece);
    }
}
