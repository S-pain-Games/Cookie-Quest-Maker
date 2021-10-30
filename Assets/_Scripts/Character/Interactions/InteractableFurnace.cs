using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFurnace : MonoBehaviour, IInteractableEntity
{
    public Event<GameStateSystem.State> _setStateCmd;

    public void Awake()
    {
        var evtSys = Admin.g_Instance.gameEventSystem;
        _setStateCmd = evtSys.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state");
    }

    public void OnInteract()
    {
        _setStateCmd.Invoke(GameStateSystem.State.CookieMaking);
    }

}
