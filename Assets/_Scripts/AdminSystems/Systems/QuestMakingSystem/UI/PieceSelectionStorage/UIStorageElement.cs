using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

[RequireComponent(typeof(UIPressable))]
public class UIStorageElement : MonoBehaviour
{
    public event Action<int> OnSelected;

    public int pieceID; // Used as an ID

    private TextMeshProUGUI _textComp;
    private UIPressable _pressable;

    private void Awake()
    {
        _pressable = GetComponent<UIPressable>();
        _textComp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _pressable.OnPointerDownEvent += OnPointerDown;
        _pressable.OnPointerUpEvent += OnPointerUpEvent;
    }

    private void OnDisable()
    {
        _pressable.OnPointerDownEvent -= OnPointerDown;
        _pressable.OnPointerUpEvent -= OnPointerUpEvent;
    }

    public void Build(UIQuestPieceComponent piece)
    {
        _textComp.text = piece.m_Name;
    }

    private void OnPointerUpEvent(PointerEventData obj)
    {
    }

    private void OnPointerDown(PointerEventData obj)
    {
        OnSelected?.Invoke(pieceID);
    }
}