using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDayIntroductionSequenceScript : MonoBehaviour
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    private void Awake()
    {
        Admin.Global.EventSystem.GetCallbackByName<EventVoid>("day_sys", "tutorial_day_started").OnInvoked += StartIntroductionSequence;
    }

    public void OnEnable()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
    }

    public void StartIntroductionSequence()
    {
        //Dentro pantalla en negro

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

    private void NarratorDialogFinishedCallback()
    {
        //Fuera pantalla en negro
        Admin.Global.EventSystem.GetCallbackByName<EventVoid>("day_sys", "tutorial_day_started").OnInvoked -= StartIntroductionSequence;
    }
}
