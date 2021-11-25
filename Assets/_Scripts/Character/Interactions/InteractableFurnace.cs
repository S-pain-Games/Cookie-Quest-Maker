using CQM.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFurnace : MonoBehaviour, IInteractableEntity
{
    public Event<GameStateSystem.State> _setStateCmd;
    public Event<ID> _playCookieMakingMusic;

    public void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _setStateCmd = evtSys.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state");
        _playCookieMakingMusic = evtSys.GetCommandByName<Event<ID>>("audio_sys", "play_music");
    }

    public void OnInteract()
    {
        _setStateCmd.Invoke(GameStateSystem.State.CookieMaking);
        _playCookieMakingMusic.Invoke(new ID("bakery_day_cookies"));
    }

}
