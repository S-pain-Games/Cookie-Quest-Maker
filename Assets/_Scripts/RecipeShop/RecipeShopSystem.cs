using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using CQM.Databases;

public class RecipeShopSystem : MonoBehaviour
{
    [SerializeField] private int selectedRecipeId = -1;

    [SerializeField] private GameObject pref_Recipe;
    [SerializeField] private List<GameObject> currentRecipes;
    [SerializeField] private Transform recipeListParent;

    [SerializeField] private TextMeshProUGUI textGoodR;
    [SerializeField] private TextMeshProUGUI textEvilR;
    [SerializeField] private TextMeshProUGUI textPrice;

    private CookieDB _cookiesData;
    private InventoryComponent _inventoryData;

    private EventVoid _enableCharMovCmd;
    private EventVoid _disableCharMovCmd;

    private EventVoid _buyRecipeCmdREFACTOR;
    private Event<InventorySys_ChangeReputationEvtArgs> _changeRepCmd;
    private Event<int> _unlockRecipeCmd;

    private void Awake()
    {
        var admin = Admin.Global;
        _cookiesData = admin.Database.Cookies;
        _inventoryData = admin.Database.Player.Inventory;

        _enableCharMovCmd = admin.EventSystem.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        _disableCharMovCmd = admin.EventSystem.GetCommandByName<EventVoid>("character_sys", "disable_movement");
        _buyRecipeCmdREFACTOR = admin.EventSystem.GetCommandByName<EventVoid>("cookie_making_sys", "buy_recipe");
        _changeRepCmd = admin.EventSystem.GetCommandByName<Event<InventorySys_ChangeReputationEvtArgs>>("inventory_sys", "change_reputation");
        _unlockRecipeCmd = admin.EventSystem.GetCommandByName<Event<int>>("inventory_sys", "unlock_recipe");
    }

    private void OnEnable()
    {
        _disableCharMovCmd.Invoke();

        if (currentRecipes.Count == 0)
        {
            List<RecipeData> recipes = _cookiesData.m_RecipeDataDB.Values.ToList(); // Slow but good enough
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
        selectedRecipeId = id;
        UpdatePrice();
    }

    //TODO: Update cookie making available recipes
    public void BuyRecipe()
    {
        if (selectedRecipeId == -1) return;

        _cookiesData.m_RecipeDataDB.TryGetValue(selectedRecipeId, out RecipeData recipe);

        if (recipe == null || _inventoryData.m_UnlockedRecipes.Contains(selectedRecipeId)) return;

        bool enoughMoneyToBuy = false;
        if (recipe.m_ReputationTypePrice == Reputation.GoodCookieReputation)
            enoughMoneyToBuy = _inventoryData.m_GoodCookieReputation >= recipe.m_Price;
        else if (recipe.m_ReputationTypePrice == Reputation.EvilCookieReputation)
            enoughMoneyToBuy = _inventoryData.m_EvilCookieReputation >= recipe.m_Price;

        if (enoughMoneyToBuy)
        {
            _changeRepCmd.Invoke(new InventorySys_ChangeReputationEvtArgs(recipe.m_ReputationTypePrice, -recipe.m_Price));
            _unlockRecipeCmd.Invoke(recipe.m_PieceID);
            _buyRecipeCmdREFACTOR.Invoke();
            UpdateTexts();
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
        _cookiesData.m_RecipeDataDB.TryGetValue(selectedRecipeId, out recipe);
        if (recipe != null)
            textPrice.text = "Price: " + recipe.m_Price;
        else
            textPrice.text = "";
    }
}
