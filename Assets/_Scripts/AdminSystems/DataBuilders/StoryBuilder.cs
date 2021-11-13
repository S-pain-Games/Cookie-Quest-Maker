﻿using CQM.Components;
using System.Collections.Generic;
using UnityEngine;
using PieceType = CQM.Components.QuestPieceFunctionalComponent.PieceType;
using Tag = CQM.Components.QPTag.TagType;


namespace CQM.Databases
{
    public class StoryBuilder : MonoBehaviour
    {
        public List<StoryInfoComponent> Stories => m_StoriesList;
        public List<StoryRepercusionComponent> Repercusions => m_Repercusions;
        public List<StoryUIDataComponent> StoryUI => m_StoryUI;
        public List<StoryRepNewspaperComponent> RepercusionNewspaperArticles => m_RepercusionNewspaperArticles;

        [SerializeField]
        private List<StoryInfoComponent> m_StoriesList = new List<StoryInfoComponent>();
        [SerializeField]
        private List<StoryRepercusionComponent> m_Repercusions = new List<StoryRepercusionComponent>();
        [SerializeField]
        private List<StoryRepNewspaperComponent> m_RepercusionNewspaperArticles = new List<StoryRepNewspaperComponent>();
        [SerializeField]
        private List<StoryUIDataComponent> m_StoryUI = new List<StoryUIDataComponent>();

        private StoryInfoComponent m_Story;
        private StoryData m_StoryData;
        private BranchOption m_Branch;
        private StoryRepercusionComponent m_Repercusion;

        [SerializeField]
        private List<References> m_References = new List<References>();

        public void LoadDataFromCode()
        {
            m_StoriesList.Clear();
            m_Repercusions.Clear();
            m_StoryUI.Clear();
            m_References.Clear();
            m_RepercusionNewspaperArticles.Clear();

            /*
            StartCreatingStory("mayors_wolves", "Mayor's Wolves", new List<string>() { "Intro 1 wolves" });

            CreateRepercusion("wolves_alive", "Wolves Alive", 15);
            AddStoryRepercusionNewspaperArticle("Wolves Trouble", "In other news, there are still wolves in the town's center");

            StartStoryBranch();
            SetRepercusionToBranch("wolves_alive");
            AddBranchCompletion_NPCDialogue(new List<string>() { "NPC_Dialogue Harm 1" }, Tag.Harm, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() { "Evith mayors wolves Harm dialogue" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() { "Nu mayors wolves Harm dialogue" });

            StartStoryBranch();
            SetRepercusionToBranch("wolves_alive");
            AddBranchCompletion_NPCDialogue(new List<string>() { "NPC_Dialogue Convince 1" }, Tag.Convince, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() { "Evith mayors wolves Convince dialogue" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() { "Nu mayors wolves Convince dialogue" });

            StartStoryBranch();
            SetRepercusionToBranch("wolves_alive");
            AddBranchCompletion_NPCDialogue(new List<string>() { "NPC_Dialogue Help 1" }, Tag.Help, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() { "Evith mayors wolves Help dialogue" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() { "Nu mayors wolves Help dialogue" });

            AddStorySelectionUIData("Wolves Party");

            FinishCreatingStory();
            */

            //  STORY 1
            StartCreatingStory("mayor_problem", "El problema del alcalde", new List<string>() {
                "Últimamente una manada de lobos nos está causando muchos problemas.",
                "Estos acechan a nuestro ganado y a los comerciantes que quieren llegar al pueblo.",
                "Si esto sigue así, tendremos que contratar a un cazador profesional, pero nos va a costar una fortuna.",
                "¡No sé qué hacer!"});

            CreateRepercusion("wolves_gone", "Wolves Gone", 15);
            AddStoryRepercusionNewspaperArticle("Los lobos desaparecen de nuestros campos.",
                "Después de varios días de gran tensión, los lobos por fin han dejado de ser un problema para nuestros ganaderos y otros viajeros.");

            CreateRepercusion("wolves_stay", "Wolves Stay", -15);
            AddStoryRepercusionNewspaperArticle("El terror de los lobos continúa.",
               "Después de varios días de calvario, los lobos siguen campando a sus anchas por nuestros campos. El Alcalde declara que va a tomar cartas en el asunto.");

            //TARGET: WOLVES
            //HARM >= 1
            StartStoryBranch();
            SetRepercusionToBranch("wolves_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¿Te acuerdas del problema aquél de los lobos del que te hablé? ¡No te lo vas a creer!",
                "Se comenta que alguien o algo los ha espantado. ¡Así tal cual, de la noche a la mañana!",
                "Al final no ha sido necesario contratar a nadie para lidiar con ellos. ¡Así que estamos a salvo!"
            }, Tag.Harm, 1, "wolves");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Por qué todo el mundo piensa que los lobos son malvados?",
                "¡Tienen las mismas cualidades que cualquier perro pero con muchas ansias de sangre, son perfectos!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Una decisión interesante la que has tomado. Sin duda traerá buenos resultados." });

            //HARM >= 3
            StartStoryBranch();
            SetRepercusionToBranch("wolves_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¿Recuerdas el asunto de los lobos del otro día? Todavía me cuesta creerlo.",
                "¡La manada entera fue aniquilada de la noche a la mañana, no ha quedado ni uno!",
                "Me alegra que el problema se haya solucionado, pero me preocupa pensar que haya una criatura",
                "más peligrosa que esos lobos merodeando en los alrededores."
            }, Tag.Harm, 3, "wolves");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Por qué todo el mundo piensa que los lobos son malvados?",
                "¡Tienen las mismas cualidades que cualquier perro pero con muchas ansias de sangre, son perfectos!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Una decisión interesante la que has tomado. Sin duda traerá buenos resultados." });

            //CONVINCE >= 1
            StartStoryBranch();
            SetRepercusionToBranch("wolves_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Acerca del asunto de los lobos que te comenté el otro día. ¡Ha ocurrido algo impensable!",
                "¡Algunos de los lobos han sido domesticados, así de la noche a la mañana! ¡Nadie sabe cómo!",
                "Los lobos domesticados ayudan a proteger el ganado, y el resto de los lobos ya no son un problema.",
                "Tenía pensado contratar a alguien para lidiar con ellos, pero parece que ya no hará falta."
            }, Tag.Convince, 1, "wolves");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Por qué todo el mundo piensa que los lobos son malvados?",
                "¡Tienen las mismas cualidades que cualquier perro pero con muchas ansias de sangre, son perfectos!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Una decisión interesante la que has tomado. Sin duda traerá buenos resultados." });

            //HELP >= 1
            StartStoryBranch();
            SetRepercusionToBranch("wolves_stay");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡¿Cómo ha ocurrido esto?!, ¡Los lobos campan a sus anchas cazando a nuestro ganado!",
                "Han conseguido entrar en algunas granjas durante la noche, de alguna forma.",
                "Nadie me hacía caso, pero tenía razón en que estas criaturas son cada vez más inteligentes.",
                "Ya he contratado a un profesional para que se haga cargo, pero estaremos endeudados una buena temporada."
            }, Tag.Help, 1, "wolves");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Ooooohh! ¡Eso sí que no me lo esperaba! Parece que no me voy a aburrir contigo." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Si bien me preocupo por el bienestar de todas las criaturas del mundo. ",
                "He de decir que los lobos son de las bestias más cercanas a los engendros infernales." });

            //TARGET: MAYOR
            //HARM >= 1
            StartStoryBranch();
            SetRepercusionToBranch("wolves_stay");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Esto va de mal en peor! ¡Anoche alguien quería matarme! No he identificado al agresor por desgracia.",
                "Por si el problema de los lobos no fuera suficiente estrés, ahora tengo que lidiar con más presión.",
                "Los lobos siguen campando a sus anchas, voy a tener que contratar a un cazador cuanto antes.",
                "Y un par de guardaespaldas, por si fuera poco."
            }, Tag.Harm, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                 "¡Ooooohh! ¡Eso sí que no me lo esperaba! Parece que no me voy a aburrir contigo." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Si bien me preocupo por el bienestar de todas las criaturas del mundo. ",
                "He de decir que los lobos son de las bestias más cercanas a los engendros infernales." });

            //HELP >= 1
            StartStoryBranch();
            SetRepercusionToBranch("wolves_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Fuí esta mañana a contratar a un cazador para lidiar con el problema de los lobos.",
                "Para mi sorpresa, aceptó el contrato por mucho menos de lo que pide normalmente",
                "No sé qué lo habrá llevado a rebajar tanto su precio, pero se lo agradezco.",
                "Pronto el problema estará resuelto."
            }, Tag.Help, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                 "¿Por qué todo el mundo piensa que los lobos son malvados?",
                "¡Tienen las mismas cualidades que cualquier perro pero con muchas ansias de sangre, son perfectos!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Una decisión interesante la que has tomado. Sin duda traerá buenos resultados." });

            //CONVINCE >= 1
            StartStoryBranch();
            SetRepercusionToBranch("wolves_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Fuí esta mañana a contratar a un cazador para lidiar con el problema de los lobos.",
                "Para mi sorpresa, aceptó el contrato por mucho menos de lo que pide normalmente",
                "No sé qué lo habrá llevado a rebajar tanto su precio, pero se lo agradezco.",
                "Pronto el problema estará resuelto."
            }, Tag.Convince, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                 "¿Por qué todo el mundo piensa que los lobos son malvados?",
                "¡Tienen las mismas cualidades que cualquier perro pero con muchas ansias de sangre, son perfectos!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Una decisión interesante la que has tomado. Sin duda traerá buenos resultados." });

            //HELP >= 3
            StartStoryBranch();
            SetRepercusionToBranch("wolves_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡No te lo vas a creer!. Esta misma mañana ha venido un cazador a ofrecer sus servicios ¡Gratis!",
                "Normalmente suelen pedir bastante dinero para este tipo de problemas con bestias salvajes.",
                "Pero ha insistido mucho en trabajar de forma gratuita. ¡Estamos salvados!",
            }, Tag.Help, 3, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                 "¿Por qué todo el mundo piensa que los lobos son malvados?",
                "¡Tienen las mismas cualidades que cualquier perro pero con muchas ansias de sangre, son perfectos!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Una decisión interesante la que has tomado. Sin duda traerá buenos resultados." });

            //CONVINCE >= 3
            StartStoryBranch();
            SetRepercusionToBranch("wolves_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡No te lo vas a creer!. Esta misma mañana ha venido un cazador a ofrecer sus servicios ¡Gratis!",
                "Normalmente suelen pedir bastante dinero para este tipo de problemas con bestias salvajes.",
                "Pero ha insistido mucho en trabajar de forma gratuita. ¡Estamos salvados!",
            }, Tag.Convince, 3, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                 "¿Por qué todo el mundo piensa que los lobos son malvados?",
                "¡Tienen las mismas cualidades que cualquier perro pero con muchas ansias de sangre, son perfectos!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Una decisión interesante la que has tomado. Sin duda traerá buenos resultados." });

            AddStorySelectionUIData("El Problema del Alcalde");

            FinishCreatingStory();

            //  STORY 2
            StartCreatingStory("out_of_lactose", "Falta de lactosa", new List<string>() {
                "Necesito tener muchos productos lácteos para vender en la Feria de verano, que se celebrará pronto.",
                "El problema es que las vacas del pueblo no dan suficiente leche para producir tanto.",
                "La sequía de este año ha disminuido la calidad de los pastos en los alrededores, por lo que no pueden alimentarse lo suficiente.",
                "Tampoco puedo comprar leche a los demás ganaderos, ya que la venden demasiado cara estos días.",
                "Quiero hacer algo al respecto, pero no puedo hacer nada."});

            CreateRepercusion("cows_harmed", "Cows Harmed", -15);
            AddStoryRepercusionNewspaperArticle("La tragedia de las vacas.",
                "La misteriosa muerte de numerosas vacas de una ganadera local ha conmocionado a los habitantes del pueblo.");

            CreateRepercusion("cows_saved", "Cows Saved", 15);
            AddStoryRepercusionNewspaperArticle("El milagro de las vacas",
               "A pesar de la dura sequía que azota a nuestros campos, una ganadera local ha logrado mantener una buena alimentación para sus vacas. ¿Cuál será su secreto?");

            //TARGET: MERI
            //HARM >= 1
            StartStoryBranch();
            SetRepercusionToBranch("cows_harmed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Fui esta mañana a revisar mis vacas como todas las mañanas y todavía me cuesta creerlo.",
                "Alguien ha matado a cuatro de mis vacas durante la noche. Ninguno de mis vecinos sabe nada al respecto.",
                "Es una tragedia, pero viendo el lado positivo… las demás vacas podrán comer más que antes.",
                "No podré vender todos los lácteos que me gustaría, pero al menos podré vender algunas carnes."
            }, Tag.Harm, 1, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Ya veo por donde vas. Me gusta tu forma de pensar, ¡yo también hubiera hecho lo mismo!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Muchas decisiones de las que debemos de tomar día a día no son fáciles.",
                "Aunque pienso que a veces hay que buscar alternativas a la violencia."});

            //HARM >= 3
            StartStoryBranch();
            SetRepercusionToBranch("cows_harmed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Ha ocurrido una tragedia! ¡La mayoría de mis vacas han muerto durante la noche!",
                "Nadie del pueblo sabe nada al respecto, ¡¿Por qué me pasan estas cosas solamente a mí?!",
                "La Feria va a ser un fracaso, pero al menos podré vender bastante carne.",
                "Eso sí, no me haré responsable de mis actos si descubro al responsable."
            }, Tag.Harm, 3, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Ya veo por donde vas. Me gusta tu forma de pensar, ¡yo también hubiera hecho lo mismo!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Muchas decisiones de las que debemos de tomar día a día no son fáciles.",
                "Aunque pienso que a veces hay que buscar alternativas a la violencia."});

            //HELP >= 1
            StartStoryBranch();
            SetRepercusionToBranch("cows_saved");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "De alguna forma mis vacas se escaparon de mi granja en mitad de la noche. ¡Una locura!",
                "Seguí sus huellas a través de una arboleda y las encontré a todas pastando en un prado verde que no conocía.",
                "Con las vacas mejor alimentadas,  llegaré a tener suficiente leche para la Feria. ¡Qué alivio!"
            }, Tag.Help, 1, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "No tengo ninguna necesidad alimenticia, ¡pero cómo me comería unos buenos filetes!",
                "Aunque no tiene pinta de que vaya a ocurrir. Una pena."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Por muy dura que sea una situación, siempre hay que buscar una solución pacífica." });

            //CONVINCE >= 1
            StartStoryBranch();
            SetRepercusionToBranch("cows_saved");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "He hablado con otros ganaderos, y algunos han accedido a venderme algo de leche por un precio algo más razonable.",
                "Es raro que bajen los precios en estas fechas, pero no me puedo quejar.",
                "Aun así debería de tener suficiente leche para vender todo lo que me gustaría en la Feria. ¡Qué alegría!"
            }, Tag.Convince, 1, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "No tengo ninguna necesidad alimenticia, ¡pero cómo me comería unos buenos filetes!",
                "Aunque no tiene pinta de que vaya a ocurrir. Una pena."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Por muy dura que sea una situación, siempre hay que buscar una solución pacífica." });

            //CONVINCE >= 3
            StartStoryBranch();
            SetRepercusionToBranch("cows_saved");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Esta misma mañana, mientras revisaba a mis vacas, ¡ha ocurrido algo maravilloso!",
                "Muchos de mis vecinos ganaderos han decidido ayudarme con los preparativos para la Feria.",
                "También me han dado consejos para fabricar pienso y rutas de pastoreo en tiempos de sequía.",
                "Con esto tendré incluso más productos de los habituales para la Feria. ¡No sabes cuánto me alegro!"
            }, Tag.Convince, 3, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "No tengo ninguna necesidad alimenticia, ¡pero cómo me comería unos buenos filetes!",
                "Aunque no tiene pinta de que vaya a ocurrir. Una pena."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Por muy dura que sea una situación, siempre hay que buscar una solución pacífica." });

            AddStorySelectionUIData("Falta de Lactosa");

            FinishCreatingStory();

            //STORY 3
            StartCreatingStory("sacred_egg", "El Huevo Sagrado", new List<string>() {
                "Hace unos días estuve de visita por las subastas de la ciudad, buscando artefactos llamativos para decorar mi salón, ya sabes.",
                "El caso es que uno de los artículos más llamativos fue un huevo dorado, que aseguran que es auténtico.",
                "Por supuesto que lo compré y me lo traje a casa. Pero desde entonces me han surgido problemas.",
                "Hay un grupo de sectarios de la ciudad que cree firmemente que el huevo es un artefacto sagrado.",
                "Por supuesto que me negué a dárselo. Pero estoy preocupada de que entren a robar o algo peor.",
                "¡Ese huevo me ha costado una fortuna, y no pienso deshacerme de él!"});

            CreateRepercusion("golden_egg_destroyed", "Golden Egg Destroyed", -15);
            AddStoryRepercusionNewspaperArticle("Demasiado bueno para ser cierto.",
                "La prestigiosa coleccionista Canela N Rama se ve envuelta en una polémica tras la realidad de su nueva adquisición, un huevo dorado, que resultó ser un huevo normal y corriente.");

            CreateRepercusion("golden_egg_safe", "Golden Egg Safe", 15);
            AddStoryRepercusionNewspaperArticle("El nuevo artefacto de Canela N Rama",
               "Un nuevo artefacto forma parte de la colección de la prestigiosa coleccionista local Canela N Rama, se trata de un misterioso huevo dorado de un valor incalculable, asegura la coleccionista.");

            CreateRepercusion("golden_egg_gone", "Golden Egg Gone", -15);
            AddStoryRepercusionNewspaperArticle("El huevo maldito.",
               "El Huevo Dorado, el artefacto que iba a ser la joya de la colección de la prestigiosa Canela N Rama, ha sido retirado de la exposición. La coleccionista insinuó que el artefacto podría estar maldito");

            //TARGET: CANELA
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("golden_egg_destroyed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Esto es un ultraje! ¡Anoche alguien entró en mi casa y ha destrozado el huevo dorado!",
                "Debió de ser uno de los sectarios, aunque no me esperaba que quisieran hacerlo añicos siendo tan importante para ellos.",
                "Y sobre el huevo… resultó ser uno normal y corriente pintado de amarillo. Tan simple como eso.",
                "Me siento avergonzada por caer en una estafa tan clara, pero al menos sé que no he sido la única."
            }, Tag.Harm, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡La violencia es siempre la solución! Y si no funciona es porque no se aplica lo suficiente." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "A veces es necesario despojarnos de nuestras posesiones más preciadas para resolver un conflicto." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("golden_egg_safe");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¿El huevo dorado? Se encuentra a salvo en mi salón, por ahora.",
                "Por lo visto anoche uno de los sectarios trató de irrumpir en mi hogar, aunque logró escapar.",
                "Se hubiera salido con la suya si no fuera por el ruido que hizo al entrar por la ventana.",
                "He tenido suerte de que hubiera cazuelas apoyadas sobre la ventana, aunque no recuerdo haberlas dejado ahí.",
                "No esperaba que realmente fueran a entrar en mi casa, tendré que contratar algunos guardias."
            }, Tag.Help, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Me pregunto a qué deidad venerarán aquellos cultistas. ¡Como sean seguidores de Nu, se las van a tener que ver conmigo!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Es menester ayudar a todo ser en necesidad de ayuda. Salvo si se trata de un cultista o de Evith, ellos son la excepción." });

            //HELP >=3
            StartStoryBranch();
            SetRepercusionToBranch("golden_egg_safe");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Te hablé el otro día sobre el huevo dorado, ¿no? Aún lo tengo expuesto en mi salón.",
                "Sé que cuesta mucho creerlo, ¡pero ayer sorprendí a un sectario que se había colado en mi finca!",
                "Estaba huyendo despavorido como si hubiera visto algo aterrador. Se disculpó conmigo y me prometió que nunca jamás volverían a molestarme.",
                "Me alegra que este asunto se haya terminado, pero me pregunto qué será lo que le aterró tanto."
            }, Tag.Help, 3, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Me pregunto a qué deidad venerarán aquellos cultistas. ¡Como sean seguidores de Nu, se las van a tener que ver conmigo!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Es menester ayudar a todo ser en necesidad de ayuda. Salvo si se trata de un cultista o de Evith, ellos son la excepción." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("golden_egg_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Acerca del huevo dorado… Estoy pensando en deshacerme de él.",
                "¿Por qué? Bueno, esta noche he escuchado ruidos extraños procedentes del salón.",
                "He revisado de arriba a abajo toda la casa y no soy capaz de entender qué los provoca.",
                "Empiezo a pensar si el huevo está maldecido o si de verdad es una reliquia de esa secta.",
                "Creo que mi mejor opción será vender el huevo a algún interesado y olvidarme del asunto."
            }, Tag.Convince, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Quizás yo también debería de empezar a coleccionar cachivaches." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Debo reconocer que tus decisiones son bastante interesantes. Me intriga saber qué haras a continuación." });

            //CONVINCE >=3
            StartStoryBranch();
            SetRepercusionToBranch("golden_egg_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡¿El huevo dorado?!, Me he deshecho de él esta misma mañana. ¡Ya no quiero saber nada de él!",
                "¡Ese huevo está maldito! No he parado de escuchar ruidos extraños en toda la noche.",
                "Y por si fuera poco, toda mi colección estaba patas arriba, ¡como si fuera obra de un espíritu!",
                "Ese huevo me ha costado un dineral, ¡pero no pienso poner en peligro mi colección por una baratija como esa!"
            }, Tag.Convince, 3, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Quizás yo también debería de empezar a coleccionar cachivaches." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Debo reconocer que tus decisiones son bastante interesantes. Me intriga saber qué haras a continuación." });

            AddStorySelectionUIData("El Huevo Sagrado");

            FinishCreatingStory();
        }

        public void SyncReferences()
        {
            for (int i = 0; i < m_References.Count; i++)
            {
                var r = m_References[i];

                for (int j = 0; j < m_StoryUI.Count; j++)
                {
                    if (r.m_ParentStoryID == m_StoryUI[j].m_ParentStoryID)
                        m_StoryUI[j].m_Sprite = r.m_StorySelectionUiSprite;
                }
            }
        }

        public void StartCreatingStory(string idName, string title, List<string> introductionDialogue)
        {
            m_StoryData = new StoryData();
            m_StoryData.m_ID = new ID(idName);

            m_StoryData.m_Title = title;
            m_StoryData.m_IntroductionDialogue = introductionDialogue;
        }

        public void StartStoryBranch()
        {
            m_Branch = new BranchOption();
        }

        public void CreateRepercusion(string idName, string repName, int happinessValue)
        {
            var rep = new StoryRepercusionComponent();
            rep.m_ID = new ID(idName);
            rep.m_Name = repName;
            rep.m_ParentStoryID = m_StoryData.m_ID;
            rep.m_Active = false;
            rep.m_Value = happinessValue;
            m_Repercusions.Add(rep);
            m_Repercusion = rep;
        }

        public void SetRepercusionToBranch(string idName)
        {
            StoryRepercusionComponent rep = null;
            for (int i = 0; i < m_Repercusions.Count; i++)
            {
                if (m_Repercusions[i].m_ID == new ID(idName))
                {
                    rep = m_Repercusions[i];
                }
            }

            if (rep == null)
            {
                Debug.LogError($"Repercusion with idName [{idName}] not found when building stories");
            }

            m_Branch.m_Repercusion = rep;
        }

        public void AddStoryRepercusionNewspaperArticle(string title, string body)
        {
            var newsArticle = new StoryRepNewspaperComponent();
            newsArticle.m_RepID = m_Repercusion.m_ID;
            newsArticle.m_Title = title;
            newsArticle.m_Body = body;
            m_RepercusionNewspaperArticles.Add(newsArticle);
        }

        public void AddBranchCompletion_NPCDialogue(List<string> npcResultDialogue, Tag tag, int tagValue, string target)
        {
            m_Branch.m_ResultNPCDialogue = npcResultDialogue;

            BranchCondition bCon = new BranchCondition
            {
                m_Tag = tag,
                m_Value = tagValue,
                m_Target = new ID(target)
            };
            m_Branch.m_Condition = bCon;
            m_StoryData.m_BranchOptions.Add(m_Branch);
        }

        public void AddBranchCompletion_EvithDeityDialogue(List<string> dialogue)
        {
            var evithDialogue = new BranchOption.DeitiesStoryDialogue();
            evithDialogue.m_DeityID = 1;
            evithDialogue.m_Dialogue = dialogue;
            m_Branch.m_DeitiesResultDialogue.Add(evithDialogue);
        }

        public void AddBranchCompletion_NuDeityDialogue(List<string> dialogue)
        {
            var evithDialogue = new BranchOption.DeitiesStoryDialogue();
            evithDialogue.m_DeityID = 0;
            evithDialogue.m_Dialogue = dialogue;
            m_Branch.m_DeitiesResultDialogue.Add(evithDialogue);
        }

        public void AddStorySelectionUIData(string title)
        {
            var s = new StoryUIDataComponent();
            s.m_Title = title;
            s.m_ParentStoryID = m_StoryData.m_ID;
            m_StoryUI.Add(s);

            var re = new References();
            re.m_ParentStoryID = m_StoryData.m_ID;
            re.m_StoryName = m_StoryData.m_Title;
            m_References.Add(re);
        }


        public void FinishCreatingStory()
        {
            m_StoryData.Build();

            m_Story = new StoryInfoComponent();
            m_Story.m_StoryData = m_StoryData;

            m_StoriesList.Add(m_Story);
        }

        [System.Serializable]
        private class References
        {
            [HideInInspector]
            public ID m_ParentStoryID;
            public string m_StoryName;
            public Sprite m_StorySelectionUiSprite;
        }
    }
}