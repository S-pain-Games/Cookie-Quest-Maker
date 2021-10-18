using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UIDraggable))]
[RequireComponent(typeof(UIPressable))]
[RequireComponent(typeof(RectTransform))]
public class UIQuestPieceBehaviour : MonoBehaviour
{
    // Events
    public event Action<UIQuestPieceBehaviour, PieceSocketBehaviour> OnSocketCorrectly;
    public event Action<UIQuestPieceBehaviour> OnSocketFailed;
    public event Action<UIQuestPieceBehaviour> OnUnsocketed;
    public event Action<UIQuestPieceBehaviour> OnSelected;
    public event Action<UIQuestPieceBehaviour> OnUnselect;

    [HideInInspector]
    public UIDraggable draggable;
    [HideInInspector]
    public UIPressable pressable;

    public QuestPiece Piece { get => m_Piece; }

    [SerializeField]
    private QuestPiece m_Piece;
    [SerializeField]
    private Canvas _canvas;
    // Raycast
    private GraphicRaycaster _raycaster;
    private List<RaycastResult> m_Results = new List<RaycastResult>();
    // We use a bool to avoid null-checking Unity Objects
    private bool m_Socketed = false;
    private PieceSocketBehaviour _currentSocket;

    // We assume there are no overlaping sockets
    private void TryToFitInSocket(PointerEventData pointerEventData)
    {
        PointerEventData pData = new PointerEventData(_canvas.GetComponent<EventSystem>());
        pData.position = Input.mousePosition;

        m_Results.Clear(); // We use a predefined list to somewhat avoid GC
        _raycaster.Raycast(pData, m_Results);

        for (int i = 0; i < m_Results.Count; i++)
        {
            // For every result check if we found a socket
            if (m_Results[i].gameObject.TryGetComponent(out PieceSocketBehaviour socket))
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

    private void Awake()
    {
        _raycaster = _canvas.GetComponent<GraphicRaycaster>();
        draggable = GetComponent<UIDraggable>();
        pressable = GetComponent<UIPressable>();
    }

    private void OnEnable()
    {
        draggable.OnBeginDragEvent += TryToUnsocket;
        draggable.OnEndDragEvent += TryToFitInSocket;
        pressable.OnPointerDownEvent += OnSelectedHandle;
        pressable.OnPointerUpEvent += OnUnselectedHandle;
    }

    private void OnDisable()
    {
        draggable.OnBeginDragEvent -= TryToUnsocket;
        draggable.OnEndDragEvent -= TryToFitInSocket;
        pressable.OnPointerDownEvent -= OnSelectedHandle;
    }

    private void OnSelectedHandle(PointerEventData obj)
    {
        OnSelected?.Invoke(this);
    }

    private void OnUnselectedHandle(PointerEventData obj)
    {
        OnUnselect?.Invoke(this);
    }
}
