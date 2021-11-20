using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEndingDialogSequence : MonoBehaviour
{
    [SerializeField] private AgentMouseListener character;
    [SerializeField] private GameObject evithPrefab;
    [SerializeField] private GameObject nuPrefab;

    [SerializeField] private GameObject thanksForPlayingPrefab;

    private EventVoid _toggleGameplayUiCmd;
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    private void Awake()
    {
        GameEventSystem evtSys = Admin.Global.EventSystem;

        _toggleGameplayUiCmd = evtSys.GetCommandByName<EventVoid>("ui_sys", "toggle_gameplay");
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
    }

    [MethodButton]
    public void ExecuteSequence()
    {
        character.SetInputActivated(false);
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
        int globalHapiness = Admin.Global.Components.m_TownComponent.m_GlobalHappiness;
        Debug.Log("Ending hapiness: "+globalHapiness);

        //Dependiendo del valor total de la felicidad de los NPCs, proseguir con una de las tres cadenas
        if (globalHapiness > 150)
            StartGoodEndingSequence();
        else if(globalHapiness < 50)
            StartBadEndingSequence();
        else
            StartNeutralEndingSequence();
    }

    #region Good Ending Dialog Sequence

    [MethodButton]
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
            "Y seguirá velando por el bien del pueblo y su gente gracias a las Galletas mágicas.",
            "FIN"},
          new ID("narrator"),
             () => { FinishEndingSequence(); }));
    }
    #endregion

    #region Bad Ending Dialog Sequence

    [MethodButton]
    private void StartBadEndingSequence()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Teníamos ciertas expectativas sobre qué harías con nuestros poderes.",
            "Sobre todo partiendo del hecho de que deseabas poder hacer algo por el pueblo." },
        new ID("nu"),
           () => { this.BadEndingSequenceOne(); }));
    }

    private void BadEndingSequenceOne()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Pues nos llevamos una sorpresa bastante grata! ¡Bueno, al menos en mi caso!",
            "Supongo que ya te imaginas a qué me refiero." },
        new ID("evith"),
           () => { this.BadEndingSequenceTwo(); }));
    }

    private void BadEndingSequenceTwo()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Visto lo visto, has utilizado a las Galletas mágicas para empeorar más aún la situación del pueblo."},
       new ID("nu"),
          () => { BadEndingSequenceThree(); }));
    }

    private void BadEndingSequenceThree()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Sabía en el fondo que serías de los míos! ¡Disfrutas del caos y te regocijas en las desgracias ajenas!"},
       new ID("evith"),
          () => { BadEndingSequenceFour(); }));
    }
    private void BadEndingSequenceFour()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "El pueblo se encuentra en un estado aún más lamentable que cuando llegamos.",
            "No pensábamos que sería posible llegar hasta este punto." },
      new ID("nu"),
         () => { BadEndingSequenceFive(); }));
    }
    private void BadEndingSequenceFive()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "No me cabe duda de que has actuado así a propósito.",
            "Te ha podido la idea de poder hacer lo que quisieras sin consecuencia alguna. ¿O será que ya eras malvado de antemano?"},
      new ID("evith"),
         () => { BadEndingSequenceSix(); }));
    }
    private void BadEndingSequenceSix()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Tanto si se trata de mezquindad o de pura negligencia. No podemos permitirte continuar con tus viles actos."},
      new ID("nu"),
         () => { BadEndingSequenceSeven(); }));
    }
    private void BadEndingSequenceSeven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Qué pena! ¡No sabes cuánto me gustaría seguir observando tus actos!",
            "¡Hay muy pocas personas capaces de llegar tan lejos!" },
     new ID("evith"),
        () => { BadEndingSequenceEight(); }));
    }
    private void BadEndingSequenceEight()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Debemos seguir con nuestro reglamento. A partir de este momento, ya no volverás a poder hornear Galletas mágicas."},
     new ID("nu"),
        () => { BadEndingSequenceNine(); }));
    }

    private void BadEndingSequenceNine()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "No nos lo tengas en cuenta, así son las cosas. ¡Si fuera por mí, le daría poderes a todo el mundo para observar qué sucedería!"},
     new ID("evith"),
        () => { BadEndingSequenceTen(); }));
    }

    private void BadEndingSequenceTen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Nuestro trabajo aquí ha terminado. Nos marchamos. Esperemos poder obtener resultados más prometedores con el próximo candidato."},
     new ID("nu"),
        () => { BadEndingSequenceEleven(); }));
    }

    private void BadEndingSequenceEleven()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Dudo bastante que haya alguien más interesante que este joven pastelero. ¡Pero nunca se sabe!" },
     new ID("evith"),
        () => { BadEndingSequenceTwelve(); }));
    }

    private void BadEndingSequenceTwelve()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "No lo sabremos hasta que nos presentemos ante él",
            "Bueno, si queda algo de justicia en este mundo, espero que te haga pagar por tus actos algún día. Nos marchamos." },
     new ID("nu"),
        () => { BadEndingSequenceThirteen(); }));
    }

    private void BadEndingSequenceThirteen()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "¡Pase lo que pase yo seguiré siendo tu fan número uno! ¡Espero poder volver a visitarte algún día! ¡Hasta otra!" },
     new ID("evith"),
        () => { BadEndingSequenceEnd(); }));
    }

    private void BadEndingSequenceEnd()
    {
        HideNuAndEvith();

        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(new List<string>() {
            "Los vecinos del pueblo maldicen su suerte sin saber que el responsable de empeorar la situación era el dueño de la pastelería a la que acuden diariamente.",
            "Aun así, los vecinos siguen viviendo con la esperanza de que la situación mejorará algún día.",
            "El joven pastelero seguirá observando las desgracias ajenas sin mover un dedo por mejorar la situación.",
            "FIN"},
          new ID("narrator"),
             () => { FinishEndingSequence(); }));
    }
    #endregion

    #region Neutral Ending Dialog Sequence

    [MethodButton]
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
        evithPrefab.SetActive(false);
        nuPrefab.SetActive(false);
    }

    private void FinishEndingSequence()
    {
        character.SetInputActivated(true);
        _toggleGameplayUiCmd.Invoke();

        //Mostrar créditos, volver al menú o algo
        thanksForPlayingPrefab.SetActive(true);
    }


}
