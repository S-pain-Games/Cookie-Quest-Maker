﻿using System;
using System.Collections.Generic;

namespace CQM.Components
{
    // Functional Data of a Quest
    [Serializable]
    public class QuestData
    {
        public List<QuestPiece> m_PiecesList = new List<QuestPiece>();
    }
}