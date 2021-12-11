using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstNightShopSecuence : MonoBehaviour
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;
    private EventVoid _disableCharMovementCmd;

    private bool tutorialActive = false;
    public void SetTutorialActive(bool state)
    {
        this.tutorialActive = state;
    }

    private void Awake()
    {
        GameEventSystem evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
        _disableCharMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "disable_movement");
    }

    [MethodButton]
    public void ShowFFirstTimeOpenShopSequence()
    {
        if (!tutorialActive)
            return;

        StartFirstTimeOpenShopSequence();
    }

    private void StartFirstTimeOpenShopSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Aquí podrás intercambiar el favor que consigas por recetas de ayudantes avanzados e ingredientes para hornear nuevas piezas de misión."},
        new ID("nu"),
           () => { this.StartFirstTimeOpenShopSequenceOne(); }));
    }

    #region First Time Open Shop Sequence
    private void StartFirstTimeOpenShopSequenceOne()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "A diferencia del ayudante básico, los ayudantes avanzados necesitan ingredientes para ser horneados."},
         new ID("evith"),
            () => { this.FirstTimeOpenShopSequenceTwo(); }));
    }
    private void FirstTimeOpenShopSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Es cierto que siempre podrás recurrir a los ayudantes básicos para que se encarguen de ciertas misiones sencillas sin importancia, pero tienen capacidades muy limitadas."},
        new ID("nu"),
           () => { this.FirstTimeOpenShopSequenceThree(); }));
    }
    private void FirstTimeOpenShopSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Si quieres asegurarte de obtener resultados de verdad para las misiones importantes, necesitarás la ayuda de los ayudantes avanzados."},
        new ID("evith"),
           () => { this.FirstTimeOpenShopSequenceFour(); }));
    }
    private void FirstTimeOpenShopSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Cada uno de los ayudantes, al igual que las Piezas de misión tienen tres atributos. Estos atributos son Ayudar, Persuasión y Agresión.",
            "El atributo de Ayudar es un indicador para medir la capacidad del ayudante para proteger y asistir a alguien a lograr sus objetivos."},
       new ID("nu"),
          () => { FirstTimeOpenShopSequenceFive(); }));
    }

    private void FirstTimeOpenShopSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Es la mejor opción cuando te interesa ayudar a un Objetivo en particular. Aunque es la más aburrida en mi opinión."},
       new ID("evith"),
          () => { FirstTimeOpenShopSequenceSix(); }));
    }

    private void FirstTimeOpenShopSequenceSix()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "El atributo de Persuasión tiene influencia en la capacidad de cambiar la voluntad de un Objetivo."},
       new ID("nu"),
          () => { FirstTimeOpenShopSequenceSeven(); }));
    }

    private void FirstTimeOpenShopSequenceSeven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Es la opción perfecta para hacer cambiar de opinión a la gente, aunque también puede ser la más impredecible de los tres.",
            "Finalmente, el atributo de Agresión, mi favorito, indica cómo de útil es el ayudante cuando quieres emplear métodos como agredir, robar o sabotear para resolver una misión."},
       new ID("evith"),
          () => { FirstTimeOpenShopSequenceEight(); }));
    }

    private void FirstTimeOpenShopSequenceEight()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Dependerá del contexto, pero generalmente la Agresión implica perjudicar de alguna manera al Objetivo."},
       new ID("nu"),
          () => { FirstTimeOpenShopSequenceNine(); }));
    }

    private void FirstTimeOpenShopSequenceNine()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Ahora que sabes qué significa cada atributo, ya estás preparado para afrontar las misiones como gustes."},
       new ID("evith"),
          () => { FirstTimeOpenShopSequenceTen(); }));
    }

    private void FirstTimeOpenShopSequenceTen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Si quieres tener mayores resultados, te recomendamos utilizar ayudantes avanzados junto con las demás Piezas de misión."},
       new ID("nu"),
          () => { FirstTimeOpenShopSequenceEleven(); }));
    }

    private void FirstTimeOpenShopSequenceEleven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Si te preguntas cómo le habrá ido a tu primera Galleta mágica, puedes preguntárselo al alcalde mañana cuando pase por aquí."},
       new ID("evith"),
          () => { FinishFirstTimeOpenShopSequence(); }));
    }

    #endregion

    private void FinishFirstTimeOpenShopSequence()
    {
        tutorialActive = false;
        _disableCharMovementCmd.Invoke();
    }
}
