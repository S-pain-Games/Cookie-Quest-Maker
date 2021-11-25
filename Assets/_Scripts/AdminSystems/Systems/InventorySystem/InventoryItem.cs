namespace CQM.Components
{
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
}