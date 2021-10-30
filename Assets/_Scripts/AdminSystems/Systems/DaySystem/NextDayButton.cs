using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextDayButton : MonoBehaviour
{
    private Button _button;
    private EventVoid _startNewDayCommand;

    private void Awake()
    {
        _button = GetComponent<Button>();

        var evtSys = Admin.g_Instance.gameEventSystem;
        _startNewDayCommand = evtSys.GetCommandByName<EventVoid>("day_sys", "start_new_day");
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
