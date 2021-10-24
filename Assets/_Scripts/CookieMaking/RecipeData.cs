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
    public int m_CookieID = -1;
    public Image m_Image;
    public Reputation m_Reputation = Reputation.GoodCookieReputation;
    public int price;
    public bool bought = false;
}
