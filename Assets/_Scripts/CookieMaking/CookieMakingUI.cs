using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CQM.Databases;
using CQM.Components;

public class CookieMakingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_CookieName;
    [SerializeField] private TextMeshProUGUI txt_CookieDescription;
    [SerializeField] private TextMeshProUGUI txt_CookieStats;

    [SerializeField] private int selectedRecipe = -1;

    [SerializeField] private GameObject pref_Recipe;
    [SerializeField] private List<GameObject> currentRecipes;
    [SerializeField] private Transform recipeListParent;

    private ComponentsContainer<CookieDataComponent> _cookieDataComponents;
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private Singleton_InventoryComponent _inventoryData;

    private CookieMakingSystem _cookieMakingSystem;

    private void Awake()
    {
        _cookieDataComponents = Admin.Global.Components.m_CookieData;
        _recipeDataComponents = Admin.Global.Components.m_RecipeData;
        _inventoryData = Admin.Global.Components.m_InventoryComponent;

        _cookieMakingSystem = Admin.Global.Systems.m_CookieMakingSystem;
    }

    private void OnEnable()
    {
        _cookieMakingSystem.OnCreateCookie += UpdateUI;
        _cookieMakingSystem.OnBuyRecipe -= UpdateCookieRecipesUI;
        _cookieMakingSystem.OnBuyRecipe += UpdateCookieRecipesUI;

        if (currentRecipes.Count == 0)
        {
            /*
            List<RecipeData> recipes = admin.cookieDB.m_BoughtRecipeDataList;
            foreach (RecipeData r in recipes)
            {
                GameObject newRecipeUI = Instantiate(pref_Recipe, recipeListParent);
                RecipeShopUI ui = newRecipeUI.GetComponent<RecipeShopUI>();
                ui.SetRecipe(r);
                ui.OnSelectRecipe += SelectRecipe;
                currentRecipes.Add(newRecipeUI);
            }
            if (recipes.Count > 0)
                cookieMakingSystem.SelectRecipe(recipes[0].m_CookieID);
            */
            CreateCookiePrefabs();
        }
        else
        {
            foreach (GameObject r in currentRecipes)
            {
                r.GetComponent<RecipeShopUI>().OnSelectRecipe += SelectRecipe;
            }
        }
    }

    private void OnDisable()
    {
        foreach (GameObject r in currentRecipes)
        {
            r.GetComponent<RecipeShopUI>().OnSelectRecipe -= SelectRecipe;
        }

        _cookieMakingSystem.OnCreateCookie -= UpdateUI;
        //cookieMakingSystem.OnBuyRecipe -= UpdateCookieRecipesUI;
    }

    public void SelectRecipe(ID id)
    {
        _cookieMakingSystem.SelectRecipe(id);
    }

    public void CreateCookie()
    {
        _cookieMakingSystem.CreateCookie();
    }

    public void UpdateUI(ID id)
    {
        CookieDataComponent cookieData = _cookieDataComponents[id];
        txt_CookieName.text = cookieData.m_CookieName;
        txt_CookieDescription.text = cookieData.m_CookieDescription;
        List<QPTag> tags = Admin.Global.Components.m_QuestPieceFunctionalComponents[id].m_Tags;
        txt_CookieStats.text = "Hero Stats: \n";
        foreach (QPTag q in tags)
        {
            txt_CookieStats.text += q.m_Type.ToString() + ": " + q.m_Value + "\n";
        }
    }

    private void UpdateCookieRecipesUI()
    {
        //Debug.Log("Entra update");
        foreach (GameObject g in currentRecipes)
        {
            //g.GetComponent<RecipeShopUI>().OnSelectRecipe -= SelectRecipe;
            Destroy(g);
        }
        currentRecipes.Clear();
        CreateCookiePrefabs();
    }

    private void CreateCookiePrefabs()
    {
        var recipesIds = _inventoryData.m_UnlockedRecipes;
        for (int i = 0; i < recipesIds.Count; i++)
        {
            RecipeDataComponent r = _recipeDataComponents[recipesIds[i]];

            GameObject newRecipeUI = Instantiate(pref_Recipe, recipeListParent);
            RecipeShopUI ui = newRecipeUI.GetComponent<RecipeShopUI>();
            ui.SetRecipe(r);
            ui.OnSelectRecipe += SelectRecipe;
            currentRecipes.Add(newRecipeUI);
        }

        if (recipesIds.Count > 0)
            _cookieMakingSystem.SelectRecipe(_recipeDataComponents[recipesIds[0]].m_PieceID);
    }
}
