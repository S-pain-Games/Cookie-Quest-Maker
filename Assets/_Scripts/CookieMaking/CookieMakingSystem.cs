using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CookieMakingSystem : MonoBehaviour
{
    private CookieDB _cookieDB; 
    public int _selectedRecipe = -1;

    public event Action<int> OnCreateCookie;

    Admin admin;

    private void Awake()
    {
        admin = Admin.g_Instance;
    }

    public void Initialize(CookieDB cookieDB)
    {
        _cookieDB = cookieDB;
    }

    public void SelectRecipe(int recipeId)
    {
        _selectedRecipe = recipeId;
        OnCreateCookie?.Invoke(_selectedRecipe);
    }

    public void CreateCookie()
    {
        RecipeData recipe;
        _cookieDB.m_RecipeDataDB.TryGetValue(_selectedRecipe, out recipe);

        if(recipe != null && !recipe.fabricated)
        {
            admin.playerPieceStorage.m_Storage.Add(_selectedRecipe);
            recipe.fabricated = true;
        }
    }
}
