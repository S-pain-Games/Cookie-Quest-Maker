using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using CQM.Databases;

public class CookieMakingSystem : ISystemEvents
{
    private CookieDB _cookieDB;
    private InventoryComponent _inventory;

    private int _selectedRecipe = -1;
    public event Action<int> OnCreateCookie;
    public event Action OnBuyRecipe;

    private Event<ItemData> _addPieceToInventoryCmd;
    private Event<ItemData> _removeIngredientToInventoryCmd;

    public void Initialize(CookieDB cookieDB, InventoryComponent inventory)
    {
        _cookieDB = cookieDB;
        _inventory = inventory;

        var evtSys = Admin.Global.EventSystem;
        _addPieceToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_piece");
        _removeIngredientToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "remove_ingredient");
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
            bool hasEnoughIngredients = true;

            if (_inventory.m_Ingredients.Count < recipe.m_IngredientsList.Count)
                hasEnoughIngredients = false;
            else
            {
                // This is massively unoptimized and might be wrong
                for (int i = 0; i < recipe.m_IngredientsList.Count; i++)
                {
                    var recipIngr = recipe.m_IngredientsList[i];
                    bool ingredientFound = false;

                    for (int j = 0; j < _inventory.m_Ingredients.Count; j++)
                    {
                        var invIngr = _inventory.m_Ingredients[j];
                        if (recipIngr.m_ItemID == invIngr.m_ItemID)
                        {
                            ingredientFound = true;
                            if (recipIngr.m_Amount > invIngr.m_Amount)
                            {
                                hasEnoughIngredients = false;
                            }
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        hasEnoughIngredients = false;
                        break;
                    }
                }
            }

            if (hasEnoughIngredients)
            {
                for (int i = 0; i < recipe.m_IngredientsList.Count; i++)
                {
                    var recipIngr = recipe.m_IngredientsList[i];
                    _removeIngredientToInventoryCmd.Invoke(new ItemData(recipIngr.m_ItemID, recipIngr.m_Amount));
                }
                _addPieceToInventoryCmd.Invoke(new ItemData(_selectedRecipe, 1));
            }
            //else
            //    Debug.Log("Not enough ingredients");
        }
        else
            Debug.LogError("NO RECIPE FOUND");
    }

    // TODO: Refactor this
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