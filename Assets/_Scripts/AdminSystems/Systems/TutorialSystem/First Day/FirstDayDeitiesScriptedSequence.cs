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

    private bool enabledTutorial = false;
    public void setEnabledTutorial(bool state)
    {
        enabledTutorial = state;
    }

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
        if (!enabledTutorial)
            return;

        evithRef = Instantiate(evithPrefab, evithSpawnPos);
        nuRef = Instantiate(nuPrefab, nuSpawnPos);
        evithRef.GetComponent<SpriteRenderer>().flipX = true;
        nuRef.GetComponent<SpriteRenderer>().flipX = true;
        _disableMovementCmd.Invoke();
        _toggleGameplayUiCmd.Invoke();
        StartNuEvithIntroSequence();
    }

    #region Dialogue Sequence
    public void StartNuEvithIntroSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Vaya, vaya, bonita pastelería tienes aquí montada, jovenzuelo!"},
         new ID("evith"),
            () => { this.EvithNuIntroSequenceTwo(); }));
    }
    private void EvithNuIntroSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Permíteme presentarme. Soy Nu, Ángel Patrón de las Galletas y guía de los habitantes de este mundo. Y mi impresentable compañera es..."},
        new ID("nu"),
           () => { this.EvithNuIntroSequenceThree(); }));
    }
    private void EvithNuIntroSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Para tí soy la majestuosa Evith, Reina del Caos Eterno y la auténtica Patrona de las Galletas! ¡No te dejes engañar por este santurrón presuntuoso!"},
        new ID("evith"),
           () => { this.EvithNuIntroSequenceFour(); }));
    }
    private void EvithNuIntroSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡No es momento de discutir! Recuerda nuestro propósito en este lugar."},
       new ID("nu"),
          () => { EvithNuIntroSequenceFive(); }));
    }
    private void EvithNuIntroSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¿Nuestro propósito? ¡Ah, sí! Hemos escuchado tu deseo."},
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
            "¿Quieres hacer algo por el pueblo, verdad? ¡Pues nosotros estamos aquí porque hemos decidido darte una oportunidad!"},
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
            "No hay ningún tipo de trampa ni de letra pequeña. Lo hemos decidido así porque hemos visto que tienes potencial."},
     new ID("evith"),
        () => { EvithNuIntroSequenceTen(); }));
    }
    private void EvithNuIntroSequenceTen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Empecemos pues. Será suficiente con ese horno y la mesa."},
     new ID("nu"),
        () => { StartEvithNuPreFurnaceSequence(); }));
    }

    // ========================================================
    //  NU & EVITH PRE FURNACE SEQUENCE
    // ========================================================

    private void StartEvithNuPreFurnaceSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Esta harina que tienes es muy normalucha. ¡Mejor usa esta Harina Encantada!"},
        new ID("evith"),
           () => { this.EvithNuPreFurnaceSequenceTwo(); }));
    }
    private void EvithNuPreFurnaceSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Con esta Harina Encantada podrás hornear Galletas mágicas que te ayudarán en tu meta."},
       new ID("nu"),
          () => { EvithNuPreFurnaceSequenceThree(); }));
    }
    private void EvithNuPreFurnaceSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Las Galletas harán cualquier tarea que les asignes. Además no necesitan alimento ni sueldo. ¡Son los sirvientes perfectos!"},
       new ID("evith"),
          () => { EvithNuPreFurnaceSequenceFour(); }));
    }
    private void EvithNuPreFurnaceSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Las Galletas necesitan instrucciones para saber qué deben hacer."},
      new ID("nu"),
         () => { EvithNuPreFurnaceSequenceFive(); }));
    }
    private void EvithNuPreFurnaceSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Pronto cobrará sentido ¡Vamos, ponte delante del horno o de la mesa y nosotros te iremos indicando!"},
      new ID("evith"),
         () => { EvithNuPreFurnaceSequenceEnd(); }));
    }
    private void EvithNuPreFurnaceSequenceEnd()
    {
        //Debug.Log("Se acabó la chapa");
        FinishSequence();
    }
    #endregion

    private void FinishSequence()
    {
        Destroy(evithRef);
        Destroy(nuRef);
        evithRef = null;
        nuRef = null;
        _enableMovementCmd.Invoke();
        _toggleGameplayUiCmd.Invoke();
        GetComponent<FirstDayFurnaceSequence>().SetTutorialActive(true);
    }
}
