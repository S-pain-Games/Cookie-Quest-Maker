using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.QuestMaking.UI
{
    public class UIPieceFiltering : MonoBehaviour
    {
        public event Action<QuestPiece.PieceType> OnTypeSelected;

        [SerializeField]
        private List<UIPieceTypeSelectionButton> m_Buttons = new List<UIPieceTypeSelectionButton>();

        private void OnEnable()
        {
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                m_Buttons[i].OnButtonClicked += ButtonClickedHandle;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                m_Buttons[i].OnButtonClicked -= ButtonClickedHandle;
            }
        }

        private void ButtonClickedHandle(QuestPiece.PieceType type)
        {
            OnTypeSelected?.Invoke(type);
        }
    }
}