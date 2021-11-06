using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Systems
{
    public class CharacterSystem : ISystemEvents
    {
        private Singleton_InputComponent m_Input;

        public void Initialize(Singleton_InputComponent input)
        {
            m_Input = input;
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            sysID = new ID("character_sys");
            commands = new EventSys();
            callbacks = new EventSys();

            var cmd = commands.AddEvent(new ID("enable_movement"));
            cmd.OnInvoked += EnableMovement;
            cmd = commands.AddEvent(new ID("disable_movement"));
            cmd.OnInvoked += DisableMovement;
        }

        private void EnableMovement()
        {
            for (int i = 0; i < m_Input.m_Character.Count; i++)
            {
                m_Input.m_Character[i].SetInputActivated(true);
            }
        }

        private void DisableMovement()
        {
            for (int i = 0; i < m_Input.m_Character.Count; i++)
            {
                m_Input.m_Character[i].SetInputActivated(false);
            }
        }
    }
}