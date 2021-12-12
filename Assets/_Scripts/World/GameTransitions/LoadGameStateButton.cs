using CQM.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadGameStateButton : MonoBehaviour
{
    private Button button;

    public EventVoid _openCookieMakingUI;
    public Event<ID> _playBakeryDayMusic;


    [SerializeField] private FirstDayFurnaceSequence tutorial;

    private void Awake()
    {
        button = GetComponent<Button>();
        var _eventSys = Admin.Global.EventSystem;
        _openCookieMakingUI = _eventSys.GetCommandByName<EventVoid>("ui_sys", "toggle_cookie_making");
        _playBakeryDayMusic = _eventSys.GetCommandByName<Event<ID>>("audio_sys", "play_music");
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
        if (!tutorial.RequirementsMet())
            return;

        _openCookieMakingUI.Invoke();
        _playBakeryDayMusic.Invoke(new ID("bakery_day"));
    }
}
