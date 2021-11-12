using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PieceCraftingUI : MonoBehaviour
{
    private Event<QuestPieceFunctionalComponent.PieceType> _selectPieceTypeCmd;
    private Event<PieceRecipeUi> _updatePieceUi;

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
        _selectPieceTypeCmd = admin.EventSystem.GetCommandByName<Event<QuestPieceFunctionalComponent.PieceType>>("piece_crafting_sys", "select_crafting_type");

        Event<PieceRecipeUi> evt = admin.EventSystem.GetCallback<Event<PieceRecipeUi>>(new ID("piece_crafting_sys"), new ID("update_piece_ui"));
        evt.OnInvoked += UpdatePieceUi;

        Admin.Global.Systems.pieceCraftingSystem.GetDefaultUi();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        
    }

    public void SelectPieceCookie()
    {
        _selectPieceTypeCmd.Invoke(QuestPieceFunctionalComponent.PieceType.Cookie);
    }
    public void SelectPieceAction()
    {
        _selectPieceTypeCmd.Invoke(QuestPieceFunctionalComponent.PieceType.Action);
    }

    private void UpdatePieceUi(PieceRecipeUi newPiece)
    {
        if(newPiece.recipe != null && newPiece.piece != null)
        {
            text_piece_name.text = newPiece.recipe.m_RecipeName;
            text_piece_description.text = newPiece.recipe.m_RecipeDescription;
            //img_recipe_big.sprite = newPiece.recipe.m_Image.sprite;
            //img_recipe_small.sprite = newPiece.recipe.m_Image.sprite;
        }
    }


}
