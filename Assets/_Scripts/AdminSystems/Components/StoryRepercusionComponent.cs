using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Components
{
    [System.Serializable]
    public class StoryRepercusionComponent
    {
        public ID m_ID;
        public ID m_ParentStoryID;

        public string m_Name;
        public bool m_Active = false;
        public int m_Value = 0;
    }
}