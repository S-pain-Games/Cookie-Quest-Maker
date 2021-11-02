using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentMouseListener))]
public class PlayerController : MonoBehaviour
{
    [Header("Enable/Disable Callbacks")]
    [SerializeField] private string _enableCallbackID;
    [SerializeField] private string _disableCallbackID;

    private EventVoid _onBakeryStateEnter;
    private EventVoid _onBakeryStateExit;

    private AgentMouseListener _input;

    private void Awake()
    {
        _input = GetComponent<AgentMouseListener>();
        var evtSys = Admin.Global.EventSystem;

        _onBakeryStateEnter = evtSys.GetCallbackByName<EventVoid>("game_state_sys", _enableCallbackID);
        _onBakeryStateExit = evtSys.GetCallbackByName<EventVoid>("game_state_sys", _disableCallbackID);
    }

    private void OnEnable()
    {
        _onBakeryStateEnter.OnInvoked += EnableMovement;
        _onBakeryStateExit.OnInvoked += DisableMovement;
    }

    private void OnDisable()
    {
        _onBakeryStateEnter.OnInvoked -= EnableMovement;
        _onBakeryStateExit.OnInvoked -= DisableMovement;
    }

    private void EnableMovement()
    {
        _input.SetInputActivated(true);
    }

    private void DisableMovement()
    {
        _input.SetInputActivated(false);
    }
}
