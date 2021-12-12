using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUIButton : MonoBehaviour
{
    [SerializeField] private string m_ToggleEventNameID;
    [SerializeField] private Button button;
    private EventVoid _toggleUI;

    private EventVoid _enableCharMovementCmd;
    private EventVoid _disableCharMovementCmd;

    private void Awake()
    {
        var _eventSys = Admin.Global.EventSystem;
        _toggleUI = _eventSys.GetCommandByName<EventVoid>("ui_sys", m_ToggleEventNameID);

        _disableCharMovementCmd = _eventSys.GetCommandByName<EventVoid>("character_sys", "disable_movement");
        _enableCharMovementCmd = _eventSys.GetCommandByName<EventVoid>("character_sys", "enable_movement");
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ToggleUI);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ToggleUI);
    }

    //Es muy horrible, pero no me queda otra
    [SerializeField] private CharacterNavMeshAgentHandler characterDay;
    [SerializeField] private CharacterNavMeshAgentHandler characterNight;
    [SerializeField] private CharacterNavMeshAgentHandler characterEnd;

    private void ToggleUI()
    {
        _toggleUI.Invoke();

        if(gameObject.name == "Settings_Button")
        {
            _disableCharMovementCmd.Invoke();
            characterDay.InterruptAgentMovement();
            characterNight.InterruptAgentMovement();
            characterEnd.InterruptAgentMovement();
        }
        else if(gameObject.name == "BtnBack")
        {
            _enableCharMovementCmd.Invoke();


            
        }
    }
}
