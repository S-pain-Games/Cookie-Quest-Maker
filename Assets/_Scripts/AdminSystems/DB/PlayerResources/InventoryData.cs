using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Storage of all the unlocked pieces and cookies that the player has
public class InventoryData
{
    // Reputation
    public int m_GoodCookieReputation;
    public int m_EvilCookieReputation;

    // List of unlocked word pieces (Actions, Modifiers, Objects)
    public List<int> m_Storage = new List<int>();

    // Cookies Inventory
    public List<InventoryItem> m_Cookies = new List<InventoryItem>();

    // Ingredients Inventory
    public List<InventoryItem> m_Ingredients = new List<InventoryItem>();

    public void Initialize()
    {
        // DEV ONLY, THE INVENTORY SHOULD BE FILLED BY
        // THE INVENTORY SYSTEM WHEN LOADING THE SAVED GAME

        m_Storage.Add("eureka".GetHashCode());
        //m_Storage.Add(ids.attack);
        //m_Storage.Add(ids.assist);
        //m_Storage.Add(ids.baseball_bat);
        //m_Storage.Add(ids.brutally);
        //m_Storage.Add(ids.kindly);
        //m_Storage.Add("dialogate".GetHashCode());

        m_Cookies.Add(new InventoryItem { m_ItemID = "plain_cookie".GetHashCode(), m_Amount = 1 });

        //m_Ingredients.Add(new InventoryItem("cookie_dough".GetHashCode(), 1));
        //m_Ingredients.Add(new InventoryItem("chocolate_chips".GetHashCode(), 1));
        //m_Ingredients.Add(new InventoryItem("red_jelly".GetHashCode(), 1));

        m_GoodCookieReputation += 100;
        m_EvilCookieReputation += 100;
    }
}

public class InventoryItem
{
    public int m_ItemID = 0;
    public int m_Amount = 0;

    public InventoryItem() { }

    public InventoryItem(int itemID, int amount)
    {
        m_ItemID = itemID;
        m_Amount = amount;
    }
}

public struct ItemData
{
    public int m_ItemID;
    public int m_Amount;

    public ItemData(int itemID, int amount)
    {
        m_ItemID = itemID;
        m_Amount = amount;
    }
}