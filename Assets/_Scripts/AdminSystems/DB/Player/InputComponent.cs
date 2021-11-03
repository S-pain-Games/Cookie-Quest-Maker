using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Databases
{
    [System.Serializable]
    public class InputComponent
    {
        public List<AgentMouseListener> m_Character = new List<AgentMouseListener>();
    }
}