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

    private Admin admin;

    private Event<ItemData> _addCookieCommand;

    [SerializeField] private TextMeshProUGUI textGoodR;
    [SerializeField] private TextMeshProUGUI textEvilR;
    [SerializeField] private TextMeshProUGUI textPrice;

    private EventVoid _enableCharMovCmd;
    private EventVoid _disableCharMovCmd;

    private void Awake()
    {
        admin = Admin.g_Instance;
        _enableCharMovCmd = admin.gameEventSystem.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        _disableCharMovCmd = admin.gameEventSystem.GetCommandByName<EventVoid>("character_sys", "disable_movement");
    }

    private void OnEnable()
    {
        _disableCharMovCmd.Invoke();

        if (currentRecipes.Count == 0)
        {
            List<RecipeData> recipes = admin.cookieDB.m_RecipeDataList;
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
            admin.cookieDB.m_RecipeDataDB.TryGetValue(selectedRecipe, out recipe);

            if (recipe != null)
            {
                if (!recipe.bought)
                {
                    if (recipe.m_Reputation == Reputation.GoodCookieReputation)
                    {
                        bool bought = admin.inventorySystem.RemoveGoodCookieRep(recipe.price);
                        if (bought)
                        {
                            admin.cookieDB.AddBoughtCookie(recipe.m_CookieID, recipe);
                            admin.cookieMakingSystem.BuyRecipe();
                            UpdateTexts();
                        }

                    }
                    else if (recipe.m_Reputation == Reputation.EvilCookieReputation)
                    {
                        bool bought = admin.inventorySystem.RemoveEvilCookieRep(recipe.price);
                        if (bought)
                        {
                            admin.cookieDB.AddBoughtCookie(recipe.m_CookieID, recipe);
                            admin.cookieMakingSystem.BuyRecipe();
                            UpdateTexts();
                        }
                    }
                }
            }
        }
    }

    private void UpdateTexts()
    {
        textGoodR.text = "Good Reputation: " + admin.inventoryData.m_GoodCookieReputation;
        textEvilR.text = "Evil Reputation: " + admin.inventoryData.m_EvilCookieReputation;
        UpdatePrice();
    }

    private void UpdatePrice()
    {
        RecipeData recipe;
        admin.cookieDB.m_RecipeDataDB.TryGetValue(selectedRecipe, out recipe);
        if (recipe != null)
            textPrice.text = "Price: " + recipe.price;
        else
            textPrice.text = "";
    }
}
