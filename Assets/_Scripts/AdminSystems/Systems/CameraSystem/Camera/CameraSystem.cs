using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _mainCam;

    private GameEventSystem _evtSys;
    private Event<Transform> _retargetCommand;

    public void Initialize(GameEventSystem evtSys)
    {
        _evtSys = evtSys;
        _evtSys.CameraSystemCommands.GetEvent("retarget_cmd".GetHashCode(), out _retargetCommand);
        _retargetCommand.OnInvoked += Retarget;
    }

    public void Retarget(Transform target)
    {
        _mainCam.Follow = target;
    }
}
