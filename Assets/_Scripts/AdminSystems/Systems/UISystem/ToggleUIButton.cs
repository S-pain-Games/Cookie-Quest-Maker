using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUIButton : MonoBehaviour
{
    [SerializeField] private string m_ToggleEventNameID;
    [SerializeField] private Button button;
    private EventVoid _toggleUI;

    private void Awake()
    {
        var _eventSys = Admin.Global.EventSystem;
        _toggleUI = _eventSys.GetCommandByName<EventVoid>("ui_sys", m_ToggleEventNameID);
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ToggleUI);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ToggleUI);
    }

    private void ToggleUI()
    {
        _toggleUI.Invoke();
    }
}
