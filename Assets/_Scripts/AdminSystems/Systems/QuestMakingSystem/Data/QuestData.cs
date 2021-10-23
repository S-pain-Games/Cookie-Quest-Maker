using System;
using System.Collections.Generic;

// Functional Data of a Quest
[Serializable]
public class QuestData
{
    public int m_ID = -1;
    public List<QuestPiece> m_PiecesList = new List<QuestPiece>();
}