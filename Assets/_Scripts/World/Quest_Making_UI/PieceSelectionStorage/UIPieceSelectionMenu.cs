using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CQM.Databases;
using CQM.Components;

namespace CQM.UI.QuestMakingTable
{
    // Handles the UI that shows all the available pieces of the selected type
    public class UIPieceSelectionMenu : MonoBehaviour
    {
        private QuestMakerTableState _state;
        private UIPieceStorageManager _storage;
        private ID m_SelectedPieceID;

        private bool m_IsAPieceSelected;
        [SerializeField] private Button _usePieceButton;
        [SerializeField] private UISelectedPieceView m_SelectedPieceStatsView;

        // Managing Piece UI Elements variables
        [SerializeField] private GameObject _piecePrefab;
        private List<UIStorageElement> m_SelectablePiecesOfType = new List<UIStorageElement>();


        private void OnEnable()
        {
            _usePieceButton.onClick.AddListener(ConfirmPieceSelection);
            UnselectPiece();
            SelectFirstPieceOfType();
        }

        private void OnDisable()
        {
            _usePieceButton.onClick.RemoveListener(ConfirmPieceSelection);
        }

        public void Initialize(QuestMakerTableState state, UIPieceStorageManager storage)
        {
            _state = state;
            _storage = storage;
        }

        public void ShowPiecesOfType(QuestPieceFunctionalComponent.PieceType pieceType)
        {
            // Delete previous elements
            for (int i = m_SelectablePiecesOfType.Count - 1; i >= 0; i--)
            {
                Destroy(m_SelectablePiecesOfType[i].gameObject);
            }
            m_SelectablePiecesOfType.Clear();

            // Prepare start position for UI Pieces
            Vector3 pos = Vector3.zero;

            // Loop over all story targets
            if (pieceType == QuestPieceFunctionalComponent.PieceType.Target)
            {
                var targetsList = Admin.Global.Components.GetComponentContainer<StoryInfoComponent>()[_state.m_SelectedStoryID].m_StoryData.m_AllPossibleTargets;

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

            Vector3 AddPieceToUI(Vector3 pos, QuestPieceFunctionalComponent questPiece)
            {
                // Create and position corresponding elements in UI
                pos += new Vector3(215, 0, 0);
                var UIstorageElem = Instantiate(_piecePrefab, pos, Quaternion.identity, transform).GetComponent<UIStorageElement>();
                UIstorageElem.transform.localPosition = pos;

                // Initialize Element Data and Events
                UIstorageElem.pieceID = questPiece.m_ID;
                UIstorageElem.OnSelected += SelectPieceWithId;

                UIQuestPieceComponent uiData = Admin.Global.Components.m_QuestPieceUIComponent[questPiece.m_ID];
                // Initialize UI element with piece data
                UIstorageElem.Build(uiData);
                m_SelectablePiecesOfType.Add(UIstorageElem);
                return pos;
            }
        }

        public void UnselectPiece()
        {
            m_IsAPieceSelected = false;
            m_SelectedPieceStatsView.Clear();
        }

        private void SelectPieceWithId(ID questPieceID)
        {
            m_SelectedPieceID = questPieceID;
            m_IsAPieceSelected = true;
            // Update UI
            UIQuestPieceComponent UIPieceData = Admin.Global.Components.m_QuestPieceUIComponent[m_SelectedPieceID];
            QuestPieceFunctionalComponent funcPiece = Admin.Global.Components.GetComponentContainer<QuestPieceFunctionalComponent>().GetComponentByID(m_SelectedPieceID);

            int convince = 0;
            int help = 0;
            int harm = 0;
            var l = funcPiece.m_Tags.Find(t => t.m_Type == QPTag.TagType.Convince);
            if (l != null)
                convince = l.m_Value;
            l = funcPiece.m_Tags.Find(t => t.m_Type == QPTag.TagType.Help);
            if (l != null)
                help = l.m_Value;
            l = funcPiece.m_Tags.Find(t => t.m_Type == QPTag.TagType.Harm);
            if (l != null)
                harm = l.m_Value;

            m_SelectedPieceStatsView.UpdateUI(UIPieceData.m_SimpleSprite, UIPieceData.m_Name, UIPieceData.m_Description, convince, help, harm);
        }

        public void SelectFirstPieceOfType()
        {
            if (m_SelectablePiecesOfType.Count == 0) return;
            ID pieceID = m_SelectablePiecesOfType[0].pieceID;
            SelectPieceWithId(pieceID);
        }

        public void ConfirmPieceSelection()
        {
            if (m_IsAPieceSelected)
                _storage.SelectPieceFromStorage(m_SelectedPieceID);
        }
    }
}
