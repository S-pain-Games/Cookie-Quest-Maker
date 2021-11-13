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

    private void Awake()
    {
        var admin = Admin.Global;
        _craftRecipe = admin.EventSystem.GetCommandByName<Event<ID>>("piece_crafting_sys", "craft_recipe");

        //Event<PieceRecipeUi> evt = admin.EventSystem.GetCallback<Event<PieceRecipeUi>>(new ID("piece_crafting_sys"), new ID("update_piece_ui"));
        //evt.OnInvoked += UpdatePieceUi;

        //Admin.Global.Systems.pieceCraftingSystem.GetDefaultUi();

        _recipeDataComponents = admin.Components.GetComponentContainer<RecipeDataComponent>();
        _pieceDataComponents = admin.Components.m_QuestPieceFunctionalComponents;
        _inventory = admin.Components.m_InventoryComponent;

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
    }

    private void SelectPieceUi()
    {
        QuestPieceFunctionalComponent piece = null;
        RecipeDataComponent recipe = null;

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
            HideUi(true);
            UpdatePieceUi(piece, recipe);
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
    }

    private void UpdatePieceUi(QuestPieceFunctionalComponent piece, RecipeDataComponent recipe)
    {
        if (recipe != null && piece != null)
        {
            text_piece_name.text = recipe.m_RecipeName;
            text_piece_description.text = recipe.m_RecipeDescription;
            //img_recipe_big.sprite = newPiece.recipe.m_Image.sprite;
            //img_recipe_small.sprite = newPiece.recipe.m_Image.sprite;
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
                if (idx_cookieRecipes < 0)
                    idx_cookieRecipes = 0;
                else if (idx_cookieRecipes >= _cookieRecipes.Count)
                    idx_cookieRecipes = _cookieRecipes.Count - 1;
                if(idx_cookieRecipes < 0)
                    idx_cookieRecipes = 0;
            }
            else
                idx_cookieRecipes = 0;
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Action)
        {
            if (_actionRecipes.Count > 0)
            {
                if (idx_actionRecipe < 0)
                    idx_actionRecipe = 0;
                else if (idx_actionRecipe >= _actionRecipes.Count)
                    idx_actionRecipe = _actionRecipes.Count - 1;
                if (idx_actionRecipe < 0)
                    idx_actionRecipe = 0;
            }
            else
                idx_actionRecipe = 0;
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Modifier)
        {
            if (_modifierRecipes.Count > 0)
            {
                if (idx_modifierRecipes < 0)
                    idx_modifierRecipes = 0;
                else if (idx_modifierRecipes >= _modifierRecipes.Count)
                    idx_modifierRecipes = _modifierRecipes.Count - 1;
                if (idx_modifierRecipes < 0)
                    idx_modifierRecipes = 0;
            }
            else
                idx_modifierRecipes = 0;
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Object)
        {
            if (_objectRecipes.Count > 0)
            {
                if (idx_objectRecipes < 0)
                    idx_objectRecipes = 0;
                else if (idx_objectRecipes >= _objectRecipes.Count)
                    idx_objectRecipes = _objectRecipes.Count - 1;
                if (idx_objectRecipes < 0)
                    idx_objectRecipes = 0;
            }
            else
                idx_objectRecipes = 0;
        }
    }

    public void SelectPieceCookie()
    {
        //_selectPieceTypeCmd.Invoke(QuestPieceFunctionalComponent.PieceType.Cookie);
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
