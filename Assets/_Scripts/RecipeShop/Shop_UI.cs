using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CQM.Components;
using DG.Tweening;

public class Shop_UI : MonoBehaviour
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private ComponentsContainer<QuestPieceFunctionalComponent> _pieceDataComponents;
    private ComponentsContainer<QuestPieceUIComponent> _recipeUiDataComponents;
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

    private List<RecipeDataComponent> _recipesToBuy;
    private List<IngredientComponent> _ingredientsToBuy;

    private int idx_recipe, idx_ingredient;

    [SerializeField] private TextMeshProUGUI text_good_rep;
    [SerializeField] private TextMeshProUGUI text_evil_rep;

    [SerializeField] private TextMeshProUGUI text_stat_1;
    [SerializeField] private TextMeshProUGUI text_stat_2;
    [SerializeField] private TextMeshProUGUI text_stat_3;

    [SerializeField] private TextMeshProUGUI text_good_price;
    [SerializeField] private TextMeshProUGUI text_evil_price;

    [SerializeField] private TextMeshProUGUI text_recipe_name;
    [SerializeField] private TextMeshProUGUI text_buy;

    [SerializeField] private Image image_recipe;

    [SerializeField] private GameObject image_good_money;
    [SerializeField] private GameObject image_evil_money;

    [SerializeField] private GameObject image_evith;
    [SerializeField] private GameObject image_nu;

    private Color colorWhite = new Color(0.5254902f, 0.3098039f, 0.1607f);
    private Color colorRed = new Color(0.6320754f, 0.06857423f, 0.1578471f);


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

        _recipesToBuy = new List<RecipeDataComponent>();
        _ingredientsToBuy = new List<IngredientComponent>();

        //EventVoid evt = admin.EventSystem.GetCallback<EventVoid>(new ID("shop_sys"), new ID("update_shop_ui"));
        Event<bool> evt = admin.EventSystem.GetCallback<Event<bool>>(new ID("shop_sys"), new ID("update_shop_ui"));
        evt.OnInvoked += UpdateUI;
    }

    private void OnEnable()
    {
        LoadRecipeLists();
    }

    private void LoadRecipeLists()
    {
        _recipesToBuy.Clear();
        _ingredientsToBuy.Clear();

        idx_recipe = 0;
        idx_ingredient = 0;

        List<RecipeDataComponent> recipes = _recipeDataComponents.GetList();
        for (int i=0; i< recipes.Count; i++)
        {
            RecipeDataComponent recipe = recipes[i];
            if (!_inventoryData.m_UnlockedRecipes.Contains(recipe.m_ID) 
                && _pieceDataComponents.GetComponentByID(recipe.m_ID).m_Type == QuestPieceFunctionalComponent.PieceType.Cookie)
            {
                _recipesToBuy.Add(recipe);
            }
        }

        List<IngredientComponent> ingredients = _ingredientsDataComponents.GetList();
        for(int i=0; i<ingredients.Count; i++)
        {
            IngredientComponent ingredient = ingredients[i];

            if(ingredient.m_ID != new ID("masa_de_galletas_encantada"))
            {
                _ingredientsToBuy.Add(ingredient);
            }
        }

        UpdateUI(false);
    }

    private void UpdateUI(bool anim)
    {
        
        if(currentRecipeType == RecipeTypes.PieceRecipes)
        {
            if (_recipesToBuy.Count > 0)
                _selectedPieceID = _recipesToBuy[idx_recipe].m_ID;
            else
                return;
        }
        else if(currentRecipeType == RecipeTypes.IngredientRecipes)
        {
            if (_ingredientsToBuy.Count > 0)
                _selectedPieceID = _ingredientsToBuy[idx_ingredient].m_ID;
            else
                return;
        }
       
        text_good_rep.text = _inventoryData.m_GoodKarma.ToString();
        text_evil_rep.text = _inventoryData.m_EvilKarma.ToString();

        //0: Convice, 1: Help, 2: Harm 
        text_stat_1.text = "0";
        text_stat_2.text = "0";
        text_stat_3.text = "0";

        image_good_money.SetActive(false);
        image_nu.SetActive(false);
        image_evil_money.SetActive(false);
        image_evith.SetActive(false);
        text_good_price.text = "";
        text_evil_price.text = "";
        text_evil_price.color = colorWhite;
        text_good_price.color = colorWhite;

        if (currentRecipeType == RecipeTypes.PieceRecipes)
        {
            QuestPieceFunctionalComponent piece = _pieceDataComponents.GetComponentByID(_selectedPieceID);
            RecipeDataComponent recipe = _recipeDataComponents.GetComponentByID(_selectedPieceID);
            QuestPieceUIComponent pieceUI = _recipeUiDataComponents.GetComponentByID(_selectedPieceID);

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

            if(recipe.m_Price_Good > 0)
            {
                image_good_money.SetActive(true);
                image_nu.SetActive(true);
                text_good_price.text = recipe.m_Price_Good.ToString();

                if(_inventoryData.m_GoodKarma < recipe.m_Price_Good)
                {
                    text_good_price.color = colorRed;
                }
                else
                    text_good_price.color = colorWhite;
            }
            if(recipe.m_Price_Evil > 0)
            {
                image_evil_money.SetActive(true);
                image_evith.SetActive(true);
                text_evil_price.text = recipe.m_Price_Evil.ToString();

                if (_inventoryData.m_EvilKarma < recipe.m_Price_Evil)
                {
                    text_evil_price.color = colorRed;
                }
                else
                    text_evil_price.color = colorWhite;
            }

            if (!_inventoryData.m_UnlockedRecipes.Contains(_selectedPieceID))
                text_buy.text = "¡Comprar!";
            else
                text_buy.text = "¡Comprado!";

            image_recipe.sprite = pieceUI.m_ShopRecipeSprite;
        }
        else
        {
            IngredientComponent ingredient = _ingredientsDataComponents.GetComponentByID(_selectedPieceID);
            text_recipe_name.text = ingredient.m_Name;

            if (ingredient.m_Price_Good > 0)
            {
                image_good_money.SetActive(true);
                image_nu.SetActive(true);
                text_good_price.text = ingredient.m_Price_Good.ToString();

                if (_inventoryData.m_GoodKarma < ingredient.m_Price_Good)
                {
                    text_good_price.color = colorRed;
                }
                else
                    text_good_price.color = colorWhite;
            }
            if (ingredient.m_Price_Evil > 0)
            {
                image_evil_money.SetActive(true);
                image_evith.SetActive(true);
                text_evil_price.text = ingredient.m_Price_Evil.ToString();

                if (_inventoryData.m_EvilKarma < ingredient.m_Price_Evil)
                {
                    text_evil_price.color = colorRed;
                }
                else
                    text_evil_price.color = colorWhite;
            }

            image_recipe.sprite = ingredient.m_Sprite;
        }
        image_recipe.preserveAspect = true;

        if(anim)
        {
            text_evil_price.transform.DOScale(2.0f, 0.15f).OnComplete(() => text_evil_price.transform.DOScale(1.0f, 0.15f));
            text_good_price.transform.DOScale(2.0f, 0.15f).OnComplete(() => text_good_price.transform.DOScale(1.0f, 0.15f));
            text_good_rep.transform.DOScale(2.0f, 0.15f).OnComplete(() => text_good_rep.transform.DOScale(1.0f, 0.15f));
            text_evil_rep.transform.DOScale(2.0f, 0.15f).OnComplete(() => text_evil_rep.transform.DOScale(1.0f, 0.15f));
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

        if(currentRecipeType == RecipeTypes.PieceRecipes)
        {
            idx_recipe += amount;
            idx_recipe = Mathf.Clamp(idx_recipe, 0, _recipesToBuy.Count - 1);
        }
        else if(currentRecipeType == RecipeTypes.IngredientRecipes)
        {
            idx_ingredient += amount;
            idx_ingredient = Mathf.Clamp(idx_ingredient, 0, _ingredientsToBuy.Count-1);
        }

        UpdateUI(false);
    }

    public void SetPieceRecipeTypes()
    {
        if(currentRecipeType != RecipeTypes.PieceRecipes)
        {
            currentRecipeType = RecipeTypes.PieceRecipes;
            UpdateUI(false);
        }
    }

    public void SetIngredientRecipeTypes()
    {
        if(currentRecipeType != RecipeTypes.IngredientRecipes)
        {
            currentRecipeType = RecipeTypes.IngredientRecipes;
            text_buy.text = "¡Comprar!";
            UpdateUI(false);
        }
    }
}
