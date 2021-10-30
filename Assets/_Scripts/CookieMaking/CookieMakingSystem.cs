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
    public event Action OnBuyRecipe;

    private Event<ItemData> _addCookieCommand;

    public void Initialize(CookieDB cookieDB)
    {
        _cookieDB = cookieDB;
        var evtSys = Admin.g_Instance.gameEventSystem;
        _addCookieCommand = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_cookie");
    }

    public void SelectRecipe(int recipeId)
    {
        _selectedRecipe = recipeId;
        OnCreateCookie?.Invoke(_selectedRecipe);
    }

    public void CreateCookie()
    {
        _cookieDB.m_RecipeDataDB.TryGetValue(_selectedRecipe, out RecipeData recipe);

        if (recipe != null)
        {
            _addCookieCommand.Invoke(new ItemData(_selectedRecipe, 1));
        }
    }

    public void BuyRecipe()
    {
        OnBuyRecipe?.Invoke();
    }
}
