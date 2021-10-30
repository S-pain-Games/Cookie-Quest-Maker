using System.Collections.Generic;
using UnityEngine;

public class PopupSystem : MonoBehaviour, ISystemEvents
{
    [SerializeField]
    private GameObject m_PopupParent;
    [SerializeField]
    private GameObject m_PopupPrefab;

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
        PopupBehaviour popUp = Object.Instantiate(m_PopupPrefab, m_PopupParent.transform).GetComponent<PopupBehaviour>();
        popUp.Initialize(popData);
    }
}

public struct PopupData
{
    public string m_Text;
    public float m_TimeAlive;
}