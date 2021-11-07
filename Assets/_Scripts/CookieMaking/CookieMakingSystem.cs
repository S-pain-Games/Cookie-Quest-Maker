using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using CQM.Databases;

public class CookieMakingSystem : ISystemEvents
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private Singleton_InventoryComponent _inventory;

    private ID _selectedCookieID;
    public event Action<ID> OnCreateCookie;
    public event Action OnBuyRecipe;

    private Event<ItemData> _addPieceToInventoryCmd;
    private Event<ItemData> _removeIngredientToInventoryCmd;


    public void Initialize(ComponentsContainer<RecipeDataComponent> recipeDataComponents,
                           Singleton_InventoryComponent inventory)
    {
        _recipeDataComponents = recipeDataComponents;
        _inventory = inventory;

        var evtSys = Admin.Global.EventSystem;
        _addPieceToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_piece");
        _removeIngredientToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "remove_ingredient");
    }

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("cookie_making_sys");

        commands.AddEvent(new ID("buy_recipe")).OnInvoked += BuyRecipe;
    }

    public void SelectRecipe(ID recipeId)
    {
        _selectedCookieID = recipeId;
        OnCreateCookie?.Invoke(_selectedCookieID);
    }

    public void CreateCookie()
    {
        RecipeDataComponent recipe = _recipeDataComponents[_selectedCookieID];

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
                _addPieceToInventoryCmd.Invoke(new ItemData(_selectedCookieID, 1));
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
}