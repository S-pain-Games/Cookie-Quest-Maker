using CQM.Components;
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

            // =============================================================================================
            //  STORY 1
            // =============================================================================================

            StartCreatingStory("mayor_problem", "El problema del alcalde", "mayor",
                "Unos lobos están amenazando a los habitantes del pueblo. El alcalde quiere hacer algo al respecto pero es muy tacaño.", new List<string>() {
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
                "Me alegra que el problema se haya solucionado, pero me preocupa pensar que haya una criatura más peligrosa que esos lobos merodeando en los alrededores."
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

            // =============================================================================================
            //  STORY 2
            // =============================================================================================

            StartCreatingStory("out_of_lactose", "Falta de lactosa", "meri",
                "Las vacas de Meri dan muy poca leche últimamente. Teme que no vaya a tener suficientes productos lácteos para la Feria.", new List<string>() {
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

            // =============================================================================================
            //STORY 3
            // =============================================================================================
            
            StartCreatingStory("sacred_egg", "El Huevo Sagrado", "canela",
                "Canela ha adquirido un huevo dorado para su exposición. Pero una secta amenaza con arrebatárselo.", new List<string>() {
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

            // =============================================================================================
            //STORY 4
            // =============================================================================================

            StartCreatingStory("explosive_chocolate", "Chocolate explosivo", "miss_chocolate",
                "Miss Chocolate quiere experimentar con pólvora para hacer chocolate experimental. Los mercaderes se niegan a vender más polvora por ser peligroso.", new List<string>() {
                "Estoy muy cerca de conseguir la receta de chocolate definitiva. ¡Va a ser un pelotazo!",
                "He teorizado una receta que puede ser toda una revolución.",
                "Mezclar chocolate con pólvora, ¡todo para lograr una mezcla de sabor explosiva!",
                "¿Que si va a ser peligroso? ¡Qué va! La receta necesitará una pizquita de nada.",
                "Tan solo necesito que los mercaderes accedan a venderme mucha más pólvora.",
                "Insisten en que puede ser muy peligroso, que podría salir el pueblo por los aires… ¡Bah!",
                "A no ser que encuentre otro ingrediente alternativo, tendré que seguir trasteando con lo que tengo."
               });

            CreateRepercusion("ms_chocolate_sabotaged", "MS Chocolate Sabotaged", -15);
            AddStoryRepercusionNewspaperArticle("Accidente explosivo.",
                "La explosión que despertó a todos los vecinos del pueblo fué causa de un accidente por parte de Miss Chocolate la Bomb, una chocolatera local. La chocolatera se encuentra ilesa y se ha disculpado públicamente.");

            CreateRepercusion("ms_chocolate_gunpowder", "MS Chocolate Gunpowder", 15);
            AddStoryRepercusionNewspaperArticle("Experimentos explosivos.",
               "Los experimentos de Miss Chocolate la Bomb tienen preocupados a toda la comunidad de vecinos tras adquirir grandes cantidades de pólvora. ¿Qué es lo que pretende la chocolatera?");

            CreateRepercusion("ms_chocolate_pepper", "MS Chocolate Pepper", 15);
            AddStoryRepercusionNewspaperArticle("El nuevo ingrediente secreto que revolucionará la chocolatería.",
               "La comunidad de chocolateros de la región se encuentra intrigada ante el nuevo ingrediente que utilizará Miss Chocolate la Bomb en sus productos, la pimienta.");

            //TARGET: MS CHOCOLATE
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_sabotaged");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡¿Qué ha podido salir mal?! ¡Lo tenía todo controlado!",
                "¿Qué si ha ocurrido algo? Nada, tan solo un pequeño accidente.",
                "Algunos de los viales con la mezcla que tenía preparados se han caído de su estante.",
                "El problema es que han caído ligeramente cerca de la chimenea... estando encendida.",
                "Nadie ha resultado herido, tan solo han saltado por los aires algunos de mis aparatos.",
                "Supongo que yo soy la culpable, tenía que haber tenido más cuidado. ¡Pero no me rendiré!"
            }, Tag.Harm, 1, "ms chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Esta noche presiento que va a ocurrir algo genial! Buena decisión, por cierto." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Hacer ver a los demás sus errores puede ser incluso más difícil que partir una montaña."});

            //HARM >=3
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_sabotaged");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "De todas las cosas que podrían haber salido mal… ¡¿Por qué ha ocurrido lo peor?!",
                "Pensaba que había tomado todas las precauciones necesarias para que algo así no ocurra.",
                "Todo indica a que la pólvora que tenía almacenada se prendió, lo que no sé es cómo.",
                "Yo estaba fuera tomando el aire fresco en el momento de la explosión, por suerte.",
                "Toda mi investigación se ha echado a perder. Tan solo han quedado intactos algunos inventos.",
                "Tendré que comenzar desde cero, ¡Pero no me daré por vencida!"
            }, Tag.Harm, 3, "ms chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Esta noche presiento que va a ocurrir algo genial! Buena decisión, por cierto." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Hacer ver a los demás sus errores puede ser incluso más difícil que partir una montaña."});

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_pepper");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "He dejado de utilizar pólvora para mi receta definitiva. ¡Porque he encontrado algo mucho mejor!",
                "Me di cuenta en el momento en que me cayó un bote de pimienta en la cabeza. ¡Qué cosas!",
                "¡El verdadero sabor explosivo que buscaba en realidad proviene del picante!",
                "Por eso utilizo ahora pimienta en lugar de pólvora, ¡En mayores cantidades!"
            }, Tag.Help, 1, "ms chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Sea lo que sea que salga de ese experimento, ¡me gustaría probarlo!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "La menor de nuestras acciones es suficiente para alterar el destino del mundo."});

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_gunpowder");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Por fin podré continuar con mis experimentos! Pensaba que ya sería imposible.",
                "Algunos mercaderes finalmente han accedido a ofrecerme toda la pólvora que necesito.",
                "Por supuesto siguen insistiendo en que es muy peligroso, por eso he accedido a experimentar con pequeñas dosis muy controladas.",
                "Mi investigación no progresará tanto como me gustaría, pero, ¡poco a poco lograré mi objetivo!"
            }, Tag.Convince, 1, "ms chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡El fin justifica los medios! Y si medio pueblo sale por los aires, ¡todavía mejor!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "El deseo de crear algo revolucionario es una meta encomiable.",
                "Aunque deja de serlo tanto si se ponen en peligro vidas en el proceso."});

            //CONVINCE >=3
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_gunpowder");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Esta misma mañana he vuelto a buscar más pólvora para la receta, pero he encontrado algo mejor.",
                "Uno de los mercaderes me ha ofrecido muestras de especias provenientes de un país lejano.",
                "Tienen propiedades casi idénticas que la pólvora, pero sin los riesgos de salir por los aires.",
                "Ese tipo de ingredientes son tan raros por aquí que es impensable comprarlos ¡Pero la suerte me sonríe!",
                "¡Estoy cada vez más cerca de lograr la fórmula del chocolate definitivo! ¡Tengo que seguir trabajando!"
            }, Tag.Convince, 3, "ms chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡El fin justifica los medios! Y si medio pueblo sale por los aires, ¡todavía mejor!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "El deseo de crear algo revolucionario es una meta encomiable.",
                "Aunque deja de serlo tanto si se ponen en peligro vidas en el proceso."});

            AddStorySelectionUIData("Chocolate explosivo");

            FinishCreatingStory();

            // =============================================================================================
            //STORY 5
            // =============================================================================================

            StartCreatingStory("not_so_dirty_rats", "Ratas no tan sucias", "mantecas",
                "Una colonia de ratas inteligentes ha invadido el granero del Mantecas y se dedican a robar sus hortalizas.", new List<string>() {
                "¡Estoy harto de que las ratas me destrocen el granero! ¡Todos los años igual!",
                "No sé qué es lo que les echa de comer el Johnny, pero te juro que son cada vez más inteligentes.",
                "¡Esta vez han ido demasiado lejos, si hasta han construido un mercadillo en mi propiedad!",
                "Es como lo oyes, ¡se rifan mis vegetales en su propio mercadillo! ¡Se están riendo de mí!"
               });

            CreateRepercusion("smart_rats_stay", "Smart rats stay", -15);
            AddStoryRepercusionNewspaperArticle("Los nuevos vecinos roedores.",
                "Una colonia de ratas aparentemente inteligentes se ha establecido en el granero de un granjero local. El dueño de la granja expresa su disgusto con la situación.");

            CreateRepercusion("smart_rats_gone", "Smart rats gone", 15);
            AddStoryRepercusionNewspaperArticle("La caída de la sociedad de las ratas.",
               "La colonia de ratas aparentemente inteligentes que atormentaba los cultivos de los granjeros locales parece haberse disuelto, para el contento de todos.");

            CreateRepercusion("smart_rats_tribute", "Smart rats tribute", 15);
            AddStoryRepercusionNewspaperArticle("¿Relación simbiótica entre humanos y ratas?",
               "La ancestral relación de odio entre humanos y ratas podría llegar a su fin. Una colonia de ratas inteligentes coopera con un granjero local a cambio de cobijo en su granero. El dueño de la granja niega tales afirmaciones.");

            //TARGET: MANTECAS
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("smart_rats_stay");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Malditas ratas inmundas! ¡No solo me roban mis vegetales si no que encima me atacan!",
                "¡Han colocado trampas en mi casa, como si fuera yo el intruso!",
                "¡¿Quién se creen que son?! ¡Esto es una declaración de guerra! ¡No saben contra quien se enfrentan!"
            }, Tag.Harm, 1, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Tengo ganas de ver qué tan lejos llegarán esas ratas. Puede que algún día exista una rata experta en repostería." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ocurra lo que ocurra, la naturaleza seguirá su curso."});

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("smart_rats_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Esto es inconcebible! ¡Otra colonia de ratas ha atacado a las ratas de mi granero!",
                "Ahora por lo menos las ratas que quedan son normales¡ ¡Pero siguen siendo ratas!",
                "Si esto es obra del Johnny, ¡te juro que va a preparar sus pociones desde el fondo del mar!"
            }, Tag.Help, 1, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Es universal que todo el mundo odie a las ratas. ¡Pero a mí me encantan!",
                "Son como hamsters pero con cola. Y pueden ser más inteligentes que muchos humanos."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ninguna nación ni imperio dura para siempre. Ese hecho se aplica a todos los seres vivos."});

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("smart_rats_tribute");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡¿Pero qué locura es esta?! ¡Las ratas están recolectando dinero del suelo!",
                "¡Sí, ahora las ratas recogen el dinero que la gente pierde por ahí y lo dejan delante de mi puerta!",
                "¡¿Piensan que por que me den tributo las voy a perdonar?! ¡Habrase visto!"
            }, Tag.Convince, 1, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Eres toda una caja de sorpresas! Ya empezaba a pensar que serías otro humano aburrido." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Estoy seguro de que esa decisión llevará a consecuencias que nadie esperaría."});

            //TARGET: RATS
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("smart_rats_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Aunque esas dichosas ratas sean inteligentes, ¡no pueden evitar sus propias plagas!",
                "Parece ser que parte de mi cosecha robada estaba en mal estado, muchas de ellas han enfermado.",
                "¡Seguro que las han envenenado con sus sucias garras! ¡Se lo tienen merecido!"
            }, Tag.Harm, 1, "rats");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Es universal que todo el mundo odie a las ratas. ¡Pero a mí me encantan!",
                "Son como hamsters pero con cola. Y pueden ser más inteligentes que muchos humanos."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ninguna nación ni imperio dura para siempre. Ese hecho se aplica a todos los seres vivos."});

            //HARM >=3
            StartStoryBranch();
            SetRepercusionToBranch("smart_rats_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Ahora las ratas se pelean entre ellas por mi comida, recurren al robo y al vandalismo.",
                "Su sociedad ha caído y vuelven a ser como antes ¡Que regresen al agujero del que salieron!",
                "Aunque la rata se vista de seda, ¡Rata se queda!"
            }, Tag.Harm, 3, "rats");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Es universal que todo el mundo odie a las ratas. ¡Pero a mí me encantan!",
                "Son como hamsters pero con cola. Y pueden ser más inteligentes que muchos humanos."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ninguna nación ni imperio dura para siempre. Ese hecho se aplica a todos los seres vivos."});

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("smart_rats_stay");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Estúpidas ratas! No contentas con robarme mis vegetales, ¡ahora me roban dinero para comprar en su mercadillo!",
                "¡Pagarán por esta ofensa! ¡No saben contra quién se están enfrentando!"
            }, Tag.Help, 1, "rats");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Tengo ganas de ver qué tan lejos llegarán esas ratas. Puede que algún día exista una rata experta en repostería." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ocurra lo que ocurra, la naturaleza seguirá su curso."});

            //HELP >=3
            StartStoryBranch();
            SetRepercusionToBranch("smart_rats_stay");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Pero esto qué es! Ya era un insulto que las ratas tuvieran su propio mercadillo a mi costa.",
                "¡Ahora resulta que han construido una fortaleza! ¡En mi propio granero! ¡Con sus torres vigía y todo!",
                "¡¿Me quieren tomar el pelo?! ¡Pienso sacarlas de ahí aunque tenga que prender fuego a toda mi granja!"
            }, Tag.Help, 3, "rats");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Tengo ganas de ver qué tan lejos llegarán esas ratas. Puede que algún día exista una rata experta en repostería." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ocurra lo que ocurra, la naturaleza seguirá su curso."});

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("smart_rats_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Esas estúpidas ratas han abandonado mi granero. ¡Ya no se atreven a enfrentarme!",
                "Parece ser que ahora se han instalado en la despensa del Johnny. ¡Se lo tiene merecido!",
                "Eso sí, ¡Como las ratas vuelvan a dar por saco, pienso hacer que se trague todas sus pócimas!"
            }, Tag.Convince, 1, "rats");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Es universal que todo el mundo odie a las ratas. ¡Pero a mí me encantan!",
                "Son como hamsters pero con cola. Y pueden ser más inteligentes que muchos humanos."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ninguna nación ni imperio dura para siempre. Ese hecho se aplica a todos los seres vivos."});

            AddStorySelectionUIData("Ratas no tan sucias");

            FinishCreatingStory();

            // =============================================================================================
            //STORY 6
            // =============================================================================================

            StartCreatingStory("stingy_taxes", "Impuestos para el tacaño", "mayor",
                "El alcalde está muy descontento con la escasa recaudación de impuestos. Por otra parte la gente está muy descontenta con sus reformas de poca utilidad.", new List<string>() {
                "Los impuestos recaudados este año han caído en mínimos históricos ¡Es una ruina!",
                "Me niego a pensar que la gente no pueda contribuir ¡La gente está guardándose el dinero, seguro!",
                "Tengo la sensación de que la gente todavía no confía en mí, ¡Después de todo lo que he hecho por ellos!",
                "¡Se han olvidado del gran puerto en la Charca local, o los tres arbustos nuevos de la Plaza!",
                "¡Si hasta hice cambiar un estante de la biblioteca y mandé construir una estatua de mi persona!",
                "Son unos desagradecidos."
               });

            CreateRepercusion("mayor_assaulted", "Mayor assaulted", -15);
            AddStoryRepercusionNewspaperArticle("El misterioso agresor nocturno.",
                "Nuestro alcalde ha sido asaltado mientras paseaba por un misterioso atacante. Se cree que es un ciudadano descontento con la reciente recaudación de impuestos.");

            CreateRepercusion("mayor_waste", "Mayor waste", -15);
            AddStoryRepercusionNewspaperArticle("Las nuevas reformas.",
               "Gracias a la exitosa recaudación de impuestos, el alcalde continúa con su proyecto de embellecer el pueblo a pesar de las voces críticas de muchos vecinos.");

            CreateRepercusion("mayor_robbed", "Mayor robbed", 15);
            AddStoryRepercusionNewspaperArticle("El ladrón misterioso.",
               "Un misterioso desconocido ha robado los fondos recaudados recientemente y los ha devuelto a los vecinos del pueblo. Algunos lo aclaman como un héroe sin corcel.");

            //TARGET: MAYOR
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("mayor_assaulted");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Qué disparate! ¡He sido agredido en plena noche mientras paseaba!",
                "Al responsable de esta afrenta se le va a caer el pelo, en cuanto sepa quien ha sido.",
                "¡Soy el que más se preocupa por el bienestar de este pueblo! ¡¿Cómo se atreven a hacerme esto?!",
                "Siempre sigo mi buen juicio, aunque a veces me pregunto si estaré haciendo algo mal."
            }, Tag.Harm, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡¿Cómo se atreve ese alcalde a construir una estatua de su persona?! ¡Tendrá su merecido!",
                "¡Toda estatua debería de representar la grandeza de la gran Evith!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Es difícil asimilar la idea de que el llamado sentido común no es tán común como suponemos."});

            //HARM >=3
            StartStoryBranch();
            SetRepercusionToBranch("mayor_assaulted");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Esto es un escándalo! ¡¿Cómo es posible que yo, el alcalde, haya sido agredido múltiples veces en una misma noche?!",
                "¡La gente ya no me respeta! ¡¿Qué es lo que estoy haciendo mal?!",
                "¡Con todo lo que he hecho por el pueblo! ¿Será que mis reformas no gustan a la gente?"
            }, Tag.Harm, 3, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡¿Cómo se atreve ese alcalde a construir una estatua de su persona?! ¡Tendrá su merecido!",
                "¡Toda estatua debería de representar la grandeza de la gran Evith!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Es difícil asimilar la idea de que el llamado sentido común no es tán común como suponemos."});

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("mayor_waste");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Esta mañana cuando he entrado en mi oficina he visto un saco con dinero sobre mi escritorio.",
                "Doy por hecho que es parte del dinero faltante, aunque preferiría que me hubiera llegado por medios oficiales.",
                "¡El caso es que ahora tendremos dinero suficiente para arreglar las aceras de mi casa y pintar mi fachada!",
                "El resto del dinero quedará en reserva en caso de emergencias, ¡nunca se sabe lo que puede ocurrir en estos tiempos!"
            }, Tag.Help, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Incluso el santurrón presuntuoso de Nu y yo podemos estar de acuerdo en que a ese alcalde le faltan un par de veranos."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "¿Realmente el alcalde sabrá lo que está haciendo? Sus intenciones parecen buenas pero..."});

            //HELP >=3
            StartStoryBranch();
            SetRepercusionToBranch("mayor_waste");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Esta misma mañana me han llegado varios sacos con dinero sobre mi escritorio.",
                "Debe de ser todo el dinero que faltaba de la recaudación, aunque me extraña que no se me haya notificado.",
                "El caso es que se ha recaudado más dinero de lo que se recauda normalmente. ¡Es maravilloso!",
                "¡Ahora podré embadurnar de plata mi estatua y ampliar el puerto de la charca!",
                "¡Ya solo falta que puedan llegar barcos mercantes! ¡La gente me adorará!"
            }, Tag.Help, 3, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Incluso el santurrón presuntuoso de Nu y yo podemos estar de acuerdo en que a ese alcalde le faltan un par de veranos."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "¿Realmente el alcalde sabrá lo que está haciendo? Sus intenciones parecen buenas pero..."});

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("mayor_robbed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Acerca de la recaudación de este año… ¡Ha desaparecido parte del dinero! ¡¿Cómo ha podido ser?!",
                "No se ha encontrado rastro alguno del ladrón. Estoy buscando al culpable por todos los medios posibles, pero no hay rastro de él.",
                "Pero más importante, ¡ya no hay dinero para ampliar el puerto de la charca!",
                "Menos mal que para estos casos tengo bastante dinero almacenado. ¡Siempre es bueno ser previsor!"
            }, Tag.Convince, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Qué harías tú si fueras el alcalde? ¡Espero que muchas tropelías!",
                 "Sinceramente no te veo siendo un santurrón como Nu." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Hay veces en las que uno debe tomar las riendas de la situación para resolver un problema ajeno.",
                "Especialmente si aquel no sabe lo que hace." });

            //CONVINCE >=3
            StartStoryBranch();
            SetRepercusionToBranch("mayor_robbed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Ha ocurrido algo terrible! ¡El dinero de la caja fuerte ha desaparecido! ¡Ya no queda nada!",
                "Debe de tratarse de alguien muy habilidoso para cometer tal hazaña. ¡Todo un profesional!",
                "Parece ser que el dinero se ha repartido por todos los habitantes del pueblo.",
                "Todo el mundo está muy feliz al respecto, como es de esperar. ¡Pero no puedo dejar que ocurran cosas así!",
                "No me queda más remedio que gastar mis ahorros para cubrir el presupuesto del resto del año."
            }, Tag.Convince, 3, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Qué harías tú si fueras el alcalde? ¡Espero que muchas tropelías!",
                 "Sinceramente no te veo siendo un santurrón como Nu." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Hay veces en las que uno debe tomar las riendas de la situación para resolver un problema ajeno.",
                "Especialmente si aquel no sabe lo que hace." });

            AddStorySelectionUIData("Impuestos para el tacaño");

            FinishCreatingStory();

            // =============================================================================================
            //STORY 7
            // =============================================================================================

            StartCreatingStory("crazy_cows", "Las vacas locas", "meri",
                "Las vacas de Meri se han transformado parcialmente en cabras tras ingerir un pienso especial. Quiere que recuperen su estado original.", new List<string>() {
                "¡Mis vacas se han vuelto locas! ¡Son como cabras!",
                "Lo digo literalmente, mis vacas ahora tienen complejo de cabras.",
                "Se suponía que el pienso especial de Johnny las iba a ayudar a atravesar montañas con facilidad.",
                "¡Ahora se comen cualquier cosa, son muy testarudas y hasta se suben a cualquier sitio!",
                "No puedo trabajar con ellas así, ¡No sé que voy a hacer!"
               });

            CreateRepercusion("cows_transformed", "Cows transformed", -15);
            AddStoryRepercusionNewspaperArticle("¿La epidemia de las vacas locas?",
                "Las vacas de una ganadera local han sufrido una extraña transformación que las ha convertido en cabras. Los vecinos tienen miedo de que sea contagioso.");

            CreateRepercusion("cows_back_to_normal", "Cows back to normal", 15);
            AddStoryRepercusionNewspaperArticle("Las vacas todoterreno.",
               "Las vacas de una ganadera local se han convertido en el centro de atención de la comunidad ganadera tras ver las impactantes imágenes de vacas escalando riscos.");

            CreateRepercusion("cows_and_goats_reversed", "Cows and goats reversed", -15);
            AddStoryRepercusionNewspaperArticle("Confusión ganadera.",
               "Un extraño incidente ha provocado que las vacas se transformen en cabras y las cabras en vacas. ¿Cuán confuso puede llegar a ser el mundo en el que vivimos?");


            //TARGET: MERI
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("cows_transformed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Esto es una locura! Mis vacas han comido más pienso especial antes de que pudiera deshacerme de él.",
                "¡Ahora las vacas balan y se dan de cabezazos entre ellas!",
                "A este paso voy a tener que dedicarme al pastoreo de cabras. ¡¿Por qué me pasa esto a mí?!"
            }, Tag.Harm, 1, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Entonces cualquier cosa que se coma el pienso especial será transformado en cabra? ¿Qué ocurriría entonces si se lo echan a las cabras?" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Si Evith se comiera el pienso especial, no creo que le surte efecto. Ya presenta cualidades de cabra." });

            //HARM >=3
            StartStoryBranch();
            SetRepercusionToBranch("cows_transformed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "El asunto de mis vacas, si pueden seguir llamándose vacas, se ha ido de madre.",
                "De alguna manera han conseguido comerse todo el pienso especial que tenía que quemar.",
                "¡Ahora, además de balar, son más pequeñas y les han salido cuernos! Lo que tengo ya no son vacas.",
                "Será mejor que Johnny no haya vertido sus pócimas al río, no quiero que se conviertan en morsas o algo similar."
            }, Tag.Harm, 3, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Entonces cualquier cosa que se coma el pienso especial será transformado en cabra? ¿Qué ocurriría entonces si se lo echan a las cabras?" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Si Evith se comiera el pienso especial, no creo que le surte efecto. Ya presenta cualidades de cabra." });


            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("cows_back_to_normal");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Esta mañana he encontrado un frasco vacío junto al pienso normal que suelo darle a mis vacas.",
                "Parece ser que alguien vino por la noche a vaciar el contenido del frasco sobre el pienso.",
                "Por suerte parece que el efecto del líquido ha sido revertir en parte la transformación de las vacas.",
                "Ahora al menos se han tranquilizado y siguen dando leche de vaca."
            }, Tag.Help, 1, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿En serio vas a ayudar a que vuelvan a la normalidad? ¡Yo quería hasta donde llegaba la cosa!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Es mejor que la naturaleza siga su curso. Hay cosas que es mejor dejar inalteradas." });

            //HELP >=3
            StartStoryBranch();
            SetRepercusionToBranch("cows_back_to_normal");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "El problema con mis vacas se ha solucionado, más o menos...",
                "Parece ser que alguien le ha dado de beber a mis vacas varias pócimas de Johnny.",
                "¿Cómo lo sé? Él mismo ha venido a mi granja a preguntar por ellas.",
                "Parece ser que alguien se las robó ayer de su laboratorio.",
                "El caso es que mis vacas ahora se comportan como vacas... generalmente.",
                "Todavía siguen subiéndose a sitios extraños, pero por lo menos puedo tratar con ellas."
            }, Tag.Help, 3, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿En serio vas a ayudar a que vuelvan a la normalidad? ¡Yo quería hasta donde llegaba la cosa!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Es mejor que la naturaleza siga su curso. Hay cosas que es mejor dejar inalteradas." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("cows_and_goats_reversed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Ahora resulta que las cabras de mi vecino se están comportando como vacas. ¡Esto no tiene sentido!",
                "Alguien le ha dado de comer el pienso especial a las cabras. ¡¿Quién en su sano juicio haría algo así?!",
                "Ahora las cabras del vecino mugen y son más amigables. ¡Y hasta dan leche de vaca!",
                "Todo esto es una locura, ¿pero el problema se ha solucionado, supongo?"
            }, Tag.Convince, 1, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Con esa decisión, parece que vas a ayudarme a resolver una duda que tenía." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Aún cuando la naturaleza se vuelve confusa, ha de seguir su curso." });

            AddStorySelectionUIData("Las vacas locas");

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

        public void StartCreatingStory(string idName, string title, string questGiver, string description, List<string> introductionDialogue)
        {
            m_StoryData = new StoryData();
            m_StoryData.m_ID = new ID(idName);

            m_StoryData.m_Title = title;
            m_StoryData.m_IntroductionDialogue = introductionDialogue;

            m_StoryData.m_QuestGiver = questGiver;
            m_StoryData.m_Description = description;
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