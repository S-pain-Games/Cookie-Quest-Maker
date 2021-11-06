using CQM.Databases;
using System.Collections.Generic;
using UnityEngine.UI;

public enum Reputation
{
    GoodCookieReputation,
    EvilCookieReputation
}

[System.Serializable]
public class RecipeData
{
    public int m_ParentID;

    public string m_RecipeName = "Unnamed";
    public string m_RecipeDescription = "No description";
    public Image m_Image;

    public Reputation m_ReputationTypePrice = Reputation.GoodCookieReputation;
    public int m_Price;

    public List<InventoryItem> m_IngredientsList = new List<InventoryItem>();

    public int m_PieceID = -1;
}
