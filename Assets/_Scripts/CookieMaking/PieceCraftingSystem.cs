using CQM.Components;
using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCraftingSystem : ISystemEvents
{
    private ComponentsContainer<RecipeDataComponent> _recipeDataComponents;
    private ComponentsContainer<QuestPieceFunctionalComponent> _pieceDataComponents;
    private Singleton_InventoryComponent _inventory;

    private ID _selectedPieceID;

    private Event<ItemData> _addPieceToInventoryCmd;
    private Event<ItemData> _removeIngredientToInventoryCmd;

    private Event<PieceRecipeUi> _updatePieceUiCallback;

    [SerializeField]
    private QuestPieceFunctionalComponent.PieceType _selectedPieceType = QuestPieceFunctionalComponent.PieceType.Cookie;

    private List<ID> _cookieRecipes;
    private List<ID> _actionRecipes;
    private List<ID> _modifierRecipes;
    private List<ID> _objectRecipes;


    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("piece_crafting_sys");

        commands.AddEvent<QuestPieceFunctionalComponent.PieceType>(new ID("select_crafting_type")).OnInvoked += SetCraftingType;
        //commands.AddEvent<PieceRecipeUi>(new ID(""));
        callbacks.AddEvent<PieceRecipeUi>(new ID("update_piece_ui"));
    }

    public void Initialize(ComponentsContainer<RecipeDataComponent> recipeDataComponents,
                           Singleton_InventoryComponent inventory, ComponentsContainer<QuestPieceFunctionalComponent> pieceDataComponents)
    {
        _recipeDataComponents = recipeDataComponents;
        _pieceDataComponents = pieceDataComponents;
        _inventory = inventory;

        var evtSys = Admin.Global.EventSystem;
        _addPieceToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_piece");
        _removeIngredientToInventoryCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "remove_ingredient");
        _updatePieceUiCallback = Admin.Global.EventSystem.GetCallbackByName<Event<PieceRecipeUi>>("piece_crafting_sys", "update_piece_ui");

        _cookieRecipes = new List<ID>();
        _actionRecipes = new List<ID>();
        _modifierRecipes = new List<ID>();
        _objectRecipes = new List<ID>();

        LoadRecipeLists();
        LoadDefaultRecipe();
    }

    private void SetCraftingType(QuestPieceFunctionalComponent.PieceType craftingType)
    {
        _selectedPieceType = craftingType;

        _selectedPieceID = _inventory.m_UnlockedRecipes[0];

        PieceRecipeUi newPiece = new PieceRecipeUi();
        newPiece.piece = _pieceDataComponents.GetComponentByID(_selectedPieceID);
        newPiece.recipe = _recipeDataComponents.GetComponentByID(_selectedPieceID);

        //Admin.Global.EventSystem.GetCallbackByName<Event<PieceRecipeUi>>("piece_crafting_sys", "update_piece_ui").Invoke(newPiece);
        _updatePieceUiCallback.Invoke(newPiece);
    }

    private void LoadDefaultRecipe()
    {
        bool selected = false;
        if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Cookie)
        {
            if(_cookieRecipes.Count > 0)
            {
                _selectedPieceID = _cookieRecipes[0];
                selected = true;
            }
        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Action)
        {

        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Modifier)
        {

        }
        else if (_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Object)
        {

        }

        if (selected)
        {
            PieceRecipeUi newPiece = new PieceRecipeUi();
            newPiece.piece = _pieceDataComponents.GetComponentByID(_selectedPieceID);
            newPiece.recipe = _recipeDataComponents.GetComponentByID(_selectedPieceID);

            _updatePieceUiCallback.Invoke(newPiece);
        }
    }

    private void LoadRecipeLists()
    {
        List<ID> recipes = _inventory.m_UnlockedRecipes;

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

    private void NextRecipe()
    {
        if(_selectedPieceType == QuestPieceFunctionalComponent.PieceType.Cookie)
        {
            int currentIdx = 0;
            for(int i = 0; i < _cookieRecipes.Count; i++)
            {
                if(_cookieRecipes[i] == _selectedPieceID)
                {
                    currentIdx = i;
                    break;
                }
            }


        }
    }

    public void GetDefaultUi()
    {
        PieceRecipeUi newPiece = new PieceRecipeUi();
        newPiece.piece = _pieceDataComponents.GetComponentByID(_selectedPieceID);
        newPiece.recipe = _recipeDataComponents.GetComponentByID(_selectedPieceID);

        _updatePieceUiCallback.Invoke(newPiece);
    }

    
}

public struct PieceRecipeUi
{
    public QuestPieceFunctionalComponent piece;
    public RecipeDataComponent recipe;
}
