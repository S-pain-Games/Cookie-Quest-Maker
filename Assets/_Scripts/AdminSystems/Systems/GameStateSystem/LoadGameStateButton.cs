using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadGameStateButton : MonoBehaviour
{
    [SerializeField]
    private GameStateSystem.State m_TargetState;
    private Button button;

    private Event<GameStateSystem.State> _setGameStateCommand;

    private void Awake()
    {
        button = GetComponent<Button>();
        var _eventSys = Admin.Global.EventSystem;
        _setGameStateCommand = _eventSys.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state");
    }

    private void OnEnable()
    {
        button.onClick.AddListener(LoadGameState);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(LoadGameState);
    }

    private void LoadGameState()
    {
        _setGameStateCommand.Invoke(m_TargetState);
    }
}
