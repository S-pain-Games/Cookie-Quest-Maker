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

    private GameEventSystem _eventSys;
    private Event<GameStateSystem.State> _setGameStateCommand;

    private void Awake()
    {
        var ids = Admin.g_Instance.ID.events;
        button = GetComponent<Button>();
        _eventSys = Admin.g_Instance.gameEventSystem;
        _eventSys.GameStateSystemMessaging.GetEvent(ids.set_game_state, out _setGameStateCommand);
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
