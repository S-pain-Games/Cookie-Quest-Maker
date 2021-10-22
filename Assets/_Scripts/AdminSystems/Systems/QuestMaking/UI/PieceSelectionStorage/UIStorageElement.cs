using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

[RequireComponent(typeof(UIPressable))]
public class UIStorageElement : MonoBehaviour
{
    public event Action<QuestPiece> OnSelected;

    public RectTransform m_RectTransf;
    public string elemName = "Unnamed";
    public QuestPiece questPiece;

    private TextMeshProUGUI _textComp;
    private UIPressable pressable;

    private void Awake()
    {
        pressable = GetComponent<UIPressable>();
        _textComp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        pressable.OnPointerDownEvent += OnPointerDown;
        pressable.OnPointerUpEvent += OnPointerUpEvent;
    }

    private void OnDisable()
    {
        pressable.OnPointerDownEvent -= OnPointerDown;
        pressable.OnPointerUpEvent -= OnPointerUpEvent;
    }

    private void OnPointerUpEvent(PointerEventData obj)
    {
    }

    private void OnPointerDown(PointerEventData obj)
    {
        OnSelected?.Invoke(questPiece);
    }

    public void Build()
    {
        _textComp.text = elemName;
    }
}