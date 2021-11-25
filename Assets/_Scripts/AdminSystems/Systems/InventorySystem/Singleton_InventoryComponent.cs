using System.Collections;
using System.Collections.Generic;

namespace CQM.Components
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
}