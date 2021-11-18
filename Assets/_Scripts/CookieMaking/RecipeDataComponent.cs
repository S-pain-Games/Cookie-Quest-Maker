using CQM.Databases;
using System.Collections.Generic;
using UnityEngine.UI;

public enum Reputation
{
    GoodCookieReputation,
    EvilCookieReputation
}

[System.Serializable]
public class RecipeDataComponent
{
    public ID m_ID;

    public string m_RecipeName = "Unnamed";
    public string m_RecipeDescription = "No description";
    public Image m_Image;

    public Reputation m_ReputationTypePrice = Reputation.GoodCookieReputation;
    public int m_Price_Good;
    public int m_Price_Evil;

    public List<InventoryItem> m_IngredientsList = new List<InventoryItem>();

    public ID m_PieceID;
}
