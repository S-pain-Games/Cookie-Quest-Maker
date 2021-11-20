using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayGameButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        var d = Admin.Global.Components.m_GameState;
        var evtSys = Admin.Global.EventSystem;
        if (d.m_GameplayStarted)
            evtSys.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state").Invoke(d.m_GameplayState);
        else
            evtSys.GetCommandByName<EventVoid>("day_sys", "start_tutorial_day").Invoke();
    }
}
