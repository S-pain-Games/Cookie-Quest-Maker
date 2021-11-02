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

    private CookieDB cookiesData;
    CookieMakingSystem cookieMakingSystem;

    private void Awake()
    {
        cookiesData = Admin.Global.Database.Cookies;
        cookieMakingSystem = Admin.Global.Systems.m_CookieMakingSystem;
    }

    private void OnEnable()
    {
        cookieMakingSystem.OnCreateCookie += UpdateUI;
        cookieMakingSystem.OnBuyRecipe -= UpdateCookieRecipesUI;
        cookieMakingSystem.OnBuyRecipe += UpdateCookieRecipesUI;

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

        cookieMakingSystem.OnCreateCookie -= UpdateUI;
        //cookieMakingSystem.OnBuyRecipe -= UpdateCookieRecipesUI;
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
        if (cookiesData.m_CookieDataDB.TryGetValue(id, out cookieData))
        {
            txt_CookieName.text = cookieData.m_CookieName;
            txt_CookieDescription.text = cookieData.m_CookieDescription;
            List<QPTag> tags = Admin.Global.Database.Quests.GetQuestPieceComponent<QuestPiece>(id).m_Tags;
            txt_CookieStats.text = "Hero Stats: \n";
            foreach (QPTag q in tags)
            {
                txt_CookieStats.text += q.m_Type.ToString() + ": " + q.m_Value + "\n";
            }
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
        List<RecipeData> recipes = cookiesData.m_BoughtRecipeDataList;
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
    }
}
