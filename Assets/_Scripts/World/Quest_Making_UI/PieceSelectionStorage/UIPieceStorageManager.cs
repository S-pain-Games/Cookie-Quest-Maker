using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PieceType = CQM.Components.QuestPieceFunctionalComponent.PieceType;


namespace CQM.UI.QuestMakingTable
{
    // Handles all the Storage UI functionality
    public class UIPieceStorageManager : MonoBehaviour
    {
        public event Action<ID> OnSelectPieceFromStorage;
        public event Action OnExit;

        public PieceType m_SelectedType;

        [SerializeField] private UIPieceTypeSelectionMenu _pieceFilteringMenu;
        [SerializeField] private UIPieceSelectionMenu _pieceSelectionMenu;
        [SerializeField] private Button _exitButton;


        public void Initialize(QuestMakerTableState state)
        {
            _pieceSelectionMenu.Initialize(state, this);
            _pieceFilteringMenu.Initialize(state, this);
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitButton);
            _pieceFilteringMenu.SelectPieceType(m_SelectedType);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitButton);
        }

        public void SelectPieceType(PieceType type)
        {
            m_SelectedType = type;
            _pieceSelectionMenu.ShowPiecesOfType(type);
            _pieceSelectionMenu.UnselectPiece();
            _pieceSelectionMenu.SelectFirstPieceOfType();
        }

        // Called by the Use UI button
        public void SelectPieceFromStorage(ID pieceID)
        {
            OnSelectPieceFromStorage.Invoke(pieceID);
        }

        private void OnExitButton()
        {
            OnExit?.Invoke();
        }
    }
}