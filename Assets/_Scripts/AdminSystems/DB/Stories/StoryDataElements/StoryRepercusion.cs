using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Components
{
    public class StoryRepercusion
    {
        public int m_ID;
        public int m_ParentStoryID;
        public string m_Name;
        public bool m_Active = false;
        public int m_Value = 0;

        // TODO: How much rep does the player gain if the repercusion is activated
    }
}