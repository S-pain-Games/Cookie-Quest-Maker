using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Databases
{
    // Stores all the created quests in the game
    [System.Serializable]
    public class QuestDB : MonoBehaviour
    {
        // Inspector Only
        [SerializeField]
        private List<QuestPieceDataContainer> qpData = new List<QuestPieceDataContainer>();

        // Contains all the quest piece data
        private Dictionary<int, QuestPieceDataContainer> qpDataDic = new Dictionary<int, QuestPieceDataContainer>();

        // Contains all quests made in the playthrough
        private Dictionary<int, QuestData> m_QuestDataDB = new Dictionary<int, QuestData>();

        public T GetQuestPieceComponent<T>(int ID) where T : class
        {
            var dataContainer = qpDataDic[ID];
            if (dataContainer.m_Functional is T)
            {
                return dataContainer.m_Functional as T;
            }
            else if (dataContainer.m_QuestBuildingPiecePrefab is T)
            {
                return dataContainer.m_QuestBuildingPiecePrefab as T;
            }
            else if (dataContainer.m_QuestSelectionUI is T)
            {
                return dataContainer.m_QuestSelectionUI as T;
            }
            return null;
        }

        public QuestData GetQuestData(int ID)
        {
            return m_QuestDataDB[ID];
        }

        public void LoadData()
        {
            for (int i = 0; i < qpData.Count; i++)
            {
                var data = qpData[i];
                data.m_Functional.m_ParentID = data.m_ID.GetHashCode();
                data.m_QuestSelectionUI.m_ParentID = data.m_ID.GetHashCode();

                qpDataDic.Add(data.m_ID.GetHashCode(), data);
            }
        }
    }

    [Serializable]
    public class QuestPieceDataContainer
    {
        public string m_ID;
        public QuestPiece m_Functional;
        public UIQuestPieceData m_QuestSelectionUI;
        public GameObject m_QuestBuildingPiecePrefab;
    }
}