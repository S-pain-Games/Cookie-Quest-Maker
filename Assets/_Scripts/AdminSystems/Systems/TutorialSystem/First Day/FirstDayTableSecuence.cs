using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDayTableSecuence : MonoBehaviour
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    private void Awake()
    {
        GameEventSystem evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
    }

    //Tutorial activo. Se activa en cuanto se terminan los tutoriales del Horno
    private bool tutorialActive = false;
    public void SetTutorialActive(bool state)
    {
        this.tutorialActive = state;
    }

    // ==============================================================================================
    // First time opened sequence
    // ==============================================================================================

    //Mostrar dialogo inicial
    private bool firstSequenceEnabled = true;

    [MethodButton]
    public void ShowFirstTimeTableOpenedSequence()
    {
        if (!tutorialActive || !firstSequenceEnabled)
            return;

        StartNuEvithTableSequence();
    }

    #region First Time Open Dialogue Sequence
    public void StartNuEvithTableSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Aqu� podr�s seleccionar la misi�n que quieres realizar."},
         new ID("evith"),
            () => { this.EvithNuTableSequenceTwo(); }));
    }
    private void EvithNuTableSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Tu primera misi�n ser� resolver el problema con los lobos del que te habl� el alcalde."},
        new ID("nu"),
           () => { this.FinishFirstSequence(); }));
    }

    #endregion

    private void FinishFirstSequence()
    {
        firstSequenceEnabled = false;
    }

    // ==============================================================================================
    // FIRST COOKIE HERO BAKED
    // ==============================================================================================

    //Galleta normal horneada
    private bool firstTimeQuestTableSequenceEnabled = true;

    [MethodButton]
    public void ShowFirstTimeQuestTableSequence()
    {
        if (!tutorialActive || !firstTimeQuestTableSequenceEnabled)
            return;

        StartFirstTimeQuestTableSequence();
    }

    #region First Time Quest Table Sequence
    public void StartFirstTimeQuestTableSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Aqu� podr�s colocar las piezas que has horneado para indicar c�mo quieres que se cumpla la misi�n."},
         new ID("evith"),
            () => { this.FirstTimeQuestTableSequenceTwo(); }));
    }
    private void FirstTimeQuestTableSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Para empezar, vamos a colocar una pieza de H�roe para indicar qu� ayudante se va a hacer cargo."},
        new ID("nu"),
           () => { this.FirstTimeQuestTableSequenceThree(); }));
    }

    private void FirstTimeQuestTableSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Todas las piezas que has horneado se encontrar�n en tu bolsa."},
        new ID("evith"),
           () => { this.FinishFirstTimeQuestTableSequence(); }));
    }

    #endregion

    private void FinishFirstTimeQuestTableSequence()
    {
        firstTimeQuestTableSequenceEnabled = false;
    }

    // ==============================================================================================
    // First time Storage Opened
    // ==============================================================================================

    //Galleta normal horneada
    private bool firstTimeStorageOpened = true;

    [MethodButton]
    public void ShowFirstTimeStorageOpenedSequence()
    {
        if (!tutorialActive || firstTimeQuestTableSequenceEnabled || !firstTimeStorageOpened)
            return;

        StartFirstTimeStorageOpenedSequence();
    }

    #region First Time Storage Opened Sequence
    public void StartFirstTimeStorageOpenedSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Aqu� puedes ver todas las piezas disponibles. Las piezas est�n separadas por su categor�a."},
         new ID("evith"),
            () => { this.FirstTimeStorageOpenedSequenceTwo(); }));
    }

    private void FirstTimeStorageOpenedSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Selecciona la pieza del ayudante b�sico que horneaste antes y col�cala en su ranura correspondiente"},
        new ID("nu"),
           () => { this.FinishFirstActionCookieBakedSequence(); }));
    }

    #endregion

    private void FinishFirstActionCookieBakedSequence()
    {
        firstTimeStorageOpened = false;
    }

    // ==============================================================================================
    // First Time Hero Piece Placed
    // ==============================================================================================

}
