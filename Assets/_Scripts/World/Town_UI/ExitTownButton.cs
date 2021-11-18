using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitTownButton : MonoBehaviour
{
    private Button button;

    private EventVoid _openUI;

    private void Awake()
    {
        button = GetComponent<Button>();
        var _eventSys = Admin.Global.EventSystem;
        _openUI = _eventSys.GetCommandByName<EventVoid>("ui_sys", "toggle_town");
    }

    private void OnEnable()
    {
        button.onClick.AddListener(LoadGameState);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(LoadGameState);
    }

    private void LoadGameState()
    {
        _openUI.Invoke();
    }
}
