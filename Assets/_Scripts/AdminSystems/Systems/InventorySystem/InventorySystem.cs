using CQM.Databases;
using System.Collections;
using UnityEngine;

public class InventorySystem : ISystemEvents
{
    private InventoryData _inventoryData;

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = "inventory_sys".GetHashCode();

        commands.AddEvent<ItemData>("add_piece".GetHashCode()).OnInvoked +=
            (args) => AddPieceToInventory(args.m_ItemID, args.m_Amount);
        commands.AddEvent<ItemData>("remove_piece".GetHashCode()).OnInvoked +=
            (args) => RemovePieceFromInventory(args.m_ItemID, args.m_Amount);

        commands.AddEvent<InventorySys_ChangeReputationEvtArgs>("change_reputation".GetHashCode()).OnInvoked +=
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

        commands.AddEvent<int>("unlock_recipe".GetHashCode()).OnInvoked += UnlockRecipe;
    }

    public void Initialize(InventoryData data)
    {
        _inventoryData = data;
    }

    private void AddPieceToInventory(int pieceID, int amount)
    {
        InventoryItem item = _inventoryData.m_Pieces.Find(i => i.m_ItemID == pieceID);
        if (item != null)
            item.m_Amount += amount;
        else
            _inventoryData.m_Pieces.Add(new InventoryItem(pieceID, amount));
    }
    private void RemovePieceFromInventory(int pieceID, int amount)
    {
        InventoryItem item = _inventoryData.m_Pieces.Find(i => i.m_ItemID == pieceID);
        if (item != null)
        {
            item.m_Amount -= amount;

            if (item.m_Amount <= 0)
                _inventoryData.m_Pieces.Remove(item);
        }
    }

    private void UnlockRecipe(int recipeID)
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