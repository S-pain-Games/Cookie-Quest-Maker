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
            "En un pequeño pueblo, un joven ha abierto una pastelería.",
            "El pueblo ha visto días mejores. Los problemas surgen por doquier.",
            "El joven escucha impotente, día tras día, los infortunios de sus clientes.",
            "Hasta que un día...",
            "'¡Quiero poder hacer algo al respecto!' Deseó el joven.",
            "Lo que no sabía en ese momento era que su deseo iba a ser concedido ese mismo día."},
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
