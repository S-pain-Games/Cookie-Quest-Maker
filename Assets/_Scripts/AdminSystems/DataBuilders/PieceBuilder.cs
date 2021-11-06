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
        [SerializeField]
        private List<BuiltPiece> m_Pieces = new List<BuiltPiece>();

        public List<QuestPieceFunctionalComponent> m_QuestPieceFunctionalComponents = new List<QuestPieceFunctionalComponent>();
        public List<UIQuestPieceComponent> m_QuestPieceUIComponent = new List<UIQuestPieceComponent>();
        public List<QuestPiecePrefabComponent> m_QuestPiecePrefabComponent = new List<QuestPiecePrefabComponent>();
        public List<CookieDataComponent> m_CookieData = new List<CookieDataComponent>();
        public List<RecipeDataComponent> m_RecipeData = new List<RecipeDataComponent>();

        public GameObject m_DefaultPiecePrefab;

        private BuiltPiece _piece;

        private QuestPieceFunctionalComponent _functionalQP;
        private UIQuestPieceComponent _uiQP;
        private QuestPiecePrefabComponent _prefabQP;
        private CookieDataComponent _cookieData;
        private RecipeDataComponent _recipeData;

        public void LoadDataFromCode()
        {
            m_Pieces.Clear();
            m_QuestPieceFunctionalComponents.Clear();
            m_QuestPieceUIComponent.Clear();
            m_QuestPiecePrefabComponent.Clear();
            m_CookieData.Clear();
            m_RecipeData.Clear();

            CreateNew();
            SetIDName("plain_cookie");
            SetPieceType(PieceType.Cookie);
            SetUIData("Plain Cookie", "Very plain");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);
            AddIngredientToRecipe("chocolate", 1);

            CreateNew();
            SetIDName("plain_cookie_2");
            SetPieceType(PieceType.Cookie);
            SetUIData("Plain Cookie 2", "Very Very plain plain");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);
            AddIngredientToRecipe("chocolate", 1);

            CreateNew();
            SetIDName("attack");
            SetPieceType(PieceType.Action);
            AddFunctionalTag(Tag.Harm, 1);
            SetUIData("Attack", "Very Agressive");
            SetRecipeData("Attack Piece Recipe", "Very aggresive", Reputation.EvilCookieReputation, 50);
            AddIngredientToRecipe("chocolate", 1);

            CreateNew();
            SetIDName("assist");
            SetPieceType(PieceType.Action);
            AddFunctionalTag(Tag.Help, 1);
            SetUIData("Assist", "Very Assistive");
            SetRecipeData("Assist Recipe", "Very Assistive", Reputation.GoodCookieReputation, 50);
            AddIngredientToRecipe("vanilla", 1);

            CreateNew();
            SetIDName("baseball_bat");
            SetPieceType(PieceType.Object);
            AddFunctionalTag(Tag.Harm, 2);
            AddFunctionalTag(Tag.Convince, 1);
            SetUIData("Baseball Bat", "Very Bat");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);
            AddIngredientToRecipe("chocolate", 2);

            CreateNew();
            SetIDName("cake");
            SetPieceType(PieceType.Object);
            AddFunctionalTag(Tag.Convince, 2);
            AddFunctionalTag(Tag.Help, 1);
            SetUIData("Cake", "Very Bat");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);
            AddIngredientToRecipe("chocolate", 2);
            AddIngredientToRecipe("vanilla", 2);

            CreateNew();
            SetIDName("mayor");
            SetPieceType(PieceType.Target);
            SetUIData("Mayor", "Very Bat");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);

            CreateNew();
            SetIDName("wolves");
            SetPieceType(PieceType.Target);
            AddFunctionalTag(Tag.Harm, 1);
            SetUIData("Wolves", "Very Bat");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);
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

            // Inspector Only
            _piece = new BuiltPiece(_cookieData, _recipeData);
            m_Pieces.Add(_piece);
        }

        private void SetIDName(string idName)
        {
            _piece.inspectorPieceName = idName;
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

        [System.Serializable]
        private class BuiltPiece
        {
            public string inspectorPieceName;
            public CookieDataComponent cookieData;
            public RecipeDataComponent recipeData;

            public BuiltPiece(CookieDataComponent cookieData, RecipeDataComponent recipeData)
            {
                this.cookieData = cookieData;
                this.recipeData = recipeData;
            }
        }
    }
}