using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using CQM.Databases;

public class CookieMakingSystem : ISystemEvents
{
    private CookieDB _cookieDB;

    private int _selectedRecipe = -1;
    public event Action<int> OnCreateCookie;
    public event Action OnBuyRecipe;

    private Event<ItemData> _addCookieCommand;

    public void Initialize(CookieDB cookieDB)
    {
        _cookieDB = cookieDB;
        var evtSys = Admin.Global.EventSystem;
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

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = "cookie_making_sys".GetHashCode();

        commands.AddEvent("buy_recipe".GetHashCode()).OnInvoked += BuyRecipe;
    }
}