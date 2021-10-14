using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Events;

public class VoidEventListener : MonoBehaviour
{
    public UnityEvent onEventRaised;

    [SerializeField]
    private VoidEventHandle _eventHandle;

    private void OnEnable()
    {
        _eventHandle.OnEvent += RaiseUnityEvent;
    }

    private void OnDisable()
    {
        _eventHandle.OnEvent -= RaiseUnityEvent;
    }

    private void RaiseUnityEvent()
    {
        onEventRaised?.Invoke();
    }
}
