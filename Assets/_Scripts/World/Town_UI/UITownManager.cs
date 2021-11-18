using CQM.Components;
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
            // We could definitely cache this
            var locationComponent = Admin.Global.Components.GetComponentContainer<LocationComponent>().GetComponentByID(building.BuildingID);
            m_currentMeter.EnableForBuilding(building.transform.position, locationComponent.m_Happiness, locationComponent.m_LocName);
        }

        public void UnselectTownBuilding(TownBuildingBehaviour building)
        {
            m_currentMeter.Hide();
        }
    }
}