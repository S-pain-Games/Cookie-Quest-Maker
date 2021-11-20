using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : ISystemEvents
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private Singleton_InventoryComponent _inventoryData;
    private ComponentsContainer<IngredientComponent> _ingredientsDataComponents;

    //private EventVoid _updateShopCallback;
    private Event<bool> _updateShopCallback;

    //private EventVoid _enableCharMovCmd;
    //private EventVoid _disableCharMovCmd;

    private Event<InventorySys_ChangeReputationEvtArgs> _changeRepCmd;
    private Event<ID> _unlockRecipeCmd;
    private Event<ItemData> _addIngredient;


    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("shop_sys");

        commands.AddEvent<ID>(new ID("buy_recipe")).OnInvoked += BuyRecipe;
        commands.AddEvent<ID>(new ID("buy_ingredient")).OnInvoked += BuyIngredient;
        callbacks.AddEvent<bool>(new ID("update_shop_ui"));
    }

    public void Initialize(ComponentsContainer<RecipeDataComponent> recipeDataComponents,
                           Singleton_InventoryComponent inventory, ComponentsContainer<IngredientComponent> ingredientsDataComponents)
    {
        _recipeDataComponents = recipeDataComponents;
        _inventoryData = inventory;
        _ingredientsDataComponents = ingredientsDataComponents;

        var evtSys = Admin.Global.EventSystem;
        //_enableCharMovCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        //_disableCharMovCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "disable_movement");
        _changeRepCmd = evtSys.GetCommandByName<Event<InventorySys_ChangeReputationEvtArgs>>("inventory_sys", "change_reputation");
        _unlockRecipeCmd = evtSys.GetCommandByName<Event<ID>>("inventory_sys", "unlock_recipe");
        _addIngredient = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_ingredient");
        _updateShopCallback = Admin.Global.EventSystem.GetCallbackByName<Event<bool>>("shop_sys", "update_shop_ui");
    }

    public void BuyRecipe(ID selectedRecipeId)
    {
        if (!selectedRecipeId.Initialized()) return;

        RecipeDataComponent recipe = _recipeDataComponents[selectedRecipeId];

        if (_inventoryData.m_UnlockedRecipes.Contains(selectedRecipeId)) return;

        bool enoughMoneyToBuy = false;
        if (recipe.m_Price_Good > 0)
            enoughMoneyToBuy = _inventoryData.m_GoodCookieReputation >= recipe.m_Price_Good;
        if (recipe.m_Price_Evil > 0)
            enoughMoneyToBuy = _inventoryData.m_EvilCookieReputation >= recipe.m_Price_Evil;

        if (enoughMoneyToBuy)
        {
            if (recipe.m_Price_Good > 0)
                _changeRepCmd.Invoke(new InventorySys_ChangeReputationEvtArgs(Reputation.GoodCookieReputation, -recipe.m_Price_Good));
            if (recipe.m_Price_Evil > 0)
                _changeRepCmd.Invoke(new InventorySys_ChangeReputationEvtArgs(Reputation.EvilCookieReputation, -recipe.m_Price_Evil));
            _unlockRecipeCmd.Invoke(recipe.m_PieceID);
            _updateShopCallback.Invoke(true);
            //_buyRecipeCmdREFACTOR.Invoke();
            //UpdateTexts();
        }
        else
        {
            _updateShopCallback.Invoke(true);
        }


    }

    public void BuyIngredient(ID selectedIngredientId)
    {
        IngredientComponent ingredient = _ingredientsDataComponents.GetComponentByID(selectedIngredientId);

        if(ingredient != null)
        {
            bool enoughMoneyToBuy = false;
            if (ingredient.m_Price_Good > 0)
                enoughMoneyToBuy = _inventoryData.m_GoodCookieReputation >= ingredient.m_Price_Good;
            if (ingredient.m_Price_Evil > 0)
                enoughMoneyToBuy = _inventoryData.m_EvilCookieReputation >= ingredient.m_Price_Evil;

            if (enoughMoneyToBuy)
            {
                if (ingredient.m_Price_Good > 0)
                    _changeRepCmd.Invoke(new InventorySys_ChangeReputationEvtArgs(Reputation.GoodCookieReputation, -ingredient.m_Price_Good));
                if (ingredient.m_Price_Evil > 0)
                    _changeRepCmd.Invoke(new InventorySys_ChangeReputationEvtArgs(Reputation.EvilCookieReputation, -ingredient.m_Price_Evil));
                //_unlockRecipeCmd.Invoke(recipe.m_PieceID);
                ItemData newIngredient = new ItemData(selectedIngredientId, 1);
                _addIngredient.Invoke(newIngredient);
                _updateShopCallback.Invoke(true);
                //_buyRecipeCmdREFACTOR.Invoke();
                //UpdateTexts();
            }
            else
            {
                _updateShopCallback.Invoke(true);
            }
        }
        
    }
}
