using CQM.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFurnace : MonoBehaviour, IInteractableEntity
{
    //public Event<GameStateSystem.State> _setStateCmd;
    public Event<ID> _playCookieMakingMusic;

    public EventVoid _openCookieMakingUI;

    public void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        //_setStateCmd = evtSys.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state");
        _playCookieMakingMusic = evtSys.GetCommandByName<Event<ID>>("audio_sys", "play_music");

        _openCookieMakingUI = evtSys.GetCommandByName<EventVoid>("ui_sys", "toggle_cookie_making");
    }

    public void OnInteract()
    {
        //_setStateCmd.Invoke(GameStateSystem.State.CookieMaking);

        _openCookieMakingUI.Invoke();



        _playCookieMakingMusic.Invoke(new ID("bakery_day_cookies"));
    }

}
