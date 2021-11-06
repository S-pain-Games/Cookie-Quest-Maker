using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Databases
{
    [System.Serializable]
    // Storage of all the unlocked pieces and cookies that the player has
    public class InventoryComponent
    {
        // Reputation
        public int m_GoodCookieReputation;
        public int m_EvilCookieReputation;

        // List of unlocked word pieces (Actions, Modifiers, Objects)
        public List<int> m_UnlockedRecipes = new List<int>();

        // Cookies Inventory
        public List<InventoryItem> m_Pieces = new List<InventoryItem>();

        // Ingredients Inventory
        public List<InventoryItem> m_Ingredients = new List<InventoryItem>();

        public void Initialize()
        {
            m_UnlockedRecipes.Add("plain_cookie".GetHashCode());
            m_UnlockedRecipes.Add("attack".GetHashCode());

            m_Ingredients.Add(new InventoryItem("vanilla".GetHashCode(), 1));
            m_Ingredients.Add(new InventoryItem("chocolate".GetHashCode(), 2));
            m_Ingredients.Add(new InventoryItem("cream".GetHashCode(), 1));

            m_GoodCookieReputation += 100;
            m_EvilCookieReputation += 100;
        }
    }

    [System.Serializable]
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

    [System.Serializable]
    public class IngredientComponent
    {
        public int m_ID;
        public string m_Name;
        public Sprite m_Sprite;
    }
}