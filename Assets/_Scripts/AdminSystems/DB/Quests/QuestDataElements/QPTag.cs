using System;

namespace CQM.Components
{

    // Quest Piece Tag
    [Serializable]
    public class QPTag
    {
        public TagType m_Type;
        public int m_Value;

        public enum TagType
        {
            // It might be a horrible idea to serialize an enum
            Harm,
            Convince,
            Help
        }
    }
}