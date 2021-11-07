using System.Collections.Generic;
using UnityEngine;

public class PopupSystem : ISystemEvents
{
    private Singleton_PopupReferencesComponent m_PopupData;

    public void Initialize(Singleton_PopupReferencesComponent popupData)
    {
        m_PopupData = popupData;
    }

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("popup_sys");

        var evt = commands.AddEvent<PopupData>(new ID("show_popup"));
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