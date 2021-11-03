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
    public event Action<int> OnUsePiece;

    public int m_CurrentStoryID;

    private int m_SelectedPieceID;

    [SerializeField] private Button _usePieceButton;
    [SerializeField] private UISelectedPieceView _uiSelectedPieceView;

    // Managing Piece UI Elements variables
    [SerializeField] private GameObject elementPrefab;
    private List<UIStorageElement> m_Elements = new List<UIStorageElement>();

    private void OnEnable()
    {
        Refresh(QuestPiece.PieceType.Cookie);
        _usePieceButton.onClick.AddListener(UsePieceButton_OnClick);
    }

    private void OnDisable()
    {
        _usePieceButton.onClick.RemoveListener(UsePieceButton_OnClick);
    }

    public void Refresh(QuestPiece.PieceType pieceType)
    {
        // Delete previous elements
        for (int i = m_Elements.Count - 1; i >= 0; i--)
        {
            Destroy(m_Elements[i].gameObject);
        }
        m_Elements.Clear();

        // Prepare start position for UI Pieces
        Vector3 pos = Vector3.zero;
        pos.y += 150;

        // Loop over all story targets
        if (pieceType == QuestPiece.PieceType.Target)
        {
            var targetsList = Admin.Global.Database.Stories.GetStoryComponent<Story>(m_CurrentStoryID).m_StoryData.m_Targets;

            for (int i = 0; i < targetsList.Count; i++)
            {
                QuestPiece targetPiece = Admin.Global.Database.Pieces.GetQuestPieceComponent<QuestPiece>(targetsList[i]);
                pos = AddPieceToUI(pos, targetPiece);
            }
            return;
        }

        // Loop over all inventory pieces
        List<InventoryItem> piecesInventory = Admin.Global.Database.Player.Inventory.m_Pieces;
        for (int i = 0; i < piecesInventory.Count; i++)
        {
            if (piecesInventory[i].m_Amount <= 0) return; // Should be innecesary because the list shouldnt have empty items but just in case

            QuestPiece piece = Admin.Global.Database.Pieces.GetQuestPieceComponent<QuestPiece>(piecesInventory[i].m_ItemID);
            // Only show the pieces that match the filter
            if (piece.m_Type == pieceType)
            {
                pos = AddPieceToUI(pos, piece);
            }
        }
    }

    private Vector3 AddPieceToUI(Vector3 pos, QuestPiece questPiece)
    {
        // Create and position corresponding elements in UI
        pos += new Vector3(250, 0, 0);
        var UIstorageElem = Instantiate(elementPrefab, pos, Quaternion.identity, transform).GetComponent<UIStorageElement>();
        UIstorageElem.transform.localPosition = pos;

        // Initialize Element Data and Events
        UIstorageElem.pieceID = questPiece.m_ParentID;
        UIstorageElem.OnSelected += StoragePiece_OnClicked;

        var uiData = Admin.Global.Database.Pieces.GetQuestPieceComponent<UIQuestPieceData>(questPiece.m_ParentID);
        // Initialize UI element with piece data
        UIstorageElem.Build(uiData);
        m_Elements.Add(UIstorageElem);
        return pos;
    }

    private void StoragePiece_OnClicked(int questPieceID)
    {
        m_SelectedPieceID = questPieceID;
        // Update UI
        var UIPieceData = Admin.Global.Database.Pieces.GetQuestPieceComponent<UIQuestPieceData>(m_SelectedPieceID);
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
public class UIQuestPieceData
{
    public Sprite m_Sprite;
    public string m_Name;
    public string m_Description;

    [HideInInspector] public int m_ParentID;
}