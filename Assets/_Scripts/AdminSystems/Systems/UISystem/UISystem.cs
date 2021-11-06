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

        public void Initialize(Singleton_UIReferencesComponent references)
        {
            _references = references;
            Debug.Assert(references != null);
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            sysID = new ID("ui_sys");
            commands = new EventSys();
            callbacks = new EventSys();

            commands.AddEvent(new ID("toggle_newspaper")).OnInvoked += ToggleNewspaper;
        }

        private void ToggleNewspaper()
        {
            var ui = _references.m_NewspaperUi;
            if (ui.activeSelf)
                ui.SetActive(false);
            else
                ui.SetActive(true);
        }
    }
}