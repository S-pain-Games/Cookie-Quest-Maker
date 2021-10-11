using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Debugging;

[RequireComponent(typeof(RectTransform))]
public class UIDraggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public event Action<PointerEventData> OnBeginDragEvent;
    public event Action<PointerEventData> OnEndDragEvent;
    public event Action<PointerEventData> OnDragEvent;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _holdScale = 1.2f;

    private RectTransform _rect;
    private Vector3 _startScale;

    #region UNITY_EDITOR
#if UNITY_EDITOR
    [SerializeField] private bool _enableLogs = false;
#endif
    #endregion

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(eventData);

        #region UNITY_EDITOR
#if UNITY_EDITOR
        if (_enableLogs)
            Logg.Log("OnBeginDrag", gameObject, filterName: "UI");
#endif
        #endregion
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(eventData);

        #region UNITY_EDITOR
#if UNITY_EDITOR
        if (_enableLogs)
            Logg.Log("OnEndDrag", gameObject, filterName: "UI");
#endif
        #endregion
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rect.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        OnDragEvent?.Invoke(eventData);

        #region UNITY_EDITOR
#if UNITY_EDITOR
        if (_enableLogs)
            Logg.Log("OnDrag", gameObject, filterName: "UI");
#endif
        #endregion
    }
}
