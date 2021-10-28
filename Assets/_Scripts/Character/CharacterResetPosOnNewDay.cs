using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterResetPosOnNewDay : MonoBehaviour
{
    private GameEventSystem _evtSys;
    private EventVoid _onDayEndedCallback;

    private Vector3 m_StartPos;

    private void Awake()
    {
        var id = Admin.g_Instance.ID.events;
        _evtSys = Admin.g_Instance.gameEventSystem;
        _evtSys.DayCallbacks.GetEvent(id.on_day_ended, out _onDayEndedCallback);
        _onDayEndedCallback.OnInvoked += OnDayEndedCallback_OnInvoked;
    }

    private void Start()
    {
        m_StartPos = transform.position;
    }

    private void OnDayEndedCallback_OnInvoked()
    {
        transform.position = m_StartPos;
    }
}
