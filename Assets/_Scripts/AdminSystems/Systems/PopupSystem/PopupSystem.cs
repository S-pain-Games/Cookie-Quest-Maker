using System.Collections.Generic;
using UnityEngine;

namespace CQM.Systems
{
    public class PopupSystem : ISystemEvents
    {
        private Singleton_PopupReferencesComponent m_PopupData;


        public void Initialize(Singleton_PopupReferencesComponent popupData)
        {
            m_PopupData = popupData;
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            commands = new EventSys();
            callbacks = new EventSys();
            sysID = new ID("popup_sys");

            commands.AddEvent<PopupData_MissionStarted>(new ID("primary_mission_started")).OnInvoked +=
                (args) => ShowPrimaryMissionStarted(args);

            commands.AddEvent<PopupData_MissionStarted>(new ID("secondary_mission_started")).OnInvoked +=
                (args) => ShowSecondaryMissionStarted(args);

            commands.AddEvent<PopupData_GenericPopup>(new ID("generic_popup")).OnInvoked +=
                (args) => ShowGenericPopup(args);
        }


        private void ShowPrimaryMissionStarted(PopupData_MissionStarted popData)
        {
            MissionPopupBehaviour popUp = Object.Instantiate(m_PopupData.m_PrimaryMissionPrefab, m_PopupData.m_InstantiationTransform).GetComponent<MissionPopupBehaviour>();
            popUp.Initialize(popData);
        }

        private void ShowSecondaryMissionStarted(PopupData_MissionStarted popData)
        {
            MissionPopupBehaviour popUp = Object.Instantiate(m_PopupData.m_SecondaryMissionPrefab, m_PopupData.m_InstantiationTransform).GetComponent<MissionPopupBehaviour>();
            popUp.Initialize(popData);
        }

        private void ShowGenericPopup(PopupData_GenericPopup popData)
        {
            GenericPopupBehaviour popUp = Object.Instantiate(m_PopupData.m_GenericPrefab, m_PopupData.m_InstantiationTransform).GetComponent<GenericPopupBehaviour>();
            popUp.Initialize(popData);
        }
    }
}