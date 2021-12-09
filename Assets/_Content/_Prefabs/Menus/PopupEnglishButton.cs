using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupEnglishButton : MonoBehaviour
{
    private Event<PopupData_GenericPopup> _OnClickedButton;
    private Button _button;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _OnClickedButton = evtSys.GetCommandByName<Event<PopupData_GenericPopup>>("popup_sys", "generic_popup");
        _button = GetComponent<Button>();
    }

    void Start()
    {
        _button.onClick.AddListener(ShowEnglishPopup);
    }

    private void ShowEnglishPopup()
    {
        PopupData_GenericPopup bData = new PopupData_GenericPopup();
        bData.m_Text = "Soon in English!";
        bData.m_TimeAlive = 3.0f;
        _OnClickedButton.Invoke(bData);
    }
}
