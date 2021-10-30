using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraSystem : MonoBehaviour, ISystemEvents
{
    [SerializeField]
    private CinemachineVirtualCamera _mainCam;

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = "camera_sys".GetHashCode();

        var evt = commands.AddEvent<Transform>("retarget_cmd".GetHashCode());
        evt.OnInvoked += Retarget;
    }

    public void Initialize()
    {
    }

    private void Retarget(Transform target)
    {
        _mainCam.Follow = target;
    }
}
