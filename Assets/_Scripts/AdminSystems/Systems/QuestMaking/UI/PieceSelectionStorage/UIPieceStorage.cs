using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.QuestMaking.UI
{
    public class UIPieceStorage : MonoBehaviour
    {
        [SerializeField]
        private UIPieceTypeSelectionMenu _typeSelectionMenu;
        private QuestPiece.PieceType m_SelectedType;

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
        }
    }
}