using UnityEngine;

namespace CQM.Databases
{
    [System.Serializable]
    public class QuestPiecePrefabComponent
    {
        public GameObject m_QuestBuildingPiecePrefab;
        [HideInInspector] public ID m_ID;
    }
}