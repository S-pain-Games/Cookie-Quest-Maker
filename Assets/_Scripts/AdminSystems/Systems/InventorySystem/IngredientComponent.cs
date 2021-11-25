using UnityEngine;

namespace CQM.Components
{
    [System.Serializable]
    public class IngredientComponent
    {
        public ID m_ID;
        public string m_Name;
        public Sprite m_Sprite;

        public Karma m_ReputationTypePrice = Karma.GoodKarma;
        public int m_Price_Good;
        public int m_Price_Evil;
    }
}