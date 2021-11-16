using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.UI.QuestMakingTable
{
    public class UIPieceTypeSelectionMenu : MonoBehaviour
    {
        public event Action<QuestPieceFunctionalComponent.PieceType> OnPieceTypeSelected;

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

        private void ButtonClickedHandle(QuestPieceFunctionalComponent.PieceType type)
        {
            OnPieceTypeSelected?.Invoke(type);
        }
    }
}