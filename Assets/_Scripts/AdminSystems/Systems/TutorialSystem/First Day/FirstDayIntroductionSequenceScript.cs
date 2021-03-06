using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDayIntroductionSequenceScript : MonoBehaviour
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    [SerializeField] private bool enabledTutorial = true;

    public void OnEnable()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
    }

    private void Awake()
    {
        Admin.Global.EventSystem.GetCallbackByName<EventVoid>("day_sys", "tutorial_day_started").OnInvoked += StartIntroductionSequence;
    }

    public void StartIntroductionSequence()
    {

        if (enabledTutorial)
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "En un peque?o pueblo, un joven ha abierto una pasteler?a.",
            "El pueblo ha visto d?as mejores. Los problemas surgen por doquier.",
            "El joven escucha impotente, d?a tras d?a, los infortunios de sus clientes.",
            "Hasta que un d?a...",
            "'?Quiero poder hacer algo al respecto!' Dese? el joven.",
            "Lo que no sab?a en ese momento era que su deseo iba a ser concedido ese mismo d?a."},
            new ID("narrator"),
               () => { NarratorDialogFinishedCallback(); }));
        }
        else
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            ""},
              new ID("narrator"),
                 () => { NarratorDialogFinishedCallback(); }));
        }
        
    }

    private void NarratorDialogFinishedCallback()
    {
        if(enabledTutorial)
            GetComponent<FirstDayDeitiesScriptedSequence>().setEnabledTutorial(true);

        Admin.Global.EventSystem.GetCallbackByName<EventVoid>("day_sys", "tutorial_day_started").OnInvoked -= StartIntroductionSequence;
        enabledTutorial = false;
    }
}
