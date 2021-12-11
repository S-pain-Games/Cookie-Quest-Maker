using CQM.Components;
using CQM.UI.QuestMakingTable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDayTableSecuence : MonoBehaviour
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    public event Action<QuestPieceFunctionalComponent> OnPieceSocketed;

    private EventVoid _enableCharMovementCmd;
    private EventVoid _disableCharMovementCmd;
    
    private void Awake()
    {
        GameEventSystem evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");

        _enableCharMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        _disableCharMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "disable_movement");


        HeroPieceSocket.OnPieceSocketed += OnHeroPieceSocketed;
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

    private bool firstSequenceEnabled = true;

    [MethodButton]
    public void ShowFirstTimeTableOpenedSequence()
    {
        if (!tutorialActive || !firstSequenceEnabled)
            return;

        StartNuEvithTableSequence();
    }

    #region First Time Open Dialogue Sequence
    private void StartNuEvithTableSequence()
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
        _disableCharMovementCmd.Invoke();
    }

    // ==============================================================================================
    // FIRST COOKIE HERO BAKED
    // ==============================================================================================

    private bool firstTimeQuestTableSequenceEnabled = true;

    [MethodButton]
    public void ShowFirstTimeQuestTableSequence()
    {
        if (!tutorialActive || !firstTimeQuestTableSequenceEnabled)
            return;

        StartFirstTimeQuestTableSequence();
    }

    #region First Time Quest Table Sequence
    private void StartFirstTimeQuestTableSequence()
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
        _disableCharMovementCmd.Invoke();
    }

    // ==============================================================================================
    // First time Storage Opened
    // ==============================================================================================

    private bool firstTimeStorageOpened = true;

    [MethodButton]
    public void ShowFirstTimeStorageOpenedSequence()
    {
        if (!tutorialActive || firstTimeQuestTableSequenceEnabled || !firstTimeStorageOpened)
            return;

        StartFirstTimeStorageOpenedSequence();
    }

    #region First Time Storage Opened Sequence
    private void StartFirstTimeStorageOpenedSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Aqu� puedes ver todas las piezas disponibles. Las piezas est�n separadas por su categor�a."},
         new ID("evith"),
            () => { this.FirstTimeStorageOpenedSequenceTwo(); }));
    }

    private void FirstTimeStorageOpenedSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Selecciona la pieza del ayudante b�sico que horneaste antes y col�cala en su ranura correspondiente."},
        new ID("nu"),
           () => { this.FinishFirstActionCookieBakedSequence(); }));
    }

    #endregion

    private void FinishFirstActionCookieBakedSequence()
    {
        firstTimeStorageOpened = false;
        _disableCharMovementCmd.Invoke();
    }

    // ==============================================================================================
    // First Time Hero Piece Placed
    // ==============================================================================================

    [SerializeField] private UIPieceSocketBehaviour HeroPieceSocket;

    private bool firstTimeHeroPiecePlaced = true;

    [MethodButton]
    public void OnHeroPieceSocketed(QuestPieceFunctionalComponent piece)
    {
        if (!tutorialActive || !firstTimeHeroPiecePlaced)
            return;

        StartFirstTimeHeroPiecePlacedSequence();
    }

    #region First Time Hero Piece Placed Sequence

    private void StartFirstTimeHeroPiecePlacedSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Has colocado correctamente la pieza de H�roe. Ahora tan solo faltan dos piezas m�s por colocar.",
            "Te preguntar�s por qu�, dado que hay cinco huecos y solo has colocado una pieza."},
         new ID("evith"),
            () => { this.StartFirstTimeHeroPiecePlacedSequenceTwo(); }));
    }

    private void StartFirstTimeHeroPiecePlacedSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Es imprescindible que hayan colocadas una pieza de H�roe, una pieza de Acci�n y una pieza de Objetivo.",
            "Con ellas el ayudante podr� hacerse cargo de la misi�n."},
        new ID("nu"),
           () => { this.FirstTimeHeroPiecePlacedSequenceThree(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Al contrario que las dem�s piezas, las piezas de Objetivo dependen del contexto de la misi�n.",
            "No se pueden hornear porque ir�n apareciendo seg�n la misi�n que quieras completar."},
         new ID("evith"),
            () => { this.FirstTimeHeroPiecePlacedSequenceSequenceFour(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Para esta misi�n, seg�n las piezas insertadas en la Tabla, puedes hacer que el ayudante ataque a los lobos o que ayude al alcalde, por ejemplo."},
        new ID("nu"),
           () => { this.FirstTimeHeroPiecePlacedSequenceFive(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "O tambi�n puedes hacer que el ayudante ataque al alcalde �Para m�, esa es la decisi�n correcta!"},
         new ID("evith"),
            () => { this.FirstTimeHeroPiecePlacedSequenceSix(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceSix()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "En cualquier caso, la decisi�n es tuya. No dudes en experimentar con las piezas para ver qu� resultados obtienes."},
        new ID("nu"),
           () => { this.FirstTimeHeroPiecePlacedSequenceSeven(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceSeven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Bueno, selecciona las piezas de Acci�n y Objetivo que quieras y col�calas en su ranura."},
         new ID("evith"),
            () => { this.FirstTimeHeroPiecePlacedSequenceEight(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceEight()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Una vez que las hayas colocado, estar� todo listo para formar la Tabla.",
            "Dejaremos que completes la tabla por tu cuenta."},
        new ID("nu"),
           () => { this.FinishFirstTimeHeroPiecePlacedSequence(); }));
    }

    #endregion

    private void FinishFirstTimeHeroPiecePlacedSequence()
    {
        firstTimeHeroPiecePlaced = false;
        _disableCharMovementCmd.Invoke();
    }

    // ==============================================================================================
    // First Quest Completed
    // ==============================================================================================

    //Galleta normal horneada
    private bool firstQuestCompleted = true;

    [MethodButton]
    public void ShowFirstQuestCompletedSequence()
    {
        if (!tutorialActive || !firstQuestCompleted)
            return;

        _disableCharMovementCmd.Invoke();

        StartFirstQuestCompletedSequence();
    }

    #region First Quest Completed Sequence
    private void StartFirstQuestCompletedSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "�Y con eso ya has horneado tu primera Tabla de misi�n! �Ha resultado ser bastante m�s sencillo de lo que parec�a, verdad?"},
         new ID("evith"),
            () => { this.FirstQuestCompletedSequenceTwo(); }));
    }

    private void FirstQuestCompletedSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Con la Tabla de misi�n horneada, el ayudante sabr� lo que tiene que hacer para resolver el problema."},
        new ID("nu"),
           () => { this.FirstQuestCompletedSequenceThree(); }));
    }

    private void FirstQuestCompletedSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Bueno, se est� haciendo un poco tarde, quiz�s deber�as de cerrar la pasteler�a por hoy."},
         new ID("evith"),
            () => { this.FinishFirstQuestCompletedSequence(); }));
    }

    #endregion

    private void FinishFirstQuestCompletedSequence()
    {
        firstQuestCompleted = false;
        tutorialActive = false;
        //Debug.Log("TUTORIAL COMPLETED");
        _enableCharMovementCmd.Invoke();


        GetComponent<FirstNightDeitiesScriptedSequence>().SetTutorialActive(true);
    }

}
