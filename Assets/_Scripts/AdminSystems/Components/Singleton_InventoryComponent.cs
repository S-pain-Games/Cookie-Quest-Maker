using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Databases
{
    [System.Serializable]
    // Storage of all the unlocked pieces and cookies that the player has
    public class Singleton_InventoryComponent
    {
        // Reputation
        public int m_GoodCookieReputation;
        public int m_EvilCookieReputation;

        // List of unlocked word pieces (Actions, Modifiers, Objects)
        public List<ID> m_UnlockedRecipes = new List<ID>();

        // Cookies Inventory
        public List<InventoryItem> m_Pieces = new List<InventoryItem>();

        // Ingredients Inventory
        public List<InventoryItem> m_Ingredients = new List<InventoryItem>();
    }


    [System.Serializable]
    public class InventoryItem
    {
        public ID m_ItemID;
        public int m_Amount = 0;

        public InventoryItem() { }

        public InventoryItem(ID itemID, int amount)
        {
            m_ItemID = itemID;
            m_Amount = amount;
        }
    }


    public struct ItemData
    {
        public ID m_ItemID;
        public int m_Amount;

        public ItemData(ID itemID, int amount)
        {
            m_ItemID = itemID;
            m_Amount = amount;
        }
    }


    [System.Serializable]
    public class IngredientComponent
    {
        public ID m_ID;
        public string m_Name;
        public Sprite m_Sprite;
    }
}