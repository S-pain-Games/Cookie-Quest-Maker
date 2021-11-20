using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSequence : MonoBehaviour
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
        evithRef = Instantiate(evithPrefab, evithSpawnPos);
        nuRef = Instantiate(nuPrefab, nuSpawnPos);
        evithRef.GetComponent<SpriteRenderer>().flipX = true;
        nuRef.GetComponent<SpriteRenderer>().flipX = true;
        _disableMovementCmd.Invoke();
        _toggleGameplayUiCmd.Invoke();

        StartPreEndingDialogSequence();
    }

    #region Pre Ending Dialog Sequence
    public void StartPreEndingDialogSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Ha pasado ya mucho tiempo. Diría que ya no es necesario seguir poniéndote a prueba."},
        new ID("nu"),
           () => { this.PreEndingDialogSequenceOne(); }));
    }

    private void PreEndingDialogSequenceOne()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Es una pena. ¡Me estaba entreteniendo bastante observando tu día a día!",
            "¡Qué rápido pasa el tiempo cuando te diviertes!" },
         new ID("evith"),
            () => { this.PreEndingDialogSequenceTwo(); }));
    }

    private void PreEndingDialogSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Tu deseo era el de poder hacer algo por el pueblo, por eso decidimos manifestarnos ante tí."},
        new ID("nu"),
           () => { this.PreEndingDialogSequenceThree(); }));
    }

    private void PreEndingDialogSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Te hemos prestado este poder durante un tiempo más que razonable. ¡Es hora de ver cómo ha ido la cosa!" },
        new ID("evith"),
           () => { this.PreEndingDialogSequenceEnd(); }));
    }

    private void PreEndingDialogSequenceEnd()
    {
        CalculateHapinessScore();
    }

    #endregion

    private void CalculateHapinessScore()
    {
        //Dependiendo del valor total de la felicidad de los NPCs, proseguir con una de las tres cadenas

        //StartGoodEndingSequence
    }

    #region Good Ending Dialog Sequence
   

    private void StartGoodEndingSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Teníamos grandes expectativas sobre qué harías por el pueblo. No sabíamos qué haría un ser humano normal y corriente con semejante poder."},
        new ID("nu"),
           () => { this.GoodEndingSequenceOne(); }));
    }

    private void GoodEndingSequenceOne()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Estaba convencida de que tarde o temprano ibas a hacer fechorías. ¡Pero no ha sido el caso! ¡Menuda decepción!"},
        new ID("evith"),
           () => { this.GoodEndingSequenceTwo(); }));
    }

    private void GoodEndingSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Parece que te has mantenido fiel a tu propósito hasta el final.",
            "Has dedicado tus esfuerzos a resolver los problemas de la gente de la forma más favorable posible."},
       new ID("nu"),
          () => { GoodEndingSequenceThree(); }));
    }

    private void GoodEndingSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Aunque no me entusiasme tu voluntad por ayudar a la gente. Tengo que admitir que tiene su mérito."},
       new ID("evith"),
          () => { GoodEndingSequenceFour(); }));
    }
    private void GoodEndingSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Al principio teníamos nuestras dudas. Pero viendo lo que has conseguido por voluntad propia. Hemos decidido lo siguiente."},
      new ID("nu"),
         () => { GoodEndingSequenceFive(); }));
    }
    private void GoodEndingSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Te permitimos conservar nuestro poder indefinidamente para que puedas utilizarlo como gustes!"},
      new ID("evith"),
         () => { GoodEndingSequenceSix(); }));
    }
    private void GoodEndingSequenceSix()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Así es. Es un privilegio que solo se le concede a unos pocos."},
      new ID("nu"),
         () => { GoodEndingSequenceSeven(); }));
    }
    private void GoodEndingSequenceSeven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Ahora llega el triste momento de la despedida."},
     new ID("evith"),
        () => { GoodEndingSequenceEight(); }));
    }
    private void GoodEndingSequenceEight()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Joven pastelero, dejamos el cuidado del pueblo en tus manos. Nosotros proseguiremos con nuestra labor."},
     new ID("nu"),
        () => { GoodEndingSequenceNine(); }));
    }

    private void GoodEndingSequenceNine()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡No nos echarás de menos! ¡Te visitaremos de allá para cuando!",
            "¡Y con esto se termina nuestra labor en este lugar! ¿Me pregunto cómo será el próximo candidato?"},
     new ID("evith"),
        () => { GoodEndingSequenceTen(); }));
    }

    private void GoodEndingSequenceTen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "No lo sabremos hasta que nos presentemos ante él. En fin, nos marchamos de inmediato. Te deseamos suerte."},
     new ID("nu"),
        () => { GoodEndingSequenceEleven(); }));
    }

    private void GoodEndingSequenceEleven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Si vas a hacer alguna fechoría, será mejor que me avises antes!¡Hasta la próxima!"},
     new ID("evith"),
        () => { EvithNuPreFurnaceSequenceEnd(); }));
    }

    private void EvithNuPreFurnaceSequenceEnd()
    {
        //Debug.Log("Se acabó la chapa");
        FinissEndingSequence();
    }
    #endregion

    private void FinissEndingSequence()
    {
        Destroy(evithRef);
        Destroy(nuRef);
        evithRef = null;
        nuRef = null;
        _enableMovementCmd.Invoke();
        _toggleGameplayUiCmd.Invoke();
    }
}
