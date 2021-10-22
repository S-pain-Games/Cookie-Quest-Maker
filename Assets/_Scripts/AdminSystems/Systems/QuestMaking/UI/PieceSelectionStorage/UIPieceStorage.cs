using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.QuestMaking.UI
{
    // Handles over all the Storage UI and functionality
    public class UIPieceStorage : MonoBehaviour
    {
        public QuestPiece.PieceType m_SelectedType;

        [SerializeField]
        private UIPieceTypeSelectionMenu _typeSelectionMenu;
        [SerializeField]
        private UIPieceStorageOfType _pieceStoragePanel;

        private void OnEnable()
        {
            _typeSelectionMenu.OnTypeSelected += PieceTypeSelectedHandle;
        }

        private void OnDisable()
        {
            _typeSelectionMenu.OnTypeSelected -= PieceTypeSelectedHandle;
        }

        private void PieceTypeSelectedHandle(QuestPiece.PieceType type)
        {
            m_SelectedType = type;
            _pieceStoragePanel.Refresh(type);
        }
    }
}