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
    private Singleton_InventoryComponent _inventoryData;

    [SerializeField] private bool goodRecipeShop = true;

    private ID _selectedPieceID;

    private Event<ID> _buyRecipe;

    private List<RecipeDataComponent> _goodRecipesToBuy;
    private List<RecipeDataComponent> _evilRecipesToBuy;

    private int idx_good, idx_evil;

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

        _buyRecipe = admin.EventSystem.GetCommandByName<Event<ID>>("shop_sys", "buy_recipe");

        _goodRecipesToBuy = new List<RecipeDataComponent>();
        _evilRecipesToBuy = new List<RecipeDataComponent>();

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

        idx_good = 0;
        idx_evil = 0;

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

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (goodRecipeShop)
            if (_goodRecipesToBuy.Count > 0)
                _selectedPieceID = _goodRecipesToBuy[idx_good].m_ID;
            else
                return;
        else
            if (_evilRecipesToBuy.Count > 0)
                _selectedPieceID = _evilRecipesToBuy[idx_good].m_ID;
            else
                return;

        text_good_rep.text = _inventoryData.m_GoodCookieReputation.ToString();
        text_evil_rep.text = _inventoryData.m_EvilCookieReputation.ToString();

        QuestPieceFunctionalComponent piece = _pieceDataComponents.GetComponentByID(_selectedPieceID);
        RecipeDataComponent recipe = _recipeDataComponents.GetComponentByID(_selectedPieceID);
        UIQuestPieceComponent pieceUI = _recipeUiDataComponents.GetComponentByID(_selectedPieceID);

        //0: Convice, 1: Help, 2: Harm 
        text_stat_1.text = "0";
        text_stat_2.text = "0";
        text_stat_3.text = "0";

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
        if(!_inventoryData.m_UnlockedRecipes.Contains(_selectedPieceID))
            text_buy.text = "¡Comprar!";
        else
            text_buy.text = "¡Comprado!";

        image_recipe.sprite = pieceUI.m_Sprite;
    }

    public void BuyRecipe()
    {
        _buyRecipe.Invoke(_selectedPieceID);
    }

    public void NextItem(bool next)
    {
        int amount = 1;
        if (!next)
            amount = -1;

        if(goodRecipeShop)
        {
            idx_good += amount;
            idx_good = Mathf.Clamp(idx_good, 0, _goodRecipesToBuy.Count - 1);
        }
        else
        {
            idx_evil += amount;
            idx_evil = Mathf.Clamp(idx_evil, 0, _evilRecipesToBuy.Count - 1);
        }

        UpdateUI();
    }
}
