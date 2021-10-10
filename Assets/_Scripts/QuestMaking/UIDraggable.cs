using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public event Action<PointerEventData> OnPointerDownEvent;
    public event Action<PointerEventData> OnPointerUpEvent;
    public event Action<PointerEventData> OnBeginDragEvent;
    public event Action<PointerEventData> OnEndDragEvent;
    public event Action<PointerEventData> OnDragEvent;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _holdScale = 1.2f;

    private RectTransform _rect;
    private Vector3 _startScale;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _startScale = _rect.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rect.localScale *= _holdScale;
        OnPointerDownEvent?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rect.localScale = _startScale;
        OnPointerUpEvent?.Invoke(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rect.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        OnDragEvent?.Invoke(eventData);
    }
}
