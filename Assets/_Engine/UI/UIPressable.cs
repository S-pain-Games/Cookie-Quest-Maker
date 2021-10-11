using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPressable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<PointerEventData> OnPointerDownEvent;
    public event Action<PointerEventData> OnPointerUpEvent;

    #region UNITY_EDITOR
#if UNITY_EDITOR
    [SerializeField] private bool _enableLogs = false;
#endif
    #endregion

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke(eventData);

        #region UNITY_EDITOR
#if UNITY_EDITOR
        if (_enableLogs)
            Logg.Log("OnPointerDown", gameObject.name, gameObject);
#endif
        #endregion
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke(eventData);

        #region UNITY_EDITOR
#if UNITY_EDITOR
        if (_enableLogs)
            Logg.Log("OnPointerUp", gameObject.name, gameObject);
#endif
        #endregion
    }
}
