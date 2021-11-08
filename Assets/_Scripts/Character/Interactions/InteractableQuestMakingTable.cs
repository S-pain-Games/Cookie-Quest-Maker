using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableQuestMakingTable : MonoBehaviour, IInteractableEntity
{
    public EventVoid _openQuestMakingUI;

    public void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _openQuestMakingUI = evtSys.GetCommandByName<EventVoid>("ui_sys", "toggle_quest_making");
    }

    public void OnInteract()
    {
        _openQuestMakingUI.Invoke();
    }
}
