using CQM.AssetReferences;
using CQM.Components;
using CQM.Databases;
using System;
using System.Collections.Generic;
using UnityEngine;
using PieceType = CQM.Components.QuestPieceFunctionalComponent.PieceType;
using Tag = CQM.Components.QPTag.TagType;


namespace CQM.DataBuilders
{
    [Serializable]
    public class PieceBuilder : BaseDataBuilder
    {
        [SerializeField] private CookieReferencesDatabase _cookieReferences;

        // Output Components
        [SerializeField] private List<QuestPieceFunctionalComponent> m_QuestPieceFunctionalComponents = new List<QuestPieceFunctionalComponent>();
        [SerializeField] private List<QuestPieceUIComponent> m_QuestPieceUIComponent = new List<QuestPieceUIComponent>();
        [SerializeField] private List<QuestPiecePrefabComponent> m_QuestPiecePrefabComponent = new List<QuestPiecePrefabComponent>();
        [SerializeField] private List<CookieDataComponent> m_CookieData = new List<CookieDataComponent>();
        [SerializeField] private List<RecipeDataComponent> m_RecipeData = new List<RecipeDataComponent>();

        public GameObject m_DefaultPiecePrefab;

        // Current Components Being Built
        private QuestPieceFunctionalComponent _functionalQP;
        private QuestPieceUIComponent _uiQP;
        private QuestPiecePrefabComponent _prefabQP;
        private CookieDataComponent _cookieData;
        private RecipeDataComponent _recipeData;


        public override void BuildData(ComponentsDatabase c)
        {
            var piecesList = m_QuestPieceUIComponent;
            for (int i = 0; i < piecesList.Count; i++)
            {
                var data = piecesList[i];
                c.m_QuestPieceUIComponent.Add(data.m_ID, data);
            }
            var functionalQuestPieces = m_QuestPieceFunctionalComponents;
            for (int i = 0; i < functionalQuestPieces.Count; i++)
            {
                var data = functionalQuestPieces[i];
                c.m_QuestPieceFunctionalComponents.Add(data.m_ID, data);
            }
            var prefabQuestPieces = m_QuestPiecePrefabComponent;
            for (int i = 0; i < prefabQuestPieces.Count; i++)
            {
                var data = prefabQuestPieces[i];
                c.m_QuestPiecePrefabComponent.Add(data.m_ID, data);
            }
            var recipeData = m_RecipeData;
            for (int i = 0; i < recipeData.Count; i++)
            {
                c.GetComponentContainer<RecipeDataComponent>().Add(recipeData[i].m_ID, recipeData[i]);
            }

            var cookieData = m_CookieData;
            for (int i = 0; i < recipeData.Count; i++)
            {
                c.m_CookieData.Add(cookieData[i].m_ParentID, cookieData[i]);
            }
        }

        public override void LoadDataFromCode()
        {
            m_QuestPieceFunctionalComponents.Clear();
            m_QuestPieceUIComponent.Clear();
            m_QuestPiecePrefabComponent.Clear();
            m_CookieData.Clear();
            m_RecipeData.Clear();

            CreateCookies();
            CreateActions();
            CreateObjects();
            CreateModifiers();
            CreateTargets();

            void CreateCookies()
            {
                CreateNew();
                SetIDName("malvavisco_fantasma_tostado");
                SetPieceType(PieceType.Cookie);
                SetUIData("Malvavisco Crudo",
                    "El Malvavisco Crudo es un postre básico dispuesto a echar una mano con lo que haga falta, aunque no sirva para mucho. Bueno para cuando no hay mucho que comer y pocas ganas de cocinar.");
                SetRecipeData("Receta para Malvavisco Crudo",
                    "El Malvavisco Crudo es un postre básico dispuesto a echar una mano con lo que haga falta, aunque no sirva para mucho.Bueno para cuando no hay mucho que comer y pocas ganas de cocinar.",
                     Karma.GoodKarma, 0, 0);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("merengue_fantasma_tostado");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 1);
                AddFunctionalTag(Tag.Convince, 1);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Merengue Fantasma Tostado",
                    "El Merengue Fantasma Tostado es un postre básico, dulce, blandito… pero a veces se cuela en las  habitaciones de la gente mientras duermen y se queda mirándolos haciendo ruidos de ultratumba. *WUO-WUO-WUO-WUO-WUO-WUO-WUO* Que alguien llame a la policía.");
                SetRecipeData("Receta de Merengue Fantasma Tostado",
                    "El Merengue Fantasma Tostado es un postre básico, dulce, blandito… pero a veces se cuela en las  habitaciones de la gente mientras duermen y se queda mirándolos haciendo ruidos de ultratumba. * WUO - WUO - WUO - WUO - WUO - WUO - WUO * Que alguien llame a la policía.",
                    Karma.GoodKarma, 75, 75);
                AddIngredientToRecipe("crema_pastelera_arcana", 2);
                AddIngredientToRecipe("vainilla_de_la_iluminacion", 1);

                CreateNew();
                SetIDName("pepito_de_ternura");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Convince, 1);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Pepito de Ternura",
                    "Cualquiera que sea el problema que tengas el Pepito de Ternura va intentar resolverlo, aunque a lo mejor no es buena idea mandarlo atacar a un a un grupo de lobos. Con su centro de crema dulce y su corazón brillante de chocolate eterno está listo para salir a abrazar al mundo.");
                SetRecipeData("Receta de Pepito de Ternura",
                    "Cualquiera que sea el problema que tengas el Pepito de Ternura va intentar resolverlo, aunque a lo mejor no es buena idea mandarlo atacar a un a un grupo de lobos. Con su centro de crema dulce y su corazón brillante de chocolate eterno está listo para salir a abrazar al mundo.",
                    Karma.GoodKarma, 250, 0);
                AddIngredientToRecipe("chocolate_negro_sempiterno", 2);
                AddIngredientToRecipe("crema_pastelera_arcana", 1);

                CreateNew();
                SetIDName("bizcotroll");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Help, 3);
                SetUIData("Bizcotroll de Naranjas Somnolientas",
                    "Se dice que el Bizcotroll forma parte del trío de postres héroes colosales, y que aunque se pasa gran parte del tiempo dormido, en cuanto se despierta no duda en ayudar.");
                SetRecipeData("Receta de Bizcotroll de Naranjas Somnolientas",
                    "Se dice que el Bizcotroll forma parte del trío de postres héroes colosales, y que aunque se pasa gran parte del tiempo dormido, en cuanto se despierta no duda en ayudar.",
                    Karma.GoodKarma, 500, 0);
                AddIngredientToRecipe("harina_de_fuerza_titanica", 1);
                AddIngredientToRecipe("caramelo_fundido_candiscente", 2);
                AddIngredientToRecipe("levadura_ancestral_de_la_pereza", 1);

                CreateNew();
                SetIDName("jauria_bombon");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Jauría Bombón Infernal",
                    "Recién salida del horno, la Jauría Bombón Infernal son un grupo de bombones escupe compota candente, especializados en el ataque. Con un núcleo de compota de mora infernal y una habanero ardiente en la cola hace arder todo a su paso.");
                SetRecipeData("Receta de Jauría Bombón Infernal",
                    "Recién salida del horno, la Jauría Bombón Infernal son un grupo de bombones escupe compota candente, especializados en el ataque. Con un núcleo de compota de mora infernal y una habanero ardiente en la cola hace arder todo a su paso.",
                    Karma.GoodKarma, 0, 250);
                AddIngredientToRecipe("compota_de_mora_infernal", 2);
                AddIngredientToRecipe("chocolate_negro_sempiterno", 1);

                CreateNew();
                SetIDName("paladin_tortita");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 3);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Paladín Tortita",
                    "El Paladín Tortita es el segundo postre colosal, tan solo con levantar su espada de caramelo solidificado imparte daño en área en un segundo. Aunque sus capas de tortitas son tan suaves que se deshacen en la boca, el caramelo que lo cubre lo protege de cualquier ataque.");
                SetRecipeData("Receta de Paladín Tortita",
                    "El Paladín Tortita es el segundo postre colosal, tan solo con levantar su espada de caramelo solidificado imparte daño en área en un segundo. Aunque sus capas de tortitas son tan suaves que se deshacen en la boca, el caramelo que lo cubre lo protege de cualquier ataque.",
                    Karma.GoodKarma, 350, 150);
                AddIngredientToRecipe("nucleo_de_cereza_animico", 1);
                AddIngredientToRecipe("caramelo_fundido_candiscente", 2);
                AddIngredientToRecipe("harina_de_fuerza_titanica", 2);

                CreateNew();
                SetIDName("bizquiborracho");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 1);
                AddFunctionalTag(Tag.Convince, 2);
                SetUIData("Bizquiborracho de Mora",
                    "El bizcoborracho es un postre achispado que se zarandea de un lado a otro con un un ritmo hipnótico. Entre su golosa capa de compota y su bizcocho… HIP... borracho te convence de lo que sea … HIP-HIP… creo que no me mantengo en pie.");
                SetRecipeData("Receta de Bizquiborracho de Mora",
                    "El bizcoborracho es un postre achispado que se zarandea de un lado a otro con un un ritmo hipnótico. Entre su golosa capa de compota y su bizcocho… HIP... borracho te convence de lo que sea … HIP-HIP… creo que no me mantengo en pie.",
                    Karma.GoodKarma, 0, 250);
                AddIngredientToRecipe("crema_pastelera_arcana", 2);
                AddIngredientToRecipe("compota_de_mora_infernal", 1);

                CreateNew();
                SetIDName("citrielfo");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Convince, 3);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Citrielfo en Copa",
                    "Los citrielfos son el tercer postre colosal. Siempre tienen la razón, o eso creen ellos, y da igual que estén equivocados, no tienen problemas en mirarte por encima del hombro desde lo alto de su copa.");
                SetRecipeData("Receta de Citrielfo en Copa",
                    "Los citrielfos son el tercer postre colosal. Siempre tienen la razón, o eso creen ellos, y da igual que estén equivocados, no tienen problemas en mirarte por encima del hombro desde lo alto de su copa.",
                    Karma.GoodKarma, 150, 350);
                AddIngredientToRecipe("esencia_de_limon_purificadora", 2);
                AddIngredientToRecipe("vainilla_de_la_iluminacion", 1);
                AddIngredientToRecipe("chocolate_negro_sempiterno", 1);
            }
            void CreateActions()
            {
                CreateNew();
                SetIDName("attack");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Harm, 1);
                SetUIData("Atacar", "Con esta galleta puedes acariciar fuertemente al objetivo hasta que por un resbalón tonto caiga al suelo. Si no quería llegar a este nivel, no sé, que se hubiese portado bien.");
                SetRecipeData("Receta de Atacar", "Con esta galleta puedes acariciar fuertemente al objetivo hasta que por un resbalón tonto caiga al suelo. Si no quería llegar a este nivel, no sé, que se hubiese portado bien.", Karma.EvilKarma, 50, 0);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("dialogate");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Dialogar", "Hablar las cosas a veces está bien, eso dicen… Mira, primero se intenta convencer, luego ya se saca a Diálogo.");
                SetRecipeData("Receta de Dialogar", "Hablar las cosas a veces está bien, eso dicen… Mira, primero se intenta convencer, luego ya se saca a Diálogo.", Karma.GoodKarma, 0, 50);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("assist");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Ayudar", "Cuando alguien esté en problemas usa esta galleta para ayudarlos, no hay nada que un abrazo no pueda hacer.");
                SetRecipeData("Receta de Ayudar", "Cuando alguien esté en problemas usa esta galleta para ayudarlos, no hay nada que un abrazo no pueda hacer.", Karma.GoodKarma, 0, 50);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("look");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Observar", "Mira. En realidad solo mira lo que pasa, observar esta bien para entender a tus enemigos, conocerlos y aprender sus habitos y debilidades para después… ATACAR Y VENGARSE POR EL DAÑO AL HONOR DE TU FAMILIA… o solo dialogar, con Diálogo.");
                SetRecipeData("Receta de Observar", "Mira. En realidad solo mira lo que pasa, observar esta bien para entender a tus enemigos, conocerlos y aprender sus habitos y debilidades para después… ATACAR Y VENGARSE POR EL DAÑO AL HONOR DE TU FAMILIA… o solo dialogar, con Diálogo.", Karma.GoodKarma, 50, 0);
                AddIngredientToRecipe("polvo_auxilio", 1);

                CreateNew();
                SetIDName("stare");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Convince, 2);
                SetUIData("Mirar fijamente", "Mira, pero no mira para observar, te persigue con la mirada, aunque no quieras, aunque cruces la calle, entres en tu casa, y te metas escondas debajo de la mesa. TE ESTAMOS VIENDO… NO PUEDES ESCONDERTE.");
                SetRecipeData("Receta de Mirar fijamente", "Mira, pero no mira para observar, te persigue con la mirada, aunque no quieras, aunque cruces la calle, entres en tu casa, y te metas escondas debajo de la mesa. TE ESTAMOS VIENDO… NO PUEDES ESCONDERTE.", Karma.GoodKarma, 50, 0);
                AddIngredientToRecipe("polvo_persuasivo", 1);

                CreateNew();
                SetIDName("steal");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Harm, 2);
                SetUIData("Robar", "Con esta galleta puedes coger prestado cosas de la víctima… quiero decir, objetivo. Un poco de despiste y lo que quieras puedes cogerlo prestado, indefinidamente.");
                SetRecipeData("Receta de Robar", "Con esta galleta puedes coger prestado cosas de la víctima… quiero decir, objetivo. Un poco de despiste y lo que quieras puedes cogerlo prestado, indefinidamente.", Karma.GoodKarma, 0, 50);
                AddIngredientToRecipe("polvo_impetuoso", 1);
            }
            void CreateObjects()
            {
                CreateNew();
                SetIDName("baseball_bat");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 2);
                SetUIData("Bate de Beisbol", "Un bate, también llamado Diálogo, es una galleta muy socorrida cuando se te ponen un poco tontos, un suave golpecito y puedes convencer a quien sea de cualquier cosa.");
                SetRecipeData("Receta de bate de beisbol", "Un bate, también llamado Diálogo, es una galleta muy socorrida cuando se te ponen un poco tontos, un suave golpecito y puedes convencer a quien sea de cualquier cosa.", Karma.GoodKarma, 50, 0);
                AddIngredientToRecipe("polvo_impetuoso", 2);

                CreateNew();
                SetIDName("scissors");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Tijeras", "Porque es por todos conocido que ayudar con unas tijeras en la mano no asusta a nadie.");
                SetRecipeData("Receta de Tijeras", "Porque es por todos conocido que ayudar con unas tijeras en la mano no asusta a nadie.", Karma.GoodKarma, 50, 10);
                AddIngredientToRecipe("polvo_impetuoso", 3);
                AddIngredientToRecipe("polvo_persuasivo", 1);

                CreateNew();
                SetIDName("flip_flops");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 1);
                AddFunctionalTag(Tag.Convince, 2);
                SetUIData("Chanclas", "Da igual dónde esté el objetivo, cuando una chancla se lanza lo encuentra y golpea el cogote de todo aquél que haya a cinco kilómetros a la redonda.  *FLIP-FLOP-FLIP-FLOP-FLIP-FLOP-FLIP-FLOP* ¡CORRE!");
                SetRecipeData("Receta de Chanclas", "Da igual dónde esté el objetivo, cuando una chancla se lanza lo encuentra y golpea el cogote de todo aquél que haya a cinco kilómetros a la redonda.  *FLIP-FLOP-FLIP-FLOP-FLIP-FLOP-FLIP-FLOP* ¡CORRE!", Karma.GoodKarma, 50, 0);
                AddIngredientToRecipe("polvo_persuasivo", 3);
                AddIngredientToRecipe("polvo_impetuoso", 1);

                CreateNew();
                SetIDName("cake");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Tarta", "En serio, ¿a qué clase de persona perturbada se le ocurrió meter un pastel como objeto? ¿Atacar con un pastel? Mirar fijamente con un pastel… perturbador.");
                SetRecipeData("Receta de Tarta", "En serio, ¿a qué clase de persona perturbada se le ocurrió meter un pastel como objeto? ¿Atacar con un pastel? Mirar fijamente con un pastel… perturbador.", Karma.GoodKarma, 10, 50);
                AddIngredientToRecipe("polvo_auxilio", 2);
            }
            void CreateModifiers()
            {
                CreateNew();
                SetIDName("violently");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Harm, 3);
                SetUIData("Violentamente", "La violencia nunca es la solución, bueno si alguien se pone un poco tonto se le puede acariciar violentamente la cara hasta que entre en razón, pero sólo cuando Diálogo no ha funcionado.");
                SetRecipeData("Receta de Violentamente", "La violencia nunca es la solución, bueno si alguien se pone un poco tonto se le puede acariciar violentamente la cara hasta que entre en razón, pero sólo cuando Diálogo no ha funcionado.", Karma.GoodKarma, 50, 10);
                AddIngredientToRecipe("polvo_impetuoso", 6);

                CreateNew();
                SetIDName("brutally");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Harm, 4);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Brutalmente", "Todo ataque se magnifica con esta galleta, pero no nos pasemos, los trajes para funerales están caros en esta época del año.");
                SetRecipeData("Receta de Brutalmente", "Todo ataque se magnifica con esta galleta, pero no nos pasemos, los trajes para funerales están caros en esta época del año.", Karma.GoodKarma, 10, 50);
                AddIngredientToRecipe("polvo_impetuoso", 10);

                CreateNew();
                SetIDName("kindly");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Help, 3);
                SetUIData("Amablemente", "Con esta galleta toda acción que hagas se suavizará, porque es por todos conocidos que atacar amablemente es más gentil que hacerlo violentamente.");
                SetRecipeData("Receta de Amablemente", "Con esta galleta toda acción que hagas se suavizará, porque es por todos conocidos que atacar amablemente es más gentil que hacerlo violentamente.", Karma.GoodKarma, 0, 50);
                AddIngredientToRecipe("polvo_auxilio", 6);

                CreateNew();
                SetIDName("convincingly");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Convince, 3);
                SetUIData("Convincentemente", "Como dice mi abuela “Más vale maña que mil pájaros volando”, bueno, esa señora nunca estuvo en sus cabales, pero lo que viene a significar es que nunca olvides el poder de la persuasión.");
                SetRecipeData("Receta de Convincentemente", "Como dice mi abuela “Más vale maña que mil pájaros volando”, bueno, esa señora nunca estuvo en sus cabales, pero lo que viene a significar es que nunca olvides el poder de la persuasión.", Karma.GoodKarma, 0, 50);
                AddIngredientToRecipe("polvo_persuasivo", 6);
            }
            void CreateTargets()
            {
                CreateNew();
                SetIDName("mayor");
                SetPieceType(PieceType.Target);
                SetUIData("Alcalde", "Es el alcalde del pueblo, y no tiene ningún problema en dejar claro que él es quien manda y que le gusta el dinero. Posee gran parte de los cultivos de trigo del pueblo.");

                CreateNew();
                SetIDName("meri");
                SetPieceType(PieceType.Target);
                SetUIData("Meri", "En realidad se llama Meri, la lechera, pero Hio le llamó Meri, la Leches una vez y así se quedó. Posee la única granja de vacas del pueblo, y se le da muy bien cuidarlas si no fuera porque ocasionalmente la cosa se vaya de las manos.");

                CreateNew();
                SetIDName("canela");
                SetPieceType(PieceType.Target);
                SetUIData("Canela", "Empresaria, exitosa, chica de portada. Saltó a la fama después de presentar la patente de su primer extracto super concentrado de canela, a partir de ahí el éxito vino solo. Últimamente se dedica a coleccionar artefactos antiguos.");

                CreateNew();
                SetIDName("johny_setas");
                SetPieceType(PieceType.Target);
                SetUIData("Er Johny el setas", "Er Johnny vive en su mundo psicodélico todo el día, lo que le lleva a ser el mejor creador de levaduras del archipiélago. Utiliza sus pócimas para ayudar a la gente del pueblo, aunque suelen tener efectos inesperados.");

                CreateNew();
                SetIDName("miss_chocolate");
                SetPieceType(PieceType.Target);
                SetUIData("Miss Chocolate", "Dulce y estilosa, la maravillosa científica de la chocolatería Miss Chocolate está lista para seducirte con su dulce sabor hasta hacerte saltar por los aires con su chocolate explosivo.");

                CreateNew();
                SetIDName("mantecas");
                SetPieceType(PieceType.Target);
                SetUIData("El Mantecas", "Gafe sí es, a este mantequero le ocurren mil desgracias, un poco merecidas, aunque puede ser porque el olor dulce de la mantequilla las atraiga, y a las ratas también.");

                CreateNew();
                SetIDName("wolves");
                SetPieceType(PieceType.Target);
                SetUIData("Lobos", "Son una manada de lobos, ¿un poco violenta? Bastante, no ha sido una buena idea construir el pueblo donde antes vivía la manada.");

                CreateNew();
                SetIDName("rats");
                SetPieceType(PieceType.Target);
                SetUIData("Ratas", "Estas ratas han formado una sociedad de roedores super inteligentes, a la mínima que puedan se van a aprovechar de la situación. Más te vale tenerlas de tu lado.");

                CreateNew();
                SetIDName("vacas");
                SetPieceType(PieceType.Target);
                SetUIData("Vacas", "Las vacas de Meri, la Leches, son unos animales tiernos que no paran de hacer leche, aunque a veces tienen rachas y necesitan un poco de ayuda para seguir con sus labores.");

                CreateNew();
                SetIDName("weirdMan");
                SetPieceType(PieceType.Target);
                SetUIData("Hombre Misterioso", "Un extraño hombre que se coloca en la plaza a vigilar a todas las personas que pasen. No tendríamos que desconfiar en él en una primera instancia, pero parece tan sospechoso… ¡Parece que va a atracar a un vecino en cualquier momento!");

                CreateNew();
                SetIDName("thief");
                SetPieceType(PieceType.Target);
                SetUIData("Ladrón", "Un ladrón de mala muerte que pasea por los alrededores del pueblo por la noche, esperando para realizar su próximo ataque. No parece muy listo, pero su cara de malas pulgas intimida hasta el más valiente de los aldeanos.");

                CreateNew();
                SetIDName("dragon");
                SetPieceType(PieceType.Target);
                SetUIData("Dragón", "¡Quién diría que, en un lugar tan recóndito del planeta, como es el archipiélago de Akalia, se pudiera encontrar un ser fantástico tan impresionante y letal como es un dragón! ¿Será tan fiero como lo describen las leyendas?");

                CreateNew();
                SetIDName("monster");
                SetPieceType(PieceType.Target);
                SetUIData("Monstruo de las Nieves", "Una criatura que vive en las montañas al norte del pueblo. Siempre ha salido en la prensa local lo que parecían avistamientos de este monstruo, pero nunca se ha visto de forma tan clara hasta el día de hoy.");

                CreateNew();
                SetIDName("ghost");
                SetPieceType(PieceType.Target);
                SetUIData("Fantasma", "Un ser espectral que merodea por las noches en el algo al sur del pueblo. Siempre se ha dicho que los fantasmas son las almas de aquellas personas que no han encontrado el descanso eterno en la muerte. ¿Qué habrá pasado para que siga aquí?");

                CreateNew();
                SetIDName("cat");
                SetPieceType(PieceType.Target);
                SetUIData("Gato", "Estos seres felinos son especialistas en meterse en problemas cuando menos los esperas… ¡O darte problemas a ti! Aun así… ¿quién no los querría? ¡son muy monos!");

                CreateNew();
                SetIDName("spiders");
                SetPieceType(PieceType.Target);
                SetUIData("Arañas", "Estas arañas no son para nada como las que conocemos: ni siquiera parecen de este mundo. Aparecen en grandes enjambres y parecen inteligentes, pero siempre gritan “harrah harrah”. ¿Qué demonios significará harrah harrah?");

                CreateNew();
                SetIDName("golem");
                SetPieceType(PieceType.Target);
                SetUIData("Golem", "Un ser hecho de rocas que, en un principio, no responde a ninguna clase de raciocinio: solo sabe que le gusta tirar piedras a la gente y que se divierte por ello. Pero si se divierte, es posible que se le pueda hablar.");
            }
        }

        #region Builder Methods
        private void CreateNew()
        {
            _functionalQP = new QuestPieceFunctionalComponent();
            _uiQP = new QuestPieceUIComponent();
            _prefabQP = new QuestPiecePrefabComponent();
            _cookieData = new CookieDataComponent(); // LEGACY
            _recipeData = new RecipeDataComponent();

            m_QuestPieceFunctionalComponents.Add(_functionalQP);
            m_QuestPieceUIComponent.Add(_uiQP);
            m_QuestPiecePrefabComponent.Add(_prefabQP);
            m_CookieData.Add(_cookieData);
            m_RecipeData.Add(_recipeData);

            _prefabQP.m_QuestBuildingPiecePrefab = m_DefaultPiecePrefab;
        }

        private void SetIDName(string idName)
        {
            ID id = new ID(idName);

            _functionalQP.m_ID = id;
            _uiQP.m_ID = id;
            _prefabQP.m_ID = id;

            _recipeData.m_ID = id;
            _recipeData.m_PieceID = id;

            _cookieData.m_ParentID = id;
        }

        private void SetPieceType(PieceType type)
        {
            _functionalQP.m_Type = type;
        }

        private void AddFunctionalTag(Tag type, int value)
        {
            _functionalQP.m_Tags.Add(new QPTag { m_Type = type, m_Value = value });
        }

        private void SetUIData(string name, string description)
        {
            _uiQP.m_Name = name;
            _uiQP.m_Description = description;

            // TODO: Unify this
            _cookieData.m_CookieName = name;
            _cookieData.m_CookieDescription = description;

            ID id = _uiQP.m_ID;
            _uiQP.m_SimpleSprite = _cookieReferences.GetSimpleSprite(id);
            _uiQP.m_CookiePieceSprite = _cookieReferences.GetFullSprite(id);

            if (_cookieReferences.GetHDSprite(id, out Sprite hdSprite))
                _uiQP.m_HDSprite = hdSprite;
            else
                _uiQP.m_HDSprite = _uiQP.m_SimpleSprite;

            if (_cookieReferences.GetShopRecipeSprite(id, out Sprite recipeShopSprite))
                _uiQP.m_ShopRecipeSprite = recipeShopSprite;
            else
                _uiQP.m_ShopRecipeSprite = _uiQP.m_CookiePieceSprite;
        }

        private void SetRecipeData(string name, string description, Karma repType, int good_price, int evil_price)
        {
            _recipeData.m_RecipeName = name;
            _recipeData.m_RecipeDescription = description;
            _recipeData.m_ReputationTypePrice = repType;
            _recipeData.m_Price_Good = good_price;
            _recipeData.m_Price_Evil = evil_price;
        }

        private void AddIngredientToRecipe(string ingredientIDname, int amount)
        {
            _recipeData.m_IngredientsList.Add(new InventoryItem(new ID(ingredientIDname), amount));
        }
        #endregion
    }
}