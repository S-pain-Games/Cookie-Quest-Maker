using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterResetPosOnNewDay : MonoBehaviour
{
    private EventVoid _onDayEndedCallback;

    private Vector3 m_StartPos;

    private void Awake()
    {
        var evtSys = Admin.g_Instance.gameEventSystem;
        _onDayEndedCallback = evtSys.GetCallbackByName<EventVoid>("day_sys", "day_ended");
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
