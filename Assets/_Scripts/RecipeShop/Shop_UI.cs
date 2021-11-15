using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CQM.Components;

public class Shop_UI : MonoBehaviour
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private ComponentsContainer<QuestPieceFunctionalComponent> _pieceDataComponents;
    private ComponentsContainer<UIQuestPieceComponent> _recipeUiDataComponents;
    private ComponentsContainer<IngredientComponent> _ingredientsDataComponents;
    private Singleton_InventoryComponent _inventoryData;

    [SerializeField] private bool goodRecipeShop = true;

    private enum RecipeTypes
    {
        PieceRecipes,
        IngredientRecipes
    }

    private RecipeTypes currentRecipeType = RecipeTypes.PieceRecipes;

    private ID _selectedPieceID;

    private Event<ID> _buyRecipe;
    private Event<ID> _buyIngredient;

    private List<RecipeDataComponent> _goodRecipesToBuy;
    private List<RecipeDataComponent> _evilRecipesToBuy;
    private List<IngredientComponent> _goodIngredientsToBuy;
    private List<IngredientComponent> _evilIngredientsToBuy;

    private int idx_good, idx_evil, idx_good_ingredient, idx_evil_ingredient;

    [SerializeField] private TextMeshProUGUI text_good_rep;
    [SerializeField] private TextMeshProUGUI text_evil_rep;

    [SerializeField] private TextMeshProUGUI text_stat_1;
    [SerializeField] private TextMeshProUGUI text_stat_2;
    [SerializeField] private TextMeshProUGUI text_stat_3;

    [SerializeField] private TextMeshProUGUI text_price;
    [SerializeField] private TextMeshProUGUI text_recipe_name;
    [SerializeField] private TextMeshProUGUI text_buy;

    [SerializeField] private Image image_recipe;

    private void Awake()
    {
        var admin = Admin.Global;
        _recipeDataComponents = admin.Components.GetComponentContainer<RecipeDataComponent>();
        _inventoryData = admin.Components.m_InventoryComponent;
        _pieceDataComponents = admin.Components.m_QuestPieceFunctionalComponents;
        _recipeUiDataComponents = admin.Components.m_QuestPieceUIComponent;
        _ingredientsDataComponents = admin.Components.GetComponentContainer<IngredientComponent>();

        _buyRecipe = admin.EventSystem.GetCommandByName<Event<ID>>("shop_sys", "buy_recipe");
        _buyIngredient = admin.EventSystem.GetCommandByName<Event<ID>>("shop_sys", "buy_ingredient");

        _goodRecipesToBuy = new List<RecipeDataComponent>();
        _evilRecipesToBuy = new List<RecipeDataComponent>();
        _goodIngredientsToBuy = new List<IngredientComponent>();
        _evilIngredientsToBuy = new List<IngredientComponent>();

        EventVoid evt = admin.EventSystem.GetCallback<EventVoid>(new ID("shop_sys"), new ID("update_shop_ui"));
        evt.OnInvoked += UpdateUI;
    }

    private void OnEnable()
    {
        LoadRecipeLists();
    }

    private void LoadRecipeLists()
    {
        _goodRecipesToBuy.Clear();
        _evilRecipesToBuy.Clear();
        _goodIngredientsToBuy.Clear();
        _evilIngredientsToBuy.Clear();

        idx_good = 0;
        idx_evil = 0;
        idx_good_ingredient = 0;
        idx_evil_ingredient = 0;

        List<RecipeDataComponent> recipes = _recipeDataComponents.GetList();
        for (int i=0; i< recipes.Count; i++)
        {
            RecipeDataComponent recipe = recipes[i];
            if (!_inventoryData.m_UnlockedRecipes.Contains(recipe.m_ID))
            {
                if(recipe.m_ReputationTypePrice == Reputation.EvilCookieReputation)
                    _evilRecipesToBuy.Add(recipe);
                else
                    _goodRecipesToBuy.Add(recipe);
            }
        }

        List<IngredientComponent> ingredients = _ingredientsDataComponents.GetList();
        for(int i=0; i<ingredients.Count; i++)
        {
            IngredientComponent ingredient = ingredients[i];

            if(ingredient.m_ID != new ID("masa_de_galletas_encantada"))
            {
                if (ingredient.m_ReputationTypePrice == Reputation.EvilCookieReputation)
                    _evilIngredientsToBuy.Add(ingredient);
                else
                    _goodIngredientsToBuy.Add(ingredient);
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (goodRecipeShop)
            if(currentRecipeType == RecipeTypes.PieceRecipes)
            {
                if (_goodRecipesToBuy.Count > 0)
                    _selectedPieceID = _goodRecipesToBuy[idx_good].m_ID;
                else
                    return;
            }
            else if(currentRecipeType == RecipeTypes.IngredientRecipes)
            {
                if (_goodIngredientsToBuy.Count > 0)
                    _selectedPieceID = _goodIngredientsToBuy[idx_good_ingredient].m_ID;
                else
                    return;
            }
        else
            {
                if (currentRecipeType == RecipeTypes.PieceRecipes)
                {
                    if (_evilRecipesToBuy.Count > 0)
                        _selectedPieceID = _evilRecipesToBuy[idx_good].m_ID;
                    else
                        return;
                }
                else if(currentRecipeType == RecipeTypes.IngredientRecipes)
                {
                    if (_evilIngredientsToBuy.Count > 0)
                        _selectedPieceID = _evilIngredientsToBuy[idx_evil_ingredient].m_ID;
                    else
                        return;
                }
            }
        

        text_good_rep.text = _inventoryData.m_GoodCookieReputation.ToString();
        text_evil_rep.text = _inventoryData.m_EvilCookieReputation.ToString();

        //0: Convice, 1: Help, 2: Harm 
        text_stat_1.text = "0";
        text_stat_2.text = "0";
        text_stat_3.text = "0";

        if(currentRecipeType == RecipeTypes.PieceRecipes)
        {
            QuestPieceFunctionalComponent piece = _pieceDataComponents.GetComponentByID(_selectedPieceID);
            RecipeDataComponent recipe = _recipeDataComponents.GetComponentByID(_selectedPieceID);
            UIQuestPieceComponent pieceUI = _recipeUiDataComponents.GetComponentByID(_selectedPieceID);

            for (int i = 0; i < piece.m_Tags.Count; i++)
            {
                if (piece.m_Tags[i].m_Type == QPTag.TagType.Convince)
                    text_stat_1.text = piece.m_Tags[i].m_Value.ToString();
                else if (piece.m_Tags[i].m_Type == QPTag.TagType.Help)
                    text_stat_2.text = piece.m_Tags[i].m_Value.ToString();
                else if (piece.m_Tags[i].m_Type == QPTag.TagType.Harm)
                    text_stat_3.text = piece.m_Tags[i].m_Value.ToString();
            }

            text_recipe_name.text = recipe.m_RecipeName;
            text_price.text = recipe.m_Price.ToString();
            if (!_inventoryData.m_UnlockedRecipes.Contains(_selectedPieceID))
                text_buy.text = "¡Comprar!";
            else
                text_buy.text = "¡Comprado!";

            image_recipe.sprite = pieceUI.m_Sprite;
        }
        else
        {
            IngredientComponent ingredient = _ingredientsDataComponents.GetComponentByID(_selectedPieceID);
            text_recipe_name.text = ingredient.m_Name;
            text_price.text = ingredient.m_Price.ToString();

            image_recipe.sprite = ingredient.m_Sprite;
        }
        
    }

    public void BuyRecipe()
    {
        if(currentRecipeType == RecipeTypes.PieceRecipes)
        {
            _buyRecipe.Invoke(_selectedPieceID);
        }
        else if(currentRecipeType == RecipeTypes.IngredientRecipes)
        {
            _buyIngredient.Invoke(_selectedPieceID);
        }
    }

    public void NextItem(bool next)
    {
        int amount = 1;
        if (!next)
            amount = -1;

        if(goodRecipeShop)
        {
            if(currentRecipeType == RecipeTypes.PieceRecipes)
            {
                idx_good += amount;
                idx_good = Mathf.Clamp(idx_good, 0, _goodRecipesToBuy.Count - 1);
            }
            else if(currentRecipeType == RecipeTypes.IngredientRecipes)
            {
                idx_good_ingredient += amount;
                idx_good_ingredient = Mathf.Clamp(idx_good_ingredient, 0, _goodIngredientsToBuy.Count-1);
            }
        }
        else
        {
            if (currentRecipeType == RecipeTypes.PieceRecipes)
            {
                idx_evil += amount;
                idx_evil = Mathf.Clamp(idx_evil, 0, _evilRecipesToBuy.Count - 1);
            }
            else if (currentRecipeType == RecipeTypes.IngredientRecipes)
            {
                idx_evil_ingredient += amount;
                idx_evil_ingredient = Mathf.Clamp(idx_evil_ingredient, 0, _evilIngredientsToBuy.Count - 1);
            }
        }

        UpdateUI();
    }

    public void SetPieceRecipeTypes()
    {
        if(currentRecipeType != RecipeTypes.PieceRecipes)
        {
            currentRecipeType = RecipeTypes.PieceRecipes;
            UpdateUI();
        }
    }

    public void SetIngredientRecipeTypes()
    {
        if(currentRecipeType != RecipeTypes.IngredientRecipes)
        {
            currentRecipeType = RecipeTypes.IngredientRecipes;
            text_buy.text = "¡Comprar!";
            UpdateUI();
        }
    }
}
