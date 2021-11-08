using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamTargetOnEnable : MonoBehaviour
{
    private Event<Transform> _retargetCommand;

    private void Awake()
    {
        var _evtSys = Admin.Global.EventSystem;
        _retargetCommand = _evtSys.GetCommandByName<Event<Transform>>("camera_sys", "retarget_cmd");
    }

    private void OnEnable()
    {
        _retargetCommand.Invoke(transform);
    }
}
