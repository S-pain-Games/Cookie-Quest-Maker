using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Singleton_NpcReferencesComponent
{
    public List<NPCBehaviour> m_NpcBehaviour = new List<NPCBehaviour>();
    public EvithBehaviour m_Evith;
    public NuBehaviour m_Nu;
}
