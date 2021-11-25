using CQM.Components;
using CQM.Databases;
using System.Collections;
using UnityEngine;

public class InventorySystem : ISystemEvents
{
    private Singleton_InventoryComponent m_InvData;

    private EventVoid OnReputationChanged;

    public void Initialize(Singleton_InventoryComponent data)
    {
        m_InvData = data;
    }

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
                        ChangeGoodCookieRep(args.m_Amount);
                        break;
                    case Reputation.EvilCookieReputation:
                        ChangeEvilCookieRep(args.m_Amount);
                        break;
                    default:
                        Debug.LogError("Oh no");
                        break;
                }
            };

        commands.AddEvent<ID>(new ID("unlock_recipe")).OnInvoked += UnlockRecipe;

        OnReputationChanged = callbacks.AddEvent(new ID("reputation_changed"));
    }


    private void AddPieceToInventory(ID pieceID, int amount)
    {
        InventoryItem item = m_InvData.m_Pieces.Find(i => i.m_ItemID == pieceID);
        if (item != null)
            item.m_Amount += amount;
        else
            m_InvData.m_Pieces.Add(new InventoryItem(pieceID, amount));
    }
    private void RemovePieceFromInventory(ID pieceID, int amount)
    {
        InventoryItem item = m_InvData.m_Pieces.Find(i => i.m_ItemID == pieceID);
        if (item != null)
        {
            item.m_Amount -= amount;

            if (item.m_Amount <= 0)
                m_InvData.m_Pieces.Remove(item);
        }
    }

    private void AddIngredientToInventory(ID pieceID, int amount)
    {
        InventoryItem item = m_InvData.m_Ingredients.Find(i => i.m_ItemID == pieceID);
        if (item != null)
            item.m_Amount += amount;
        else
            m_InvData.m_Ingredients.Add(new InventoryItem(pieceID, amount));
    }
    private void RemoveIngredientFromInventory(ID pieceID, int amount)
    {
        InventoryItem item = m_InvData.m_Ingredients.Find(i => i.m_ItemID == pieceID);
        if (item != null)
        {
            item.m_Amount -= amount;

            if (item.m_Amount <= 0)
                m_InvData.m_Ingredients.Remove(item);
        }
    }


    private void UnlockRecipe(ID recipeID)
    {
        if (m_InvData.m_UnlockedRecipes.Contains(recipeID))
            Debug.LogError("Oh no");

        m_InvData.m_UnlockedRecipes.Add(recipeID);
    }

    public void ChangeGoodCookieRep(int amount)
    {
        if (m_InvData.m_GoodCookieReputation + amount >= 0)
            m_InvData.m_GoodCookieReputation += amount;
        else
            Debug.LogError("Tried to remove more currency than available");

        OnReputationChanged.Invoke();
    }
    public void ChangeEvilCookieRep(int amount)
    {
        if (m_InvData.m_EvilCookieReputation + amount >= 0)
            m_InvData.m_EvilCookieReputation += amount;
        else
            Debug.LogError("Tried to remove more currency than available");

        OnReputationChanged.Invoke();
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