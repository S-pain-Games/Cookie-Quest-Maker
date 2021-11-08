using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBakeryButton : MonoBehaviour
{
    private EventVoid _onDailyStoriesCompletedCallback;
    private EventVoid _setGameStateCommand;

    [SerializeField]
    private GameObject _buttonGameObject;
    private Button _button;

    private void Awake()
    {
        // Subscribe to Callbacks and get Commands
        var evtSys = Admin.Global.EventSystem;
        _onDailyStoriesCompletedCallback = evtSys.GetCallbackByName<EventVoid>("day_sys", "all_daily_stories_completed");
        _setGameStateCommand = evtSys.GetCommandByName<EventVoid>("day_sys", "begin_night");

        _button = _buttonGameObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _onDailyStoriesCompletedCallback.OnInvoked += TryToEnableButton;
        _button.onClick.AddListener(() =>
        {
            _setGameStateCommand.Invoke();
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
