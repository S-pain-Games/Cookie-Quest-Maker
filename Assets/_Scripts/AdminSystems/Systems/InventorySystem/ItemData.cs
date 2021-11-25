namespace CQM.Databases
{
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
}