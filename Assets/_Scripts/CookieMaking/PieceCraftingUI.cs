using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CQM.Databases;

public class PieceCraftingUI : MonoBehaviour
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private ComponentsContainer<QuestPieceFunctionalComponent> _pieceDataComponents;
    private ComponentsContainer<UIQuestPieceComponent> _recipeUiDataComponents;
    private ComponentsContainer<IngredientComponent> _ingredientDataComponents;
    private ComponentsContainer<CookieDataComponent> _cookieDataComponents;
    private Singleton_InventoryComponent _inventory;

    private List<ID> _cookieRecipes;
    private List<ID> _actionRecipes;
    private List<ID> _modifierRecipes;
    private List<ID> _objectRecipes;

    private int idx_cookieRecipes, idx_actionRecipe, idx_modifierRecipes, idx_objectRecipes;

    private ID _selectedPieceID;
    [SerializeField]
    private QuestPieceFunctionalComponent.PieceType _selectedPieceType = QuestPieceFunctionalComponent.PieceType.Cookie;

    private Event<QuestPieceFunctionalComponent.PieceType> _selectPieceTypeCmd;
    private Event<ID> _craftRecipe;

    [SerializeField] private TextMeshProUGUI text_piece_name;
    [SerializeField] private TextMeshProUGUI text_piece_description;
    [SerializeField] private Image img_recipe_big;
    [SerializeField] private Image img_recipe_small;

    [SerializeField] private TextMeshProUGUI text_stat_1;
    [SerializeField] private TextMeshProUGUI text_stat_2;
    [SerializeField] private TextMeshProUGUI text_stat_3;

    [SerializeField] private GameObject obj_ingredient_1;
    [SerializeField] private GameObject obj_ingredient_2;
    [SerializeField] private GameObject obj_ingredient_3;

    [SerializeField] private TextMeshProUGUI text_ingredient_name_1;
    [SerializeField] private TextMeshProUGUI text_ingredient_name_2;
    [SerializeField] private TextMeshProUGUI text_ingredient_name_3;

    [SerializeField] private TextMeshProUGUI text_ingredient_amount_1;
    [SerializeField] private TextMeshProUGUI text_ingredient_amount_2;
    [SerializeField] private TextMeshProUGUI text_ingredient_amount_3;

    [SerializeField] private Image image_ingredient_1;
    [SerializeField] private Image image_ingredient_2;
    [SerializeField] private Image image_ingredient_3;

    private void Awake()
    {
        var admin = Admin.Global;
        _craftRecipe = admin.EventSystem.GetCommandByName<Event<ID>>("piece_crafting_sys", "craft_recipe");

        Event<ID> evt = admin.EventSystem.GetCallback<Event<ID>>(new ID("piece_crafting_sys"), new ID("update_ingredients_ui"));
        evt.OnInvoked += UpdateRecipeIngredients;

        _recipeDataComponents = admin.Components.GetComponentContainer<RecipeDataComponent>();
        _pieceDataComponents = admin.Components.m_QuestPieceFunctionalComponents;
        _inventory = admin.Components.m_InventoryComponent;
        _recipeUiDataComponents = admin.Components.m_QuestPieceUIComponent;
        _ingredientDataComponents = admin.Components.GetComponentContainer<IngredientComponent>();
        _cookieDataComponents = admin.Components.m_CookieData;

        _cookieRecipes = new List<ID>();
        _actionRecipes = new List<ID>();
        _modifierRecipes = new List<ID>();
        _objectRecipes = new List<ID>();

        LoadRecipeLists();
        SelectPieceUi();
    }

    private void OnEnable()
    {
        CheckChanges();
    }

    private void OnDisable()
    {
        
    }

    private void CheckChanges()
    {
        if(_cookieRecipes.Count + _actionRecipes.Count + _modifierRecipes.Count + _objectRecipes.Count < _inventory.m_UnlockedRecipes.Count)
        {
            LoadRecipeLists();
            SelectPieceUi();
        }
    }

    private void LoadRecipeLists()
    {
        List<ID> recipes = _inventory.m_UnlockedRecipes;
        _cookieRecipes.Clear();
        _actionRecipes.Clear();
        _modifierRecipes.Clear();
        _objectRecipes.Clear();

        for (int i = 0; i < recipes.Count; i++)
        {
            QuestPieceFunctionalComponent piece = _pieceDataComponents.GetComponentByID(recipes[i]);
            QuestPieceFunctionalComponent.PieceType currentType = piece.m_Type;

            if (currentType == QuestPieceFunctionalComponent.PieceType.Cookie)
                _cookieRecipes.Add(recipes[i]);
            else if (currentType == QuestPieceFunctionalComponent.PieceType.Action)
                _actionRecipes.Add(recipes[i]);
            else if (currentType == QuestPieceFunctionalComponent.PieceType.Modifier)
                _modifierRecipes.Add(recipes[i]);
            else if (currentType == QuestPieceFunctionalComponent.PieceType.Object)
                _objectRecipes.Add(recipes[i]);
        }

        Debug.Log("modifiers: " + _modifierRecipes.Count);
        Debug.Log("objects: " + _objectRecipes.Count);
    }

    private void SelectPieceUi()
    {
        QuestPieceFunctionalComponent piece = null;
        RecipeDataComponent recipe = null;
        UIQuestPieceComponent recipeUi = null;

        bool selected = false;

        if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Cookie)
        {
            if (_cookieRecipes.Count > 0)
            {
                _selectedPieceID = _cookieRecipes[idx_cookieRecipes];
                selected = true;
            }
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Action)
        {
            if (_actionRecipes.Count > 0)
            {
                _selectedPieceID = _actionRecipes[idx_actionRecipe];
                selected = true;
            }
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Modifier)
        {
            if (_modifierRecipes.Count > 0)
            {
                _selectedPieceID = _modifierRecipes[idx_modifierRecipes];
                selected = true;
            }
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Object)
        {
            if (_objectRecipes.Count > 0)
            {
                _selectedPieceID = _objectRecipes[idx_objectRecipes];
                selected = true;
            }
        }

        if (selected)
        {
            piece = _pieceDataComponents.GetComponentByID(_selectedPieceID);
            recipe = _recipeDataComponents.GetComponentByID(_selectedPieceID);
            recipeUi = _recipeUiDataComponents.GetComponentByID(_selectedPieceID);
            HideUi(true);
            UpdatePieceUi(piece, recipe, recipeUi);
        }
        else
        {
            HideUi(false);
        }
    }

    private void HideUi(bool state)
    {
        text_piece_name.gameObject.SetActive(state);
        text_piece_description.gameObject.SetActive(state);
        img_recipe_big.gameObject.SetActive(state);
        img_recipe_small.gameObject.SetActive(state);

        text_stat_1.gameObject.SetActive(state);
        text_stat_2.gameObject.SetActive(state);
        text_stat_3.gameObject.SetActive(state);

        obj_ingredient_1.SetActive(state);
        obj_ingredient_2.SetActive(state);
        obj_ingredient_3.SetActive(state);
    }

    private void UpdatePieceUi(QuestPieceFunctionalComponent piece, RecipeDataComponent recipe, UIQuestPieceComponent recipeUi)
    {
        if (recipe != null && piece != null)
        {
            text_piece_name.text = recipeUi.m_Name;
            text_piece_description.text = recipeUi.m_Description;

            if(_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Cookie)
            {
                img_recipe_big.sprite = recipeUi.m_HDSprite;
                img_recipe_small.sprite = recipeUi.m_ShopRecipeSprite;
            }
            else
            {
                img_recipe_big.sprite = recipeUi.m_ShopRecipeSprite;
                img_recipe_small.sprite = recipeUi.m_SimpleSprite;
            }
                
            img_recipe_big.preserveAspect = true;
            img_recipe_small.preserveAspect = true;
            //1: Convice, 2: Help, 0: Harm 
            text_stat_1.text = "0";
            text_stat_2.text = "0";
            text_stat_3.text = "0";
            for (int i=0; i<piece.m_Tags.Count; i++)
            {
                if(piece.m_Tags[i].m_Type == QPTag.TagType.Convince)
                    text_stat_1.text = piece.m_Tags[i].m_Value.ToString();
                else if (piece.m_Tags[i].m_Type == QPTag.TagType.Help)
                    text_stat_2.text = piece.m_Tags[i].m_Value.ToString();
                else if (piece.m_Tags[i].m_Type == QPTag.TagType.Harm)
                    text_stat_3.text = piece.m_Tags[i].m_Value.ToString();
            }

            UpdateRecipeIngredients(recipe.m_ID);

        }
    }

    public void UpdateRecipeIngredients(ID recipeId)
    {
        obj_ingredient_1.SetActive(false);
        obj_ingredient_2.SetActive(false);
        obj_ingredient_3.SetActive(false);

        RecipeDataComponent recipe = _recipeDataComponents.GetComponentByID(recipeId);

        for (int i = 0; i < recipe.m_IngredientsList.Count; i++)
        {
            IngredientComponent ingredient = _ingredientDataComponents.GetComponentByID(recipe.m_IngredientsList[i].m_ItemID);
           
            int m_amount = 0;
            for (int j = 0; j < _inventory.m_Ingredients.Count; j++)
            {
                if (_inventory.m_Ingredients[j].m_ItemID == ingredient.m_ID)
                {
                    m_amount = _inventory.m_Ingredients[j].m_Amount;
                    break;
                }
            }
            if (i == 0)
            {
                obj_ingredient_1.SetActive(true);
                text_ingredient_name_1.text = ingredient.m_Name;
                text_ingredient_amount_1.text = m_amount + " / " + recipe.m_IngredientsList[i].m_Amount;
                if (ingredient.m_ID == new ID("masa_de_galletas_encantada"))
                    text_ingredient_amount_1.text = "\u221E" + " / " + recipe.m_IngredientsList[i].m_Amount;
                image_ingredient_1.sprite = ingredient.m_Sprite;
            }
            else if (i == 1)
            {
                obj_ingredient_2.SetActive(true);
                text_ingredient_name_2.text = ingredient.m_Name;
                text_ingredient_amount_2.text = m_amount + " / " + recipe.m_IngredientsList[i].m_Amount;
                image_ingredient_2.sprite = ingredient.m_Sprite;
            }
            else if (i == 2)
            {
                obj_ingredient_3.SetActive(true);
                text_ingredient_name_3.text = ingredient.m_Name;
                text_ingredient_amount_3.text = m_amount + " / " + recipe.m_IngredientsList[i].m_Amount;
                image_ingredient_3.sprite = ingredient.m_Sprite;
            }
        }
    }

    public void NextRecipe(bool next)
    {
        int num = 1;
        if (!next)
            num = -1;

        if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Cookie)
        {
            idx_cookieRecipes += num;
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Action)
        {
            idx_actionRecipe += num;
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Modifier)
        {
            idx_modifierRecipes += num;
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Object)
        {
            idx_objectRecipes += num;
        }
        ClampIdx();
        SelectPieceUi();
    }

    private void ClampIdx()
    {
        if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Cookie)
        {
            if (_cookieRecipes.Count > 0)
            {
                idx_cookieRecipes = Mathf.Clamp(idx_cookieRecipes, 0, _cookieRecipes.Count-1);
            }
            else
                idx_cookieRecipes = 0;
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Action)
        {
            if (_actionRecipes.Count > 0)
            {
                idx_actionRecipe = Mathf.Clamp(idx_actionRecipe, 0, _actionRecipes.Count - 1);
            }
            else
                idx_actionRecipe = 0;
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Modifier)
        {
            if (_modifierRecipes.Count > 0)
            {
                idx_modifierRecipes = Mathf.Clamp(idx_modifierRecipes, 0, _modifierRecipes.Count - 1);
            }
            else
                idx_modifierRecipes = 0;
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Object)
        {
            if (_objectRecipes.Count > 0)
            {
                idx_objectRecipes = Mathf.Clamp(idx_objectRecipes, 0, _objectRecipes.Count - 1);
            }
            else
                idx_objectRecipes = 0;
        }
    }

    public void SelectPieceCookie()
    {
        _selectedPieceType = QuestPieceFunctionalComponent.PieceType.Cookie;
        SelectPieceUi();
    }
    public void SelectPieceAction()
    {
        _selectedPieceType = QuestPieceFunctionalComponent.PieceType.Action;
        SelectPieceUi();
    }
    public void SelectPieceModifier()
    {
        _selectedPieceType = QuestPieceFunctionalComponent.PieceType.Modifier;
        SelectPieceUi();
    }
    public void SelectPieceObject()
    {
        _selectedPieceType = QuestPieceFunctionalComponent.PieceType.Object;
        SelectPieceUi();
    }

    public void CraftRecipe()
    {
        _craftRecipe.Invoke(_selectedPieceID);
    }


}
