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
            "Aquí podrás seleccionar la misión que quieres realizar."},
         new ID("evith"),
            () => { this.EvithNuTableSequenceTwo(); }));
    }
    private void EvithNuTableSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Tu primera misión será resolver el problema con los lobos del que te habló el alcalde."},
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
            "Aquí podrás colocar las piezas que has horneado para indicar cómo quieres que se cumpla la misión."},
         new ID("evith"),
            () => { this.FirstTimeQuestTableSequenceTwo(); }));
    }
    private void FirstTimeQuestTableSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Para empezar, vamos a colocar una pieza de Héroe para indicar qué ayudante se va a hacer cargo."},
        new ID("nu"),
           () => { this.FirstTimeQuestTableSequenceThree(); }));
    }

    private void FirstTimeQuestTableSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Todas las piezas que has horneado se encontrarán en tu bolsa."},
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
            "Aquí puedes ver todas las piezas disponibles. Las piezas están separadas por su categoría."},
         new ID("evith"),
            () => { this.FirstTimeStorageOpenedSequenceTwo(); }));
    }

    private void FirstTimeStorageOpenedSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Selecciona la pieza del ayudante básico que horneaste antes y colócala en su ranura correspondiente."},
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
            "Has colocado correctamente la pieza de Héroe. Ahora tan solo faltan dos piezas más por colocar.",
            "Te preguntarás por qué, dado que hay cinco huecos y solo has colocado una pieza."},
         new ID("evith"),
            () => { this.StartFirstTimeHeroPiecePlacedSequenceTwo(); }));
    }

    private void StartFirstTimeHeroPiecePlacedSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Es imprescindible que hayan colocadas una pieza de Héroe, una pieza de Acción y una pieza de Objetivo.",
            "Con ellas el ayudante podrá hacerse cargo de la misión."},
        new ID("nu"),
           () => { this.FirstTimeHeroPiecePlacedSequenceThree(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Al contrario que las demás piezas, las piezas de Objetivo dependen del contexto de la misión.",
            "No se pueden hornear porque irán apareciendo según la misión que quieras completar."},
         new ID("evith"),
            () => { this.FirstTimeHeroPiecePlacedSequenceSequenceFour(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Para esta misión, según las piezas insertadas en la Tabla, puedes hacer que el ayudante ataque a los lobos o que ayude al alcalde, por ejemplo."},
        new ID("nu"),
           () => { this.FirstTimeHeroPiecePlacedSequenceFive(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "O también puedes hacer que el ayudante ataque al alcalde ¡Para mí, esa es la decisión correcta!"},
         new ID("evith"),
            () => { this.FirstTimeHeroPiecePlacedSequenceSix(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceSix()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "En cualquier caso, la decisión es tuya. No dudes en experimentar con las piezas para ver qué resultados obtienes."},
        new ID("nu"),
           () => { this.FirstTimeHeroPiecePlacedSequenceSeven(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceSeven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Bueno, selecciona las piezas de Acción y Objetivo que quieras y colócalas en su ranura."},
         new ID("evith"),
            () => { this.FirstTimeHeroPiecePlacedSequenceEight(); }));
    }

    private void FirstTimeHeroPiecePlacedSequenceEight()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Una vez que las hayas colocado, estará todo listo para formar la Tabla.",
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
            "¡Y con eso ya has horneado tu primera Tabla de misión! ¿Ha resultado ser bastante más sencillo de lo que parecía, verdad?"},
         new ID("evith"),
            () => { this.FirstQuestCompletedSequenceTwo(); }));
    }

    private void FirstQuestCompletedSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Con la Tabla de misión horneada, el ayudante sabrá lo que tiene que hacer para resolver el problema."},
        new ID("nu"),
           () => { this.FirstQuestCompletedSequenceThree(); }));
    }

    private void FirstQuestCompletedSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Bueno, se está haciendo un poco tarde, quizás deberías de cerrar la pastelería por hoy."},
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
