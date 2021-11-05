using System;
using System.Collections.Generic;

namespace CQM.Components
{
    // Functional Data of a Quest
    [Serializable]
    public class QuestData
    {
        public List<QuestPieceFunctionalComponent> m_PiecesList = new List<QuestPieceFunctionalComponent>();
    }
}