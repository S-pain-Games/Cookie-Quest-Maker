using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieDB
{
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
        RecipeData recipe = new RecipeData();
        recipe.m_RecipeName = "Plain Cookie Recipe";
        recipe.m_RecipeDescription = "Plain Cookie recipe description.";
        recipe.m_PieceID = "plain_cookie".GetHashCode();
        recipe.m_ReputationTypePrice = Reputation.GoodCookieReputation;
        recipe.m_Price = 50;
        m_RecipeDataDB.Add("plain_cookie".GetHashCode(), recipe);

        recipe = new RecipeData();
        recipe.m_RecipeName = "Plain Cookie 2 Recipe";
        recipe.m_RecipeDescription = "Plain Cookie 2 recipe description.";
        recipe.m_PieceID = "plain_cookie_2".GetHashCode();
        recipe.m_ReputationTypePrice = Reputation.EvilCookieReputation;
        recipe.m_Price = 50;
        m_RecipeDataDB.Add("plain_cookie_2".GetHashCode(), recipe);

        recipe = new RecipeData();
        recipe.m_RecipeName = "Attack Recipe";
        recipe.m_RecipeDescription = "Attack recipe description.";
        recipe.m_PieceID = "attack".GetHashCode();
        recipe.m_ReputationTypePrice = Reputation.EvilCookieReputation;
        recipe.m_Price = 50;
        m_RecipeDataDB.Add("attack".GetHashCode(), recipe);
    }

    private void LoadCookies(IDQuestPieces pIds)
    {
        CookieData cookie = new CookieData();
        cookie.m_CookieName = "Plain Cookie";
        cookie.m_CookieDescription = "Plain Cookie description";
        m_CookieDataDB.Add(pIds.plain_cookie, cookie);

        cookie = new CookieData();
        cookie.m_CookieName = "Plain Cookie 2";
        cookie.m_CookieDescription = "Plain Cookie 2 description";
        m_CookieDataDB.Add(pIds.plain_cookie_2, cookie);

        cookie = new CookieData();
        cookie.m_CookieName = "Attack";
        cookie.m_CookieDescription = "Very Agressive";
        m_CookieDataDB.Add("attack".GetHashCode(), cookie);
    }
}
