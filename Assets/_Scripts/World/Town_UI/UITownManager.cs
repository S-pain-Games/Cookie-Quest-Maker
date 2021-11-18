using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.UI.Town
{
    public class UITownManager : MonoBehaviour
    {
        [SerializeField] private List<TownBuildingBehaviour> m_Buildings = new List<TownBuildingBehaviour>();
        [SerializeField] private HappinessMeterAnimations m_currentMeter;

        public void OnEnable()
        {
            for (int i = 0; i < m_Buildings.Count; i++)
            {
                m_Buildings[i].Initialize(this);
            }
        }

        public void SelectTownBuilding(TownBuildingBehaviour building)
        {
            m_currentMeter.EnableForBuilding(building.transform.position);
        }

        public void UnselectTownBuilding(TownBuildingBehaviour building)
        {
            m_currentMeter.Hide();
        }
    }
}