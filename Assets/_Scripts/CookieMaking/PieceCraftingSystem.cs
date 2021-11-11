using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCraftingSystem : ISystemEvents
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private Singleton_InventoryComponent _inventory;

    private ID _selectedPieceID;

    private Event<ItemData> _addPieceToInventoryCmd;
    private Event<ItemData> _removeIngredientToInventoryCmd;

    private CraftingType _selectedCraftingType = CraftingType.Cookie;

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("piece_crafting_sys");

        commands.AddEvent<CraftingType>(new ID("select_crafting_type")).OnInvoked += SetCraftingType;
    }

    public void Initialize(ComponentsContainer<RecipeDataComponent> recipeDataComponents,
                           Singleton_InventoryComponent inventory)
    {
        _recipeDataComponents = recipeDataComponents;
        _inventory = inventory;

        var evtSys = Admin.Global.EventSystem;
        _addPieceToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_piece");
        _removeIngredientToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "remove_ingredient");
    }

    private void SetCraftingType(CraftingType craftingType)
    {
        _selectedCraftingType = craftingType;
    }



}
public enum CraftingType
{
    Cookie,
    Action,
    Modifier,
    Object
}
