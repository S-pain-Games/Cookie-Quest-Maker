using CQM.Components;
using DG.Tweening;
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
            transform.DOKill();
            transform.DOScale(1.7f, 0.3f).OnComplete(() => transform.DOScale(1.5f, 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
        }

        public void SetAsUnselected()
        {
            transform.DOKill();
            transform.DOScale(1.0f, 0.3f);
        }
    }
}