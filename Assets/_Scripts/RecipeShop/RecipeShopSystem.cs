using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeShopSystem : MonoBehaviour
{
    [SerializeField] private int selectedRecipe = -1;

    [SerializeField] private GameObject pref_Recipe;
    [SerializeField] private List<GameObject> currentRecipes;
    [SerializeField] private Transform recipeListParent;

    private Admin admin;

    private void Awake()
    {
        admin = Admin.g_Instance;
    }

    private void OnEnable()
    {
        if (currentRecipes.Count == 0)
        {
            List<RecipeData> recipes = admin.cookieDB.m_RecipeDataList;
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
    }

    public void SelectRecipe(int id)
    {
        selectedRecipe = id;
    }

    //TODO: Update cookie making available recipes
    public void BuyRecipe()
    {
        if(selectedRecipe != -1)
        {
            RecipeData recipe;
            admin.cookieDB.m_RecipeDataDB.TryGetValue(selectedRecipe, out recipe);

            if(recipe != null)
            {
                if(!recipe.bought)
                {
                    if (recipe.m_Reputation == Reputation.GoodCookieReputation)
                    {
                        bool bought = admin.reputationSystem.RemoveGoodCookieRep(recipe.price);
                        if(bought)
                        {
                            admin.cookieDB.AddBoughtCookie(recipe.m_CookieID, recipe);
                        }
                        
                    }
                    else if (recipe.m_Reputation == Reputation.EvilCookieReputation)
                    {
                        bool bought = admin.reputationSystem.RemoveEvilCookieRep(recipe.price);
                        if (bought)
                        {
                            admin.cookieDB.AddBoughtCookie(recipe.m_CookieID, recipe);
                        } 
                    }
                }
            }
        }
    }
}
