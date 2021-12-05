using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDayFurnaceSequence : MonoBehaviour
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    private void Awake()
    {
        GameEventSystem evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
    }

    //Tutorial activo. Se activa en cuanto se termina de hablar con Evith y Nu antes de abrir el horno
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
    public void ShowFirstTimeFurnaceOpenedSequence()
    {
        if (!tutorialActive || !firstSequenceEnabled)
            return;

        StartNuEvithFurnaceSequence();
    }

    #region First Time Open Dialogue Sequence
    public void StartNuEvithFurnaceSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Bueno, empecemos por lo más básico. Si quieres tener ayudantes, necesitas hornearlos primero."},
         new ID("evith"),
            () => { this.EvithNuFurnaceSequenceTwo(); }));
    }
    private void EvithNuFurnaceSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "De momento será suficiente con el ayudante básico. Para hacer ayudantes tan solo requieren de harina encantada."},
        new ID("nu"),
           () => { this.EvithNuFurnaceSequenceThree(); }));
    }
    private void EvithNuFurnaceSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "No son muy diestros, pero para tareas sencillas son más que suficiente."},
        new ID("evith"),
           () => { this.EvithNuFurnaceSequenceFour(); }));
    }
    private void EvithNuFurnaceSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Procede a hornear al ayudante."},
       new ID("nu"),
          () => { FinishFirstSequence(); }));
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
    private bool firstCookieBakedSequenceEnabled = true;

    [MethodButton]
    public void ShowFirstCookieBakedSequence()
    {
        if (!tutorialActive || !firstCookieBakedSequenceEnabled)
            return;

        StartFirstCookieBakedSequence();
    }

    #region First Time Open Dialogue Sequence
    public void StartFirstCookieBakedSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Muy bien! Ya tienes un pequeño sirviente dispuesto a obedecer órdenes."},
         new ID("evith"),
            () => { this.FirstCookieBakedSequenceTwo(); }));
    }
    private void FirstCookieBakedSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Sin embargo, por sí solo no puede hacer gran cosa. Necesita indicaciones para saber qué es lo que tiene que hacer."},
        new ID("nu"),
           () => { this.FirstCookieBakedSequenceThree(); }));
    }
    private void FirstCookieBakedSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Los ayudantes necesitan un manual de instrucciones para poder actuar, por así decirlo."},
        new ID("evith"),
           () => { this.FirstCookieBakedSequenceFour(); }));
    }
    private void FirstCookieBakedSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Para indicar qué deberá de hacer el ayudante, necesitas rellenar una Tabla de Misión. La Tabla de Misión se compone de 5 tipos de piezas."},
       new ID("nu"),
          () => { FirstCookieBakedSequenceFive(); }));
    }

    private void FirstCookieBakedSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Las piezas son indicaciones acerca de la misión."},
       new ID("evith"),
          () => { FirstCookieBakedSequenceSix(); }));
    }

    private void FirstCookieBakedSequenceSix()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "La primera pieza es la de Héroe, que representa al ayudante que se hará cargo de la misión, como la que acabas de hornear.",
            "La segunda pieza es de Acción, para indicar el método de resolución de la misión."},
       new ID("nu"),
          () => { FirstCookieBakedSequenceSeven(); }));
    }

    private void FirstCookieBakedSequenceSeven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Con ella puedes indicar si el ayudante actuará de forma agresiva, pacífica o persuasiva cuando se ponga a ello."},
       new ID("evith"),
          () => { FirstCookieBakedSequenceEight(); }));
    }

    private void FirstCookieBakedSequenceEight()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "El tercer y cuarto tipo de pieza son los de Modificador y Objeto."},
       new ID("nu"),
          () => { FirstCookieBakedSequenceNine(); }));
    }

    private void FirstCookieBakedSequenceNine()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Sirven para añadir detalles adicionales sobre maneras de actuar o con qué utensilio."},
       new ID("evith"),
          () => { FirstCookieBakedSequenceTen(); }));
    }

    private void FirstCookieBakedSequenceTen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "A continuación hornea una pieza de Acción."},
       new ID("nu"),
          () => { FinishFirstCookieBakedSequence(); }));
    }

    #endregion

    private void FinishFirstCookieBakedSequence()
    {
        firstCookieBakedSequenceEnabled = false;
    }

    // ==============================================================================================
    // First Action Cookie Baked
    // ==============================================================================================

    //Galleta normal horneada
    private bool firstActionCookieBaked = true;

    [MethodButton]
    public void ShowFirstActionCookieBakedSequence()
    {
        if (!tutorialActive || firstCookieBakedSequenceEnabled || !firstActionCookieBaked)
            return;

        StartFirstActionCookieBakedSequence();
    }

    #region First Time Open Dialogue Sequence
    public void StartFirstActionCookieBakedSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Ahora tienes horneadas una pieza de Héroe y una pieza de Acción, con esto ya tienes suficiente para montar una Tabla de misión."},
         new ID("evith"),
            () => { this.FirstActionCookieBakedSequenceTwo(); }));
    }

    private void FirstActionCookieBakedSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Ya dispones de las piezas necesarias para hornear tu primera Tabla de misión."},
        new ID("nu"),
           () => { this.FirstActionCookieBakedSequenceThree(); }));
    }

    public void FirstActionCookieBakedSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Vamos, acércate a la mesa! Te seguiremos dando indicaciones."},
         new ID("evith"),
            () => { this.FinishFirstActionCookieBakedSequence(); }));
    }

    #endregion

    private void FinishFirstActionCookieBakedSequence()
    {
        firstActionCookieBaked = false;

        //Activar tutorial de la mesa
        GetComponent<FirstDayTableSecuence>().SetTutorialActive(true);
    }

}
