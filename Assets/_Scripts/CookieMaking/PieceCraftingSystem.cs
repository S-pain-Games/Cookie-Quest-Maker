using CQM.Components;
using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCraftingSystem : ISystemEvents
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private ComponentsContainer<QuestPieceFunctionalComponent> _pieceDataComponents;
    private Singleton_InventoryComponent _inventory;

    private Event<ItemData> _addPieceToInventoryCmd;
    private Event<ItemData> _removeIngredientToInventoryCmd;

    //private Event<PieceRecipeUi> _updatePieceUiCallback;


    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("piece_crafting_sys");

        //commands.AddEvent<QuestPieceFunctionalComponent.PieceType>(new ID("select_crafting_type")).OnInvoked += SetCraftingType;
        commands.AddEvent<ID>(new ID("craft_recipe")).OnInvoked += CraftRecipe;
        //callbacks.AddEvent<PieceRecipeUi>(new ID("update_piece_ui"));
    }

    public void Initialize(ComponentsContainer<RecipeDataComponent> recipeDataComponents,
                           Singleton_InventoryComponent inventory, ComponentsContainer<QuestPieceFunctionalComponent> pieceDataComponents)
    {
        _recipeDataComponents = recipeDataComponents;
        _pieceDataComponents = pieceDataComponents;
        _inventory = inventory;

        var evtSys = Admin.Global.EventSystem;
        _addPieceToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_piece");
        _removeIngredientToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "remove_ingredient");
        //_updatePieceUiCallback = Admin.Global.EventSystem.GetCallbackByName<Event<PieceRecipeUi>>("piece_crafting_sys", "update_piece_ui");

        /*
        _cookieRecipes = new List<ID>();
        _actionRecipes = new List<ID>();
        _modifierRecipes = new List<ID>();
        _objectRecipes = new List<ID>();

        LoadRecipeLists();
        LoadDefaultRecipe();
        */
    }

    public void CraftRecipe(ID _selectedCookieID)
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
                Debug.Log("Piece added");
            }
            else
                Debug.Log("Not enough ingredients");
        }
        else
            Debug.LogError("NO RECIPE FOUND");
    }

}