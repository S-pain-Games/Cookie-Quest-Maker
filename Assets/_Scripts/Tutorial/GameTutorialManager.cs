using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorialManager : MonoBehaviour
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    private EventVoid _enableCharMovementCmd;
    private EventVoid _disableCharMovementCmd;

    [SerializeField] private bool enabledTutorial = true;

    // Start is called before the first frame update
    void Start()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");


        _enableCharMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        _disableCharMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "disable_movement");

        StartNuEvithIntroSequence();
    }

    private void OnEnable()
    {
        
    }

    public void StartIntroductionSequence()
    {
        if (!enabledTutorial)
            return;

        //Dentro pantalla en negro

        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "En un peque?o pueblo, un joven ha abierto una pasteler?a.",
            "El pueblo ha visto d?as mejores. Los problemas surgen por doquier.",
            "El joven escucha impotente, d?a tras d?a, los infortunios de sus clientes.",
            "Hasta que un d?a...",
            "'?Quiero poder hacer algo al respecto!' Dese? el joven.",
            "Lo que no sab?a en ese momento era que su deseo iba a ser concedido ese mismo d?a."},
            new ID("narrator"),
               () => { NarratorDialogFinishedCallback(); }));

        //Fuera pantalla en negro
    }

    private void NarratorDialogFinishedCallback()
    {

    }

    // ========================================================
    //  NU & EVITH INTRO SEQUENCE 1
    // ========================================================

    public void StartNuEvithIntroSequence()
    {
        if (!enabledTutorial)
            return;

        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "?Vaya, vaya, bonita pasteler?a tienes aqu? montada, jovenzuelo!"},
         new ID("evith"),
            () => { this.EvithNuIntroSequenceTwo(); }));
    }


    private void EvithNuIntroSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Perm?teme presentarme. Soy Nu, ?ngel Patr?n de las Galletas y gu?a de los habitantes de este mundo. Y mi impresentable compa?era es..."},
        new ID("nu"),
           () => { this.EvithNuIntroSequenceThree(); }));
    }

    private void EvithNuIntroSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "?Para t? soy la majestuosa Evith, Reina del Caos Eterno y la aut?ntica Patrona de las Galletas! ?No te dejes enga?ar por este santurr?n presuntuoso!"},
        new ID("evith"),
           () => { this.EvithNuIntroSequenceFour(); }));
    }

    private void EvithNuIntroSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "?No es momento de discutir! Recuerda nuestro prop?sito en este lugar."},
       new ID("nu"),
          () => { EvithNuIntroSequenceFive(); }));
    }

    private void EvithNuIntroSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "?Nuestro prop?sito? ?Ah, s?! Hemos escuchado tu deseo."},
       new ID("evith"),
          () => { EvithNuIntroSequenceSix(); }));
    }

    private void EvithNuIntroSequenceSix()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "No quieres seguir escuchando los problemas de la gente sin poder hacer nada al respecto."},
      new ID("nu"),
         () => { EvithNuIntroSequenceSeven(); }));
    }

    private void EvithNuIntroSequenceSeven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "?Quieres hacer algo por el pueblo, verdad? ?Pues nosotros estamos aqu? porque hemos decidido darte una oportunidad!"},
      new ID("evith"),
         () => { EvithNuIntroSequenceEight(); }));
    }

    private void EvithNuIntroSequenceEight()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Durante un tiempo limitado, vamos a ofrecerte parte de nuestro poder para que puedas tomar cartas en el asunto."},
      new ID("nu"),
         () => { EvithNuIntroSequenceNine(); }));
    }

    private void EvithNuIntroSequenceNine()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "No hay ning?n tipo de trampa ni de letra peque?a. Lo hemos decidido as? porque hemos visto que tienes potencial."},
     new ID("evith"),
        () => { EvithNuIntroSequenceTen(); }));
    }

    private void EvithNuIntroSequenceTen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Empecemos pues. Ser? suficiente con ese horno y la mesa."},
     new ID("nu"),
        () => { StartEvithNuPreFurnaceSequence(); }));
    }

    // ========================================================
    //  NU & EVITH PRE FURNACE SEQUENCE
    // ========================================================

    private void StartEvithNuPreFurnaceSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Esta harina que tienes es muy normalucha. ?Mejor usa esta Harina Encantada!"},
        new ID("evith"),
           () => { this.EvithNuPreFurnaceSequenceTwo(); }));
    }

    private void EvithNuPreFurnaceSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Con esta Harina Encantada podr?s hornear Galletas m?gicas que te ayudar?n en tu meta."},
       new ID("nu"),
          () => { EvithNuPreFurnaceSequenceThree(); }));
    }

    private void EvithNuPreFurnaceSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Las Galletas har?n cualquier tarea que les asignes. Adem?s no necesitan alimento ni sueldo. ?Son los sirvientes perfectos!"},
       new ID("evith"),
          () => { EvithNuPreFurnaceSequenceFour(); }));
    }

    private void EvithNuPreFurnaceSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Las Galletas necesitan instrucciones para saber qu? deben hacer."},
      new ID("nu"),
         () => { EvithNuPreFurnaceSequenceFive(); }));
    }

    private void EvithNuPreFurnaceSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Pronto cobrar? sentido ?Vamos, ponte delante del horno o de la mesa y nosotros te iremos indicando!"},
      new ID("evith"),
         () => { EvithNuPreFurnaceSequenceEnd(); }));
    }

    private void EvithNuPreFurnaceSequenceEnd()
    {
        Debug.Log("Se acab? la chapa");
        enabledTutorial = false;
        GetComponent<FirstDayDeitiesScriptedSequence>().setEnabledTutorial(true);
    }

}
