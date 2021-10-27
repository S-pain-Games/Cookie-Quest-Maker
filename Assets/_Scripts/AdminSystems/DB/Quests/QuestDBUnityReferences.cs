using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to get unity references into the QuestDB System
public class QuestDBUnityReferences : MonoBehaviour
{
    // References to quest piece prefabs used in the quest building UI
    [SerializeField] public List<QuestPiecePrefabRef> QuestBuildingPiecePrefabs = new List<QuestPiecePrefabRef>();

    [System.Serializable]
    public struct QuestPiecePrefabRef
    {
        public string m_NameID;
        public GameObject m_Prefab;
    }
}