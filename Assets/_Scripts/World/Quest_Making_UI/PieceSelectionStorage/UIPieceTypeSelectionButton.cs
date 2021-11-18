using CQM.Components;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CQM.UI.QuestMakingTable
{
    public class UIPieceTypeSelectionButton : MonoBehaviour
    {
        public event Action<QuestPieceFunctionalComponent.PieceType> OnPieceTypeSelected;
        public event Action OnSelected;
        public event Action OnUnselected;

        public QuestPieceFunctionalComponent.PieceType PieceType => m_PieceType;

        [SerializeField] private Sprite m_SelectedSprite;
        [SerializeField] private Sprite m_UnselectedSprite;
        [SerializeField] private QuestPieceFunctionalComponent.PieceType m_PieceType;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;


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
            _image.transform.DOKill();
            _image.transform.DOScale(1.7f, 0.3f).OnComplete(() => _image.transform.DOScale(1.5f, 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
            _image.sprite = m_SelectedSprite;
        }

        public void SetAsUnselected()
        {
            _image.transform.DOKill();
            _image.transform.DOScale(1.0f, 0.3f);
            _image.sprite = m_UnselectedSprite;
        }
    }
}