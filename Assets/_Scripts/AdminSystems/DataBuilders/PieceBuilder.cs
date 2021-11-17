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
                SetIDName("plain_cookie");
                SetPieceType(PieceType.Cookie);
                SetUIData("Galleta normal", "Es una galleta normal.");
                SetRecipeData("Grandma's Plain Cookie Recipe", "Description Plain Cookie", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("plain_cookie_2");
                SetPieceType(PieceType.Cookie);
                SetUIData("La jauría", "Descripción de galleta jauría.");
                SetRecipeData("Grandma's Plain Cookie 2 Recipe", "Description Plain Cookie 2", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 1);
                AddIngredientToRecipe("chocolate_negro_sempiterno", 1);
            }
            void CreateActions()
            {
                CreateNew();
                SetIDName("attack");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Harm, 4);
                SetUIData("Attack", "Very Agressive");
                SetRecipeData("Attack Piece Recipe", "Very aggresive", Reputation.EvilCookieReputation, 50);
                AddIngredientToRecipe("masa_de_galletas_encantada", 1);

                CreateNew();
                SetIDName("dialogate");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Convince, 4);
                SetUIData("Dialogate", "Very Assistive");
                SetRecipeData("Dialogate Recipe", "Very Assistive", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 1);

                CreateNew();
                SetIDName("assist");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Help, 4);
                SetUIData("Assist", "Very Assistive");
                SetRecipeData("Assist Recipe", "Very Assistive", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 1);

                CreateNew();
                SetIDName("look");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Convince, 4);
                SetUIData("Look", "Very Assistive");
                SetRecipeData("Look Recipe", "Very Assistive", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 1);

                CreateNew();
                SetIDName("stare");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Help, 5);
                AddFunctionalTag(Tag.Harm, 2);
                SetUIData("Stare", "Very Assistive");
                SetRecipeData("Stare Recipe", "Very Assistive", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 1);

                CreateNew();
                SetIDName("steal");
                SetPieceType(PieceType.Action);
                AddFunctionalTag(Tag.Harm, 5);
                SetUIData("Steal", "Very Assistive");
                SetRecipeData("Steal Recipe", "Very Assistive", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 1);
            }
            void CreateObjects()
            {
                CreateNew();
                SetIDName("baseball_bat");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Baseball Bat", "Very Bat");
                SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 2);

                CreateNew();
                SetIDName("scissors");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Scissor", "Very Bat");
                SetRecipeData("Scissor Recipe", "Desc", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 2);

                CreateNew();
                SetIDName("flip_flops");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Harm, 2);
                AddFunctionalTag(Tag.Convince, 1);
                SetUIData("Flip Flops", "Very Bat");
                SetRecipeData("Flip Flops Recipe", "Desc", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 2);

                CreateNew();
                SetIDName("cake");
                SetPieceType(PieceType.Object);
                AddFunctionalTag(Tag.Convince, 2);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Cake", "Very Bat");
                SetRecipeData("Cake Recipe", "Desc", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("compota_de_mora_infernal", 2);
                AddIngredientToRecipe("nucleo_de_cereza_animico", 2);
            }
            void CreateModifiers()
            {
                CreateNew();
                SetIDName("violently");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Convince, 2);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Violently", "Very Bat");
                SetRecipeData("Violently Recipe", "Desc", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("nucleo_de_cereza_animico", 2);
                AddIngredientToRecipe("crema_pastelera_arcana", 2);

                CreateNew();
                SetIDName("brutally");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Convince, 2);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Brutally", "Very Bat");
                SetRecipeData("Brutally Recipe", "Desc", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("nucleo_de_cereza_animico", 2);
                AddIngredientToRecipe("crema_pastelera_arcana", 2);

                CreateNew();
                SetIDName("kindly");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Convince, 2);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Kindly", "Very Bat");
                SetRecipeData("Kindly Recipe", "Desc", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("nucleo_de_cereza_animico", 2);
                AddIngredientToRecipe("crema_pastelera_arcana", 2);

                CreateNew();
                SetIDName("convincingly");
                SetPieceType(PieceType.Modifier);
                AddFunctionalTag(Tag.Convince, 2);
                AddFunctionalTag(Tag.Help, 1);
                SetUIData("Convincingly", "Very Bat");
                SetRecipeData("Convincingly Recipe", "Desc", Reputation.GoodCookieReputation, 50);
                AddIngredientToRecipe("nucleo_de_cereza_animico", 2);
                AddIngredientToRecipe("crema_pastelera_arcana", 2);
            }
            void CreateTargets()
            {
                CreateNew();
                SetIDName("mayor");
                SetPieceType(PieceType.Target);
                SetUIData("Mayor", "Very Bat");

                CreateNew();
                SetIDName("meri");
                SetPieceType(PieceType.Target);
                SetUIData("Mery", "Very Bat");

                CreateNew();
                SetIDName("canela");
                SetPieceType(PieceType.Target);
                SetUIData("Canela", "Very Bat");

                CreateNew();
                SetIDName("johny_setas");
                SetPieceType(PieceType.Target);
                SetUIData("Er Johny el setas", "Very Bat");

                CreateNew();
                SetIDName("miss_chocolate");
                SetPieceType(PieceType.Target);
                SetUIData("Miss Chocolate", "Very Bat");

                CreateNew();
                SetIDName("mantecas");
                SetPieceType(PieceType.Target);
                SetUIData("El Mantecas", "Very Bat");

                CreateNew();
                SetIDName("wolves");
                SetPieceType(PieceType.Target);
                AddFunctionalTag(Tag.Harm, 1);
                SetUIData("Lobos", "Very Bat");

                CreateNew();
                SetIDName("rats");
                SetPieceType(PieceType.Target);
                SetUIData("Ratas", "Very Bat");

                CreateNew();
                SetIDName("vacas");
                SetPieceType(PieceType.Target);
                SetUIData("Vacas", "Deep Mooooo");
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

        private void SetRecipeData(string name, string description, Reputation repType, int price)
        {
            _recipeData.m_RecipeName = name;
            _recipeData.m_RecipeDescription = description;
            _recipeData.m_ReputationTypePrice = repType;
            _recipeData.m_Price = price;
        }

        private void AddIngredientToRecipe(string ingredientIDname, int amount)
        {
            _recipeData.m_IngredientsList.Add(new InventoryItem(new ID(ingredientIDname), amount));
        }
        #endregion
    }
}