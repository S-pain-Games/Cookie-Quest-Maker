using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBakeryButton : MonoBehaviour
{
    private GameEventSystem _eventSystem;
    private EventVoid _onDailyStoriesCompletedCallback;
    private Event<GameStateSystem.State> _setGameStateCommand;

    [SerializeField]
    private GameObject _buttonGameObject;
    private Button _button;

    private void Awake()
    {
        // Subscribe to Callbacks and get Commands
        var ids = Admin.g_Instance.ID.events;
        _eventSystem = Admin.g_Instance.gameEventSystem;
        _eventSystem.DayCallbacks.GetEvent(ids.on_daily_stories_completed, out _onDailyStoriesCompletedCallback);
        _eventSystem.GameStateSystemMessaging.GetEvent(ids.set_game_state, out _setGameStateCommand);

        _button = _buttonGameObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _onDailyStoriesCompletedCallback.OnInvoked += TryToEnableButton;
        _button.onClick.AddListener(() =>
        {
            _setGameStateCommand.Invoke(GameStateSystem.State.BakeryNight);
            _buttonGameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _onDailyStoriesCompletedCallback.OnInvoked -= TryToEnableButton;
        _button.onClick.RemoveAllListeners();
    }

    private void TryToEnableButton()
    {
        _buttonGameObject.SetActive(true);
    }
}
