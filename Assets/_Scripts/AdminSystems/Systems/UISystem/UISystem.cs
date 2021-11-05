using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Systems
{
    public class UISystem : ISystemEvents
    {
        private UIReferencesComponent _references;

        public void Initialize(UIReferencesComponent references)
        {
            _references = references;
            Debug.Assert(references != null);
        }

        public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
        {
            sysID = "ui_sys".GetHashCode();
            commands = new EventSys();
            callbacks = new EventSys();

            commands.AddEvent("toggle_newspaper".GetHashCode()).OnInvoked += ToggleNewspaper;
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