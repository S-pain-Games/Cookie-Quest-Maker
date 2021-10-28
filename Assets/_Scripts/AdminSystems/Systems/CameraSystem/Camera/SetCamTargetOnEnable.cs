using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamTargetOnEnable : MonoBehaviour
{
    private GameEventSystem _evtSys;
    private Event<Transform> _retargetCommand;

    private void Awake()
    {
        _evtSys = Admin.g_Instance.gameEventSystem;
        _evtSys.CameraSystemCommands.GetEvent("retarget_cmd".GetHashCode(), out _retargetCommand);
    }

    private void OnEnable()
    {
        _retargetCommand.Invoke(transform);
    }
}
