using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

// Handles the UI that shows all the available pieces of the selected type
public class UIPieceSelection : MonoBehaviour
{
    // Use Piece Handling
    public event Action<int> OnUsePiece;

    private int m_SelectedPieceID;

    [SerializeField] private Button _usePieceButton;
    [SerializeField] private UISelectedPieceView _uiSelectedPieceView;

    // Managing Piece UI Elements variables
    [SerializeField] private GameObject elementPrefab;
    private RectTransform rectTransf;
    private List<UIStorageElement> m_Elements = new List<UIStorageElement>();

    private void Awake()
    {
        rectTransf = GetComponent<RectTransform>();
    }

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

        // Loop over all storage elements
        var storage = Admin.g_Instance.playerPieceStorage.m_Storage;
        for (int i = 0; i < storage.Count; i++)
        {
            var questPiece = Admin.g_Instance.questDB.m_QPiecesDB[storage[i]];
            if (questPiece.m_Type == pieceType)
            {
                // Create and position corresponding elements in UI
                pos += new Vector3(250, 0, 0);
                var UIstorageElem = Instantiate(elementPrefab, pos, Quaternion.identity, transform).GetComponent<UIStorageElement>();
                UIstorageElem.transform.localPosition = pos;

                // Initialize Element Data and Events
                UIstorageElem.pieceID = storage[i];
                UIstorageElem.OnSelected += StoragePiece_OnClicked;

                // Build UI Element using previously initialized data
                UIstorageElem.Build();
                m_Elements.Add(UIstorageElem);
            }
        }
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
}

[Serializable]
public class UISelectedPieceView
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

[Serializable]
public class UIQuestPieceData
{
    public Sprite sprite;
    public string name;
    public string description;
}