using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using CQM.QuestMaking;

public class CookieMakingSystem : MonoBehaviour
{
    private CookieDB _cookieDB;

    public int _selectedRecipe = -1;

    public event Action<int> OnCreateCookie;

    private Event<ItemData> _addCookieCommand;

    public void Initialize(CookieDB cookieDB)
    {
        _cookieDB = cookieDB;
        var evtSys = Admin.g_Instance.gameEventSystem;
        evtSys.InventorySystemCommands.GetEvent("add_cookie".GetHashCode(), out _addCookieCommand);
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

        if (recipe != null && !recipe.fabricated)
        {
            _addCookieCommand.Invoke(new ItemData(_selectedRecipe, 1));
            recipe.fabricated = true;
        }
    }
}
