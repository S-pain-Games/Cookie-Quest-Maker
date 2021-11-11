using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using CQM.Databases;
using CQM.Components;

// Handles the UI that shows all the available pieces of the selected type
public class UIPieceSelection : MonoBehaviour
{
    // Use Piece Handling
    public event Action<ID> OnUsePiece;

    public ID m_CurrentStoryID;

    private ID m_SelectedPieceID;

    [SerializeField] private Button _usePieceButton;
    [SerializeField] private UISelectedPieceView _uiSelectedPieceView;

    // Managing Piece UI Elements variables
    [SerializeField] private GameObject elementPrefab;
    private List<UIStorageElement> m_Elements = new List<UIStorageElement>();

    private void OnEnable()
    {
        Refresh(QuestPieceFunctionalComponent.PieceType.Cookie);
        _usePieceButton.onClick.AddListener(UsePieceButton_OnClick);
    }

    private void OnDisable()
    {
        _usePieceButton.onClick.RemoveListener(UsePieceButton_OnClick);
    }

    public void Refresh(QuestPieceFunctionalComponent.PieceType pieceType)
    {
        // Delete previous elements
        for (int i = m_Elements.Count - 1; i >= 0; i--)
        {
            Destroy(m_Elements[i].gameObject);
        }
        m_Elements.Clear();

        // Prepare start position for UI Pieces
        Vector3 pos = Vector3.zero;
        pos.y += 100;

        // Loop over all story targets
        if (pieceType == QuestPieceFunctionalComponent.PieceType.Target)
        {
            var targetsList = Admin.Global.Components.GetComponentContainer<StoryInfoComponent>()[m_CurrentStoryID].m_StoryData.m_AllPossibleTargets;

            for (int i = 0; i < targetsList.Count; i++)
            {
                QuestPieceFunctionalComponent targetPiece = Admin.Global.Components.m_QuestPieceFunctionalComponents[targetsList[i]];
                pos = AddPieceToUI(pos, targetPiece);
            }
            return;
        }

        // Loop over all inventory pieces
        List<InventoryItem> piecesInventory = Admin.Global.Components.m_InventoryComponent.m_Pieces;
        for (int i = 0; i < piecesInventory.Count; i++)
        {
            if (piecesInventory[i].m_Amount <= 0) return; // Should be innecesary because the list shouldnt have empty items but just in case

            QuestPieceFunctionalComponent piece = Admin.Global.Components.m_QuestPieceFunctionalComponents[piecesInventory[i].m_ItemID];
            // Only show the pieces that match the filter
            if (piece.m_Type == pieceType)
            {
                pos = AddPieceToUI(pos, piece);
            }
        }
    }

    private Vector3 AddPieceToUI(Vector3 pos, QuestPieceFunctionalComponent questPiece)
    {
        // Create and position corresponding elements in UI
        pos += new Vector3(250, 0, 0);
        var UIstorageElem = Instantiate(elementPrefab, pos, Quaternion.identity, transform).GetComponent<UIStorageElement>();
        UIstorageElem.transform.localPosition = pos;

        // Initialize Element Data and Events
        UIstorageElem.pieceID = questPiece.m_ID;
        UIstorageElem.OnSelected += StoragePiece_OnClicked;

        UIQuestPieceComponent uiData = Admin.Global.Components.m_QuestPieceUIComponent[questPiece.m_ID];
        // Initialize UI element with piece data
        UIstorageElem.Build(uiData);
        m_Elements.Add(UIstorageElem);
        return pos;
    }

    private void StoragePiece_OnClicked(ID questPieceID)
    {
        m_SelectedPieceID = questPieceID;
        // Update UI
        UIQuestPieceComponent UIPieceData = Admin.Global.Components.m_QuestPieceUIComponent[m_SelectedPieceID];
        _uiSelectedPieceView.UpdateUI(UIPieceData.m_Sprite, UIPieceData.m_Name, UIPieceData.m_Description);
    }

    public void UsePieceButton_OnClick()
    {
        OnUsePiece?.Invoke(m_SelectedPieceID);
    }

    [Serializable]
    private class UISelectedPieceView
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameTextComp;
        [SerializeField] private TextMeshProUGUI _descTextComp;

        public void UpdateUI(Sprite sprite, string name, string description)
        {
            _image.sprite = sprite;
            _nameTextComp.text = name;
            _descTextComp.text = description;
        }
    }
}



[Serializable]
public class UIQuestPieceComponent
{
    public Sprite m_Sprite;
    public string m_Name;
    public string m_Description;

    [HideInInspector] public ID m_ID;
}