using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Databases
{
    public class CookieDB : MonoBehaviour
    {
        [SerializeField]
        private PieceBuilder _piecesBuilder;

        // Contains all the cookie recipes
        public Dictionary<int, RecipeData> m_RecipeDataDB = new Dictionary<int, RecipeData>();
        // Contains all the data for the cookies
        public Dictionary<int, CookieData> m_CookieDataDB = new Dictionary<int, CookieData>();

        public void LoadData()
        {
            var pIds = Admin.Global.ID.pieces;

            LoadRecipes(pIds);
            LoadCookies(pIds);
        }

        private void LoadRecipes(IDQuestPieces pIds)
        {
            var recipeData = _piecesBuilder.RecipesData;

            for (int i = 0; i < recipeData.Count; i++)
            {
                m_RecipeDataDB.Add(recipeData[i].m_ParentID, recipeData[i]);
            }
        }

        private void LoadCookies(IDQuestPieces pIds)
        {
            var recipeData = _piecesBuilder.CookieData;

            for (int i = 0; i < recipeData.Count; i++)
            {
                m_CookieDataDB.Add(recipeData[i].m_ParentID, recipeData[i]);
            }
        }
    }
}