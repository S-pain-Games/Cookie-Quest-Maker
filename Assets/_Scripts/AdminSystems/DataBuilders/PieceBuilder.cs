using CQM.AssetReferences;
using CQM.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using PieceType = CQM.Components.QuestPieceFunctionalComponent.PieceType;
using Tag = CQM.Components.QPTag.TagType;

namespace CQM.Databases
{
    [Serializable]
    public class PieceBuilder : MonoBehaviour
    {
        [SerializeField] private CookieReferencesDatabase _cookieReferences;

        // Output Components
        public List<QuestPieceFunctionalComponent> m_QuestPieceFunctionalComponents = new List<QuestPieceFunctionalComponent>();
        public List<UIQuestPieceComponent> m_QuestPieceUIComponent = new List<UIQuestPieceComponent>();
        public List<QuestPiecePrefabComponent> m_QuestPiecePrefabComponent = new List<QuestPiecePrefabComponent>();
        public List<CookieDataComponent> m_CookieData = new List<CookieDataComponent>();
        public List<RecipeDataComponent> m_RecipeData = new List<RecipeDataComponent>();

        public GameObject m_DefaultPiecePrefab;

        // Data of the piece that is currently being built
        private QuestPieceFunctionalComponent _functionalQP;
        private UIQuestPieceComponent _uiQP;
        private QuestPiecePrefabComponent _prefabQP;
        private CookieDataComponent _cookieData;
        private RecipeDataComponent _recipeData;


        public void BuildPieces(ComponentsDatabase c)
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

        public void LoadDataFromCode()
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
                SetUIData("Malvavisco Fantasma Tostado", "No es el más hábil, pero actúa con entusiasmo.");
                SetRecipeData("Receta para Malvavisco Fantasma Tostado", "No es el más hábil, pero actúa con entusiasmo.", Reputation.GoodCookieReputation, 0, 0);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("merengue_fantasma_tostado");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 1);
                AddFunctionalTag(Tag.Convince, 1);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Merengue Fantasma Tostado", "Ayudante todoterreno perfecto para tareas sencillas.");
                SetRecipeData("Receta de Merengue Fantasma Tostado", "Ayudante todoterreno perfecto para tareas sencillas.", Reputation.GoodCookieReputation, 75, 75);
                AddIngredientToRecipe("crema_pastelera_arcana", 2);
                AddIngredientToRecipe("vainilla_de_la_iluminacion", 1);

                CreateNew();
                SetIDName("pepito_de_ternura");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Convince, 1);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Pepito de Ternura", "Siempre dispuesto a ayudar.");
                SetRecipeData("Receta de Pepito de Ternura", "Siempre dispuesto a ayudar.", Reputation.GoodCookieReputation, 250, 0);
                AddIngredientToRecipe("chocolate_negro_sempiterno", 2);
                AddIngredientToRecipe("crema_pastelera_arcana", 1);

                CreateNew();
                SetIDName("bizcotroll");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Help, 3);
                SetUIData("Bizcotroll de Naranjas Somnolientas", "Es muy pacífico, pero no controla su propia fuerza");
                SetRecipeData("Receta de Bizcotroll de Naranjas Somnolientas", "Es muy pacífico, pero no controla su propia fuerza", Reputation.GoodCookieReputation, 500, 0);
                AddIngredientToRecipe("harina_de_fuerza_titanica", 1);
                AddIngredientToRecipe("caramelo_fundido_candiscente", 2);
                AddIngredientToRecipe("levadura_ancestral_de_la_pereza", 1);

                CreateNew();
                SetIDName("jauria_bombon");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Jauría Bombón Infernal", "Tratan de ayudar de la forma más violenta posible. Los favoritos de Evith.");
                SetRecipeData("Receta de Jauría Bombón Infernal", "Tratan de ayudar de la forma más violenta posible. Los favoritos de Evith.", Reputation.GoodCookieReputation, 0, 250);
                AddIngredientToRecipe("compota_de_mora_infernal", 2);
                AddIngredientToRecipe("chocolate_negro_sempiterno", 1);

                CreateNew();
                SetIDName("paladin_tortita");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 3);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Paladín Tortita", "Gran defensor de la justicia con métodos muy violentos");
                SetRecipeData("Receta de Paladín Tortita", "Gran defensor de la justicia con métodos muy violentos", Reputation.GoodCookieReputation, 350, 150);
                AddIngredientToRecipe("nucleo_de_cereza_animico", 1);
                AddIngredientToRecipe("caramelo_fundido_candiscente", 2);
                AddIngredientToRecipe("harina_de_fuerza_titanica", 2);

                CreateNew();
                SetIDName("bizquiborracho");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Harm, 1);
                AddFunctionalTag(Tag.Convince, 2);
                SetUIData("Bizquiborracho de Mora", "Intenta ser persuasivo, pero se le puede ir la mano.");
                SetRecipeData("Receta de Bizquiborracho de Mora", "Intenta ser persuasivo, pero se le puede ir la mano.", Reputation.GoodCookieReputation, 0, 250);
                AddIngredientToRecipe("crema_pastelera_arcana", 2);
                AddIngredientToRecipe("compota_de_mora_infernal", 1);

                CreateNew();
                SetIDName("citrielfo");
                SetPieceType(PieceType.Cookie);
                AddFunctionalTag(Tag.Convince, 3);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Citrielfo en Copa", "Es capaz de persuadir incluso a una mula. Aprobado por Nu.");
                SetRecipeData("Receta de Citrielfo en Copa", "Es capaz de persuadir incluso a una mula. Aprobado por Nu.", Reputation.GoodCookieReputation, 150, 350);
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
                SetUIData("Atacar", "Para cuando la violencia puede ser la solución");
                SetRecipeData("Receta de Atacar", "Para cuando la violencia puede ser la solución.", Reputation.EvilCookieReputation, 50, 0);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("dialogate");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Dialogar", "La solución más diplomática ante un problema");
                SetRecipeData("Receta de Dialogar", "La solución más diplomática ante un problema", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("assist");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Ayudar", "Para echar una mano a alguien en apuros");
                SetRecipeData("Receta de Ayudar", "Para echar una mano a alguien en apuros", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("look");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Observar", "Observa detenidamente la situación antes de actuar");
                SetRecipeData("Receta de Observar", "Observa detenidamente la situación antes de actuar", Reputation.GoodCookieReputation, 50, 0);
                AddIngredientToRecipe("polvo_auxilio", 1);

                CreateNew();
                SetIDName("stare");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Convince, 2);
                SetUIData("Mirar fijamente", "Observa hasta el punto de llegar a ser incómodo");
                SetRecipeData("Receta de Mirar fijamente", "Observa hasta el punto de llegar a ser incómodo", Reputation.GoodCookieReputation, 50, 0);
                AddIngredientToRecipe("polvo_persuasivo", 1);

                CreateNew();
                SetIDName("steal");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Harm, 2);
                SetUIData("Robar", "Para la redistribución de bienes ajenos");
                SetRecipeData("Receta de Robar", "Para la redistribución de bienes ajenos", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("polvo_impetuoso", 1);
            }
            void CreateObjects()
            {
                CreateNew();
                SetIDName("baseball_bat");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 2);
                SetUIData("Bate de Beisbol", "Una muestra de tus no tan buenas intenciones");
                SetRecipeData("Receta de bate de beisbol", "Una muestra de tus no tan buenas intenciones", Reputation.GoodCookieReputation, 50, 0);
                AddIngredientToRecipe("polvo_impetuoso", 2);

                CreateNew();
                SetIDName("scissors");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Tijeras", "Muy puntiagudas. No es recomendable correr con ellas en la mano");
                SetRecipeData("Receta de Tijeras", "Muy puntiagudas. No es recomendable correr con ellas en la mano.", Reputation.GoodCookieReputation, 50, 10);
                AddIngredientToRecipe("polvo_impetuoso", 3);
                AddIngredientToRecipe("polvo_persuasivo", 1);

                CreateNew();
                SetIDName("flip_flops");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 1);
                AddFunctionalTag(Tag.Convince, 2);
                SetUIData("Chanclas", "El utensilio disuasorio por excelencia");
                SetRecipeData("Receta de Chanclas", "El utensilio disuasorio por excelencia", Reputation.GoodCookieReputation, 50, 0);
                AddIngredientToRecipe("polvo_persuasivo", 3);
                AddIngredientToRecipe("polvo_impetuoso", 1);

                CreateNew();
                SetIDName("cake");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Help, 2);
                SetUIData("Tarta", "Una muestra de tus buenas intenciones");
                SetRecipeData("Receta de Tarta", "Una muestra de tus buenas intenciones", Reputation.GoodCookieReputation, 10, 50);
                AddIngredientToRecipe("polvo_auxilio", 2);
            }
            void CreateModifiers()
            {
                CreateNew();
                SetIDName("violently");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Harm, 3);
                SetUIData("Violentamente", "Insiste de manera agresiva");
                SetRecipeData("Receta de Violentamente", "Insiste de manera agresiva", Reputation.GoodCookieReputation, 50, 10);
                AddIngredientToRecipe("polvo_impetuoso", 3);

                CreateNew();
                SetIDName("brutally");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Harm, 4);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Brutalmente", "La paz nunca fue una opción");
                SetRecipeData("Receta de Brutalmente", "La paz nunca fue una opción", Reputation.GoodCookieReputation, 10, 50);
                AddIngredientToRecipe("polvo_impetuoso", 5);

                CreateNew();
                SetIDName("kindly");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Help, 3);
                SetUIData("Amablemente", "Actúa con ternura");
                SetRecipeData("Receta de Amablemente", "Actúa con ternura", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("polvo_auxilio", 3);

                CreateNew();
                SetIDName("convincingly");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Convince, 3);
                SetUIData("Convincentemente", "Demuestra que están equivocados");
                SetRecipeData("Receta de Convincentemente", "Demuestra que están equivocados", Reputation.GoodCookieReputation, 0, 50);
                AddIngredientToRecipe("polvo_persuasivo", 3);
            }
            void CreateTargets()
            {
                CreateNew();
                SetIDName("mayor");
                SetPieceType(PieceType.Target);
                SetUIData("Mayor", "El alcalde del pueblo.");

                CreateNew();
                SetIDName("meri");
                SetPieceType(PieceType.Target);
                SetUIData("Meri", "Cuidadora de vacas.");

                CreateNew();
                SetIDName("canela");
                SetPieceType(PieceType.Target);
                SetUIData("Canela", "Coleccionista de artefactos.");

                CreateNew();
                SetIDName("johny_setas");
                SetPieceType(PieceType.Target);
                SetUIData("Er Johny el setas", "El peculiar alquimista.");

                CreateNew();
                SetIDName("miss_chocolate");
                SetPieceType(PieceType.Target);
                SetUIData("Miss Chocolate", "La excéntrica chocolatera.");

                CreateNew();
                SetIDName("mantecas");
                SetPieceType(PieceType.Target);
                SetUIData("El Mantecas", "El agricultor malhumorado.");

                CreateNew();
                SetIDName("wolves");
                SetPieceType(PieceType.Target);
                AddFunctionalTag(Tag.Harm, 1);
                SetUIData("Lobos", "Una manada de lobos.");

                CreateNew();
                SetIDName("rats");
                SetPieceType(PieceType.Target);
                SetUIData("Ratas", "Ratas particularmente inteligentes.");

                CreateNew();
                SetIDName("vacas");
                SetPieceType(PieceType.Target);
                SetUIData("Vacas", "Las mayores víctimas en esta historia.");
            }
        }

        #region Builder Methods
        private void CreateNew()
        {
            _functionalQP = new QuestPieceFunctionalComponent();
            _uiQP = new UIQuestPieceComponent();
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