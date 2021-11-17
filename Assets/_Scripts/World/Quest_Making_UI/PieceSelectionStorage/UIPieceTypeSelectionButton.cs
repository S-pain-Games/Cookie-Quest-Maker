using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CQM.UI.QuestMakingTable
{
    [RequireComponent(typeof(Button))]
    public class UIPieceTypeSelectionButton : MonoBehaviour
    {
        public event Action<QuestPieceFunctionalComponent.PieceType> OnPieceTypeSelected;
        public event Action OnSelected;
        public event Action OnUnselected;

        public QuestPieceFunctionalComponent.PieceType PieceType => m_PieceType;


        [SerializeField] private QuestPieceFunctionalComponent.PieceType m_PieceType;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickHandle);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickHandle);
        }

        private void OnClickHandle()
        {
            OnPieceTypeSelected?.Invoke(m_PieceType);
        }

        public void SetAsSelected()
        {
            OnSelected?.Invoke();
        }

        public void SetAsUnselected()
        {
            OnUnselected?.Invoke();
        }
    }
}