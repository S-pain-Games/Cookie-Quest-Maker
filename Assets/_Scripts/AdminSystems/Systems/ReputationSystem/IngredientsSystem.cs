public class IngredientsSystem
{
    private PlayerBakingIngredients _ingr;

    public void Initialize(PlayerBakingIngredients ingr)
    {
        _ingr = ingr;
        LoadData();
    }

    private void LoadData()
    {
        // This is where we should load the player data
        _ingr.m_CookieDough += 5;
    }

    public void AddCookieDough(int amount) => _ingr.m_CookieDough += amount;
    public void RemoveCookieDough(int amount) => _ingr.m_CookieDough += amount;
}
