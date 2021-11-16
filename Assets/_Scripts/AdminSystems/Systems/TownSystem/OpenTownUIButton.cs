using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenTownUIButton : MonoBehaviour
{
    private Button _button;

    private EventVoid _openNewspaperCmd;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _openNewspaperCmd = Admin.Global.EventSystem.GetCommandByName<EventVoid>("ui_sys", "toggle_town");
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenNewspaper);
    }

    private void OpenNewspaper()
    {
        _openNewspaperCmd.Invoke();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenNewspaper);
    }
}
