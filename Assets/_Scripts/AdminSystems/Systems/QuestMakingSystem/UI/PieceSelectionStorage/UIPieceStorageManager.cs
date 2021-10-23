using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.QuestMaking.UI
{
    // Handles over all the Storage UI functionality and
    // important data
    public class UIPieceStorageManager : MonoBehaviour
    {
        public event Action<int> OnUsePiece;

        public QuestPiece.PieceType m_SelectedType;

        [SerializeField] private PieceFilteringMenu _pieceFiltering;
        [SerializeField] private UIPieceSelection _pieceSelector;

        private void OnEnable()
        {
            _pieceFiltering.OnFilterSelected += PieceFiltering_OnFilterSelected;
            _pieceSelector.OnUsePiece += PieceSelection_OnUsePiece;
        }

        private void OnDisable()
        {
            _pieceFiltering.OnFilterSelected -= PieceFiltering_OnFilterSelected;
            _pieceSelector.OnUsePiece -= PieceSelection_OnUsePiece;
        }

        private void PieceFiltering_OnFilterSelected(QuestPiece.PieceType type)
        {
            m_SelectedType = type;
            _pieceSelector.Refresh(type);
        }

        // Called by the Use UI button
        private void PieceSelection_OnUsePiece(int pieceID)
        {
            OnUsePiece?.Invoke(pieceID);
        }
    }
}