using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeShopSystem : MonoBehaviour
{
    [SerializeField] private int selectedRecipe = -1;

    [SerializeField] private GameObject pref_Recipe;
    [SerializeField] private List<GameObject> currentRecipes;
    [SerializeField] private Transform recipeListParent;

    [SerializeField] private TextMeshProUGUI textGoodR;
    [SerializeField] private TextMeshProUGUI textEvilR;
    [SerializeField] private TextMeshProUGUI textPrice;

    private CookieDB _cookiesData;
    private InventoryData _inventoryData;

    private EventVoid _enableCharMovCmd;
    private EventVoid _disableCharMovCmd;

    private EventVoid _buyRecipeCmd;

    private void Awake()
    {
        var admin = Admin.Global;
        _cookiesData = admin.Database.Cookies;
        _inventoryData = admin.Database.Player.Inventory;
        _enableCharMovCmd = admin.EventSystem.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        _disableCharMovCmd = admin.EventSystem.GetCommandByName<EventVoid>("character_sys", "disable_movement");
        _buyRecipeCmd = admin.EventSystem.GetCommandByName<EventVoid>("cookie_making_sys", "buy_recipe");
    }

    private void OnEnable()
    {
        _disableCharMovCmd.Invoke();

        if (currentRecipes.Count == 0)
        {
            List<RecipeData> recipes = _cookiesData.m_RecipeDataList;
            foreach (RecipeData r in recipes)
            {
                GameObject newRecipeUI = Instantiate(pref_Recipe, recipeListParent);
                RecipeShopUI ui = newRecipeUI.GetComponent<RecipeShopUI>();
                ui.SetRecipe(r);
                ui.OnSelectRecipe += SelectRecipe;
                currentRecipes.Add(newRecipeUI);
            }
        }
        else
        {
            foreach (GameObject r in currentRecipes)
            {
                r.GetComponent<RecipeShopUI>().OnSelectRecipe += SelectRecipe;
            }
        }
        UpdateTexts();
    }

    private void OnDisable()
    {
        _enableCharMovCmd.Invoke();
        foreach (GameObject r in currentRecipes)
        {
            r.GetComponent<RecipeShopUI>().OnSelectRecipe -= SelectRecipe;
        }
    }

    public void SelectRecipe(int id)
    {
        selectedRecipe = id;
        UpdatePrice();
    }

    //TODO: Update cookie making available recipes
    public void BuyRecipe()
    {
        if (selectedRecipe != -1)
        {
            RecipeData recipe;
            _cookiesData.m_RecipeDataDB.TryGetValue(selectedRecipe, out recipe);

            if (recipe != null)
            {
                if (!recipe.bought)
                {
                    if (recipe.m_Reputation == Reputation.GoodCookieReputation)
                    {
                        bool bought = Admin.Global.Systems.m_InventorySystem.RemoveGoodCookieRep(recipe.price); // TODO
                        if (bought)
                        {
                            _cookiesData.AddBoughtCookie(recipe.m_CookieID, recipe);
                            _buyRecipeCmd.Invoke();
                            UpdateTexts();
                        }

                    }
                    else if (recipe.m_Reputation == Reputation.EvilCookieReputation)
                    {
                        bool bought = Admin.Global.Systems.m_InventorySystem.RemoveEvilCookieRep(recipe.price);
                        if (bought)
                        {
                            _cookiesData.AddBoughtCookie(recipe.m_CookieID, recipe);
                            _buyRecipeCmd.Invoke();
                            UpdateTexts();
                        }
                    }
                }
            }
        }
    }

    private void UpdateTexts()
    {
        textGoodR.text = "Good Reputation: " + _inventoryData.m_GoodCookieReputation;
        textEvilR.text = "Evil Reputation: " + _inventoryData.m_EvilCookieReputation;
        UpdatePrice();
    }

    private void UpdatePrice()
    {
        RecipeData recipe;
        _cookiesData.m_RecipeDataDB.TryGetValue(selectedRecipe, out recipe);
        if (recipe != null)
            textPrice.text = "Price: " + recipe.price;
        else
            textPrice.text = "";
    }
}
