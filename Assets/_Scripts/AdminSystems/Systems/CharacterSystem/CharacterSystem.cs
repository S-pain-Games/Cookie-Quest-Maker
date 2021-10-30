using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : MonoBehaviour, ISystemEvents
{
    [SerializeField]
    private List<AgentMouseListener> _character; // this is weird af

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
        for (int i = 0; i < _character.Count; i++)
        {
            _character[i].SetInputActivated(true);
        }
    }

    private void DisableMovement()
    {
        for (int i = 0; i < _character.Count; i++)
        {
            _character[i].SetInputActivated(false);
        }
    }
}
