using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Gameplay
{
    public class PieceFilteringMenu : MonoBehaviour
    {
        public event Action<QuestPieceFunctionalComponent.PieceType> OnFilterSelected;

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
            OnFilterSelected?.Invoke(type);
        }
    }
}