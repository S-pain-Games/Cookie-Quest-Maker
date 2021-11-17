using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CQM.UI.QuestMakingTable
{
    public class UIPieceTypeSelectionMenu : MonoBehaviour
    {
        [SerializeField] private List<UIPieceTypeSelectionButton> m_Buttons = new List<UIPieceTypeSelectionButton>();
        private UIPieceStorageManager _storage;
        private QuestMakerTableState _state;

        public void Initialize(QuestMakerTableState state, UIPieceStorageManager storage)
        {
            _state = state;
            _storage = storage;
        }

        private void Awake()
        {
            // We never unsubscribe from the events, this *could* be bad
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                m_Buttons[i].OnPieceTypeSelected += SelectPieceType;
            }
        }

        public void SelectPieceType(QuestPieceFunctionalComponent.PieceType type)
        {
            UnselectAllButtons();
            SelectButtonOfType(type);

            _storage.SelectPieceType(type);
        }

        private void UnselectAllButtons()
        {
            for (int i = 0; i < m_Buttons.Count; i++)
                m_Buttons[i].SetAsUnselected();
        }

        private void SelectButtonOfType(QuestPieceFunctionalComponent.PieceType m_SelectedType)
        {
            var b = m_Buttons.Find((b) => b.PieceType == m_SelectedType);
            b.SetAsSelected();
        }
    }
}