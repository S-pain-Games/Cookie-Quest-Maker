using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextDayButton : MonoBehaviour
{
    private Button _button;
    private GameEventSystem _eventSystem;
    private EventVoid _startNewDayCommand;

    private void Awake()
    {
        var ids = Admin.g_Instance.ID.events;
        _button = GetComponent<Button>();
        _eventSystem = Admin.g_Instance.gameEventSystem;
        _eventSystem.DaySystemCommands.GetEvent(ids.start_new_day, out _startNewDayCommand);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ButtonClicked);

    }

    private void ButtonClicked()
    {
        _startNewDayCommand.Invoke();
    }
}
