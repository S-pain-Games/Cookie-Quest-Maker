using CQM.Components;
using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCraftingSystem : ISystemEvents
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private Singleton_InventoryComponent _inventory;

    private Event<ItemData> _addPieceToInventoryCmd;
    private Event<ItemData> _removeIngredientToInventoryCmd;

    //private Event<ID> _updateIngredientsCallback;
    private Event<bool> _updateIngredientsCallback;
    private Event<bool> _updateBakeAnimCallback;

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("piece_crafting_sys");

        commands.AddEvent<ID>(new ID("craft_recipe")).OnInvoked += CraftRecipe;
        //callbacks.AddEvent<ID>(new ID("update_ingredients_ui"));
        callbacks.AddEvent<bool>(new ID("update_ingredients_ui"));
        callbacks.AddEvent<bool>(new ID("update_bake_anim"));
    }

    public void Initialize(ComponentsContainer<RecipeDataComponent> recipeDataComponents,
                           Singleton_InventoryComponent inventory)
    {
        _recipeDataComponents = recipeDataComponents;
        _inventory = inventory;

        var evtSys = Admin.Global.EventSystem;
        _addPieceToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_piece");
        _removeIngredientToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "remove_ingredient");
        _updateIngredientsCallback = Admin.Global.EventSystem.GetCallbackByName<Event<bool>>("piece_crafting_sys", "update_ingredients_ui");
        _updateBakeAnimCallback = Admin.Global.EventSystem.GetCallbackByName<Event<bool>>("piece_crafting_sys", "update_bake_anim");
    }

    public void CraftRecipe(ID _selectedCookieID)
    {
        RecipeDataComponent recipe = _recipeDataComponents[_selectedCookieID];

        if (recipe != null)
        {
            // This ingredient amount is infinite
            if (recipe.m_IngredientsList.Count > 0 && recipe.m_IngredientsList[0].m_ItemID == new ID("masa_de_galletas_encantada"))
            {
                _addPieceToInventoryCmd.Invoke(new ItemData(_selectedCookieID, 1));
                //_updateIngredientsCallback.Invoke(_selectedCookieID);
                _updateIngredientsCallback.Invoke(true);
                _updateBakeAnimCallback.Invoke(true);
                Debug.Log("Piece added");
                return;
            }

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
                //_updateIngredientsCallback.Invoke(_selectedCookieID);
                _updateIngredientsCallback.Invoke(true);
                _updateBakeAnimCallback.Invoke(true);
                Debug.Log("Piece added");
            }
            else
            {
                _updateBakeAnimCallback.Invoke(false);
                Debug.Log("Not enough ingredients");
            }
                
        }
        else
        {
            _updateBakeAnimCallback.Invoke(false);
            Debug.LogError("NO RECIPE FOUND");
        }
            
    }

}