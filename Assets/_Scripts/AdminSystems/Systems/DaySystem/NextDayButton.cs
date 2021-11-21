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
    public Event<ID> _playBakeryDayMusic;

    private void Awake()
    {
        _button = GetComponent<Button>();

        var evtSys = Admin.Global.EventSystem;
        _startNewDayCommand = evtSys.GetCommandByName<EventVoid>("day_sys", "start_new_day");
        _playBakeryDayMusic = evtSys.GetCommandByName<Event<ID>>("audio_sys", "play_music");
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
        _playBakeryDayMusic.Invoke(new ID("bakery_day"));
    }
}
