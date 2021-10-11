using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Debugging;
using Events;
using System;

[RequireComponent(typeof(UIDraggable))]
[RequireComponent(typeof(UIPressable))]
[RequireComponent(typeof(RectTransform))]
public class WordBehaviour : MonoBehaviour
{
    public Word Piece { get => _piece; }

    [Header("Piece Settings")]
    [SerializeField] private Word _piece;

    [Header("UI Dependencies")]
    [SerializeField] private Canvas _canvas;

    // Events
    public event Action OnSocketCorrectly;
    public event Action OnSocketFailed;
    public event Action OnUnsocket;

    // Private Dependencies
    private RectTransform _rect;
    private UIDraggable _draggable;

    // Raycast
    private GraphicRaycaster _raycaster;
    private List<RaycastResult> _results = new List<RaycastResult>();

    // We use a bool to avoid null-checking Unity Objects
    private bool _socketed = false;
    private WordSocketBehaviour _currentSocket;

    private void Awake()
    {
        _raycaster = _canvas.GetComponent<GraphicRaycaster>();
        _draggable = GetComponent<UIDraggable>();
        _rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _draggable.OnBeginDragEvent += Unsocket;
        _draggable.OnEndDragEvent += TryToFitInSocket;
    }

    private void OnDisable()
    {
        _draggable.OnBeginDragEvent -= Unsocket;
        _draggable.OnEndDragEvent -= TryToFitInSocket;
    }

    private void Unsocket(PointerEventData obj)
    {
        if (_socketed)
        {
            // We could deffer the logic to the Current Socket class
            // but given that this is the only place where it should be
            // modified it would just increase complexity
            _currentSocket.filled = false;
            _currentSocket.piece = null;

            _currentSocket = null;
            _socketed = false;

            OnUnsocket?.Invoke();
        }
    }

    // We assume there are no overlaping sockets
    public void TryToFitInSocket(PointerEventData pointerEventData)
    {
        // Generate Unity Event Data (GC Alloc)
        PointerEventData eventData = new PointerEventData(_canvas.GetComponent<EventSystem>());
        eventData.position = pointerEventData.position;

        _results.Clear(); // We use a predefined list to somewhat avoid GC
        _raycaster.Raycast(eventData, _results);

        for (int i = 0; i < _results.Count; i++)
        {
            // For every result check if we found a socket
            if (_results[i].gameObject.TryGetComponent(out WordSocketBehaviour socket))
            {
                // Check if the Socket is already filled or the Word required is differentn
                if (!socket.filled && socket.requiredType == _piece.Type)
                {
                    // Snap the word object to the socket
                    transform.position = _results[i].gameObject.transform.position;
                    _socketed = true;

                    _currentSocket = socket;
                    _currentSocket.filled = true;
                    _currentSocket.piece = _piece;

                    OnSocketCorrectly?.Invoke();
                }
                else
                {
                    OnSocketFailed?.Invoke();
                }

                // We only check the first socket that we find
                break;
            }
        }
    }
}
