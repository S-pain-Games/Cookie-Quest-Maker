using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CQM.UI.QuestMakingTable
{
    // Handles all the Storage UI functionality
    public class UIPieceStorageManager : MonoBehaviour
    {
        public event Action<ID> OnPickPiece;
        public event Action OnExit;

        public QuestPieceFunctionalComponent.PieceType m_SelectedType;

        [SerializeField] private UIPieceTypeSelectionMenu _pieceFilteringMenu;
        [SerializeField] private UIPieceSelectionMenu _pieceSelectionMenu;
        [SerializeField] private Button _exitButton;


        public void Initialize(QuestMakerTableState state)
        {
            _pieceSelectionMenu.Initialize(state);
        }

        private void OnEnable()
        {
            _pieceFilteringMenu.OnPieceTypeSelected += PieceFiltering_OnFilterSelected;
            _pieceSelectionMenu.OnUsePiece += PieceSelection_OnUsePiece;

            _exitButton.onClick.AddListener(OnExitButton);
        }

        private void OnDisable()
        {
            _pieceFilteringMenu.OnPieceTypeSelected -= PieceFiltering_OnFilterSelected;
            _pieceSelectionMenu.OnUsePiece -= PieceSelection_OnUsePiece;

            _exitButton.onClick.RemoveListener(OnExitButton);
        }

        private void PieceFiltering_OnFilterSelected(QuestPieceFunctionalComponent.PieceType type)
        {
            m_SelectedType = type;
            _pieceSelectionMenu.RefreshSelectablePieces(type);
        }

        // Called by the Use UI button
        private void PieceSelection_OnUsePiece(ID pieceID)
        {
            OnPickPiece.Invoke(pieceID);
        }

        private void OnExitButton()
        {
            OnExit?.Invoke();
        }
    }
}