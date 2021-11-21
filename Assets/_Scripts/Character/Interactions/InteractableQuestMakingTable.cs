using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableQuestMakingTable : MonoBehaviour, IInteractableEntity
{
    public EventVoid _openQuestMakingUI;

    public Event<ID> _playQuestMakingMusic;

    public void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _openQuestMakingUI = evtSys.GetCommandByName<EventVoid>("ui_sys", "toggle_quest_making");

        _playQuestMakingMusic = evtSys.GetCommandByName<Event<ID>>("audio_sys", "play_music");
    }

    public void OnInteract()
    {
        _openQuestMakingUI.Invoke();

        _playQuestMakingMusic.Invoke(new ID("bakery_day_missions"));
    }
}
