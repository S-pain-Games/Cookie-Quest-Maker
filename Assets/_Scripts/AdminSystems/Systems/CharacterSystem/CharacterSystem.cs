using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : ISystemEvents
{
    private InputComponent m_Input;

    public void Initialize(InputComponent input)
    {
        m_Input = input;
    }

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        sysID = "character_sys".GetHashCode();
        commands = new EventSys();
        callbacks = new EventSys();

        var cmd = commands.AddEvent("enable_movement".GetHashCode());
        cmd.OnInvoked += EnableMovement;
        cmd = commands.AddEvent("disable_movement".GetHashCode());
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
