using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcDBUnityReferences : MonoBehaviour
{
    // this class is just for consistency in the systems design
    public List<NPCBehaviour> m_NpcBehaviour = new List<NPCBehaviour>();

    private void Awake()
    {
        // This is quite expensive an unnecesary but it temporarily fixes problems
        m_NpcBehaviour = FindObjectsOfType<NPCBehaviour>(true).ToList();

#if UNITY_EDITOR
        if (m_NpcBehaviour.Count == 0)
        {
            Debug.LogError("No npcs found for the NPC System");
        }
#endif
    }
}
