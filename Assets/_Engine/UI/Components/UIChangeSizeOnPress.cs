using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UIPressable))]
[RequireComponent(typeof(RectTransform))]
public class UIChangeSizeOnPress : MonoBehaviour
{
    [SerializeField] private float _holdScale = 1.2f;
    private Vector3 _startScale;

    private RectTransform _rect;
    private UIPressable _pressable;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _pressable = GetComponent<UIPressable>();
    }

    private void Start()
    {
        _startScale = _rect.localScale;
    }

    private void OnEnable()
    {
        _pressable.OnPointerDownEvent += OnPointerDown;
        _pressable.OnPointerUpEvent += OnPointerUp;
    }

    private void OnDisable()
    {
        _pressable.OnPointerDownEvent -= OnPointerDown;
        _pressable.OnPointerUpEvent -= OnPointerUp;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rect.localScale = _holdScale * _startScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rect.localScale = _startScale;
    }
}