using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBakeryButton : MonoBehaviour
{
    private GameEventSystem _eventSystem;
    private EventVoid _onDailyStoriesCompleted;

    [SerializeField]
    private GameObject _buttonGameObject;
    private Button _button;

    private void Awake()
    {
        var ids = Admin.g_Instance.ID.events;
        _eventSystem = Admin.g_Instance.gameEventSystem;
        _eventSystem.DayCallbacks.GetEvent(ids.on_daily_stories_completed, out _onDailyStoriesCompleted);

        _button = _buttonGameObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _onDailyStoriesCompleted.OnInvoked += TryToEnableButton;
        _button.onClick.AddListener(() =>
        {
            // TODO
            Admin.g_Instance.gameStateSystem.SetState(GameStateSystem.State.BakeryNight);
            //_startNewDayCommand.Invoke();
            _buttonGameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _onDailyStoriesCompleted.OnInvoked -= TryToEnableButton;
        _button.onClick.RemoveAllListeners();
    }

    private void TryToEnableButton()
    {
        _buttonGameObject.SetActive(true);
    }
}
