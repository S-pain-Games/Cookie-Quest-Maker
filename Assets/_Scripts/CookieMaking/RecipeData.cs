using UnityEngine.UI;

public enum Reputation
{
    GoodCookieReputation,
    EvilCookieReputation
}

[System.Serializable]
public class RecipeData
{
    public string m_RecipeName = "Unnamed";
    public string m_RecipeDescription = "No description";
    public Image m_Image;

    public Reputation m_ReputationTypePrice = Reputation.GoodCookieReputation;
    public int m_Price;

    public int m_PieceID = -1;
}
