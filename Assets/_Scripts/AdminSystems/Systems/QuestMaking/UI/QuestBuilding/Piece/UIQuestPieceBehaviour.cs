using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CQM.QuestMaking.UI
{
    [RequireComponent(typeof(UIDraggable))]
    [RequireComponent(typeof(UIPressable))]
    [RequireComponent(typeof(RectTransform))]
    public class UIQuestPieceBehaviour : MonoBehaviour
    {
        public QuestPiece Piece { get => m_Piece; }
        public UIDraggable Draggable { get => _draggable; set => _draggable = value; }
        public UIPressable Pressable { get => _pressable; set => _pressable = value; }

        // Events
        public event Action<UIQuestPieceBehaviour, UIPieceSocketBehaviour> OnSocketCorrectly;
        public event Action<UIQuestPieceBehaviour> OnSocketFailed;
        public event Action<UIQuestPieceBehaviour> OnUnsocketed;
        public event Action<UIQuestPieceBehaviour> OnSelected;
        public event Action<UIQuestPieceBehaviour> OnUnselect;

        [SerializeField]
        private QuestPiece m_Piece;
        [SerializeField]
        private Canvas _canvas;

        private bool m_Socketed = false;
        private UIPieceSocketBehaviour _currentSocket;
        private UIDraggable _draggable;
        private UIPressable _pressable;
        private GraphicRaycaster _raycaster;
        private List<RaycastResult> m_Results = new List<RaycastResult>();

        private void Awake()
        {
            _raycaster = _canvas.GetComponent<GraphicRaycaster>();
            _draggable = GetComponent<UIDraggable>();
            _pressable = GetComponent<UIPressable>();
        }

        private void OnEnable()
        {
            _draggable.OnBeginDragEvent += TryToUnsocket;
            _draggable.OnEndDragEvent += TryToFitInSocket;
            _pressable.OnPointerDownEvent += OnSelectedHandle;
            _pressable.OnPointerUpEvent += OnUnselectedHandle;
        }

        private void OnDisable()
        {
            _draggable.OnBeginDragEvent -= TryToUnsocket;
            _draggable.OnEndDragEvent -= TryToFitInSocket;
            _pressable.OnPointerDownEvent -= OnSelectedHandle;
        }

        // We assume there are no overlaping sockets
        private void TryToFitInSocket(PointerEventData pointerEventData)
        {
            //Fill the results array with raycast hits
            FillRaycastData(Input.mousePosition);

            for (int i = 0; i < m_Results.Count; i++)
            {
                // For every result check if we found a socket
                if (m_Results[i].gameObject.TryGetComponent(out UIPieceSocketBehaviour socket))
                {
                    if (socket.TryToSetPiece(m_Piece))
                    {
                        m_Socketed = true;
                        _currentSocket = socket;
                        OnSocketCorrectly?.Invoke(this, socket);
                    }
                    else
                    {
                        OnSocketFailed?.Invoke(this);
                    }

                    //// We only check the first socket that we find
                    break;
                }
            }
        }

        private void FillRaycastData(Vector2 pos)
        {
            PointerEventData pData = new PointerEventData(_canvas.GetComponent<EventSystem>());
            pData.position = pos;

            m_Results.Clear();
            _raycaster.Raycast(pData, m_Results);
        }

        private void TryToUnsocket(PointerEventData obj)
        {
            if (m_Socketed)
            {
                _currentSocket.RemovePiece();
                _currentSocket = null;
                m_Socketed = false;
                OnUnsocketed?.Invoke(this);
            }
        }

        private void OnSelectedHandle(PointerEventData obj)
        {
            // Somewhat redundant right now
            OnSelected?.Invoke(this);
        }

        private void OnUnselectedHandle(PointerEventData obj)
        {
            OnUnselect?.Invoke(this);
        }
    }
}