using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookieMakingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_CookieName;
    [SerializeField] private TextMeshProUGUI txt_CookieDescription;
    [SerializeField] private TextMeshProUGUI txt_CookieStats;

    [SerializeField] private int selectedRecipe = -1;

    [SerializeField] private GameObject pref_Recipe;
    [SerializeField] private List<GameObject> currentRecipes;
    [SerializeField] private Transform recipeListParent;

    private Admin admin;
    CookieMakingSystem cookieMakingSystem;

    private void Awake()
    {
        admin = Admin.g_Instance;
        cookieMakingSystem = admin.cookieMakingSystem;
    }

    private void OnEnable()
    {
        cookieMakingSystem.OnCreateCookie += UpdateUI;

        if (currentRecipes.Count == 0)
        {
            List<RecipeData> recipes = admin.cookieDB.m_BoughtRecipeDataList;
            foreach (RecipeData r in recipes)
            {
                GameObject newRecipeUI = Instantiate(pref_Recipe, recipeListParent);
                RecipeShopUI ui = newRecipeUI.GetComponent<RecipeShopUI>();
                ui.myRecipe = r;
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
    }

    private void OnDisable()
    {
        foreach (GameObject r in currentRecipes)
        {
            r.GetComponent<RecipeShopUI>().OnSelectRecipe -= SelectRecipe;
        }

        cookieMakingSystem.OnCreateCookie -= UpdateUI;
    }

    public void SelectRecipe(int id)
    {
        cookieMakingSystem.SelectRecipe(id);
    }

    public void CreateCookie()
    {
        cookieMakingSystem.CreateCookie();
    }

    public void UpdateUI(int id)
    {
        CookieData cookieData;
        if(admin.cookieDB.m_CookieDataDB.TryGetValue(id, out cookieData))
        {
            txt_CookieName.text = cookieData.m_CookieName;
            txt_CookieDescription.text = cookieData.m_CookieDescription;

        }
        
    }
}
