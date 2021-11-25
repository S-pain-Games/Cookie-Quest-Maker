using UnityEngine;

namespace CQM.Components
{
    [System.Serializable]
    public class IngredientComponent
    {
        public ID m_ID;
        public string m_Name;
        public Sprite m_Sprite;

        public Reputation m_ReputationTypePrice = Reputation.GoodCookieReputation;
        public int m_Price_Good;
        public int m_Price_Evil;
    }
}