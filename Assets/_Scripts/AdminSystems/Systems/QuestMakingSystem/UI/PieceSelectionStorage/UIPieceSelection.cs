using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using CQM.QuestMaking;

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

        // Loop over all unlocked word pieces
        List<int> storage = Admin.g_Instance.inventoryData.m_Storage;
        Dictionary<int, QuestPiece> qPiecesDB = Admin.g_Instance.questDB.m_QPiecesDB;
        for (int i = 0; i < storage.Count; i++)
        {
            var questPiece = qPiecesDB[storage[i]];
            // Only show the pieces that match the filter
            if (questPiece.m_Type == pieceType)
            {
                pos = AddPieceToUI(pos, questPiece);
            }
        }

        // Loop over all cookie inventory
        if (pieceType == QuestPiece.PieceType.Cookie)
        {
            List<InventoryItem> cookiesStorage = Admin.g_Instance.inventoryData.m_Cookies;
            if (cookiesStorage.Count <= 0) return;

            for (int i = 0; i < cookiesStorage.Count; i++)
            {
                if (cookiesStorage[i].m_Amount <= 0) return; // Should be innecesary because the list shouldnt have empty items but just in case

                QuestPiece cookiePiece = qPiecesDB[cookiesStorage[i].m_ItemID];
                // Only show the pieces that match the filter
                pos = AddPieceToUI(pos, cookiePiece);
            }
        }
        // Loop over all story targets
        else if (pieceType == QuestPiece.PieceType.Target)
        {
            // TODO: ONLY HANDLES ONE TARGET
            int targetID = Admin.g_Instance.storyDB.m_StoriesDB[m_CurrentStoryID].m_StoryData.m_Target;
            QuestPiece targetPiece = Admin.g_Instance.questDB.m_QPiecesDB[targetID];

            pos = AddPieceToUI(pos, targetPiece);
        }
    }

    private Vector3 AddPieceToUI(Vector3 pos, QuestPiece questPiece)
    {
        // Create and position corresponding elements in UI
        pos += new Vector3(250, 0, 0);
        var UIstorageElem = Instantiate(elementPrefab, pos, Quaternion.identity, transform).GetComponent<UIStorageElement>();
        UIstorageElem.transform.localPosition = pos;

        // Initialize Element Data and Events
        UIstorageElem.pieceID = questPiece.m_ID;
        UIstorageElem.OnSelected += StoragePiece_OnClicked;

        // Initialize UI element with piece data
        UIstorageElem.Build(questPiece);
        m_Elements.Add(UIstorageElem);
        return pos;
    }

    private void StoragePiece_OnClicked(int questPieceID)
    {
        m_SelectedPieceID = questPieceID;
        // Update UI
        var UIPieceData = Admin.g_Instance.questDB.m_UIQuestPieces[m_SelectedPieceID];
        _uiSelectedPieceView.UpdateUI(UIPieceData.sprite, UIPieceData.name, UIPieceData.description);
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
    public Sprite sprite;
    public string name;
    public string description;
}