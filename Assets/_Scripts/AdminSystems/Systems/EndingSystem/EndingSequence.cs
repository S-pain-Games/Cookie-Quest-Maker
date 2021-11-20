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
        () => { GoodEndingSequenceEnd(); }));
    }

    private void GoodEndingSequenceEnd()
    {
        HideNuAndEvith();

        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Gracias a sus buenos actos, el pueblo volvió a prosperar.",
            "Los problemas inusuales seguían ocurriendo día a día, pero el joven pastelero siguió actuando como defensor del pueblo desde las sombras.",
            "Y seguirá velando por el bien del pueblo y su gente gracias a las Galletas mágicas." +
            "FIN"},
          new ID("narrator"),
             () => { FinishEndingSequence(); }));
    }
    #endregion

    #region Neutral Ending Dialog Sequence

    private void StartNeutralEndingSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Teníamos nuestras dudas de si sería seguro dejarte a cargo de resolver los problemas del pueblo haciendo uso de nuestros poderes."},
        new ID("nu"),
           () => { this.NeutralEndingSequenceOne(); }));
    }

    private void NeutralEndingSequenceOne()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Y al final se ha cumplido la opción con mayor probabilidad!",
            "Eres tan solo un tipo normal que toma buenas o malas decisiones, ya sea a propósito o no."},
        new ID("evith"),
           () => { this.NeutralEndingSequenceTwo(); }));
    }

    private void NeutralEndingSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Seguimos pensando que tienes buenas intenciones. Pero parece ser que has optado por mantener el estado natural del pueblo."},
       new ID("nu"),
          () => { NeutralEndingSequenceThree(); }));
    }

    private void NeutralEndingSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Sí, sólo hace falta mirar a tu alrededor, ¡la situación no ha cambiado gran cosa desde el primer día!",
            "¡Yo sinceramente pensaba que irías a optar por uno de los dos extremos!"},
       new ID("evith"),
          () => { NeutralEndingSequenceFour(); }));
    }
    private void NeutralEndingSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "En cualquier caso, el pueblo permanece en su estado natural. Quizás esa sea la decisión correcta."},
      new ID("nu"),
         () => { NeutralEndingSequenceFive(); }));
    }
    private void NeutralEndingSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Bueno, ¡el tiempo de prueba se ha acabado! Lamentablemente ya no podrás seguir horneando Galletas mágicas."},
      new ID("evith"),
         () => { NeutralEndingSequenceSix(); }));
    }
    private void NeutralEndingSequenceSix()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "No es nada personal. De hecho pienso que has mantenido el equilibrio de una forma sublime teniendo en cuenta las circunstancias."},
      new ID("nu"),
         () => { NeutralEndingSequenceSeven(); }));
    }
    private void NeutralEndingSequenceSeven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Sí, y de todas formas… ¡¿Qué narices ocurre en este pueblo para que ocurran tantos sucesos inusuales?!",
            "¡¿No estará el pueblo maldecido o algo?!"},
     new ID("evith"),
        () => { NeutralEndingSequenceEight(); }));
    }
    private void NeutralEndingSequenceEight()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Sea como fuere, nuestro trabajo ha terminado en este lugar."},
     new ID("nu"),
        () => { NeutralEndingSequenceNine(); }));
    }

    private void NeutralEndingSequenceNine()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Sí, es el momento de la triste despedida.",
            "¡Qué lástima tener que marcharnos! ¡En este pueblo nunca te aburres, siempre pasa de todo!"},
     new ID("evith"),
        () => { NeutralEndingSequenceTen(); }));
    }

    private void NeutralEndingSequenceTen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Joven pastelero, aunque no puedas seguir horneando Galletas mágicas, estoy convencido de que seguirás con tu propósito.",
            "Velarás por la seguridad del pueblo con nuestros poderes o sin ellos.."},
     new ID("nu"),
        () => { NeutralEndingSequenceEleven(); }));
    }

    private void NeutralEndingSequenceEleven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡No nos eches de menos! De hecho puede que nos pasemos por aquí algún día.",
            "¡Tengo ganas de ver si el próximo candidato será igual!" },
     new ID("evith"),
        () => { NeutralEndingSequenceTwelve(); }));
    }

    private void NeutralEndingSequenceTwelve()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "No lo sabremos hasta que nos presentemos ante él. En fin, nos marchamos. Te deseamos suerte."},
     new ID("nu"),
        () => { NeutralEndingSequenceThirteen(); }));
    }

    private void NeutralEndingSequenceThirteen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Si de repente piensas en hacer fechorías, será mejor que me avises antes! ¡Hasta la próxima!" },
     new ID("evith"),
        () => { NeutralEndingSequenceEnd(); }));
    }

    private void NeutralEndingSequenceEnd()
    {
        HideNuAndEvith();

        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Aún habiendo hecho uso de las Galletas mágicas, el pueblo se mantuvo en su estado natural, lejos de la prosperidad." ,
            "Los problemas inusuales seguían ocurriendo día a día, pero aún con todo, los vecinos no han perdido la esperanza de que algún día la situación mejorará.",
            "El joven pastelero seguirá velando por el bien del pueblo y su gente sin la ayuda de las Galletas mágicas.",
            "FIN"},
          new ID("narrator"),
             () => { FinishEndingSequence(); }));
    }
    #endregion


    private void HideNuAndEvith()
    {
        Destroy(evithRef);
        Destroy(nuRef);
        evithRef = null;
        nuRef = null;
    }

    private void FinishEndingSequence()
    {
        _enableMovementCmd.Invoke();
        _toggleGameplayUiCmd.Invoke();

        //Mostrar créditos, volver al menú o algo
    }


}
