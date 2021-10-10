using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Debugging;
using Events;

public class WordBehaviour : MonoBehaviour
{
    public WordPiece Piece { get => _piece; }

    [Header("Piece Settings")]
    [SerializeField] private WordPiece _piece;

    [Header("UI Dependencies")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _rect;
    [SerializeField] private UIDraggable _draggable;

    private GraphicRaycaster _raycaster;
    private List<RaycastResult> _results = new List<RaycastResult>();

    private void Awake()
    {
        _raycaster = _canvas.GetComponent<GraphicRaycaster>();
    }

    private void OnEnable()
    {
        _draggable.OnPointerUpEvent += TryToFitInSocket;
    }

    private void OnDisable()
    {
        _draggable.OnPointerUpEvent -= TryToFitInSocket;
    }

    public void TryToFitInSocket(PointerEventData pointerEventData)
    {
        PointerEventData eventData = new PointerEventData(_canvas.GetComponent<EventSystem>());
        eventData.position = pointerEventData.position;

        _results.Clear();
        _raycaster.Raycast(eventData, _results);

        for (int i = 0; i < _results.Count; i++)
        {
            if (_results[i].gameObject.TryGetComponent(out WordPieceSocket socket))
            {
                Logg.Log("Socketed");
                transform.position = _results[i].gameObject.transform.position;
            }
        }
    }
}
