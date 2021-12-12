using CQM.AssetReferences;
using CQM.Components;
using CQM.Databases;
using System.Collections.Generic;
using UnityEngine;
using PieceType = CQM.Components.QuestPieceFunctionalComponent.PieceType;
using Tag = CQM.Components.QPTag.TagType;


namespace CQM.DataBuilders
{
    public class StoryBuilder : BaseDataBuilder
    {
        [SerializeField] private StoriesReferencesDatabase _assetRefs;

        [SerializeField] private List<StoryInfoComponent> m_StoriesList = new List<StoryInfoComponent>();
        [SerializeField] private List<StoryUIDataComponent> m_StoryUI = new List<StoryUIDataComponent>();
        [SerializeField] private List<StoryRepercusionComponent> m_Repercusions = new List<StoryRepercusionComponent>();
        [SerializeField] private List<StoryRepNewspaperComponent> m_RepercusionNewspaperArticles = new List<StoryRepNewspaperComponent>();

        // Current Components Being Built
        private StoryInfoComponent m_Story;
        private StoryData m_StoryData;
        private BranchOption m_Branch;
        private StoryRepercusionComponent m_Repercusion;


        public override void BuildData(ComponentsDatabase c)
        {
            for (int i = 0; i < m_StoriesList.Count; i++)
            {
                var story = m_StoriesList[i];
                c.GetComponentContainer<StoryInfoComponent>().Add(story.m_StoryData.m_ID, story);
            }
            for (int i = 0; i < m_Repercusions.Count; i++)
            {
                var rep = m_Repercusions[i];
                c.GetComponentContainer<StoryRepercusionComponent>().Add(rep.m_ID, rep);
            }
            for (int i = 0; i < m_StoryUI.Count; i++)
            {
                var ui = m_StoryUI[i];
                c.GetComponentContainer<StoryUIDataComponent>().Add(ui.m_ParentStoryID, ui);
            }
            for (int i = 0; i < m_RepercusionNewspaperArticles.Count; i++)
            {
                var news = m_RepercusionNewspaperArticles[i];
                c.m_Newspaper.m_NewspaperStories.Add(news.m_RepID, news);
            }
        }

        public override void LoadDataFromCode()
        {
            m_StoriesList.Clear();
            m_Repercusions.Clear();
            m_StoryUI.Clear();
            m_RepercusionNewspaperArticles.Clear();

            // =============================================================================================
            //  STORY 1
            // =============================================================================================

            StartCreatingStory("mayor_problem", "El problema del alcalde", "mayor",
                "Unos lobos están amenazando a los habitantes del pueblo. El alcalde quiere hacer algo al respecto pero es muy tacaño.", new List<string>() {
                "Últimamente una manada de lobos nos está causando muchos problemas.",
                "Estos acechan a nuestro ganado y a los comerciantes que quieren llegar al pueblo.",
                "Si esto sigue así, tendremos que contratar a un cazador profesional, pero nos va a costar una fortuna.",
                "¡No sé qué hacer!"});

            CreateRepercusion("wolves_gone", "Wolves Gone", 10);
            AddStoryRepercusionNewspaperArticle("Los lobos desaparecen de nuestros campos.",
                "Después de varios días de gran tensión, los lobos por fin han dejado de ser un problema para nuestros ganaderos y otros viajeros.");

            CreateRepercusion("wolves_stay", "Wolves Stay", -10);
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
                "No subestimes a las Galletas mágicas. Incluso unos temibles lobos no deberían de ser problema para ellas." });

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
                 "No subestimes a las Galletas mágicas. Incluso unos temibles lobos no deberían de ser problema para ellas." });

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
                 "No subestimes a las Galletas mágicas. Unos simples lobos no deberían de ser problema para ellas." });

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
                "Tengo la sensación de que no me voy a aburrir contigo.", "Espero que no me decepciones." });
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
                 "Tengo la sensación de que no me voy a aburrir contigo.", "Espero que no me decepciones." });
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
                "No subestimes a las Galletas mágicas. Unos simples lobos no deberían de ser problema para ellas." });

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
                "No subestimes a las Galletas mágicas. Unos simples lobos no deberían de ser problema para ellas." });

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
                "No subestimes a las Galletas mágicas. Unos simples lobos no deberían de ser problema para ellas." });

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
                "No subestimes a las Galletas mágicas. Unos simples lobos no deberían de ser problema para ellas." });

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

            CreateRepercusion("cows_harmed", "Cows Harmed", -10);
            AddStoryRepercusionNewspaperArticle("La tragedia de las vacas.",
                "La misteriosa muerte de numerosas vacas de una ganadera local ha conmocionado a los habitantes del pueblo.");

            CreateRepercusion("cows_saved", "Cows Saved", 10);
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
                "Veo que ya le vas pillando el tranquillo a esto de hornear Galletas mágicas.",
                "Y viendo las decisiones que tomas... ¡Me gusta tu estilo!"});
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
                "Eso sí, no me haré responsable de mis actos si descubro a ese malnacido."
            }, Tag.Harm, 3, "meri");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                 "Veo que ya le vas pillando el tranquillo a esto de hornear Galletas mágicas.",
                "Y viendo las decisiones que tomas... ¡Me gusta tu estilo!"});
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

            CreateRepercusion("golden_egg_destroyed", "Golden Egg Destroyed", -10);
            AddStoryRepercusionNewspaperArticle("Demasiado bueno para ser cierto.",
                "La prestigiosa coleccionista Canela N Rama se ve envuelta en una polémica tras la realidad de su nueva adquisición, un huevo dorado, que resultó ser un huevo normal y corriente.");

            CreateRepercusion("golden_egg_safe", "Golden Egg Safe", 10);
            AddStoryRepercusionNewspaperArticle("El nuevo artefacto de Canela N Rama",
               "Un nuevo artefacto forma parte de la colección de la prestigiosa coleccionista local Canela N Rama, se trata de un misterioso huevo dorado de un valor incalculable, asegura la coleccionista.");

            CreateRepercusion("golden_egg_gone", "Golden Egg Gone", -10);
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

            CreateRepercusion("ms_chocolate_sabotaged", "MS Chocolate Sabotaged", -10);
            AddStoryRepercusionNewspaperArticle("Accidente explosivo.",
                "La explosión que despertó a todos los vecinos del pueblo fué causa de un accidente por parte de Miss Chocolate la Bomb, una chocolatera local. La chocolatera se encuentra ilesa y se ha disculpado públicamente.");

            CreateRepercusion("ms_chocolate_gunpowder", "MS Chocolate Gunpowder", 10);
            AddStoryRepercusionNewspaperArticle("Experimentos explosivos.",
               "Los experimentos de Miss Chocolate la Bomb tienen preocupados a toda la comunidad de vecinos tras adquirir grandes cantidades de pólvora. ¿Qué es lo que pretende la chocolatera?");

            CreateRepercusion("ms_chocolate_pepper", "MS Chocolate Pepper", 10);
            AddStoryRepercusionNewspaperArticle("El nuevo ingrediente secreto que revolucionará la chocolatería.",
               "La comunidad de chocolateros de la región se encuentra intrigada ante el nuevo ingrediente que utilizará Miss Chocolate la Bomb en sus productos, la pimienta.");

            //TARGET: miss_chocolate
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
            }, Tag.Harm, 1, "miss_chocolate");
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
            }, Tag.Harm, 3, "miss_chocolate");
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
            }, Tag.Help, 1, "miss_chocolate");
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
            }, Tag.Convince, 1, "miss_chocolate");
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
            }, Tag.Convince, 3, "miss_chocolate");
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

            CreateRepercusion("smart_rats_stay", "Smart rats stay", -10);
            AddStoryRepercusionNewspaperArticle("Los nuevos vecinos roedores.",
                "Una colonia de ratas aparentemente inteligentes se ha establecido en el granero de un granjero local. El dueño de la granja expresa su disgusto con la situación.");

            CreateRepercusion("smart_rats_gone", "Smart rats gone", 10);
            AddStoryRepercusionNewspaperArticle("La caída de la sociedad de las ratas.",
               "La colonia de ratas aparentemente inteligentes que atormentaba los cultivos de los granjeros locales parece haberse disuelto, para el contento de todos.");

            CreateRepercusion("smart_rats_tribute", "Smart rats tribute", 20);
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

            CreateRepercusion("mayor_assaulted", "Mayor assaulted", -10);
            AddStoryRepercusionNewspaperArticle("El misterioso agresor nocturno.",
                "Nuestro alcalde ha sido asaltado mientras paseaba por un misterioso atacante. Se cree que es un ciudadano descontento con la reciente recaudación de impuestos.");

            CreateRepercusion("mayor_waste", "Mayor waste", 10);
            AddStoryRepercusionNewspaperArticle("Las nuevas reformas.",
               "Gracias a la exitosa recaudación de impuestos, el alcalde continúa con su proyecto de embellecer el pueblo a pesar de las voces críticas de muchos vecinos.");

            CreateRepercusion("mayor_robbed", "Mayor robbed", -10);
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

            CreateRepercusion("cows_transformed", "Cows transformed", -10);
            AddStoryRepercusionNewspaperArticle("¿La epidemia de las vacas locas?",
                "Las vacas de una ganadera local han sufrido una extraña transformación que las ha convertido en cabras. Los vecinos tienen miedo de que sea contagioso.");

            CreateRepercusion("cows_back_to_normal", "Cows back to normal", 10);
            AddStoryRepercusionNewspaperArticle("Las vacas todoterreno.",
               "Las vacas de una ganadera local se han convertido en el centro de atención de la comunidad ganadera tras ver las impactantes imágenes de vacas escalando riscos.");

            CreateRepercusion("cows_and_goats_reversed", "Cows and goats reversed", 0);
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

            // =============================================================================================
            //STORY 8
            // =============================================================================================

            StartCreatingStory("the_cake_was_not_a_lie", "La tarta no era mentira", "johny_setas",
               "Johnny quiere participar en el próximo concurso de repostería. Piensa utilizar hongos que podrían poner en peligro el concurso.", new List<string>() {
                "La Feria va a ser pronto, de normal no me suelen interesar esos eventos, son muy aburridos, colega.",
                "Pero por lo visto este año va a hacer un concurso de repostería, eso de hacer tartas y tal, colega.",
                "¡Pienso hacer la tarta más flipante que se haya visto, colega!",
                "Tan solo necesito una Seta de la Pirueta, con ella los jueces darán saltos del sabor, colega.",
                "Iré esta misma noche a buscar algunas, espero tener suerte, porque son muy raras, colega."
               });

            CreateRepercusion("competition_judges_stoned", "Competition Judges Stoned", 10);
            AddStoryRepercusionNewspaperArticle("La Tarta del Caos",
                "El pasado concurso de repostería se ha cancelado debido a los efectos que ha provocado una tarta en los jueces. Los jueces sufren alucinaciones hasta el día de hoy. Se sospecha de un alquimista local.");

            CreateRepercusion("competition_safe", "Competition Safe", 10);
            AddStoryRepercusionNewspaperArticle("Un concurso sin incidentes",
                "El concurso anual de repostería ha finalizado sin incidentes.");

            //TARGET: JOHNNY
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("competition_judges_stoned");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Fuí ayer a buscar un par de setas para la tarta, pero no he podido sacar ninguna, colega.",
                "Los animalejos del bosque están cada año más agresivos. No me han dejado en paz, colega.",
                "Pero no todo está perdido. Tengo algunas Setas de la Pirueta guardadas del año pasado, colega.",
                "No estarán en las mejores condiciones, ¡pero seguro que algún premio me llevo, colega!"
            }, Tag.Harm, 1, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡La cosa va a ponerse muy interesante en el concurso! ¡Tengo un presentimiento de que así será!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ese tal Johnny parece un sujeto interesante. Tiene un aura muy peculiar." });

            //HARM >=3
            StartStoryBranch();
            SetRepercusionToBranch("competition_safe");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Tíiiiiiio, la cosa se ha puesto fina! Fuí a por setas por la noche y cuando volví mi laboratorio estaba ardiendo, colega!",
                "Se han echado a perder muchas pócimas e ingredientes que tenía guardados, colega.",
                "¿El concurso?, ya no va a poder ser. Estas cosas pasan, colega."
            }, Tag.Harm, 3, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡¿Por qué interferir en el concurso?! ¡Deja que el mundo siga su curso, que se iba a poner interesante!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Estoy convencido de que vas a evitar una situación… inusual con esa decisión." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("competition_judges_stoned");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "He encontrado las setas que buscaba, colega.",
                "Suerte que alguien ha puesto señales por el bosque para encontrarlas, colega.",
                "¿Qué si es raro que haya señales? No sé, mucha gente se perderá por el bosque, colega.",
                "En fin, ¡mi tarta va a ser tremenda, colega!"
            }, Tag.Help, 1, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡La cosa va a ponerse muy interesante en el concurso! ¡Tengo un presentimiento de que así será!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ese tal Johnny parece un sujeto interesante. Tiene un aura muy peculiar." });

            //HELP >=3
            StartStoryBranch();
            SetRepercusionToBranch("competition_judges_stoned");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Tíiiiiiio! Fuí a buscar cobre y encontré oro, colega.",
                "Buscando un par de Setas de la Pirueta me he topado con otra mucho más rara, colega.",
                "Nada más y nada menos que el Champiñón de la Visión, ¡mi favorito, colega!",
                "Ese champiñón te permite ver todo, colega, ¡toooodo!",
                "¡Los jueces van a flipar, colega!"
            }, Tag.Help, 3, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡La cosa va a ponerse muy interesante en el concurso! ¡Tengo un presentimiento de que así será!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Ese tal Johnny parece un sujeto interesante. Tiene un aura muy peculiar." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("competition_safe");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Encontré todas las setas que necesitaba, colega.",
                "Pero, lo he estado meditando y ya no me apetece participar, colega.",
                "¿Mi meditación? Dejo que las setas me indiquen el camino a seguir, así de simple, colega.",
                "He visto cosas más extrañas de lo habitual, eso no puede ser buena señal, colega."
            }, Tag.Convince, 1, "johny_setas");

            //CONVINCE >=3
            StartStoryBranch();
            SetRepercusionToBranch("competition_safe");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Al final encontré todas las setas que necesitaba y muchas más, colega.",
                "Pero, ya no voy a participar en el concurso, no merece la pena, colega.",
                "El Señor Galleta tiene razón, los jueces no sabrían apreciar mi obra, colega.",
                "¿El Señor Galleta? Lo he visto durante mi meditación nocturna, colega.",
                "¡Todo es posible cuando se consumen Champiñones de la Visión, son alucinantes, colega!"
            }, Tag.Convince, 3, "johny_setas");

            AddStorySelectionUIData("La tarta no era de mentira");
            FinishCreatingStory();

            // =============================================================================================
            //STORY 9
            // =============================================================================================

            StartCreatingStory("the_lord_of_the_ducks", "El Señor de los Patos", "mantecas",
             "Unos patos muy agresivos causan el caos destrozando los cultivos en la granja del Mantecas.", new List<string>() {
                "¡Siempre tiene que pasar algo en mi granja! ¡Nunca me dejarán en paz!",
                "¡Si no son ratas que me roban mis hortalizas, ahora son patos que me destrozan lo sembrado!",
                "Esto tiene que ser cosa del Johnny ¡Ese merluzo debe de haber vertido sus mejunjes en la charca!",
                "¡Los patos se han vuelto muy agresivos, no puedo acercarme a mis cultivos!",
                "Voy a tener que tomar medidas al respecto. ¡No se saldrán con la suya!"
             });

            CreateRepercusion("ducks_stay", "Ducks Stay", -10);
            AddStoryRepercusionNewspaperArticle("Invasión aviar",
                "Una bandada de patos muy agresivos han ocupado el granero de un granjero local. El dueño de la granja expresa su descontento con un rastrillo en mano.");

            CreateRepercusion("ducks_gone", "Ducks Gone", 10);
            AddStoryRepercusionNewspaperArticle("La Paz de los Patos.",
                "La guerra entre la bandada de patos agresivos y un granjero local ha finalizado. Los patos aceptaron su derrota y partieron fuera del pueblo. ¿Buscarán otras tierras que conquistar?");

            CreateRepercusion("ducks_help", "Ducks Help", 20);
            AddStoryRepercusionNewspaperArticle("Lo nuevo en pesticidas.",
                "La comunidad agrícola se encuentra asombrada ante el nuevo método de control de plagas que un granjero local está utilizando. Se trata nada más y nada menos que de patos. Con el cuidado adecuado, estos se encargan de cualquier alimaña sin que afecte a los cultivos.");

            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("ducks_stay");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Sucias ratas del aire! ¡Los patos han ocupado mi granero! ¡¿Cómo diantres han logrado entrar?!",
                "Este va a ser un problema mucho mayor que las ratas, no voy a poder echarlas de allí tan fácilmente.",
                "¡¿Con quién creen que están tratando?! ¡Han venido a conquistar la granja equivocada!"
            }, Tag.Harm, 1, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Imagínate qué guay sería que los patos pudieran llevar armas!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Nadie se hubiera imaginado aves tan gráciles pudieran llegar a ser tan agresivas." });

            //HARM >=3
            StartStoryBranch();
            SetRepercusionToBranch("ducks_stay");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡La próxima vez que vea al Johnny, va a continuar con sus experimentos desde el fondo de un pozo!",
                "¡Los patos se han apoderado de mi casa! ¡¿Cómo es esto posible?! ¡¿Cómo han entrado para empezar?!",
                "¡Esto tiene que ser una broma! ¡Unos estúpidos patos me han vencido, a mí!",
                "No me queda más remedio que darles parte de mi cosecha para que me dejen tranquilo."
            }, Tag.Harm, 3, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Imagínate qué guay sería que los patos pudieran llevar armas!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Nadie se hubiera imaginado aves tan gráciles pudieran llegar a ser tan agresivas." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("ducks_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Ayer llegó otra bandada de patos a destrozar mi granja! ¡Por si una sola bandada no fuera suficiente!",
                "¡Encima se están peleando entre ellas, haciendo aún más destrozos!",
                "¡Esto es un circo! ¡Es inaudito! Al menos será más fácil echarlas de mi granja."
            }, Tag.Help, 1, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Ojalá pudiera tener una bandada de patos a mi disposición! ¡Me lo pasaría en grande!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Habría que evitar que ese tal Johnny siguiera provocando situaciones como esta." });

            //HELP >=3
            StartStoryBranch();
            SetRepercusionToBranch("ducks_gone");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Los patos se han largado de mi granja.¡ Aunque la cosa está lejos de estar bien!",
                "¡Las ratas han regresado de su escondrijo y han echado a los patos! ¡Me están tomando el pelo!",
                "No sé que las habrá hecho volver, pero prefiero lidiar con ratas inteligentes antes que con esos patos.",
                "¡Si esto es obra del Johnny, me encargaré de trasladar su laboratorio al fondo del mar!"
            }, Tag.Help, 3, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Ojalá pudiera tener una bandada de patos a mi disposición! ¡Me lo pasaría en grande!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Habría que evitar que ese tal Johnny siguiera provocando situaciones como esta." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("ducks_help");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Por fín se han tranquilizado los patos! han dejado de causarme problemas, por ahora.",
                "Todavía siguen rondando por mi granja, pero ya no son tanto problema.",
                "En esta época del año hay bichos a punta pala, y tengo que andar fumigando los cultivos todo el día.",
                "Por suerte los patos se comen los bichos que pululan por mis cultivos.",
                "¡Pero esto no significa que vayan a quedarse! ¡En cuanto llegue el momento, me encargaré de echarlos!"
            }, Tag.Convince, 1, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Se puede realmente negociar con un pato? Espero que al menos entiendan el lenguaje universal de la violencia." });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Pienso que todos los seres del mundo pueden llegar a convivir en paz.",
                "Aunque a veces pienso que a algunos seres es mejor tenerlos apartados."});

            AddStorySelectionUIData("El Señor de los Patos");
            FinishCreatingStory();

            // =============================================================================================
            //STORY 10
            // =============================================================================================

            StartCreatingStory("natural_chocolate_milkshake", "Batido de chocolate 100% natural", "miss_chocolate",
             "Miss Chocolate quiere experimentar con las vacas de Meri para obtener de estas un batido de chocolate al ordeñarlas.", new List<string>() {
                "¿El sabor explosivo? Lo he dejado de lado por el momento, pero tengo una idea mucho mejor.",
                "La mayor preocupación de toda chocolatera que se precie es de donde obtener chocolate de calidad.",
                "He oído rumores de pueblos lejanos que hablan de que los batidos de chocolate se obtienen de las vacas marrones.",
                "Estoy segura de que las vacas son capaces de producir batidos de chocolate con la alimentación adecuada.",
                "Después de insistir mucho, he logrado que Meri me ceda sus vacas para este noble propósito.",
                "¡Qué emoción! ¡No puedo esperar a ponerme manos a la obra!"
             });

            CreateRepercusion("cows_poisoned", "Cows Poisoned", -10);
            AddStoryRepercusionNewspaperArticle("Experimentos extremos.",
                "La célebre chocolatera local, Miss Chocolate la Bomb ha sido el foco de una nueva polémica tras el fracaso rotundo en su nuevo experimento. El experimento ha resultado en el envenenamiento leve de las vacas de una ganadera local.");

            CreateRepercusion("cows_experiment_success", "Cows Experiment Success", 10);
            AddStoryRepercusionNewspaperArticle("Los batidos de chocolate 100% naturales.",
                "La conocida chocolatera local, Miss Chocolate la Bomb, ha obtenido resultados impresionantes en su nueva investigación. De alguna manera ha logrado obtener muestras de batido de chocolate al ordeñar vacas marrones. ¿Ha llegado la ciencia demasiado lejos?");

            CreateRepercusion("cows_experiment_delayed", "Cows Experiment Delayed", -10);
            AddStoryRepercusionNewspaperArticle("Rechazo vacuno.",
                "La famosa chocolatera local, Miss Chocolate la Bomb, ha sufrido heridas leves tras ser atacada por las vacas con las que experimentaba.");

            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("cows_poisoned");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "El experimento no va por buen camino, algunas vacas están enfermando.",
                "Quiero estudiar muestras de leche de las vacas alimentadas con mi pienso especial.",
                "Pero parece que he cometido un error garrafal, ¡añadir bellotas a la mezcla!",
                "No me he dado cuenta hasta que lo he revisado, ¡¿Cómo he sido tan descuidada?!",
                "Si la cosa no mejora, voy a tener que abandonar el experimento. Meri ya me está mirando mal."
            }, Tag.Harm, 1, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Me pregunto hasta qué punto será posible obtener batidos de chocolate de las vacas.",
                "Bueno, me da a mí que va a ser tarde para averiguarlo."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Hay algunas personas que quieren jugar a ser Dios. Y luego están quienes lo intentan de verdad." });

            //HARM >=3
            StartStoryBranch();
            SetRepercusionToBranch("cows_poisoned");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡¿Cómo ha podido salir tan mal?! Las vacas están para el arrastre. A Meri le va a dar un patatús.",
                "¡¿Dónde me he equivocado?! Juraría haber utilizado los ingredientes correctos para el pienso especial.",
                "Empiezo a sospechar que alguien ha manipulado el pienso antes de dárselo a las vacas.",
                "En fin, tendré que pedir a otro vecino que me ceda sus vacas, aunque será difícil después de esto."
            }, Tag.Harm, 3, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Me pregunto hasta qué punto será posible obtener batidos de chocolate de las vacas.",
                "Bueno, me da a mí que va a ser tarde para averiguarlo."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Hay algunas personas que quieren jugar a ser Dios. Y luego están quienes lo intentan de verdad." });


            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("cows_experiment_success");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Mis experimentos con las vacas dan frutos, aunque también surgen complicaciones.",
                "Tras ingerir un pienso especial que he preparado yo misma, han empezado a comportarse de manera extraña.",
                "Poco después de ingerir el pienso las vacas empezaron a ladrar, ¡Te lo juro!",
                "Meri casi se desmaya al verlo, parece ser que ya ha pasado por algo parecido.",
                "Al menos las muestras de leche ordeñadas contienen muestras de chocolate, que ya es algo."
            }, Tag.Help, 1, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Hasta dónde llegará esa tal Miss Chocolate con tal de lograr sus experimentos? Quizás sea de las mías."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "No tiene nada de malo ayudar en una causa aparentemente imposible. No será malo, pero sí que puede ser estúpido." });

            //HELP >=5
            StartStoryBranch();
            SetRepercusionToBranch("cows_experiment_success");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡El experimento ha salido mucho mejor de lo que esperaba!",
                "Después de ingerir el pienso especial con galletas y dulces que he preparado, ¡he logrado resultados!",
                "Eso sí, parece ser que, en un descuido mío, se ha derramado una de las pócimas de Johnny sobre el pienso.",
                "No lo he sabido hasta mucho después de que se lo hayan comido. ¡Puede que esa sea la clave!",
                "La leche ordeñada de las vacas se asemeja a un batido de chocolate, o a algo parecido al menos.",
                "¡Qué alegría! Estoy cerca de cumplir mi objetivo. ¡Tengo que seguir trabajando!"
            }, Tag.Help, 5, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Hasta dónde llegará esa tal Miss Chocolate con tal de lograr sus experimentos? Quizás sea de las mías."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "No tiene nada de malo ayudar en una causa aparentemente imposible. No será malo, pero sí que puede ser estúpido." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("cows_experiment_delayed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "No estoy avanzando apenas en mi investigación con las vacas. Son más agresivas de lo que esperaba.",
                "Meri me dijo que eran muy apacibles con todo el mundo, ¿Les caeré mal?",
                "¡Qué fastidio! Parece que tengo que buscar otras opciones. ¡Pero no me rendiré!"
            }, Tag.Convince, 1, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Incluso yo, que me regocijo en el caos, adoro a las vacas ¡Nadie debería hacerles daño!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "A veces la gente necesita un pequeño susto para darse cuenta del error que cometen." });

            //CONVINCE >=3
            StartStoryBranch();
            SetRepercusionToBranch("cows_experiment_delayed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡¿Cómo hace Meri para tratar con sus vacas día a día?! Son muy agresivas conmigo.",
                "Pensaba hacerlas ingerir un pienso especial, pero parece que va a ser imposible.",
                "¡No creas que he abandonado mi búsqueda, es sólo que tengo que cambiar mi enfoque!"
            }, Tag.Convince, 3, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Incluso yo, que me regocijo en el caos, adoro a las vacas ¡Nadie debería hacerles daño!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "A veces la gente necesita un pequeño susto para darse cuenta del error que cometen." });

            AddStorySelectionUIData("Batido de chocolate 100% natural");
            FinishCreatingStory();


            // =============================================================================================
            //STORY 11
            // =============================================================================================

            StartCreatingStory("the_stolen_pendant", "El Colgante Robado", "canela",
             "La última adquisición de Canela, un colgante de mucho valor, ha sido robado por una rival suya. Su rival afirma que Canela tiene en su posesión artefactos suyos.", new List<string>() {
                "¡Qué dura es la vida de una adquisidora de reliquias! Nunca te aburres, lo creas o no",
                "Volví de una de mis visitas a las subastas de la ciudad e hice una nueva adquisición.",
                "Se trataba de un colgante enjoyado muy antiguo de mucho valor. ¡Y esta vez es auténtico, no como el huevo aquel!",
                "El caso es que he perdido el colgante, debe de ser cosa de mi rival, otra adquisidora de reliquias.",
                "¡Me tiene mucha manía! Insiste en que muchos de mis artefactos le pertenecen a su familia por derecho.",
                "Quiero recuperar el colgante, ¡yo lo he comprado así que tengo más derecho que ella!"
             });

            CreateRepercusion("artifacts_stolen", "Artifacts Stolen", -20);
            AddStoryRepercusionNewspaperArticle("Robo en la exposición",
                "Muchos de los artefactos de la coleccionista, Canela N Rama, han desaparecido sin dejar rastro. No se han encontrado pruebas en la escena del crimen.");

            CreateRepercusion("pendant_damaged", "Pendant Damaged", -10);
            AddStoryRepercusionNewspaperArticle("La nueva joya de la exposición regresa a su vitrina",
                "El colgante enjoyado que se creía desaparecido ha vuelto a las manos de Canela N Rama. Por desgracia, se encuentra en un estado lamentable.");

            CreateRepercusion("pendant_recovered", "Pendant Recovered", 20);
            AddStoryRepercusionNewspaperArticle("La  joya de la exposición regresa intacta.",
                "El colgante enjoyado desaparecido de la colección ha regresado intacto a la exposición.");

            CreateRepercusion("pendant_lost", "Pendant Lost", -10);
            AddStoryRepercusionNewspaperArticle("El colgante enjoyado sigue desaparecido.",
                "La nueva joya de la exposición de Canela N Rama sigue en paradero desconocido.");

            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("artifacts_stolen");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Mi colección está arruinada, sabía que esto podía pasar!",
                "Alguien entró en mi casa por la noche y me ha robado mis preciados artefactos.",
                "¡¿Por qué tengo guardias tan incompetentes?! ¡Reforcé la seguridad desde el problema con la secta!",
                "No han encontrado rastro del culpable, ¡pero ha tenido que ser cosa de mi rival, seguro!"
            }, Tag.Harm, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "El robo me parece bien. Al fin y al cabo, es mejor que te roben a que te destrocen las cosas."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "¿Qué ocurriría al robar a un ladrón? Es discutible, pero pienso que te conviertes en otro." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("pendant_damaged");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¿Mi colgante? Ha regresado a mis manos, pero… Está muy dañado.",
                "Alguien ha debido de recuperarlo por mí, pero casi desearía que no lo hubiera hecho.",
                "Quien quiera que haya sido, parece que no era consciente de lo frágil que era.",
                "Al menos lo he recuperado, así que no me puedo quejar, supongo."
            }, Tag.Help, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Un ladrón que roba a otro ladrón? Soy más partidaria de eliminar a la competencia."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Estoy bastante seguro de haberme encontrado con alguno de esos artefactos en su época." });

            //HELP >=5
            StartStoryBranch();
            SetRepercusionToBranch("pendant_recovered");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¿El colgante? ¡Ha regresado a mis manos, no sabes cómo de aliviada estoy!",
                "Lo que me extraña todavía es quién se ha encargado de traerlo de vuelta.",
                "El rumor se ha debido de extender por el pueblo, ¡y algún justiciero se ha hecho cargo!",
                "Ha debido de ser todo un profesional, porque el colgante se encuentra en perfectas condiciones.",
                "¡Ahora podré exponerlo junto al resto de mi colección!"
            }, Tag.Help, 5, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "No entiendo el afán de acaparar tantos cachivaches. ¿No era un síndrome o algo así?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Quizás de aquí a cinco mil años tu pala se considerará como un artefacto valioso." });

            //CONVINCE >= 1
            StartStoryBranch();
            SetRepercusionToBranch("pendant_lost");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Me han enviado un colgante que se parece mucho al original, empezaba a pensar que podría ser valioso.",
                "Pero resultó ser una burda falsificación ¡¿Qué clase de broma es esta?! ¡Esa necia se está burlando de mí!",
                "¡No dejaré que esa ladrona se ría de mí! ¡Pienso recuperar mi colgante!",
                "¡Y si no puedo recuperarlo, lo haré añicos con tal de que no lo tenga ella!"
            }, Tag.Convince, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Siempre me he preguntado cuánto tiempo tiene que pasar para que el saqueo de tumbas pase a considerarse arqueología."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "No veo nada malo en conservar artefactos antiguos, siempre que no sean peligrosos." });

            //CONVINCE >= 3
            StartStoryBranch();
            SetRepercusionToBranch("pendant_recovered");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "He recuperado mi colgante… Bueno, realmente no es el mismo colgante pero como si lo fuera.",
                "Ha debido de ser mi rival, aunque no pensaba que fuera capaz de regalarme otro colgante en compensación.",
                "Lo he estudiado y tiene un valor muy similar al que tenía originalmente. Pero sigo sin estar conforme.",
                "No me malinterpretes, sigo disgustada por lo que ha hecho, aunque quizás ella no sea tan ruin como pensaba."
            }, Tag.Convince, 3, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "No entiendo el afán de acaparar tantos cachivaches. ¿No era un síndrome o algo así?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Quizás de aquí a cinco mil años tu pala se considerará como un artefacto valioso." });

            AddStorySelectionUIData("El Colgante Robado");
            FinishCreatingStory();

            // =============================================================================================
            //STORY 12
            // =============================================================================================

            StartCreatingStory("a_regular_day", "Un día normal", "mantecas",
            "El Mantecas está muy preocupado después de tener unos días de tranquilidad. Está esperando a que ocurra el siguiente desastre en su granja.", new List<string>() {
                "Últimamente no ha ocurrido nada inusual en mi granja. Todo va bien, ¡demasiado bien!",
                "¡¿Por qué estoy enfadado, me preguntas?! ¡Pues porque claramente se avecinan nuevos problemas!",
                "¡Lo siento en mis huesos, mis momentos de tranquilidad se van a acabar pronto!",
                "En el momento que baje la guardia… ¡Pum! El desastre volverá a mis tierras."
            });

            CreateRepercusion("mantecas_farm_damaged", "Mantecas Farm Damaged", -10);
            AddStoryRepercusionNewspaperArticle("Una nueva plaga asola nuestros campos.",
                "Una plaga de topos ha hecho estragos en los cultivos de una granja local. Sorprendentemente, el dueño no parecía estar muy disgustado al respecto.");

            CreateRepercusion("mantecas_farm_devastated", "Mantecas Farm Devastated", -20);
            AddStoryRepercusionNewspaperArticle("La devastación de nuestros campos.",
                "Una terrible plaga de langostas, topos y ratas han arrasado de la noche a la mañana el esfuerzo de un agricultor local. Para nuestra sorpresa, el dueño esbozó una sonrisa.");

            CreateRepercusion("mantecas_farm_revitalized", "Mantecas Farn Revitalized", 10);
            AddStoryRepercusionNewspaperArticle("Buenos tiempos para nuestros campos.",
                "La prolongada serie de desgracias que acechaban a los cultivos de un granjero local parece haberse detenido. Aun así, las preocupaciones se mantienen, declaró el dueño.");

            CreateRepercusion("mantecas_paranoic", "mantecas_paranoic", -10);
            AddStoryRepercusionNewspaperArticle("¿Se avecinan tiempos terribles?",
                "Un granjero local asegura con total seguridad que un nuevo desastre se avecina sobre sus campos después de percatarse de fenómenos extraños. ¿Podría tratarse de una broma?");

            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("mantecas_farm_damaged");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Han aparecido topos en mi granja! ¡Están destrozando mis cultivos!",
                "¡Sabía que algo estaba por suceder! ¡Mi intuición nunca falla!",
                "¡Esos malditos estaban esperando su oportunidad para amargarme la existencia! ¡Tendrán su merecido!"
            }, Tag.Harm, 1, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Veo que te has tomado muy en serio la petición de ese tipo. ¡Conseguirá lo que desea!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Me da un poco de pena ese granjero, parece que sólo vive para sobrevivir un desastre tras otro." });

            //HARM >=5
            StartStoryBranch();
            SetRepercusionToBranch("mantecas_farm_devastated");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡El caos ha vuelto a mi granja! ¡Sabía que algo gordo se avecinaba!",
                "¡Han aparecido topos y langostas para destrozarme los cultivos! ¡Además las ratas han vuelto a ocupar mi granero!",
                "¡Sucias alimañas! ¡¿Creen que pueden conmigo?! ¡Qué vengan! ¡Se van a llevar su merecido!"
            }, Tag.Harm, 5, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Lo estás dando todo para cumplir el deseo de ese tipo, ¿no? ¡Espero que no se arrepienta!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Si bien la petición de ese granjero es muy extraña, no creo necesario llegar tan lejos." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("mantecas_farm_revitalized");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Todavía no ha ocurrido nada raro, de hecho, todo lo contrario, ¡mi granja va bien!",
                "¡Mis cultivos están en mejores condiciones y los pastos son más verdes!",
                "¿Puede ser que después de tantas desgracias, por fin mi trabajo haya dado sus frutos?",
                "¡Ni de broma! ¡Algo va a pasar, seguro!"
            }, Tag.Help, 1, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Pensaba que ibas a ayudar a ese pobre granjero con su escasez de desastres. ¡Una pena!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "No siempre lo que queremos tener y lo que necesitamos de verdad coinciden." });

            //HELP >=5
            StartStoryBranch();
            SetRepercusionToBranch("mantecas_farm_revitalized");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "No ha vuelto a ocurrir nada en mi granja. ¡De hecho, está mejor que nunca!",
                "Mis cultivos crecen sanos, la calidad del suelo ha mejorado y no hay ninguna plaga que erradicar.",
                "¡¿Me toman por tonto?!	¡¿Cuándo van a volver a fastidiarme?! ¡Me estoy impacientando!",
                "¡Es imposible que de repente todo me vaya bien! ¡Impensable! ¡Debe de avecinarse el fin del mundo!"
            }, Tag.Help, 5, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Pensaba que ibas a ayudar a ese pobre granjero con su escasez de desastres. ¡Una pena!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "No siempre lo que queremos tener y lo que necesitamos de verdad coinciden." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("mantecas_paranoic");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Una nueva amenaza se cierne sobre mi granja! ¡Tengo que prepararme para lo peor!",
                "¡Han aparecido unas marcas extrañas en la pared de mi casa, y también he escuchado ruidos extraños por la noche!",
                "¡Hay algo ahí fuera que se está preparando para amargarme la vida, pero estoy preparado para cualquier cosa!"
            }, Tag.Convince, 1, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Iría yo misma a causar el caos en su granja, pero soy una deidad muy ocupada, ¿sabes?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "¿Será mala suerte lo de ese granjero o él mismo se busca sus problemas?" });

            //CONVINCE >=3
            StartStoryBranch();
            SetRepercusionToBranch("mantecas_paranoic");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Se avecinan grandes problemas en mi granja!  ¡Vuelven a ocurrir cosas extrañas!",
                "¡Han aparecido formas en mis cultivos con forma de calavera! ¡Esto solo puede ser una señal de que algo se aproxima!",
                "¡Va a ocurrir en cualquier momento, me estoy impacientando!",
                "¡Ardo en deseos de ver a qué me enfrentaré esta vez! ¡Sea lo que sea, mi granja no caerá!"
            }, Tag.Convince, 3, "mantecas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Iría yo misma a causar el caos en su granja, pero soy una deidad muy ocupada, ¿sabes?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "¿Será mala suerte lo de ese granjero o él mismo se busca sus problemas?" });

            AddStorySelectionUIData("Un día normal");
            FinishCreatingStory();


            // =============================================================================================
            //STORY 13
            // =============================================================================================

            StartCreatingStory("mushroom_profecy", "La profecía del hongo", "johny_setas",
          "En su última sesión de meditación, Johnny ha tenido la visión de una luz muy brillante. Está convencido de que se trata de un augurio.", new List<string>() {
                "Estoy muy rayado estos días, en plan, no soy capaz de pensar en cualquier otra cosa, colega.",
                "De normal cuando medito, veo formas y colores fuera de la comprensión humana, colega.",
                "El caso es que el otro día me llamó la atención una luz más resplandeciente que cualquier cosa, colega.",
                "No sabría describirte nada más, pero tiene que ser una señal de algo importante, colega."
          });

            CreateRepercusion("johnny_sabotaged", "Johnny Sabotaged", -10);
            AddStoryRepercusionNewspaperArticle("Incendio en la casa del alquimista.",
                "El repentino incendio que por poco asola la casa del alquimista local y que podría haber afectado al bosque ha sido detenido con éxito.  Por suerte, el alquimista se encuentra en su estado habitual.");

            CreateRepercusion("johnny_enhanced", "Johnny Enhanced", 10);
            AddStoryRepercusionNewspaperArticle("La alquimia renovada.",
                "La comunidad de vecinos se alegra por el gran hallazgo de un alquimista local. Con dicho hallazgo hará grandes cosas para el pueblo, declaró el alquimista. ¿Qué tendrá preparado? ¡Esperemos que nada malo!");

            CreateRepercusion("johnny_neutral", "Johnny Neutral", 10);
            AddStoryRepercusionNewspaperArticle("El alquimista ausente.",
                "Parte de la comunidad de vecinos se encuentra preocupada por la salud del alquimista local. Este misteriosamente ya no abre tanto su tienda como lo hacía antes. ¿Qué le ocurre al joven alquimista?");

            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("johnny_sabotaged");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡He descubierto el significado de esa luz, colega!",
                "Bueno, menos mal que lo he descubierto pronto, ¡porque se podía liar bastante parda, colega!",
                "Estaba yo meditando como todas las noches y ocurrió de repente, así sin previo aviso, colega.",
                "¡El saco con mis... materiales de meditación empezó a arder, tal cual te lo digo colega!",
                "Suerte que no estaba en un trance profundo, porque si no, no me hubiera ni inmutado, colega.",
                "Un poco más y se me quema la casa. ¡Pero definitivamente la luz que ví se trataba de eso, colega!"
            }, Tag.Harm, 1, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
               "La meditación es para la gente que quiere dormir pero no puede dormir. No le veo la gracia."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Entrar en el mundo de la meditación es demasiado peligroso para gente corriente.",
            "En serio, un novicio podría perder facultades mentales si se adentra demasiado."});

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("johnny_enhanced");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡La suerte me sonríe! Vagando por el bosque me he encontrado con un pequeño tesoro, colega.",
                "¿Quién hubiera pensado que encontraría un saco con monedas en mitad del bosque? ¡Es flipante, colega!",
                "Parecía que algo me guiaba hacia el lugar donde lo encontré, yo tan solo me dejé llevar, colega.",
                "Estoy seguro de que la luz que ví fue el brillo del pequeño tesoro. ¡Es pura lógica, colega!",
                "Aunque no sea gran cosa, puedo permitirme comprar algunos materiales raros, colega."
            }, Tag.Help, 1, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Nu es muy fan de la meditación y ese tipo de actividades inactivas. ¡Pero yo las odio!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "¿Podría ese joven alcanzar el estado más profundo del trance? Muy pocos lo han logrado." });

            //HELP >=5
            StartStoryBranch();
            SetRepercusionToBranch("johnny_enhanced");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Tíiiiio, ayer encontré algo flipante!  ¡Nada más y nada menos que un Ámbar Perezoso, colega!",
                "¡Es súper resplandeciente, tiene el mismo brillo que ví en la profecía, colega!",
                "¡Llevaba mucho tiempo buscándolo! Pensaba que ya no quedaba ninguno por aquí, colega.",
                "Hice bien en vagar por el bosque de noche, mi intuición me llevó hasta donde se encontraba, colega.",
                "Si trituras uno de esos con cuidado, se pueden utilizar para medicina y alquimia, colega.",
                "¡Y además era uno de los gordos, con esto podré hacer cosas flipantes, colega!"
            }, Tag.Help, 5, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Nu es muy fan de la meditación y ese tipo de actividades inactivas. ¡Pero yo las odio!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "¿Podría ese joven alcanzar el estado más profundo del trance? Muy pocos lo han logrado." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("johnny_neutral");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Llevo ya bastante rato meditando sobre la luz que ví, colega.",
                "Aún no lo tengo muy claro, pero ayer escuché voces extrañas, diferentes a las habituales, colega.",
                "¡Esto solo puede significar que estoy adentrándome en un trance cada vez más profundo, colega!",
                "¿El trance? Es un estado mental donde se puede apreciar las costuras de la realidad, colega.",
                "Se necesita mucha concentración para alcanzarlo, y muchas setas, colega."
            }, Tag.Convince, 1, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "No entiendo el interés de Nu por ese tal Johnny. ¡Si sólo se dedica a hacer el vago!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Quizás sea adecuado persuadir a ese joven antes de que pueda llegar a perjudicarse más." });

            //CONVINCE >=3
            StartStoryBranch();
            SetRepercusionToBranch("johnny_neutral");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Tíiiiiio, se me ha revelado el auntèntico significado de la luz que ví el otro día, colega!",
                "Mientras meditaba, el legendario Señor Galleta me reveló su significado, colega.",
                "¿El Señor Galleta? Pude vislumbrar su figura ¡Se necesita mucha concentración para eso, colega!",
                "Cuanto más profundo es el trance, más cosas increíbles puedes llegar a ver, colega.",
                "¿La luz? Resultó ser la luz del Sol dándome en la cara, ¡una señal de un mañana mejor, colega!"
            }, Tag.Convince, 3, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "No entiendo el interés de Nu por ese tal Johnny. ¡Si sólo se dedica a hacer el vago!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Quizás sea adecuado persuadir a ese joven antes de que pueda llegar a perjudicarse más." });

            AddStorySelectionUIData("La profecía del hongo");
            FinishCreatingStory();

            // =============================================================================================
            //STORY 14
            // =============================================================================================

            StartCreatingStory("mayor_worries", "Preocupaciones mayores", "mayor",
            "El alcalde está preocupado por el disgusto general de la población con su gestión. Quiere hacer las cosas bien pero teme que el pueblo se rebele en su contra.", new List<string>() {
                "Desde hace ya un tiempo, no paran de ocurrir problemas en el pueblo, estoy muy preocupado.",
                "Lo peor es que mucha gente me echa las culpas a mí, como si fuera mi culpa.",
                "Siempre he actuado en beneficio del pueblo, aunque eso seguro que ya lo sabes.",
                "Aunque bueno, lo cierto es que normalmente…hago medidas a muy largo plazo.",
                "¡No quiero que la gente se rebele contra mí, entonces sí que estaríamos en la ruina!",
                "Mi mayor prioridad va a ser arreglar las cosas en el pueblo, pero no sé que puedo hacer."
            });

            CreateRepercusion("mayor_alerted", "Mayor Alerted", -10);
            AddStoryRepercusionNewspaperArticle("Convocada asamblea de emergencia.",
                "Nuestro excelentísimo alcalde ha convocado la asamblea mensual mucho antes de la fecha habitual. Según declara, es necesario tomar medidas inmediatamente y escuchar las sugerencias de los vecinos.");

            CreateRepercusion("mayor_relaxed", "Mayor Relaxed", 10);
            AddStoryRepercusionNewspaperArticle("Un cambio de planes inesperado.",
                "El alcalde ha sustituído su plan de reformas por otro nuevo de manera repentina. El alcalde declara que los planes de embellecimiento del pueblo y la ampliación del puerto se dejarán para otro momento.");

            CreateRepercusion("mayor_ignore", "Mayor_Ignore", 10);
            AddStoryRepercusionNewspaperArticle("Las críticas continúan.",
                "El alcalde sigue siendo objeto de duras críticas tras resumir su plan de ampliación del puerto local. Muchos vecinos creen que es una pérdida de tiempo, pero el alcalde planea seguir el plan a rajatabla.");

            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("mayor_alerted");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Es mucho peor de lo que pensaba! ¡La gente me odia y no duda en hacer pintadas en mi casa!",
                "Para variar, no sé quién es el responsable de tales actos, pero eso no importa ahora mismo.",
                "¡¿No se dan cuenta de que ya estoy haciendo todo lo que puedo?! ¡Deberían ser más pacientes!",
                "Tendré que adelantar la asamblea del mes que viene para ver qué se puede hacer.",
                "No nos queda demasiado dinero después de los gastos del mes pasado, pero para emergencias cubre."
            }, Tag.Harm, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Cuándo estallará entonces la revolución? ¡Quiero estar allí en primera fila liderando los saqueos!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Amenazar a un gobernante puede ser una forma efectiva de hacerlo razonar. Si respeta a su pueblo, claro está." });

            //HARM >=3
            StartStoryBranch();
            SetRepercusionToBranch("mayor_alerted");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Esto es un desastre! ¡No solo me asaltan por la calle si no que además me envían amenazas por correo!",
                "¡Menuda falta de respeto a mi persona! ¡Es inaceptable!",
                "Pero ahora no hay tiempo de buscar al responsable. ¡Muy pronto puede ocurrir una revuelta!",
                "¡Mi cabeza rodará por las escaleras del ayuntamiento! Tengo que hacer algo al respecto yo mismo.",
                "Voy a convocar una asamblea de emergencia mañana mismo."
            }, Tag.Harm, 3, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Cuándo estallará entonces la revolución? ¡Quiero estar allí en primera fila liderando los saqueos!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Amenazar a un gobernante puede ser una forma efectiva de hacerlo razonar. Si respeta a su pueblo, claro está." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("mayor_relaxed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Puede ser que me haya precipitado al pensar que la situación se iba a descontrolar en el pueblo.",
                "Durante la noche, alguien ha hecho pintadas en las paredes del ayuntamiento.",
                "Son pintadas sobre el mal estado de los caminos por el campo y sobre lo intransitables que son.",
                "Hacer pintadas es un acto vandálico, sí. Pero… me doy cuenta de que tienen un poco de razón.",
                "Llevo tanto tiempo centrado en la decoración de los parques que había olvidado todo lo demás.",
                "Supongo que tendré que posponer mis planes para otro momento."
            }, Tag.Help, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿El puerto de la charca? ¡¿El alcalde piensa hacer que los barcos lleguen por tierra o qué?!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Pienso que sería más plausible construir máquinas voladoras antes que un puerto en un lugar como ese." });

            //HELP >=3
            StartStoryBranch();
            SetRepercusionToBranch("mayor_relaxed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Todo ha sido un malentendido! Me había hecho la idea equivocada de la situación.",
                "Alguien ha hecho múltiples pintadas en la fachada de mi casa, y no son pequeñas precisamente.",
                "Normalmente condenaría tal acto vandálico, pero he captado el mensaje del perpetrador en ellas.",
                "La gente del pueblo no necesita grandes estatuas de mi persona, ni arbustos nuevos en los parques.",
                "Y lo que más me duele, ¡tampoco necesitan un puerto mercante en la Charca!",
                "¡Jamás abandonaré mi proyecto de construir un bullicioso puerto, aunque sea para muy largo plazo!",
                "Pero aun así no me queda más remedio que destinar fondos a otros asuntos más importantes."
            }, Tag.Help, 3, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿El puerto de la charca? ¡¿El alcalde piensa hacer que los barcos lleguen por tierra o qué?!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Pienso que sería más plausible construir máquinas voladoras antes que un puerto en un lugar como ese." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("mayor_ignore");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Parece ser que me estaba preocupando demasiado por el descontento de la gente.",
                "He recibido una carta anónima esta mañana diciendo maravillas sobre cómo ha quedado la plaza.",
                "¡Cuánta razón tiene, esa estatua mía le queda de maravilla! Sabía que quedaría bien.",
                "Estamos lejos de estar en nuestro mejor momento, pero la cosa va mejorando poco a poco.",
                "¡El día en que lleguen barcos mercantes al puerto, la prosperidad llegará a la ciudad, ya lo verán!"
            }, Tag.Convince, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Ese alcalde no sabe hacer sufrir a su pueblo en condiciones! ¡Debería de asesorarle para que lo haga mejor!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "No es raro que un gobernante pierda el sentido común con el tiempo. Aunque este caso es un tanto exagerado." });


            AddStorySelectionUIData("Preocupaciones mayores");
            FinishCreatingStory();

            // =============================================================================================
            //STORY 15
            // =============================================================================================

            StartCreatingStory("old_friends", "Viejos amigos", "meri",
            "Los lobos y las ratas inteligentes han vuelto para hacer estragos en la granja de Meri. Por suerte, las ratas defienden la granja de los lobos.", new List<string>() {
                "¿Recuerdas a las ratas inteligentes con las que estuvo lidiando el Mantecas? ¡Han vuelto otra vez!",
                "¡Y no han escogido otro lugar que mi granja como su nuevo hogar! ¡Qué espanto!",
                "¡Pero la cosa no termina ahí! ¿Recuerdas a los lobos que nos dieron problemas hace un tiempo?",
                "¡Pues adivina! ¡También han decidido volver para acosar a mis pobres vacas!",
                "Al menos las ratas, siendo tan territoriales, están defendiendo la granja de los lobos, no todo es malo.",
                "Obviamente quiero que dejen mi granja en paz, ¡¿Pero qué puedo hacer ante este caos?!"
            });

            CreateRepercusion("wolves_victorious", "Wolves victorious", -10);
            AddStoryRepercusionNewspaperArticle("El baluarte de los roedores cae.",
                "La colonia de ratas inteligentes ha abandonado la granja de una ganadera local después de capitular ante la manada de lobos. ¿Qué ocurrirá con la granja? ¿Será ahora ocupada por los lobos?");

            CreateRepercusion("rats_victorious", "Rats Victorious", 10);
            AddStoryRepercusionNewspaperArticle("El asedio a los roedores termina.",
                "La colonia de ratas inteligentes, que ha invadido la granja de una ganadera local, ha logrado de manera audaz repeler a la manada de lobos que amenazaba la granja. La impresionante hazaña ha dejado perplejos a los vecinos. ¿Estaremos ante el alzamiento de una nueva urbe de ratas? Por el bien de la desafortunada ganadera, esperemos que no.");

            CreateRepercusion("rats_mounting_wolves", "Rats Mounting Wolves", 20);
            AddStoryRepercusionNewspaperArticle("El galope de los jinetes roedores.",
                "Una inusual imagen quedará grabada en la mente de los vecinos. La colonia de ratas, que invadía la granja de una ganadera local, ha abandonado el pueblo a lomos de la manada de lobos que hasta hace poco se enfrentaba. ¿Podrían los lobos cabalgar sobre las ratas? ¡La ciencia afirma que no!");


            //TARGET: RATS
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("wolves_victorious");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "No entiendo cómo ha podido ocurrir, pero desde anoche no he visto muchas ratas por mi granja.",
                "Dudo bastante que hayan sido los lobos, pero al menos será más fácil echar a las que quedan.",
                "Los lobos siguen siendo un problema. Las ratas ya no serán de ayuda, ¡pero defenderé mi granja!"
            }, Tag.Harm, 1, "rats");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Me cuesta mucho decidirme por un bando ganador! ¿No podría haber alguna forma de que ganen ambos lados?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "En este conflicto solo veo un claro perdedor. La pobre ganadera y sus vacas." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("rats_victorious");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "La situación ha mejorado en parte, al menos por parte de los lobos, que ya no son mucho problema.",
                "Había oído del Mantecas las hazañas de las ratas inteligentes, pero no pensaba que fueran más allá.",
                "Por lo visto han construido una cerca reforzada alrededor de mi granja para repeler a los lobos.",
                "Ahora puedo ocuparme de las ratas sin que los lobos se coman a mis vacas, que ya es algo."
            }, Tag.Help, 1, "rats");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Alguien debería de enseñarle a esas ratas cómo forjar y portar armas",
                "Aunque seguro que aprenderán ellas solas si les damos tiempo."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "No sería mala idea a estas alturas entablar relaciones diplomáticas con las ratas." });

            //HELP >=7
            StartStoryBranch();
            SetRepercusionToBranch("rats_mounting_wolves");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "La cosa se ha tranquilizado un poco en mi granja. Aunque no doy crédito por lo que ha ocurrido.",
                "¡De alguna manera, las ratas han conseguido domesticar a los lobos! ¡Los utilizan de montura y todo!",
                "Por lo menos ya han abandonado mi granja, supongo que ahora pueden ir a donde quieran."
            }, Tag.Help, 7, "rats");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Y si las ratas pudieran cabalgar sobre las vacas? ¡¿No sería genial?!",
                "¡Tengo que intentarlo un día de estos!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "¿Cómo resolvería yo esta situación? Intentaría que las ratas y los lobos pudieran convivir en paz.",
                "Aunque no creo que vaya a ser posible." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("wolves_victorious");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "De la noche a la mañana, las ratas han abandonado mi granja. No tengo ni idea del por qué",
                "Parece que han regresado al granero del Mantecas, el hombre no está muy contento al respecto.",
                "¿Cómo lo sé? ¡Se pueden oír sus gritos desde mi granja, y eso que vive en la otra punta del pueblo!",
                "Aún tengo que lidiar con los lobos. ¡No pienso permitir que se coman a mis vacas!"
            }, Tag.Convince, 1, "rats");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Me cuesta mucho decidirme por un bando ganador! ¿No podría haber alguna forma de que ganen ambos lados?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "En este conflicto solo veo un claro perdedor. La pobre ganadera y sus vacas." });

            //TARGET: WOLVES
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("rats_victorious");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Mi problema con los lobos y las ratas se ha solucionado en parte. Aunque ha ocurrido muy rápido.",
                "Parece ser que las ratas han logrado repeler a los lobos. Me alivia que ya no sean un problema pero… ",
                "He visto algunos cadáveres de lobo en los alrededores y… ",
                "Me da miedo pensar en lo que me podrían llegar a hacer las ratas si las hago enfadar.",
                "Voy a tener que pedir ayuda al Mantecas. Él ya tiene experiencia lidiando con esas ratas."
            }, Tag.Harm, 1, "wolves");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Alguien debería de enseñarle a esas ratas cómo forjar y portar armas",
                "Aunque seguro que aprenderán ellas solas si les damos tiempo."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "No sería mala idea a estas alturas entablar relaciones diplomáticas con las ratas." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("wolves_victorious");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Mi problema con los lobos y las ratas ha empeorado mucho.",
                "De alguna forma los lobos se han vuelto mucho más inteligentes ¡No doy crédito!",
                "¡Han excavado un túnel bajo la verja de mi granja y han campado a sus anchas dentro de mi recinto!",
                "Suerte que han atacado a las ratas y han ignorado a mis vacas. Pero son un problema muy serio.",
                "Ya no quedan muchas ratas, pero los lobos pueden entrar cuando quieran, voy a tener que pedir ayuda. "
            }, Tag.Help, 1, "wolves");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Me cuesta mucho decidirme por un bando ganador! ¿No podría haber alguna forma de que ganen ambos lados?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "En este conflicto solo veo un claro perdedor. La pobre ganadera y sus vacas." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("rats_victorious");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Los lobos han dejado de molestarme, aunque no sé por qué se han ido. Ahora solo quedan las ratas.",
                "Por lo visto han ido a atacar al Johnny mientras deambulaba por el bosque.",
                "¡Espera a que termine! El Johnny se encuentra perfectamente, bueno.. a su manera, ya sabes.",
                "Dicen que consiguió salvarse gracias a que estaba impregnado con el fuerte olor de sus mejunjes.",
                "Es un tipo bastante raro, y nos suele dar problemas con sus experimentos. Pero no es un mal tipo."
            }, Tag.Convince, 1, "wolves");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Alguien debería de enseñarle a esas ratas cómo forjar y portar armas",
                "Aunque seguro que aprenderán ellas solas si les damos tiempo."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "No sería mala idea a estas alturas entablar relaciones diplomáticas con las ratas." });

            AddStorySelectionUIData("Viejos amigos");
            FinishCreatingStory();

            // =============================================================================================
            //STORY 16
            // =============================================================================================

            StartCreatingStory("high_voltage_treatment", "Tratamiento de alto voltaje", "miss_chocolate",
            "Miss Chocolate necesita la energía de un rayo para sus experimentos. Necesita un artefacto de Canela para que sirva de pararrayos, pero esta se niega a prestárselo.", new List<string>() {
                "¿Los batidos de chocolate extraídos directamente de las vacas? No obtuve grandes resultados.",
                "Después de darle muchas vueltas, he decidido cambiar mi enfoque.",
                "¡Quiero comprobar si puedo transformar el chocolate mediocre que ya tengo en un chocolate puro!",
                "Tengo todo planeado, es un proceso complicado pero, resumiendo, necesito la energía de un rayo.",
                "Además estoy de suerte porque se avecina esta noche una tormenta de las grandes.",
                "Y Canela tiene un artefacto perfecto como pararrayos, pero se niega a prestármelo.",
                "Insiste mucho en que se podría romper. ¡Qué tontería, si atraer rayos es su función para empezar!"
            });

            CreateRepercusion("ms_chocolate_experiment_canceled", "Ms Chocolate Experiment Canceled", -10);
            AddStoryRepercusionNewspaperArticle("La gran decepción.",
                "El muy esperado experimento de Miss Chocolate la Bomb ha tenido que ser retrasado debido a condiciones imprevistas.");

            CreateRepercusion("ms_chocolate_experiment_success", "Ms Chocolate Experiment Success", 10);
            AddStoryRepercusionNewspaperArticle("El experimento chocolatero da sus frutos.",
                "El último experimento de la célebre Miss Chocolate la Bomb ha sido un relativo éxito. Todo gracias a la colaboración con la prestigiosa coleccionista Canela N Rama.");

            CreateRepercusion("ms_chocolate_great_success", "Ms Chocolate Great Success", 20);
            AddStoryRepercusionNewspaperArticle("La destilación del chocolate purificado.",
                "La célebre chocolatera local, Miss Chocolate la Bomb, ha logrado con su último experimento purificar veinte litros de chocolate líquido en un par de gotas de chocolate purificado. Lamentablemente, la chocolatera ha declarado que no tiene intenciones de seguir investigando el asunto.");

            //TARGET MS CHOCOLATE
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_experiment_canceled");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Anoche repentinamente empezaron a fallar varios de mis aparatos para el experimento.",
                "Supongo que no hice un mantenimiento adecuado. Estas cosas pasan, pero aun así me frustra.",
                "Por mucho que me duela, he tenido que cancelar el experimento. Tendré que esperar a la siguiente tormenta."
            }, Tag.Harm, 1, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Es un experimento estúpido que no llevará a ninguna parte, y lo sabes!",
                "Pero por si acaso, lo impediría por todos los medios. No sea que salga bien.",
                "No quiero arriesgarme a estar equivocada. ¡¿Qué problema hay con eso?!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Evith es alérgica a estar equivocada. Nada la hace sufrir más que admitir sus errores." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_experiment_success");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¿El artefacto? No esperaba que Canela fuera a prestármelo, estaba a punto de darme por vencida.",
                "Bueno, el artefacto me llegó a mi casa en una caja, así que voy a suponer que cambió de opinión.",
                "Eso sí, después de que le cayese un rayo se hizo añicos, Canela no va a estar muy contenta.",
                "Pero al final he obtenido resultados mucho menores de lo esperado. ¡Pero siguen siendo positivos!"
            }, Tag.Help, 1, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "A mí solo me gustan los experimentos violentos. Deberías de realizarlos algún día.",
                "¡Comprobar qué cosas pueden destrozar otras cosas es fascinante! ¡Nunca me canso de ellos!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Nunca sabrás si algo funciona hasta que no se ponga a prueba. Aunque pueda ser estúpido." });

            //HELP >=7
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_great_success");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Puedo declarar orgullosamente que el experimento ha sido todo un éxito!",
                "Canela me cedió un cachivache diferente, mucho más prometedor que el que quería utilizar.",
                "Lo raro es que no me lo haya dado en persona, simplemente lo dejó delante de mi casa.",
                "Pero bueno, ¡dejé todo listo y logré obtener los resultados que esperaba!",
                "¡Los veinte litros de chocolate mediocre se transformaron en dos gotas de chocolate puro!",
                "Claramente es muy poco para considerarse un método viable, ¡Pero he demostrado que es posible!"
            }, Tag.Help, 7, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡¿En qué momento dejar que le caiga un rayo a las cosas se le llama experimento?!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "El afán por hacer nuevos descubrimientos es una bendición y una maldición a la vez.",
                "Por muchos avances que hagas, nunca llegarás a estar satisfecho."});

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_experiment_canceled");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "El experimento va a tener que esperar. A pesar de que Canela envió el artefacto a mi casa.",
                "Pensaba que me lo daría en persona, pero bueno, el caso es que el artefacto resulta ser inservible.",
                "Resultó ser mucho más frágil de lo que había estimado, se lo he devuelto antes de venir aquí.",
                "Es curioso porque ella misma parece no acordarse de habérmelo dado. Estará liada con sus cosas."
            }, Tag.Convince, 1, "miss_chocolate");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Es un experimento estúpido que no llevará a ninguna parte, y lo sabes!",
                "Pero por si acaso, lo impediría por todos los medios. No sea que salga bien.",
                "No quiero arriesgarme a estar equivocada. ¡¿Qué problema hay con eso?!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Evith es alérgica a estar equivocada. Nada la hace sufrir más que admitir sus errores." });

            //TARGET CANELA
            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_experiment_canceled");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Lo del experimento… va a tener que posponerse indefinidamente.",
                "Parece ser que ayer alguien hizo un destrozo en la colección de Canela. ¡Estaba hecha una furia!",
                "Va a ser imposible que me ceda el artefacto. Tendré que buscar otra alternativa ¡Pero no me rendiré!"
            }, Tag.Harm, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Es un experimento estúpido que no llevará a ninguna parte, y lo sabes!",
                "Pero por si acaso, lo impediría por todos los medios. No sea que salga bien.",
                "No quiero arriesgarme a estar equivocada. ¡¿Qué problema hay con eso?!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Evith es alérgica a estar equivocada. Nada la hace sufrir más que admitir sus errores." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_experiment_success");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Después de insistir mucho, Canela por fin me prestó su artefacto. Pensaba que no cambiaría de opinión.",
                "Estaba muy contenta, por lo visto ha encontrado tirado cerca de su casa un artefacto valiosísimo.",
                "Me dijo que me quede con el artefacto, ya que lo va a sustituir por el nuevo que ha encontrado.",
                "Yo por mi parte obtuve los resultados que buscaba. No sale nada rentable, ¡pero he cumplido!"
            }, Tag.Help, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "A mí solo me gustan los experimentos violentos. Deberías de realizarlos algún día.",
                "¡Comprobar qué cosas pueden destrozar otras cosas es fascinante! ¡Nunca me canso de ellos!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Nunca sabrás si algo funciona hasta que no se ponga a prueba. Aunque pueda ser estúpido." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("ms_chocolate_experiment_success");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Canela me cedió el artefacto inmediatamente después de pedírselo de nuevo.",
                "De hecho dice que me lo quede, por lo visto el artefacto realmente es una baratija sin valor.",
                "Me da un poco de pena, últimamente no está teniendo suerte con su colección.",
                "¿El experimento? Pude llevarlo a cabo, aunque todavía me falta mucho para considerarlo un éxito."
            }, Tag.Convince, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "A mí solo me gustan los experimentos violentos. Deberías de realizarlos algún día.",
                "¡Comprobar qué cosas pueden destrozar otras cosas es fascinante! ¡Nunca me canso de ellos!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Nunca sabrás si algo funciona hasta que no se ponga a prueba. Aunque pueda ser estúpido." });


            AddStorySelectionUIData("Tratamiento de alto voltaje");
            FinishCreatingStory();

            // =============================================================================================
            //STORY 17
            // =============================================================================================

            StartCreatingStory("full_artifact_panic", "Pánico de antiguallas", "canela",
            "Varios artefactos de Canela cobran vida tras haberlos rociado con un abrillantador especial. Los artefactos vivientes son muy agresivos y están aislados.", new List<string>() {
                "¡Es la última vez que pido ayuda a Johnny! ¡¿Cómo se las apaña ese hombre para hacer todo mal?!",
                "El otro día le compré un producto para limpiar mis artefactos y dejarlos relucientes, algo normal.",
                "Pero en el momento que rocié el producto sobre uno de mis artefactos, ¡empezó a moverse!",
                "¡No estoy de broma cuando digo que parte de mis artefactos tienen vida propia! ¡Y son agresivos!",
                "Por suerte, los artefactos solo cobran vida durante la noche, me he encargado de encerrarlos en mi despensa.",
                "Si no encuentro pronto una solución, voy a tener que deshacerme de ellos."
            });

            CreateRepercusion("living_artifacts_destroyed", "Living Artifacts Destroyed", -10);
            AddStoryRepercusionNewspaperArticle("La noche de los artefactos vivientes.",
                "La colección de artefactos de la prestigiosa Canela N Rama ha sido dañada en muy extrañas circunstancias. La coleccionista ha declarado que sus propios artefactos cobraron vida durante la noche. ¿Quién hubiera pensado que los artefactos tendrían tan mal genio?");

            CreateRepercusion("living_artifacts_stopped", "Living Artifacts Stopped", 10);
            AddStoryRepercusionNewspaperArticle("La epidemia de los artefactos vivientes termina.",
                "La prestigiosa coleccionista Canela N Rama declara haber resuelto la inusual situación de los artefactos vivientes. Durante la entrevista, nos dió útiles consejos de qué hacer en este tipo de casos. ¡Se lo contaremos a continuación!");

            CreateRepercusion("living_artifacts_servants", "Living Artifacts Servants", 20);
            AddStoryRepercusionNewspaperArticle("Los nuevos ayudantes de Canela N Rama.",
                "La prestigiosa coleccionista Canela N Rama nos ha sorprendido a todos con su nueva mano de obra. Se trata nada más y nada menos que de artefactos vivientes. Estos se encargan de mantener y de proteger al resto de la colección, declaró la coleccionista.");

            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("living_artifacts_destroyed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Los artefactos vivientes ya no son un problema, ¡pero aun así me duelen mucho las pérdidas!",
                "Esta mañana cuando he ido a revisarlos los he encontrado completamente destruidos.",
                "Espero que sea un efecto del mejunje, ¡porque no quiero tener ahora vándalos entrando y saliendo de mi casa!"
            }, Tag.Harm, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Si en vez de artefactos fueran platos y cubiertos, serían amables o mucho más agresivos?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Aunque vivas una eternidad, siempre habrá más cosas y situaciones sorprendentes." });

            //HARM >=7
            StartStoryBranch();
            SetRepercusionToBranch("living_artifacts_destroyed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Mi colección se ha ido al traste! ¡No sé si podré recuperarme de esta tragedia!",
                "¡De alguna manera los artefactos aislados han derribado la puerta y han sembrado el caos!",
                "Es culpa mía haberlos subestimado. No he tenido más remedio que destruirlos allí mismo.",
                "¡Cualquier día de estos voy a momificar vivo al besugo Johnny para exponerlo!"
            }, Tag.Harm, 7, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Si en vez de artefactos fueran platos y cubiertos, serían amables o mucho más agresivos?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Aunque vivas una eternidad, siempre habrá más cosas y situaciones sorprendentes." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("living_artifacts_stopped");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Mi colección se encuentra a salvo, aunque todavía sigo tomando precauciones por si las moscas.",
                "Ayer durante la noche noté cómo los artefactos vivientes dejaron de hacer ruido de repente.",
                "Cuando entré en la habitación a ver qué había ocurrido me di cuenta de a qué se debía.",
                "Por lo visto, los artefactos han sido rociados con lejía mientras se movían por la habitación.",
                "No esperaba que la lejía fuera a ser su punto débil, aunque a estas alturas ya poco me sorprende.",
                "Lo malo es que los artefactos han perdido su brillo en el proceso, pero al menos se ha terminado."
            }, Tag.Help, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Artefactos inanimados súper agresivos? ¡¿Por qué no se me había ocurrido antes?!",
                "¡Es una idea genial para sembrar el caos y el desconcierto, mis dos cosas favoritas!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "El orden natural de las cosas ha de ser restaurado. Lo que permanece inanimado debe quedar inanimado." });

            //HELP >= 5
            StartStoryBranch();
            SetRepercusionToBranch("living_artifacts_stopped");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Mi colección por fin está fuera de peligro! Los artefactos vivientes ahora son sólo artefactos.",
                "Por lo visto los artefactos dejaron de moverse tras entrar en contacto con chocolate.",
                "¡No me preguntes por qué, yo tampoco tengo ni idea! Habrá sido una completa casualidad.",
                "¡Por lo menos mis artefactos ahora tienen un brillo espectacular!"
            }, Tag.Help, 5, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿Artefactos inanimados súper agresivos? ¡¿Por qué no se me había ocurrido antes?!",
                "¡Es una idea genial para sembrar el caos y el desconcierto, mis dos cosas favoritas!" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "El orden natural de las cosas ha de ser restaurado. Lo que permanece inanimado debe quedar inanimado." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("living_artifacts_servants");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Anoche ocurrió algo más inusual si cabe! ¡Los artefactos vivientes se han vuelto inofensivos!",
                "En mitad de la noche dejaron de aporrear la puerta y empezaron a limpiar el destrozo de mi despensa.",
                "¡Todavía no doy crédito! No entiendo el cambio tan repentino, pero estoy pensando en convertirlos en  sirvientes.",
                "¡Es lo mínimo por haberme causado tanto estrés, pienso yo! ¡Pero me alegro de que haya terminado!"
            }, Tag.Convince, 1, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Los artefactos serían unos sirvientes muy aburridos. ¡Nada supera a las Galletas mágicas!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Por el bien de mantener el orden en el mundo. Ese abrillantador debería de ser destruido." });

            //CONVINCE >=7
            StartStoryBranch();
            SetRepercusionToBranch("living_artifacts_servants");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¿Los artefactos vivientes? Aún cobran vida durante la noche, aunque ya no son un problema",
                "¡De repente se han vuelto súper amistosos conmigo y se han puesto a limpiar toda mi casa!",
                "Además limpian y abrillantan a los demás artefactos de mi colección. ¡Están mejor que nunca!",
                "Definitivamente los voy a convertir en sirvientes. Después de este calvario no pienso dejar que acabe en nada.",
                "Quizás me pueda convertir en coleccionista de artefactos vivientes. ¡Sería aún más famosa por ello!"
            }, Tag.Convince, 7, "canela");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Los artefactos serían unos sirvientes muy aburridos. ¡Nada supera a las Galletas mágicas!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Por el bien de mantener el orden en el mundo. Ese abrillantador debería de ser destruido." });


            AddStorySelectionUIData("Pánico de antiguallas");
            FinishCreatingStory();

            // =============================================================================================
            //STORY 18
            // =============================================================================================

            StartCreatingStory("fungal_metamorphosis", "Metaforfosis Fúngica", "johny_setas",
            "El cuerpo de Johnny está sufriendo una preocupante transformación tras sus cada vez más intensas sesiones de meditación. Quizá haya que hacer algo al respecto.", new List<string>() {
                "Últimamente me están pasando cosas muy raras en plan, cosas mazo chungas, colega.",
                "Después de meditar, me doy cuenta de que me salen raíces y me crecen bultos en la piel, colega.",
                "Pienso que debe de tratarse de que me estoy acercando al trance más profundo, el Hipnos, colega.",
                "¿Te había hablado antes del trance? ¡Es un estado superior de la consciencia, colega!",
                "Tan solo tienes que meditar como yo y ser uno con los hongos. ¡Cuanto más tiempo mejor, colega!",
                "Lo malo es que poco a poco tu cuerpo se transforma irreversiblemente, ¡pero vale la pena, colega!",
                "¡Llevaba mucho esperando este momento! ¡Esta noche va a ser flipante, colega!"
            });

            CreateRepercusion("johnny_trance_interruptded", "Johnny Trance Interrupted", -10);
            AddStoryRepercusionNewspaperArticle("La extraña enfermedad del alquimista",
                "Los vecinos se encuentran preocupados por la salud de un alquimista local. El joven presenta extraños bultos en partes de su cuerpo. Afortunadamente busca atención médica.");

            CreateRepercusion("johnny_transformed", "Johnny Transformed", -20);
            AddStoryRepercusionNewspaperArticle("El abominable hombre seta.",
                "Los vecinos se encuentran consternados ante la preocupante apariencia de un alquimista local. El joven se encuentra cubierto de hongos y enredaderas que salen de su cuerpo.");

            CreateRepercusion("johnny_trance_achieved", "Johnny Trance Achieved", 20);
            AddStoryRepercusionNewspaperArticle("El retorno del alquimista.",
                "El joven alquimista cuyo aspecto preocupaba a los vecinos ha regresado con un aspecto mucho más saludable. ¿Cuál será su secreto?");

            CreateRepercusion("johnny_trance_convinced", "Johnny Trance Convinced", 10);
            AddStoryRepercusionNewspaperArticle("La nueva afición del alquimista.",
                "El joven alquimista, cuyo aspecto preocupaba a los vecinos, ha afirmado que va a dedicarse a la venta de galletas de manera paralela a su negocio habitual.");

            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("johnny_trance_interruptded");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Tíiio, voy a dejar esto de la meditación de lado de momento! ¡No veas como me duele todo, colega!",
                "Ayer, en medio de mi meditación profunda, empecé a sentir dolores punzantes en el cuerpo, colega.",
                "Supongo que será cosa de los bultos y las raíces que brotan de mi piel, ¡pero de normal no duele tanto, colega!"
            }, Tag.Harm, 1, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Ese tal Johnny parece un tipo aburrido. Pero tengo curiosidad de ver hasta donde llega."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Es una necedad poner en peligro tu salud para lograr un objetivo." });

            //HARM >=5
            StartStoryBranch();
            SetRepercusionToBranch("johnny_trance_interruptded");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Tíiio, he estado a nada de entrar en el trance más profundo! ¡Me he quedado a las puertas, colega!",
                "Justo cuando iba a dejar todo preparado, ¡Pum! Se cae parte del techo de mi casa encima, colega!",
                "He salido ileso, pero casi todo mi material se ha echado a perder. ¡Qué mala suerte tengo, colega!",
                "No podré reponer mi material hasta el próximo invierno. Así que hasta el año que viene nada, colega."
            }, Tag.Harm, 5, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "Ese tal Johnny parece un tipo aburrido. Pero tengo curiosidad de ver hasta donde llega."});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Es una necedad poner en peligro tu salud para lograr un objetivo." });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("johnny_transformed");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Tío, no veas que rayada! ¡Ayer logré alcanzar el Hipnos! ¡Pero fué solo por un momento, colega!",
                "Lo más raro fué que cuando salí del trance no podía moverme, ¡estaba enraizado en el sitio, colega!",
                "Además, de mis bultos en la espalda han brotado setas. Me pregunto de qué tipo serán, colega.",
                "Por algún motivo las raíces se marchitaron poco antes de amanecer, Si no, ¡todavía estaría allí plantado, colega!"
            }, Tag.Help, 1, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¡Si un ser cubierto de hongos se me acerca, le prenderé fuego! ¡Me dan asco las setas!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Espero que el joven alquimista pueda recapacitar a tiempo." });

            //HELP >=7
            StartStoryBranch();
            SetRepercusionToBranch("johnny_trance_achieved");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Tíiiiiio, lo he alcanzado! ¡He alcanzado el Hipnos! ¡Fué una sensación indescriptible, colega!",
                "¡Lo he visto todo, colega, toooooodo! ¡Tengo acceso a conocimientos que nadie comprende, colega!",
                "¿Los bultos y las raíces? Pues, diría que han desaparecido, así por completo, colega.",
                "Ayer encontré en casa un Champiñón Solar, normalmente no me molesto en recolectarlos, colega.",
                "Para cambiar un poco, lo utilicé para mi polvo especial. No sabía que servía para contrarrestar la transformación, colega."
            }, Tag.Help, 7, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "No entiendo nada de todo esto. ¡Que alguien me explique por qué es tan importante!"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Las consecuencias de entrar en lo más profundo del trance varía de persona en persona." });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("johnny_trance_convinced");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Tío, creo que voy a abandonar la búsqueda del Hipnos. No creo que sea lo mío, colega.",
                "Me lo dijo el Señor Galleta en una visión. Dijo que debería de preocuparme por los bultos y las raíces, colega.",
                "¿El Señor Galleta? A veces he notado su presencia, ¡pero esta vez me habló directamente, colega!",
                "Seguramente no entiendas nada de nada, pero se dice que el Señor Galleta rige el destino del mundo, colega."
            }, Tag.Convince, 1, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿No tendrá algo mejor que hacer ese hombre que dedicarse a dormir profundamente?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Muy pocos seres han alcanzado el Hipnos. Quizás ese joven pueda alcanzarlo con el apoyo suficiente" });

            //CONVINCE >=7
            StartStoryBranch();
            SetRepercusionToBranch("johnny_trance_convinced");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                "Anoche, mientras entraba en trance, tuve una visión que no me esperaba, colega.",
                "El legendario Señor Galleta vino a visitarme. Ya percibía su presencia, ¡pero ayer lo ví claramente, colega!",
                "No recuerdo los detalles, pero sé que ahora tengo otra meta en este plano terrenal, colega.",
                "¡Lo tengo clarinete! ¡Quiero dedicarme a hacer galletas! Y no de cualquier tipo, colega",
                "Quiero que contengan todo mi conocimiento sobre los hongos. ¡Será una pasada, colega!",
                "Quizá así algún día me vuelva a visitar el Señor Galleta, ¡o quizás el Dios Galleta, colega!"
            }, Tag.Convince, 7, "johny_setas");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                "¿No tendrá algo mejor que hacer ese hombre que dedicarse a dormir profundamente?"});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "Muy pocos seres han alcanzado el Hipnos. Quizás ese joven pueda alcanzarlo con el apoyo suficiente" });

            AddStorySelectionUIData("Metamorfosis Fúngica");
            FinishCreatingStory();

            /*
            // =============================================================================================
            //STORY X
            // =============================================================================================

            StartCreatingStory("quest_id", "quest name", "giver",
            "desc", new List<string>() {
                "Tal tal tal"
            });

            CreateRepercusion("", "", -15);
            AddStoryRepercusionNewspaperArticle("",
                "");

            CreateRepercusion("", "", -30);
            AddStoryRepercusionNewspaperArticle("",
                "");

            CreateRepercusion("", "", 15);
            AddStoryRepercusionNewspaperArticle("",
                "");

            //HARM >=1
            StartStoryBranch();
            SetRepercusionToBranch("");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                ""
            }, Tag.Harm, 1, "");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                ""});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "" });

            //HELP >=1
            StartStoryBranch();
            SetRepercusionToBranch("");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                ""
            }, Tag.Help, 1, "");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                ""});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "" });

            //CONVINCE >=1
            StartStoryBranch();
            SetRepercusionToBranch("");
            AddBranchCompletion_NPCDialogue(new List<string>() {
                ""
            }, Tag.Convince, 1, "");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() {
                ""});
            AddBranchCompletion_NuDeityDialogue(new List<string>() {
                "" });


            AddStorySelectionUIData("Story title");
            FinishCreatingStory();
            */

            SecondaryStories();

            void SecondaryStories()
            {
                CreateRepercusion("default_negative", "Default Negative", -100);
                CreateRepercusion("default_positive", "Default Positive", 100);

                ColectaFloral();
                void ColectaFloral()
                {
                    StartCreatingStory("floral_collect", "Colecta Floral", null,
                    "Hay quienes afirman que el pueblo debería de estar mejor decorado con flores. Quizás se pueda hacer algo al respecto", new List<string>() {
                "El pueblo se ve un poco soso con esos arbustos feos que hay plantados. ¡Creo que le vendría bien un poco de color!",
                "Me parece que hay un campo de flores al oeste del pueblo. Ojalá nos pongamos todos de acuerdo en usar esas flores para darle vida al pueblo."
               });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Parece que la idea de decorar el pueblo con flores no ha gustado demasiado. ¡El campo de flores está destrozado!",
                "No lo entiendo, con lo bonitas que son. No sé quién haría algo así. Y para colmo, el pueblo seguirá luciendo aburrido."
            }, Tag.Harm, 1, "mayor");

                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡No me lo puedo creer! ¡El pueblo ha amanecido más bello que nunca! Al parecer, alguien se ha dedicado a plantar las flores durante la noche.",
                "Se me hace un poco raro, pero quien quiera que sea, se preocupa mucho por el pueblo."
            }, Tag.Help, 1, "mayor");

                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Todos han decidido recoger flores para mejorar la imagen del pueblo! Sin duda, ésto atraerá a más turistas y la economía mejorará."
            }, Tag.Convince, 1, "mayor");
                    AddStorySelectionUIData("Colecta Floral");
                    FinishCreatingStory();
                }

                NosQuedamosSinPan();
                void NosQuedamosSinPan()
                {
                    StartCreatingStory("without_bread", "¿Nos quedamos sin pan?", null,
                "El único panadero del pueblo ha vuelto a enfermar una vez más. Quizás alguien debería de ocupar su puesto temporalmente.", new List<string>() {
                "¿Te has enterado? El panadero del pueblo ha caído muy enfermo otra vez y no abrirá en un tiempo. Es una pena, ojalá se recupere.",
                "Y como no hay más panaderos por aquí, no tendremos más remedio que comprar el pan fuera del pueblo."
                });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Pero quién habrá podido ser! El pobre panadero tiene muy mala suerte. Han destrozado su panadería en la noche.",
                "Además de enfermo, ¡ahora estará arruinado! Ojalá pillen al responsable de esto."
            }, Tag.Harm, 1, "mayor");

                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡No te lo vas a creer! Han aparecido cajas con bastantes barras de pan en todo el pueblo.",
                "Parece que alguien de fuera se ha enterado y ha decidido echarnos una mano mandándonos provisiones de pan mientras el panadero se recupera.",
                "¡Aún hay gente buena en el mundo!"
            }, Tag.Help, 1, "mayor");

                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Al parecer, algunas personas han decidido aprender el oficio del panadero para ayudarle en su negocio. ¡Qué gente más considerada!",
                "Puede que la panadería abra antes de lo esperado, ¡o incluso puede que se abran más!"
            }, Tag.Convince, 1, "mayor");


                    AddStorySelectionUIData("¿Nos quedamos sin pan?");
                    FinishCreatingStory();
                }

                PastelesPorDoquier();
                void PastelesPorDoquier()
                {
                    StartCreatingStory("cupcakes_everywhere", "Pasteles por doquier", null,
                "Es una buena costumbre regalar pasteles a los vecinos de vez en cuando. Aunque quizás puedas contribuir de forma diferente.", new List<string>() {
                "¿Te gustan los pasteles? Está claro que sí. Por algo tienes una pastelería.",
                "Como regalo, los pasteles alegran a cualquiera y no está de más darle uno a alguien como muestra de aprecio.",
                "Además, ¡son deliciosos! Deberías hacer pasteles más a menudo."
                });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Creo que han tirado trozos de pastel a la fachada de una granja.",
                "No creo que le haga mucha gracia al dueño, sobre todo sabiendo que luego tendrá que lidiar con las criaturas que vengan por el olor."
            }, Tag.Harm, 1, "meri");

                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "¿Lo sabías? Han aparecido tartas envueltas con un moño en las puertas de las casas del pueblo.",
                "Parece que alguien le tiene mucho aprecio a toda la gente de aquí.",
                "¡No veas qué alegría me he llevado tras ver una al abrir la puerta!"
            }, Tag.Help, 1, "meri");

                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Me he enterado que pronto van a organizar un concurso de pasteles en la plaza del pueblo.",
                "¡Vendrán también pasteleros de las ciudades vecinas! Además, tras acabar el concurso, ¡invitarán a todos a tarta! No va a quedar ni uno."
            }, Tag.Convince, 1, "meri");


                    AddStorySelectionUIData("Pasteles por doquier");
                    FinishCreatingStory();
                }

                CampanaOlvidada();
                void CampanaOlvidada()
                {
                    StartCreatingStory("forgotten_bell", "Campana Olvidada", null,
                "La campana del ayuntamiento está en malas condiciones. Los vecinos echan de menos su sonido. Se podría hacer algo al respecto.", new List<string>() {
                "¿Sabías que tenemos una campana en el ayuntamiento? Ya está un poco vieja y oxidada, no se le da uso desde hace años.",
                "Tampoco se han molestado en mantenerla en buen estado.",
                "Estaría bien que volvieran a hacerla sonar, le puede dar un toque acogedor al pueblo."
                });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Me he enterado que la vieja campana del ayuntamiento ha amanecido hoy hecha una pena.",
                "Alguien la ha pintarrajeado y le han abierto más grietas.",
                "Creo que han decidido sacarla y tirarla a la basura. Creía que al menos iban a guardarla como recuerdo."
            }, Tag.Harm, 1, "mayor");

                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Algún amante de las campanas ha decidido restaurar por completo la vieja campana del ayuntamiento.",
                "¡Brilla más que cuando la compraron de segunda mano!",
                "Además, suena mucho mejor cuando la probaron. Creo que van a empezar a usarla de nuevo."
            }, Tag.Help, 1, "mayor");

                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Han anunciado que la vieja campana del ayuntamiento volverá a sonar.",
                "Lo hará para dar comienzo a las ceremonias o eventos que haya en el pueblo.",
                "¡Qué alivio! Antes sonaba cada dos por tres y despertaba a todo el mundo de la siesta."
            }, Tag.Convince, 1, "mayor");


                    AddStorySelectionUIData("Campana Olvidada");
                    FinishCreatingStory();
                }

                LaBestia();
                void LaBestia()
                {
                    StartCreatingStory("the_beast", "La Bestia", null,
                "Se rumorea que un monstruo potencialmente peligroso merodea en las afueras del pueblo. Nadie parece capacitado para lidiar con él. ¿O sí?", new List<string>() {
                "Últimamente se oyen unos rugidos en las afueras del pueblo, sobre todo durante la noche.",
                "Creo que hay algún tipo de monstruo acechando cerca, pero creo que se siente atraído por las granjas, ya que se oye más fuerte desde esos lugares.",
                "Me preocupa que pueda hacer daño a la gente o a sus animales."
                });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡El monstruo que rugía en las noches resultó ser un lobo! Solo que era bastante más grande de lo normal.",
                "Alguien ha decidido hacerle frente y de alguna manera lo ha aniquilado.",
                "Es un alivio saber que hay una amenaza menos."
            }, Tag.Harm, 1, "wolves");

                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡Ha ocurrido una tragedia! El monstruo ha empezado a atacar a los animales de las granjas.",
                "No sabemos cómo ha podido sortear las robustas verjas de las granjas, alguien ha tenido que sabotearlas.",
                "No se me ocurre otra explicación."
            }, Tag.Help, 1, "wolves");

                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "¡No te lo vas a creer! El monstruo era en realidad un lobo muy grande.",
                "Lo sé porque esta mañana apareció en medio de la plaza, pero estaba muy tranquilo y, al parecer, sin ninguna intención de atacar a nadie.",
                "La gente, tras debatirlo mucho, ha decidido adoptarlo y ahora es la mascota del pueblo. ¡Quién lo iba a decir!"
            }, Tag.Convince, 1, "wolves");


                    AddStorySelectionUIData("La Bestia");
                    FinishCreatingStory();
                }

                HayQueSalvarElTrigo();
                void HayQueSalvarElTrigo()
                {
                    StartCreatingStory("saving_the_wheat", "Hay Que Salvar El Trigo", null,
            "Las plagas asolan los campos de cereal nuevamente. Alguien debería de hacer algo al respecto.", new List<string>() {
                "¡La cosecha de cereales está en peligro! Las plagas este año están más problemáticas que de costumbre.",
                "Si no hacemos algo pronto, ¡no habrá cereales y las pérdidas serán cuantiosas!"
            });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Lo que me temía. Las plagas han destrozado los cultivos de los cereales de la noche a la mañana.",
                "Este año habrá que comprarlas de otro sitio, pero no serán baratas."
            }, Tag.Harm, 1, "wolves");

                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Por alguna razón, están dejando de aparecer los plagas que se estaban comiendo los cereales.",
                "¡Es un gran golpe de suerte! Ojalá que ésto siga así."
            }, Tag.Help, 1, "wolves");

                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>() {
                "Se ha decidido empezar a cosechar hoy mismo los cereales, aunque no todos estén del todo maduros.",
                "¡Mejor prevenir que quedarse sin cereales!"
            }, Tag.Convince, 1, "wolves");
                    AddStorySelectionUIData("Hay Que Salvar El Trigo");
                    FinishCreatingStory();
                }

                HuelgaTrabajadores();
                void HuelgaTrabajadores()
                {
                    StartCreatingStory("workers_strike", "Trabajadores en Huelga", null,
                        "Hay un grupo de trabajadores en huelga, hay que encontrar la manera de resolverlo.", new List<string>() {
                            "Hay un grupo de trabajadores en huelga que se están manifestando en la plaza y solicitan al ayuntamiento mejores condiciones.",
                            "¡Si no se soluciona el problema rápido, la cosecha se retrasará y se echará a perder!"
                            });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Parece que alguien se ha enfrentado al alcalde por la noche y ha accedido a lo que solicitaban los trabajadores.",
                            "Ahora podrán volver al trabajo con más energia y la cosecha no se retrasará."
                            }, Tag.Harm, 1, "mayor");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Los trabajadores han abandonado la manifestación refunfuñando y han vuelto a los campos de trabajo malhumorados.",
                            "Parece que en el ayuntamiento han tenido ayuda para echarlos de la plaza."
                            }, Tag.Help, 1, "mayor");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Hoy por la mañana el ayuntamiento ha hablado con los trabajadores y ha accedido a lo que solicitaban.",
                            "Ahora podrán volver al trabajo con más energia y la cosecha no se retrasará."
                            }, Tag.Convince, 1, "mayor");
                    AddStorySelectionUIData("Trabajadores en Huelga");
                    FinishCreatingStory();
                }

                GolemBolas();
                void GolemBolas()
                {
                    StartCreatingStory("golem_attack", "El Incordio del Golem", null,
                        "Un golem está atormentando a los vecinos del pueblo, ¡hay que hacer algo!", new List<string>() {
                            "Ha aparecido un golem en la ciudad y está tirando bolas de barro a todo el mundo que pasa por delante.",
                            "No hace mucho daño pero empieza a ser molesto, así que ten cuidado si pasas por delante."
                            });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "¡Hoy por la mañana el golem estaba más enfadado y ahora también lanza piedras!",
                            "No se que habrá podido pasar para enfadarlo tanto."
                            }, Tag.Harm, 1, "golem");
                    //HARM >=3
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "He pasado por donde estaba el golem y no te lo vas a creer, ¡ya no estaba!",
                            "Ahora se puede andar por el pueblo sin preocupaciones, aunque me pregunto donde habrá ido."
                            }, Tag.Harm, 3, "golem");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "He pasado por donde estaba el golem y no te lo vas a creer, ¡ya no estaba!",
                            "Pero nos han llegado noticias del pueblo de al lado de que ha aparecido un golem, asi que ya sabemos donde ha ido."
                            }, Tag.Help, 1, "golem");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "He pasado por donde estaba el golem y no te lo vas a creer, ¡no me ha atacado!",
                            "Solo estaba ahí sentado admirando el pueblo, espero que no vuelva a atacar."
                            }, Tag.Convince, 1, "golem");
                    AddStorySelectionUIData("El Incordio del Golem");
                    FinishCreatingStory();
                }

                HarrahHarrah();
                void HarrahHarrah()
                {
                    StartCreatingStory("harrah_spiders", "Arañas Gigantes en el Pueblo", null,
                        "Hay arañas gigantes en el pueblo y están asustando a los ciudadanos.", new List<string>() {
                            "Anoche estaba dando un paseo alrededor del pueblo y se me acercaron unas arañas gigantes que ",
                            "hacian un ruido muy raro, 'harrah harrah'. Salí corriendo en dirección contraria, pasé mucho miedo."
                            });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Han aparecido arañas muertas por todo el pueblo, algo o alguien se ha encargado de ellas.",
                            "Ahora tendremos que limpiarlas, pero al menos no nos asustarán más."
                            }, Tag.Harm, 1, "spiders");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "¡Las arañas están atacando a los animales! Muerden a todo lo que ven al grito de 'harrah harrah'.",
                            "Deberias tener mucho cuidado si sales, no quiero imaginarme que pasará si nos muerden a alguno."
                            }, Tag.Help, 1, "spiders");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Anoche volví a ver a las arañas, se estaban yendo, pero, ¡gritaban que volverán con refuerzos!",
                            "Por ahora nos hemos librado de ellas, pero tengo miedo de que vuelvan.."
                            }, Tag.Convince, 1, "spiders");
                    AddStorySelectionUIData("Arañas Gigantes en el Pueblo");
                    FinishCreatingStory();
                }

                GatoArbol();
                void GatoArbol()
                {
                    StartCreatingStory("cat_tree", "Gato en el Árbol", null,
                        "Hay un gato atrapado en un árbol, ¡hay que ayudarlo!", new List<string>() {
                            "Un gato se asustó de unas arañas y se subió a un arbol, y ahora está atrapado y no puede bajar.",
                            "Parece que le da miedo saltar. ¡Alguien tiene que ayudarlo!"
                            });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Alguien tiró una piedra al gato y este se asustó y ha subido más el árbol. ¡Ahora es más complicado que baje!"
                            }, Tag.Harm, 1, "cat");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Alguien ayudó al gato a bajar y ahora está sano y salvo con su dueño. ¡Me alegro mucho por los dos!"
                            }, Tag.Help, 1, "cat");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Alguien convenció al gato para que saltara, ¡y lo hizo!",
                            "Me asusté cuando saltó pero cayó sano y salvo y ya está con su dueño."
                            }, Tag.Convince, 1, "cat");
                    AddStorySelectionUIData("Gato en el Árbol");
                    FinishCreatingStory();
                }

                FantasmaLago();
                void FantasmaLago()
                {
                    StartCreatingStory("lake_ghost", "El Fantasma del Lago", null,
                        "Se ha visto un ser en el lago. ¿Será un fantasma?", new List<string>() {
                            "No se si me vas a creer, pero, ¡anoche vi un fantasma en el lago!",
                            "Era borroso y translucido, pero sé lo que vi. ¿Estará relacionado con los rumores?"
                            });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Anoche vi el fantasma en la entrada del pueblo, no se que habrá pasado, pero nunca se habia acercado tanto."
                            }, Tag.Harm, 1, "ghost");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Estuve caminando por la zona del lago y se siente diferente, ya no me dan escalofríos cada vez que me acerco.",
                            "¿Que habrá cambiado?"
                            }, Tag.Help, 1, "ghost");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Estuve caminando por la zona del lago y vi una sombra alejarse, ¿sería el fantasma alejandose del lago?"
                            }, Tag.Convince, 1, "ghost");
                    AddStorySelectionUIData("El Fantasma del Lago");
                    FinishCreatingStory();
                }

                FantasmaLago2();
                void FantasmaLago2()
                {
                    StartCreatingStory("lake_ghost_2", "El Misterio del Lago", null,
                        "Hay rumores sobre algo que pasó en el lago. ¿Que pasaría?", new List<string>() {
                            "¿Has ido por la zona del lago? Si te fijas está muy descuidada. Como eres nuevo en el pueblo es probable ",
                            "que todavia no lo sepas, pero está tan descuidada porque poca gente se atreve a acercarse, ",
                            "ya que según se cuenta en el lago murió ahogado el antiguo alcalde, pero solo son rumores.",
                            "Se dice que las personas más influyentes de la ciudad saben qué pasó de verdad."
                            });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Ayer por la noche oí gritos y alborotos en el ayuntamiento. Quien estuviera dentro parecía muy asustado.",
                            "¿Que pasaría? ¿Habrá fantasmas en el ayuntamiento?"
                            }, Tag.Harm, 1, "mayor");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Anoche vi a alguien andar con mucha preocupación al lago. ¿Porqué querría alguien acercarse a ese lugar?"
                            }, Tag.Help, 1, "mayor");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "¿Has visto el tablón de anuncios? Ha aparecido una nota pidiendo perdón. ¿A que se referirá?"
                            }, Tag.Convince, 1, "mayor");
                    AddStorySelectionUIData("El Misterio del Lago");
                    FinishCreatingStory();
                }

                MonstruoMontana();
                void MonstruoMontana()
                {
                    StartCreatingStory("monster_mountain", "El Monstruo de la Montaña", null,
                        "Parece que hay un monstruo en la montaña que está sembrando el terror.", new List<string>() {
                            "¡HAY UN MONSTRUO EN LA MONTAÑA! ¡HAY UN MONSTRUO EN LA MONTAÑA! ¡HAY UN MONSTRUO EN LA MONTAÑA!",
                            "¡ALGUIEN TIENE QUE HACER ALGO!"
                            });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "¿Te has enterado? Ha habido un alud en la montaña, espero que todo el mundo esté bien."
                            }, Tag.Harm, 1, "monster");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Ha desaparecido comida del almacén, pero ya no está el monstruo de la montaña, ¿habrá sido él?"
                            }, Tag.Help, 1, "monster");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "No he visto al monstruo de la montaña, ¿se habrá ido?"
                            }, Tag.Convince, 1, "monster");
                    AddStorySelectionUIData("El Monstruo de la Montaña");
                    FinishCreatingStory();
                }

                DragonMontana();
                void DragonMontana()
                {
                    StartCreatingStory("dragon_mountain", "Un Dragón en la Cima", null,
                        "Se cree que hay un dragón en la cima de la montaña, ¿será verdad?", new List<string>() {
                            "¿Sabías que se cree que hay un dragón en lo alto de la montaña? Eso cuentan las historias, ",
                            "pero nadie ha subido tan alto para comprobarlo."
                            });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "¡UN DRAGÓN! ¡UN DRAGÓN! ¿Por qué hay un dragón sobrevolando el pueblo?"
                            }, Tag.Harm, 1, "dragon");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "Ayer por la noche decidí investigar lo del dragón y vi a unos seres diminutos curando al dragón.",
                            "Parece que su existencia era real pero no lo habíamos visto porque estaba herido. Cuando terminaron de curarle, el dragón se fue volando.",
                            "No sé que serían esos seres diminutos, pero después de ver a un dragón, me creo cualquier cosa."
                            }, Tag.Help, 1, "dragon");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                            "¡UN DRAGÓN! ¡UN DRAGÓN! ¿Por qué hay un dragon sobrevolando el pueblo? ¡Y los árboles de la montaña están en llamas!"
                            }, Tag.Convince, 1, "dragon");
                    AddStorySelectionUIData("Un Dragón en la Cima");
                    FinishCreatingStory();
                }

                SenorMisterioso();
                void SenorMisterioso()
                {
                    StartCreatingStory("mysterious_man", "El Hombre Extraño de la Plaza", null,
                       "Un señor con gabardina aparece todas las tardes en la plaza. Da muy mala espina...", new List<string>() {
                            "Mira que nuestro pueblo no suele recibir visitas... pero últimamente aparece todas las tardes un señor con gabardina ",
                            "con aire bastante sospechoso. No sabemos qué hace: simplemente se queda en la plaza, mirando fijamente a los transeúntes.",
                            "Algunos dicen que vende cosas extrañas... otros que es un demonio que nos vigila, ¡Qué locura!",
                            "Sea como fuere, no me da para nada buena espina, espero que se vaya pronto."
                           });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                        "Esta mañana, al pasar por la plaza, he visto huellas de barro donde se solía colocar ese señor tan misterioso.",
                        "Es como si hubiera huido despavorido por algo.",
                        "Lo más raro es que se le ha caído una foto, y en ella aparecían un señor mayor y un niño pequeño.",
                        "Pobre... ¿Estaría buscando a alguien?"
                        }, Tag.Harm, 1, "weirdMan");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                       "¿Te acuerdas del señor misterioso? pues resulta que es nieto del viejo Tiburcio, que en paz descanse.",
                       "Esta mañana lo han visto dirigiéndose al pequeño cementerio que está en el bosque, con unas flores.",
                       "Aunque sea de esta forma, me alegro de que se pueda reencontrar la familia."
                        }, Tag.Help, 1, "weirdMan");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                        "Hace un tiempo que no se ve al señor misterioso por ningún lado. Nadie del pueblo me ha dicho si hablaron con él o no.",
                        "De la noche a la mañana desapareció, sin dejar rastro... Me pregunto qué demonios estaría haciendo aquí."
                        }, Tag.Convince, 1, "weirdMan");
                    AddStorySelectionUIData("El Hombre Extraño de la Plaza");
                    FinishCreatingStory();
                }
                LadronFiero();
                void LadronFiero()
                {
                    StartCreatingStory("thievery", "El Ladrón Fiero", null,
                       "Un ladrón se está metiendo en las casas del pueblo a robar. ¡hay que hacer algo!", new List<string>() {
                            "Ultimamente la gente del pueblo está reportando robos de objetos preciados en sus casas...",
                            "¿Es que no tenemos ya suficiente con el día a día? Parece un ladrón bastante violento, pero por fortuna ningún vecino ",
                            "ha resultado herido. Ojalá alguien pudiera darle su merecido... sea el que sea."
                           });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                        "¡Parece que por fin alguien se ha armado del suficiente valor para enfrentarse a ese dichoso ladrón!",
                        "¡Y lo mejor de todo es que lo ha conseguido! No se sabe quién es, pero para todos los habitantes, es un héroe sin capa."
                        }, Tag.Harm, 1, "thief");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                        "Por fin se fue ese maldito ladrón, pero ha conseguido entrar en más casas.",
                        "No solo eso, sino que ha robado también muchas pertenencias pequeñas, por alguna razón que no entendemos.",
                        "Menos mal que no son tan valiosas, pero es un golpe duro para el pueblo."
                        }, Tag.Help, 1, "thief");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                        "Sigo sin creérmelo...",
                        "¿Quién en su sano juicio hablaría con ese ladrón para convencerlo de que devuelva las pertenencias?",
                        "Lo peor de todo es que ha funcionado. No me quejo, pero es, sin duda, impresionante."
                        }, Tag.Convince, 1, "thief");
                    AddStorySelectionUIData("El Ladrón Fiero");
                    FinishCreatingStory();
                }

                SenorMisterioso2();
                void SenorMisterioso2()
                {
                    StartCreatingStory("mysterious_man_2", "El Hombre Extraño del Lago", null,
                       "Una persona misteriosa merodea por el lago...", new List<string>() {
                            "¿Has visto la persona misteriosa que merodea por el lago? ¿Quién será?",
                            "¿Qué estará buscando? Me da mala espina, espero que se vaya pronto."
                           });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                        "Ayer andaba tranquilamente por las afueras del pueblo y oí alboroto en el lago, asi que me asomé y creo que me vieron, ",
                        "asi que salí corriendo hacia el centro del pueblo. Según huia escuché como si alguien se cayera al lago.",
                        "¿Qué habrá pasado? Ultimamente pasan cosas muy raras en el pueblo..."
                        }, Tag.Harm, 1, "weirdMan");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                       "¿Has visto al hombre misterioso? Al parecer era un periodista investigando los rumores del lago.",
                       "Por eso ha estado merodeando por la zona. Parece que ya ha recabado suficiente información para su historia, ",
                       "esta tarde se va del pueblo. Espero que hable bien de su gente."
                        }, Tag.Help, 1, "weirdMan");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                        "¿Te has enterado? El hombre misterioso que merodeaba por el lago ya se ha ido.",
                        "Espero que no vuelva, ese hombre me daba escalofrios."
                        }, Tag.Convince, 1, "weirdMan");
                    AddStorySelectionUIData("El Hombre Extraño del Lago");
                    FinishCreatingStory();
                }

                FestivalPrimavera();
                void FestivalPrimavera()
                {
                    StartCreatingStory("spring_festival", "El Festival de Primavera", null,
                       "El festival de primavera es mañana y los preparativos no estarán listos a tiempo.", new List<string>() {
                            "Mañana es el festival de primavera y con los recientes problemas del pueblo no hemos podido recoger suficientes flores.",
                            "¡El festival será un fracaso! ¡Seremos el hazmerreír de la comarca!"
                           });

                    //HARM >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_negative");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                        "No se como ha podido suceder, pero hoy por la mañana todas las flores estaban destrozadas, ¡no podremos celebrar el festival!",
                        "Los habitantes del pueblo están devastados."
                        }, Tag.Harm, 1, "canela");
                    //HELP >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                       "No se como ha sucedido pero hoy estaban todas las flores para el festival, ¡es el mejor festival que recuerdo!"
                        }, Tag.Help, 1, "canela");
                    //CONVINCE >=1
                    StartStoryBranch();
                    SetRepercusionToBranch("default_positive");
                    AddBranchCompletion_NPCDialogue(new List<string>(){
                        "Al final no se han completado todos los preparativos del festival, pero con lo que tenemos podremos celebrar el festival aunque sea pequeño.",
                        "No será el mejor festival de la comarca, pero después de todos los problemas del pueblo se agradece un dia de celebración."
                        }, Tag.Convince, 1, "canela");
                    AddStorySelectionUIData("El Festival de Primavera");
                    FinishCreatingStory();
                }

            }
        }

        #region Builder Methods

        private void StartCreatingStory(string idName, string title, string questGiver, string description, List<string> introductionDialogue)
        {
            m_StoryData = new StoryData();
            m_StoryData.m_ID = new ID(idName);

            m_StoryData.m_Title = title;
            m_StoryData.m_IntroductionDialogue = introductionDialogue;

            m_StoryData.m_QuestGiver = questGiver;
            m_StoryData.m_Description = description;
        }

        private void StartStoryBranch()
        {
            m_Branch = new BranchOption();
        }

        private void CreateRepercusion(string idName, string repName, int happinessValue)
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

        private void SetRepercusionToBranch(string idName)
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

        private void AddStoryRepercusionNewspaperArticle(string title, string body)
        {
            var newsArticle = new StoryRepNewspaperComponent();
            newsArticle.m_CharacterID = new ID(m_StoryData.m_QuestGiver);
            newsArticle.m_RepID = m_Repercusion.m_ID;
            newsArticle.m_Title = title;
            newsArticle.m_Body = body;
            m_RepercusionNewspaperArticles.Add(newsArticle);
        }

        private void AddBranchCompletion_NPCDialogue(List<string> npcResultDialogue, Tag tag, int tagValue, string target)
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

        private void AddBranchCompletion_EvithDeityDialogue(List<string> dialogue)
        {
            var evithDialogue = new BranchOption.DeitiesStoryDialogue();
            evithDialogue.m_DeityID = 1;
            evithDialogue.m_Dialogue = dialogue;
            m_Branch.m_DeitiesResultDialogue.Add(evithDialogue);
        }

        private void AddBranchCompletion_NuDeityDialogue(List<string> dialogue)
        {
            var evithDialogue = new BranchOption.DeitiesStoryDialogue();
            evithDialogue.m_DeityID = 0;
            evithDialogue.m_Dialogue = dialogue;
            m_Branch.m_DeitiesResultDialogue.Add(evithDialogue);
        }

        private void AddStorySelectionUIData(string title)
        {
            var s = new StoryUIDataComponent();
            s.m_Title = title;
            s.m_ParentStoryID = m_StoryData.m_ID;
            s.m_Sprite = _assetRefs.GetStorySelectionSprite(s.m_ParentStoryID);
            m_StoryUI.Add(s);
        }

        private void FinishCreatingStory()
        {
            m_StoryData.Build();

            m_Story = new StoryInfoComponent();
            m_Story.m_StoryData = m_StoryData;

            m_StoriesList.Add(m_Story);
        }

        #endregion
    }
}