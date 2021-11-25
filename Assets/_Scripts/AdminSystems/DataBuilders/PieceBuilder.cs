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
                     Reputation.GoodCookieReputation, 0, 0);
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
                    Reputation.GoodCookieReputation, 75, 75);
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
                    Reputation.GoodCookieReputation, 250, 0);
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
                    Reputation.GoodCookieReputation, 500, 0);
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
                    Reputation.GoodCookieReputation, 0, 250);
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
                    Reputation.GoodCookieReputation, 350, 150);
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
                    Reputation.GoodCookieReputation, 0, 250);
                AddIngredientToRecipe("crema_pastelera_arcana", 2);
                AddIngredientToRecipe("compota_de_mora_infernal", 1);

                CreateNew();
                SetIDName("citrielfo");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Convince, 3);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Citrielfo en Copa",
                    "Los citrielfos son el tercer postre colosal. Siempre tienen la razón, o eso creen ellos, y da igual que estén equivocados, no tienen problemas en mirarte por encima del hombre desde lo alto de su copa. Tan pronto como la cara de su enemigo ponga expresión de haber chupado un limón sabrá que su hechizo ha hecho efecto.");
                SetRecipeData("Receta de Citrielfo en Copa",
                    "Los citrielfos son el tercer postre colosal. Siempre tienen la razón, o eso creen ellos, y da igual que estén equivocados, no tienen problemas en mirarte por encima del hombre desde lo alto de su copa. Tan pronto como la cara de su enemigo ponga expresión de haber chupado un limón sabrá que su hechizo ha hecho efecto.",
                    Reputation.GoodCookieReputation, 150, 350);
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
                SetUIData("Atacar", "Con esta galleta puedes acariciar fuertemente al objetivo hasta que por un resvalón tonto caiga al suelo. Si no quería llegar a este nivel, no sé, que se hubiese portado bien.");
                SetRecipeData("Receta de Atacar", "Con esta galleta puedes acariciar fuertemente al objetivo hasta que por un resvalón tonto caiga al suelo. Si no quería llegar a este nivel, no sé, que se hubiese portado bien.", Reputation.EvilCookieReputation, 50, 0);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("dialogate");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Dialogar", "Hablar las cosas a veces está bien, eso dicen… Mira primero se intenta convencer, luego ya se saca a Diálogo.");
                SetRecipeData("Receta de Dialogar", "Hablar las cosas a veces está bien, eso dicen… Mira primero se intenta convencer, luego ya se saca a Diálogo.", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("assist");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Ayudar", "Cuando alguien esté en problemas usa esta galleta para ayudarlos, no hay nada que un abrazo no pueda hacer.");
                SetRecipeData("Receta de Ayudar", "Cuando alguien esté en problemas usa esta galleta para ayudarlos, no hay nada que un abrazo no pueda hacer.", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("look");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Observar", "Mira. En realidad solo mira lo que pasa, observar esta bien para entender a tus enemigos, conocerlos y aprender sus habitos y debilidades para después… ATACAR Y VENGARSE POR EL DAÑO AL HONOR DE TU FAMILIA… o solo dialogar, con Diálogo.");
                SetRecipeData("Receta de Observar", "Mira. En realidad solo mira lo que pasa, observar esta bien para entender a tus enemigos, conocerlos y aprender sus habitos y debilidades para después… ATACAR Y VENGARSE POR EL DAÑO AL HONOR DE TU FAMILIA… o solo dialogar, con Diálogo.", Reputation.GoodCookieReputation, 50, 0);
                AddIngredientToRecipe("polvo_auxilio", 1);

                CreateNew();
                SetIDName("stare");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Convince, 2);
                SetUIData("Mirar fijamente", "Mira, pero no mira para observar, te persigue con la mirada, aunque no quieras, aunque cruces la calle, entres en tu casa, y te metas escondas debajo de la mesa. TE ESTAMOS VIENDO… NO PUEDES ESCONDERTE.");
                SetRecipeData("Receta de Mirar fijamente", "Mira, pero no mira para observar, te persigue con la mirada, aunque no quieras, aunque cruces la calle, entres en tu casa, y te metas escondas debajo de la mesa. TE ESTAMOS VIENDO… NO PUEDES ESCONDERTE.", Reputation.GoodCookieReputation, 50, 0);
                AddIngredientToRecipe("polvo_persuasivo", 1);

                CreateNew();
                SetIDName("steal");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Harm, 2);
                SetUIData("Robar", "Con esta galleta puedes coger prestado cosas de la víctima… quiero decir, objetivo. Un poco de despiste y lo que quieras puedes cogerlo prestado, indefinidamente.");
                SetRecipeData("Receta de Robar", "Con esta galleta puedes coger prestado cosas de la víctima… quiero decir, objetivo. Un poco de despiste y lo que quieras puedes cogerlo prestado, indefinidamente.", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("polvo_impetuoso", 1);
            }
            void CreateObjects()
            {
                CreateNew();
                SetIDName("baseball_bat");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 2);
                SetUIData("Bate de Beisbol", "Un bate, también llamado Diálogo, es una galleta muy socorrida cuando se te ponen un poco tontos, un suave golpecito y puedes convencer a quien sea de cualquier cosa.");
                SetRecipeData("Receta de bate de beisbol", "Un bate, también llamado Diálogo, es una galleta muy socorrida cuando se te ponen un poco tontos, un suave golpecito y puedes convencer a quien sea de cualquier cosa.", Reputation.GoodCookieReputation, 50, 0);
                AddIngredientToRecipe("polvo_impetuoso", 2);

                CreateNew();
                SetIDName("scissors");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Tijeras", "Porque es por todos conocido que ayudar con unas tijeras en la mano no asusta a nadie.");
                SetRecipeData("Receta de Tijeras", "Porque es por todos conocido que ayudar con unas tijeras en la mano no asusta a nadie.", Reputation.GoodCookieReputation, 50, 10);
                AddIngredientToRecipe("polvo_impetuoso", 3);
                AddIngredientToRecipe("polvo_persuasivo", 1);

                CreateNew();
                SetIDName("flip_flops");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 1);
                AddFunctionalTag(Tag.Convince, 2);
                SetUIData("Chanclas", "Da igual dónde esté el objetivo, cuando una chancla se lanza lo encuentra y golpea el cogote de todo aquél que haya a cinco kilómetros a la redonda.  *FLIP-FLOP-FLIP-FLOP-FLIP-FLOP-FLIP-FLOP* ¡CORRE!");
                SetRecipeData("Receta de Chanclas", "Da igual dónde esté el objetivo, cuando una chancla se lanza lo encuentra y golpea el cogote de todo aquél que haya a cinco kilómetros a la redonda.  *FLIP-FLOP-FLIP-FLOP-FLIP-FLOP-FLIP-FLOP* ¡CORRE!", Reputation.GoodCookieReputation, 50, 0);
                AddIngredientToRecipe("polvo_persuasivo", 3);
                AddIngredientToRecipe("polvo_impetuoso", 1);

                CreateNew();
                SetIDName("cake");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Tarta", "En serio, ¿a qué clase de persona perturbada se le ocurrió meter un pastel como objeto? ¿Atacar con un pastel? Mirar fijamente con un pastel… perturbador.");
                SetRecipeData("Receta de Tarta", "En serio, ¿a qué clase de persona perturbada se le ocurrió meter un pastel como objeto? ¿Atacar con un pastel? Mirar fijamente con un pastel… perturbador.", Reputation.GoodCookieReputation, 10, 50);
                AddIngredientToRecipe("polvo_auxilio", 2);
            }
            void CreateModifiers()
            {
                CreateNew();
                SetIDName("violently");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Harm, 3);
                SetUIData("Violentamente", "La violencia nunca es la solución, bueno si alguien se pone un poco tonto se le puede acaricial violentamente la cara hasta que entre en razón, pero sólo cuando Diálogo no ha funcionado.");
                SetRecipeData("Receta de Violentamente", "La violencia nunca es la solución, bueno si alguien se pone un poco tonto se le puede acaricial violentamente la cara hasta que entre en razón, pero sólo cuando Diálogo no ha funcionado.", Reputation.GoodCookieReputation, 50, 10);
                AddIngredientToRecipe("polvo_impetuoso", 3);

                CreateNew();
                SetIDName("brutally");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Harm, 4);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Brutalmente", "Todo ataque se magnifica con esta galleta, pero  no nos pasemos, los trajes para funerales están caros en esta época del año.");
                SetRecipeData("Receta de Brutalmente", "Todo ataque se magnifica con esta galleta, pero  no nos pasemos, los trajes para funerales están caros en esta época del año.", Reputation.GoodCookieReputation, 10, 50);
                AddIngredientToRecipe("polvo_impetuoso", 5);

                CreateNew();
                SetIDName("kindly");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Help, 3);
                SetUIData("Amablemente", "Con esta galleta toda acción que hagas se suavizará, porque es por todos conocidos que atacar amablemente es más gentil que hacerlo violentamente.");
                SetRecipeData("Receta de Amablemente", "Con esta galleta toda acción que hagas se suavizará, porque es por todos conocidos que atacar amablemente es más gentil que hacerlo violentamente.", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("polvo_auxilio", 3);

                CreateNew();
                SetIDName("convincingly");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Convince, 3);
                SetUIData("Convincentemente", "Como dice mi abuela “Más vale maña que mil pájaros volando”, bueno, esa señora nunca estuvo en sus cabales, pero lo que viene a significar es que nunca olvides el poder de la persuasión.");
                SetRecipeData("Receta de Convincentemente", "Como dice mi abuela “Más vale maña que mil pájaros volando”, bueno, esa señora nunca estuvo en sus cabales, pero lo que viene a significar es que nunca olvides el poder de la persuasión.", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("polvo_persuasivo", 3);
            }
            void CreateTargets()
            {
                CreateNew();
                SetIDName("mayor");
                SetPieceType(PieceType.Target);
                SetUIData("Alcalde", "Es el alcalde del pueblo, y no tienen ningún problema en dejarte claro dos cosas, que es el ALCALDE, y que le gustan los dineros. Posee gran parte de los cultivos de trigo del pueblo, allá donde te alcance la vista es propiedad suya, y tiene un sospechoso gran parentesco a Hio…");

                CreateNew();
                SetIDName("meri");
                SetPieceType(PieceType.Target);
                SetUIData("Meri", "En realidad se llama Meri, la lechera, pero Hio le llamó Meri, la Leches una vez y así se quedó. Posee la única granja de vacas del pueblo, y se le da muy bien cuidarlas, pero en ocasiones… pasan cosas y no puede controlarlas, y cuando eso pasa… la cosa se le va de las manos.");

                CreateNew();
                SetIDName("canela");
                SetPieceType(PieceType.Target);
                SetUIData("Canela", "Empresaria, exitosa, chica de portada, todo lo que Canela N Rama quiera lo tiene. Saltó a la fama después de presentar la patente de su primer extracto super concentrado de canela, a partir de ahí el éxito vino solo.");

                CreateNew();
                SetIDName("johny_setas");
                SetPieceType(PieceType.Target);
                SetUIData("Er Johny el setas", "Er Johnny vive en su mundo psicodélico todo el día, lo que le lleva a ser el mejor creador de levaduras del archipiélago. Si hay alguien capaz de desvelar el secreto sobre la magia de Hio es Johnny, cuanto más se mete en su mundo, más puede interactuar con la magia del pastelero… aunque crea que son deidades galleta.");

                CreateNew();
                SetIDName("miss_chocolate");
                SetPieceType(PieceType.Target);
                SetUIData("Miss Chocolate", "Dulce y estilosa, esta drag queen está lista para seducirte con su dulce sabor hasta hacerte saltar por los aires con su chocolate explosivo. Es una científica chocolatera imponente, pero cari, no te atrevas a ponerte en su camino, porque cielo, esta reina está lista para servir su lado más amargo.");

                CreateNew();
                SetIDName("mantecas");
                SetPieceType(PieceType.Target);
                SetUIData("El Mantecas", "Gafe sí es, a este mantequero le ocurren mil desgracias, un poco merecidas, aunque puede ser porque el olor dulce de la mantequilla las atraiga, y a las ratas también.");

                CreateNew();
                SetIDName("wolves");
                SetPieceType(PieceType.Target);
                AddFunctionalTag(Tag.Harm, 1);
                SetUIData("Lobos", "Son una manada de lobos, ¿un poco violenta? Pues sí chica, pero no haber construido el pueblo donde antes vivía la manada.");

                CreateNew();
                SetIDName("rats");
                SetPieceType(PieceType.Target);
                SetUIData("Ratas", "Estas ratas han formado una sociedad de roedores super inteligentes, a la mínima que puedan se van a aprovechar de la situación. Más te vale tenerlas de tu lado.");

                CreateNew();
                SetIDName("vacas");
                SetPieceType(PieceType.Target);
                SetUIData("Vacas", "Las vacas de Meri, la Leches, son unos animales tiernos que no paran de hacer leche, aunque a veces tienen rachas y necesitan un poco de ayuda para seguir con sus labores.");
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

        private void SetRecipeData(string name, string description, Reputation repType, int good_price, int evil_price)
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