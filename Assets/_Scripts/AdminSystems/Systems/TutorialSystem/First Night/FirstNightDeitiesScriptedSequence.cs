using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstNightDeitiesScriptedSequence : MonoBehaviour
{
    private EventVoid _enableMovementCmd;
    private EventVoid _disableMovementCmd;
    private EventVoid _toggleGameplayUiCmd;
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

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
        _disableMovementCmd.Invoke();
        _toggleGameplayUiCmd.Invoke();

        StartNuEvithFirstNightSequence();
    }

    #region Dialogue Sequence
    public void StartNuEvithFirstNightSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Estamos aquí de vuelta! ¿Te preguntarás qué ha sido de la Galleta mágica, verdad?"},
         new ID("evith"),
            () => { this.EvithNuFirstNightSequenceTwo(); }));
    }


    private void EvithNuFirstNightSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "La Galleta mágica ya ha partido con la Tabla de la misión mientras cerrabas la pastelería."},
        new ID("nu"),
           () => { this.EvithNuFirstNightSequenceThree(); }));
    }

    private void EvithNuFirstNightSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Las Galletas mágicas se marcharán automáticamente al anochecer. Seguirán al pie de la letra las instrucciones de la misión."},
        new ID("evith"),
           () => { this.EvithNuFirstNightSequenceFour(); }));
    }

    private void EvithNuFirstNightSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Además actuarán en completo sigilo, no dejarán que nadie las detecte."},
       new ID("nu"),
          () => { EvithNuFirstNightSequenceFive(); }));
    }

    private void EvithNuFirstNightSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Por lo cual, tanto si decides hacer fechorías como si no, ¡nadie sabrá que eres el responsable! ¡¿No es genial?!"},
       new ID("evith"),
          () => { EvithNuFirstNightSequenceSix(); }));
    }

    private void EvithNuFirstNightSequenceSix()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "En cualquier caso, no esperes su regreso. Tras haber cumplido su cometido, las Galletas desaparecerán al amanecer sin dejar rastro."},
      new ID("nu"),
         () => { EvithNuFirstNightSequenceSeven(); }));
    }

    private void EvithNuFirstNightSequenceSeven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Todas las piezas utilizadas para la Tabla de misión también desaparecerán. Por lo que tendrás que hornear más para las siguientes misiones."},
      new ID("evith"),
         () => { EvithNuFirstNightSequenceEight(); }));
    }

    private void EvithNuFirstNightSequenceEight()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Durante el día podrás hornear las piezas que quieras, siempre y cuando tengas materiales suficientes."},
      new ID("nu"),
         () => { EvithNuFirstNightSequenceNine(); }));
    }

    private void EvithNuFirstNightSequenceNine()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Tendrás toda la Harina Encantada que quieras, pero el resto de materiales van a ser más complicados de conseguir."},
     new ID("evith"),
        () => { EvithNuFirstNightSequenceTen(); }));
    }

    private void EvithNuFirstNightSequenceTen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Te podemos ofrecer nuevos moldes de piezas e ingredientes para tus próximas misiones."},
     new ID("nu"),
        () => { EvithNuFirstNightSequenceEleven(); }));
    }


    private void EvithNuFirstNightSequenceEleven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡A un módico precio, por supuesto! No pienses que te daremos todo hecho."},
        new ID("evith"),
           () => { this.EvithNuFirstNightSequenceTwelve(); }));
    }

    private void EvithNuFirstNightSequenceTwelve()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "En función de los resultados de las misiones, tendrás mi apoyo si consigues resolver los problemas de forma favorable."},
       new ID("nu"),
          () => { EvithNuFirstNightSequenceThirteen(); }));
    }

    private void EvithNuFirstNightSequenceThirteen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Por el contrario, si tus decisiones empeoran la situación, ¡obtendrás mi favor, efectivamente!"},
       new ID("evith"),
          () => { EvithNuFirstNightSequenceFourteen(); }));
    }

    private void EvithNuFirstNightSequenceFourteen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "En cualquier caso, recuerda que te estamos poniendo a prueba."},
      new ID("nu"),
         () => { EvithNuFirstNightSequenceFifteen(); }));
    }

    private void EvithNuFirstNightSequenceFifteen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Así es! ¡Queremos ver lo que eres capaz de hacer con este poder!"},
      new ID("evith"),
         () => { EvithNuFirstNightSequenceEnd(); }));
    }

    private void EvithNuFirstNightSequenceEnd()
    {
        FinishSequence();
    }
    #endregion

    private void FinishSequence()
    {
        _enableMovementCmd.Invoke();
        _toggleGameplayUiCmd.Invoke();
    }
}
