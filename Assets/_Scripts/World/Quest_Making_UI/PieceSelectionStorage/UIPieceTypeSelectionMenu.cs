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
                m_Buttons[i].OnPieceTypeSelected += ButtonClickedHandle;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                m_Buttons[i].OnPieceTypeSelected -= ButtonClickedHandle;
            }
        }

        private void ButtonClickedHandle(QuestPieceFunctionalComponent.PieceType type)
        {
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                m_Buttons[i].SetAsUnselected();
            }

            var b = m_Buttons.Find((b) => b.PieceType == type);
            b.SetAsSelected();

            OnPieceTypeSelected?.Invoke(type);
        }

        public void SetSelectedType(QuestPieceFunctionalComponent.PieceType m_SelectedType)
        {
            var b = m_Buttons.Find((b) => b.PieceType == m_SelectedType);
            b.SetAsSelected();
        }
    }
}