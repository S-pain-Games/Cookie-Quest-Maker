using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CQM.QuestMaking.UI
{
    [RequireComponent(typeof(UIPressable))]
    [RequireComponent(typeof(UIDraggable))]
    public class UIStoredPiece : MonoBehaviour
    {
        public event Action<UIStoredPiece> OnSelected;

        private UIPressable _pressable;
        private UIDraggable _draggable;

        private void Awake()
        {
            _pressable = GetComponent<UIPressable>();
            _draggable = GetComponent<UIDraggable>();
        }

        private void OnEnable()
        {
            _pressable.OnPointerDownEvent += OnPressedHandle;
        }

        private void OnPressedHandle(PointerEventData obj)
        {
            OnSelected?.Invoke(this);
        }

        private void OnDisable()
        {

        }
    }
}