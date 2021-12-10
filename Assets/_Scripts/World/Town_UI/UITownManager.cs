using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CQM.UI.Town
{
    public class UITownManager : MonoBehaviour
    {
        [SerializeField] private List<TownBuildingBehaviour> m_Buildings = new List<TownBuildingBehaviour>();
        [SerializeField] private HappinessMeterAnimations m_currentMeter;
        [SerializeField] private TextMeshProUGUI globalHapinessText;

        private int baseHapinessValue = 100;    //50%
        //Mínimo 0, Máximo 200
 
        public void OnEnable()
        {
            for (int i = 0; i < m_Buildings.Count; i++)
            {
                m_Buildings[i].Initialize(this);
            }

            //CalculateGlobalHapiness();
            globalHapinessText.text = "FELICIDAD " + Admin.Global.Components.m_TownComponent.m_GlobalHappiness + "%";
        }

        private void CalculateGlobalHapiness()
        {
            int totalHapinessModifier = baseHapinessValue;

            for (int i = 0; i < m_Buildings.Count; i++)
            {
                var locationComponent = Admin.Global.Components.GetComponentContainer<LocationComponent>().GetComponentByID(m_Buildings[i].BuildingID);
                Debug.Log(locationComponent.m_LocName + " hapiness: " + locationComponent.m_Happiness);

                totalHapinessModifier += locationComponent.m_Happiness;
            }

            int hapinessPercent = totalHapinessModifier * 50 / 100;

            globalHapinessText.text = "FELICIDAD "+ hapinessPercent+"%";
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