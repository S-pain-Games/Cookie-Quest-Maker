using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Databases
{
    public class CookieDB
    {
        // Contains all the cookie recipes
        public Dictionary<int, RecipeData> m_RecipeDataDB = new Dictionary<int, RecipeData>();
        // Contains all the data for the cookies
        public Dictionary<int, CookieData> m_CookieDataDB = new Dictionary<int, CookieData>();

        public void LoadData(PieceBuilder piecesBuilder)
        {
            var recipeData = piecesBuilder.RecipesData;
            for (int i = 0; i < recipeData.Count; i++)
            {
                m_RecipeDataDB.Add(recipeData[i].m_ParentID, recipeData[i]);
            }

            var cookieData = piecesBuilder.CookieData;
            for (int i = 0; i < recipeData.Count; i++)
            {
                m_CookieDataDB.Add(cookieData[i].m_ParentID, cookieData[i]);
            }
        }
    }
}