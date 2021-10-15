using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UIDraggable))]
[RequireComponent(typeof(UIPressable))]
[RequireComponent(typeof(RectTransform))]
public class QuestPieceBehaviour : MonoBehaviour
{
    // Events
    public event Action OnSocketCorrectly;
    public event Action OnSocketFailed;
    public event Action OnUnsocketed;

    public QuestPiece Piece { get => _piece; }

    [Header("Piece Settings")]
    [SerializeField]
    private QuestPiece _piece;

    [Header("UI Dependencies")]
    [SerializeField]
    private Canvas _canvas;

    // Private Dependencies
    private UIDraggable _draggable;

    // Raycast
    private GraphicRaycaster _raycaster;
    private List<RaycastResult> m_Results = new List<RaycastResult>();

    // We use a bool to avoid null-checking Unity Objects
    private bool m_Socketed = false;
    private PieceSocketBehaviour _currentSocket;

    // We assume there are no overlaping sockets
    public void TryToFitInSocket(PointerEventData pointerEventData)
    {
        // Generate Unity Event Data (GC Alloc)
        PointerEventData eventData = new PointerEventData(_canvas.GetComponent<EventSystem>());
        eventData.position = pointerEventData.position;

        m_Results.Clear(); // We use a predefined list to somewhat avoid GC
        _raycaster.Raycast(eventData, m_Results);

        for (int i = 0; i < m_Results.Count; i++)
        {
            // For every result check if we found a socket
            if (m_Results[i].gameObject.TryGetComponent(out PieceSocketBehaviour socket))
            {
                if (socket.TryToSetPiece(_piece))
                {
                    transform.position = m_Results[i].gameObject.transform.position;
                    m_Socketed = true;
                    _currentSocket = socket;
                    OnSocketCorrectly?.Invoke();
                }
                else
                {
                    OnSocketFailed?.Invoke();
                }

                //// We only check the first socket that we find
                break;
            }
        }
    }

    private void Awake()
    {
        _raycaster = _canvas.GetComponent<GraphicRaycaster>();
        _draggable = GetComponent<UIDraggable>();
    }

    private void OnEnable()
    {
        _draggable.OnBeginDragEvent += TryToUnsocket;
        _draggable.OnEndDragEvent += TryToFitInSocket;
    }

    private void OnDisable()
    {
        _draggable.OnBeginDragEvent -= TryToUnsocket;
        _draggable.OnEndDragEvent -= TryToFitInSocket;
    }

    private void TryToUnsocket(PointerEventData obj)
    {
        if (m_Socketed)
        {
            _currentSocket.RemovePiece();
            _currentSocket = null;
            m_Socketed = false;
            OnUnsocketed?.Invoke();
        }
    }
}
