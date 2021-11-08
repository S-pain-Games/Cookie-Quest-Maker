using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ContinueDialogueButton : MonoBehaviour
{
    private Button _button;

    private EventVoid _continueDialogueCmd;

    private void Awake()
    {
        _button = GetComponent<Button>();
        var evtSys = Admin.Global.EventSystem;
        _continueDialogueCmd = evtSys.GetCommandByName<EventVoid>("dialogue_sys", "continue_dialogue");
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(InvokeContinueDialogueCmd);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(InvokeContinueDialogueCmd);
    }

    private void InvokeContinueDialogueCmd()
    {
        _continueDialogueCmd.Invoke();
    }
}
