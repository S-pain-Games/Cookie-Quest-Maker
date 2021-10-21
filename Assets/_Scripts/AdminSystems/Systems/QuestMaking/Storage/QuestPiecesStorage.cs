using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.QuestMaking
{
    [System.Serializable]
    public class QuestPiecesStorage
    {
        public List<QuestPiece> m_AvailablePieces = new List<QuestPiece>();

        public void AddPiece(QuestPiece piece)
        {
            if (!m_AvailablePieces.Contains(piece))
                m_AvailablePieces.Add(piece);
        }

        public void RemovePiece(QuestPiece piece)
        {
            m_AvailablePieces.Remove(piece);
        }
    }
}