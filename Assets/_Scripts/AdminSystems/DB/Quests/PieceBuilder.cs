using System;
using System.Collections.Generic;
using UnityEngine;
using PieceType = QuestPiece.PieceType;
using Tag = QPTag.TagType;

namespace CQM.Databases
{
    [Serializable]
    public class PieceBuilder : MonoBehaviour
    {
        [SerializeField]
        private List<BuiltPiece> m_BuiltPieces = new List<BuiltPiece>();

        public List<QuestPieceDataContainer> PiecesData { get => m_PiecesList; }
        public List<CookieData> CookieData { get => m_CookieDataList; }
        public List<RecipeData> RecipesData { get => m_RecipeDataList; }

        [SerializeField, HideInInspector] private List<QuestPieceDataContainer> m_PiecesList = new List<QuestPieceDataContainer>();
        [SerializeField, HideInInspector] private List<CookieData> m_CookieDataList = new List<CookieData>();
        [SerializeField, HideInInspector] private List<RecipeData> m_RecipeDataList = new List<RecipeData>();
        public GameObject defaultPiecePrefab;

        private BuiltPiece _piece;
        private QuestPieceDataContainer _qpData;
        private CookieData _cookieData;
        private RecipeData _recipeData;

        [MethodButton]
        public void LoadDataFromCode()
        {
            m_BuiltPieces.Clear();
            m_PiecesList.Clear();
            m_CookieDataList.Clear();
            m_RecipeDataList.Clear();

            CreateNew();
            SetIDName("plain_cookie");
            SetPieceType(PieceType.Cookie);
            SetUIData("Plain Cookie", "Very plain");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);

            CreateNew();
            SetIDName("plain_cookie_2");
            SetPieceType(PieceType.Cookie);
            SetUIData("Plain Cookie 2", "Very Very plain plain");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);

            CreateNew();
            SetIDName("attack");
            SetPieceType(PieceType.Action);
            AddFunctionalTag(Tag.Harm, 1);
            SetUIData("Attack", "Very Agressive");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);

            CreateNew();
            SetIDName("assist");
            SetPieceType(PieceType.Action);
            AddFunctionalTag(Tag.Help, 1);
            SetUIData("Assist", "Very Assistive");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);

            CreateNew();
            SetIDName("baseball_bat");
            SetPieceType(PieceType.Object);
            AddFunctionalTag(Tag.Harm, 2);
            AddFunctionalTag(Tag.Convince, 1);
            SetUIData("Baseball Bat", "Very Bat");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);

            CreateNew();
            SetIDName("cake");
            SetPieceType(PieceType.Object);
            AddFunctionalTag(Tag.Convince, 2);
            AddFunctionalTag(Tag.Help, 1);
            SetUIData("Cake", "Very Bat");
            SetRecipeData("Grandma's Plain Cookie Recipe", "Desc", Reputation.GoodCookieReputation, 50);

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
            _qpData = new QuestPieceDataContainer();
            _qpData.m_QuestBuildingPiecePrefab = defaultPiecePrefab;
            _qpData.m_Functional = new QuestPiece();
            _qpData.m_QuestSelectionUI = new UIQuestPieceData();

            _cookieData = new CookieData();
            _recipeData = new RecipeData();

            m_PiecesList.Add(_qpData);
            m_CookieDataList.Add(_cookieData);
            m_RecipeDataList.Add(_recipeData);

            _piece = new BuiltPiece(_qpData, _cookieData, _recipeData);
            m_BuiltPieces.Add(_piece);
        }

        private void SetIDName(string idName)
        {
            _piece.inspectorPieceName = idName;
            int id = idName.GetHashCode();

            _qpData.m_ID = id;
            _qpData.m_Functional.m_ParentID = id;
            _qpData.m_QuestSelectionUI.m_ParentID = id;

            _recipeData.m_ParentID = id;
            _recipeData.m_PieceID = id;

            _cookieData.m_ParentID = id;
        }

        private void SetPieceType(PieceType type)
        {
            _qpData.m_Functional.m_Type = type;
        }

        private void AddFunctionalTag(Tag type, int value)
        {
            _qpData.m_Functional.m_Tags.Add(new QPTag { m_Type = type, m_Value = value });
        }

        private void SetUIData(string name, string description)
        {
            _qpData.m_QuestSelectionUI.m_Name = name;
            _qpData.m_QuestSelectionUI.m_Description = description;

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
        #endregion

        [System.Serializable]
        private class BuiltPiece
        {
            public string inspectorPieceName;
            public QuestPieceDataContainer qp;
            public CookieData cookieData;
            public RecipeData recipeData;

            public BuiltPiece(QuestPieceDataContainer qp, CookieData cookieData, RecipeData recipeData)
            {
                this.qp = qp;
                this.cookieData = cookieData;
                this.recipeData = recipeData;
            }
        }
    }
}