using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Systems
{
    public class UISystem : ISystemEvents
    {
        private Singleton_UIReferencesComponent _references;

        private EventVoid _enableCharacterMovementCmd;
        private EventVoid _disableCharacterMovementCmd;


        public void Initialize(Singleton_UIReferencesComponent references)
        {
            _references = references;
            Debug.Assert(references != null);

            _enableCharacterMovementCmd = Admin.Global.EventSystem.GetCommandByName<EventVoid>("character_sys", "enable_movement");
            _disableCharacterMovementCmd = Admin.Global.EventSystem.GetCommandByName<EventVoid>("character_sys", "disable_movement");
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            sysID = new ID("ui_sys");
            commands = new EventSys();
            callbacks = new EventSys();

            commands.AddEvent(new ID("toggle_newspaper")).OnInvoked += () => ToggleUIBlockingMovement(_references.m_NewspaperUi);
            commands.AddEvent(new ID("toggle_quest_making")).OnInvoked += () => ToggleUIBlockingMovement(_references.m_QuestMakingUi);
            commands.AddEvent(new ID("toggle_town")).OnInvoked += () => ToggleUIBlockingMovement(_references.m_TownUi);
            commands.AddEvent(new ID("toggle_settings")).OnInvoked += () => ToggleUIBlockingMovement(_references.m_SettingsGameplayUi);
            commands.AddEvent(new ID("toggle_gameplay")).OnInvoked += () => ToggleUIBlockingMovement(_references.m_GameplayUi);
        }

        private void ToggleUI(GameObject ui)
        {
            if (ui.activeSelf)
                ui.SetActive(false);
            else
                ui.SetActive(true);
        }

        private void ToggleUIBlockingMovement(GameObject ui)
        {
            if (ui.activeSelf)
            {
                ui.SetActive(false);
                _enableCharacterMovementCmd.Invoke();
            }
            else
            {
                _disableCharacterMovementCmd.Invoke();
                ui.SetActive(true);
            }
        }
    }
}