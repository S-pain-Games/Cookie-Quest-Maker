using CQM.Databases;
using System.Collections;
using UnityEngine;

public class InventorySystem : ISystemEvents
{
    private Singleton_InventoryComponent _inventoryData;


    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("inventory_sys");

        commands.AddEvent<ItemData>(new ID("add_piece")).OnInvoked +=
            (args) => AddPieceToInventory(args.m_ItemID, args.m_Amount);
        commands.AddEvent<ItemData>(new ID("remove_piece")).OnInvoked +=
            (args) => RemovePieceFromInventory(args.m_ItemID, args.m_Amount);

        commands.AddEvent<ItemData>(new ID("add_ingredient")).OnInvoked +=
            (args) => AddIngredientToInventory(args.m_ItemID, args.m_Amount);
        commands.AddEvent<ItemData>(new ID("remove_ingredient")).OnInvoked +=
            (args) => RemoveIngredientFromInventory(args.m_ItemID, args.m_Amount);

        commands.AddEvent<InventorySys_ChangeReputationEvtArgs>(new ID("change_reputation")).OnInvoked +=
            (args) =>
            {
                switch (args.m_RepType)
                {
                    case Reputation.GoodCookieReputation:
                        ChangueGoodCookieRep(args.m_Amount);
                        break;
                    case Reputation.EvilCookieReputation:
                        ChangueEvilCookieRep(args.m_Amount);
                        break;
                    default:
                        Debug.LogError("Oh no");
                        break;
                }
            };

        commands.AddEvent<ID>(new ID("unlock_recipe")).OnInvoked += UnlockRecipe;
    }

    public void Initialize(Singleton_InventoryComponent data)
    {
        _inventoryData = data;
    }

    private void AddPieceToInventory(ID pieceID, int amount)
    {
        InventoryItem item = _inventoryData.m_Pieces.Find(i => i.m_ItemID == pieceID);
        if (item != null)
            item.m_Amount += amount;
        else
            _inventoryData.m_Pieces.Add(new InventoryItem(pieceID, amount));
    }
    private void RemovePieceFromInventory(ID pieceID, int amount)
    {
        InventoryItem item = _inventoryData.m_Pieces.Find(i => i.m_ItemID == pieceID);
        if (item != null)
        {
            item.m_Amount -= amount;

            if (item.m_Amount <= 0)
                _inventoryData.m_Pieces.Remove(item);
        }
    }

    private void AddIngredientToInventory(ID pieceID, int amount)
    {
        InventoryItem item = _inventoryData.m_Ingredients.Find(i => i.m_ItemID == pieceID);
        if (item != null)
            item.m_Amount += amount;
        else
            _inventoryData.m_Ingredients.Add(new InventoryItem(pieceID, amount));
    }
    private void RemoveIngredientFromInventory(ID pieceID, int amount)
    {
        InventoryItem item = _inventoryData.m_Ingredients.Find(i => i.m_ItemID == pieceID);
        if (item != null)
        {
            item.m_Amount -= amount;

            if (item.m_Amount <= 0)
                _inventoryData.m_Ingredients.Remove(item);
        }
    }


    private void UnlockRecipe(ID recipeID)
    {
        if (_inventoryData.m_UnlockedRecipes.Contains(recipeID))
            Debug.LogError("Oh no");

        _inventoryData.m_UnlockedRecipes.Add(recipeID);
    }

    // TODO: Refactor this
    public void ChangueGoodCookieRep(int amount)
    {
        if (_inventoryData.m_GoodCookieReputation + amount >= 0)
            _inventoryData.m_GoodCookieReputation += amount;
        else
            Debug.LogError("Tried to remove more currency than available");
    }

    public void ChangueEvilCookieRep(int amount)
    {
        if (_inventoryData.m_EvilCookieReputation + amount >= 0)
            _inventoryData.m_EvilCookieReputation += amount;
        else
            Debug.LogError("Tried to remove more currency than available");
    }
}

public struct InventorySys_ChangeReputationEvtArgs
{
    public Reputation m_RepType;
    public int m_Amount;

    public InventorySys_ChangeReputationEvtArgs(Reputation repType, int amount)
    {
        m_RepType = repType;
        m_Amount = amount;
    }
}