using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : ISystemEvents
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private Singleton_InventoryComponent _inventoryData;

    private EventVoid _updateShopCallback;

    //private EventVoid _enableCharMovCmd;
    //private EventVoid _disableCharMovCmd;

    private Event<InventorySys_ChangeReputationEvtArgs> _changeRepCmd;
    private Event<ID> _unlockRecipeCmd;


    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("shop_sys");

        commands.AddEvent<ID>(new ID("buy_recipe")).OnInvoked += BuyRecipe;
        callbacks.AddEvent(new ID("update_shop_ui"));
    }

    public void Initialize(ComponentsContainer<RecipeDataComponent> recipeDataComponents,
                           Singleton_InventoryComponent inventory)
    {
        _recipeDataComponents = recipeDataComponents;
        _inventoryData = inventory;

        var evtSys = Admin.Global.EventSystem;
        //_enableCharMovCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        //_disableCharMovCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "disable_movement");
        _changeRepCmd = evtSys.GetCommandByName<Event<InventorySys_ChangeReputationEvtArgs>>("inventory_sys", "change_reputation");
        _unlockRecipeCmd = evtSys.GetCommandByName<Event<ID>>("inventory_sys", "unlock_recipe");
    }

    public void BuyRecipe(ID selectedRecipeId)
    {
        if (!selectedRecipeId.Initialized()) return;

        RecipeDataComponent recipe = _recipeDataComponents[selectedRecipeId];

        if (_inventoryData.m_UnlockedRecipes.Contains(selectedRecipeId)) return;

        bool enoughMoneyToBuy = false;
        if (recipe.m_ReputationTypePrice == Reputation.GoodCookieReputation)
            enoughMoneyToBuy = _inventoryData.m_GoodCookieReputation >= recipe.m_Price;
        else if (recipe.m_ReputationTypePrice == Reputation.EvilCookieReputation)
            enoughMoneyToBuy = _inventoryData.m_EvilCookieReputation >= recipe.m_Price;

        if (enoughMoneyToBuy)
        {
            _changeRepCmd.Invoke(new InventorySys_ChangeReputationEvtArgs(recipe.m_ReputationTypePrice, -recipe.m_Price));
            _unlockRecipeCmd.Invoke(recipe.m_PieceID);
            _updateShopCallback.Invoke();
            //_buyRecipeCmdREFACTOR.Invoke();
            //UpdateTexts();
        }
    }
}
