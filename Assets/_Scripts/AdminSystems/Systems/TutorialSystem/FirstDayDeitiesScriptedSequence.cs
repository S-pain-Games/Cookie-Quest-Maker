using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDayDeitiesScriptedSequence : MonoBehaviour
{
    [SerializeField] private Transform evithSpawnPos;
    [SerializeField] private Transform nuSpawnPos;

    [SerializeField] private GameObject evithPrefab;
    [SerializeField] private GameObject nuPrefab;

    private GameObject evithRef;
    private GameObject nuRef;

    private EventVoid _enableMovementCmd;
    private EventVoid _disableMovementCmd;
    private EventVoid _toggleGameplayUiCmd;
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    private void Awake()
    {
        GameEventSystem evtSys = Admin.Global.EventSystem;

        _enableMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        _disableMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "disable_movement");
        _toggleGameplayUiCmd = evtSys.GetCommandByName<EventVoid>("ui_sys", "toggle_gameplay");
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
    }

    [MethodButton]
    public void ExecuteSequence()
    {
        evithRef = Instantiate(evithPrefab, evithSpawnPos);
        nuRef = Instantiate(nuPrefab, nuSpawnPos);
        _disableMovementCmd.Invoke();
        _toggleGameplayUiCmd.Invoke();
        StartDialogue();
    }

    private void StartDialogue()
    {
        ShowDialogueEvtArgs d = new ShowDialogueEvtArgs(new List<string>() { "Dialogue1" }, new ID("narrator"), ShowDialogue_2);
        _showDialogueCmd.Invoke(d);
    }

    private void ShowDialogue_2()
    {
        ShowDialogueEvtArgs d = new ShowDialogueEvtArgs(new List<string>() { "Dialogue1" }, new ID("nu"), ShowDialogue_3);
        _showDialogueCmd.Invoke(d);
    }

    private void ShowDialogue_3()
    {
        ShowDialogueEvtArgs d = new ShowDialogueEvtArgs(new List<string>() { "Dialogue1" }, new ID("evith"), ShowDialogue_4);
        _showDialogueCmd.Invoke(d);
    }

    private void ShowDialogue_4()
    {
        ShowDialogueEvtArgs d = new ShowDialogueEvtArgs(new List<string>() { "Dialogue1" }, new ID("nu"), FinishSequence);
        _showDialogueCmd.Invoke(d);
    }

    private void FinishSequence()
    {
        Destroy(evithRef);
        Destroy(nuRef);
        evithRef = null;
        nuRef = null;
        _enableMovementCmd.Invoke();
        _toggleGameplayUiCmd.Invoke();
    }
}
