using System;
using System.Collections.Generic;

namespace CQM.Components
{
    [Serializable]
    public class QuestDataComponent
    {
        public List<QuestPieceFunctionalComponent> m_PiecesList = new List<QuestPieceFunctionalComponent>();
    }
}