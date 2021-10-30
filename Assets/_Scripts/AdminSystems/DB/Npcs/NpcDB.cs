using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class NpcDB
{
    // Populated in the inspector
    public List<NPCBehaviour> m_NpcBehaviour = new List<NPCBehaviour>();

    public void LoadData(NpcDBUnityReferences npcDBRef)
    {
        m_NpcBehaviour = npcDBRef.m_NpcBehaviour;
    }
}
