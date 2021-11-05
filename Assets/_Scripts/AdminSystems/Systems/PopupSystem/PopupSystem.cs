using System.Collections.Generic;
using UnityEngine;

public class PopupSystem : ISystemEvents
{
    private PopupComponent m_PopupData;

    public void Initialize(PopupComponent popupData)
    {
        m_PopupData = popupData;
    }

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = "popup_sys".GetHashCode();

        var evt = commands.AddEvent<PopupData>("show_popup".GetHashCode());
        evt.OnInvoked += (args) => ShowPopup(args);
    }

    private void ShowPopup(PopupData popData)
    {
        PopupBehaviour popUp = Object.Instantiate(m_PopupData.m_PopupPrefab, m_PopupData.m_InstantiationTransform).GetComponent<PopupBehaviour>();
        popUp.Initialize(popData);
    }
}

public struct PopupData
{
    public string m_Text;
    public float m_TimeAlive;
}