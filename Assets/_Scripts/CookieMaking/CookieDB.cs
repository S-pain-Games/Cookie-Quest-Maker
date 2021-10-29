using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieDB
{
    //Contains all the cookie recipes
    public Dictionary<int, RecipeData> m_RecipeDataDB = new Dictionary<int, RecipeData>();
    public List<RecipeData> m_RecipeDataList = new List<RecipeData>();
    //Contains all the data for the cookies
    public Dictionary<int, CookieData> m_CookieDataDB = new Dictionary<int, CookieData>();

    //Contains all the bought cookie recipes
    public Dictionary<int, RecipeData> m_BoughtRecipeDataDB = new Dictionary<int, RecipeData>();
    public List<RecipeData> m_BoughtRecipeDataList = new List<RecipeData>();

    public void LoadData()
    {
        var pIds = Admin.g_Instance.ID.pieces;

        LoadRecipes(pIds);
        LoadCookies(pIds);

    }

    private void LoadRecipes(IDQuestPieces pIds)
    {
        RecipeData recipe = new RecipeData();
        recipe.m_RecipeName = "Plain Cookie Recipe";
        recipe.m_RecipeDescription = "Plain Cookie recipe description.";
        recipe.m_CookieID = pIds.plain_cookie;
        recipe.m_Reputation = Reputation.GoodCookieReputation;
        recipe.price = 50;
        m_RecipeDataDB.Add(pIds.plain_cookie, recipe);
        m_RecipeDataList.Add(recipe);
        AddBoughtCookie(recipe.m_CookieID, recipe);

        recipe = new RecipeData();
        recipe.m_RecipeName = "Plain Cookie 2 Recipe";
        recipe.m_RecipeDescription = "Plain Cookie 2 recipe description.";
        recipe.m_CookieID = pIds.plain_cookie_2;
        recipe.m_Reputation = Reputation.EvilCookieReputation;
        recipe.price = 50;
        m_RecipeDataDB.Add(pIds.plain_cookie_2, recipe);
        m_RecipeDataList.Add(recipe);
        //AddBoughtCookie(recipe.m_CookieID, recipe);
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
    }

    public void AddBoughtCookie(int id, RecipeData recipe)
    {
        m_BoughtRecipeDataDB.Add(id, recipe);
        m_BoughtRecipeDataList.Add(recipe);
        recipe.bought = true;
    }
}
