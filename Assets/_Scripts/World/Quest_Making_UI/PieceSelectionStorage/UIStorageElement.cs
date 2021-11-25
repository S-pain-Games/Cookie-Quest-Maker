using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class UIStorageElement : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<ID> OnSelected;

    public ID pieceID; // Used as an ID

    [SerializeField] private Sprite m_IdleSprite;
    [SerializeField] private Sprite m_HoverSelectedSprite;
    private TextMeshProUGUI _textComp;
    private Image _image;

    private void Awake()
    {
        _textComp = GetComponentInChildren<TextMeshProUGUI>();
        _image = GetComponent<Image>();
    }

    public void Build(QuestPieceUIComponent piece)
    {
        _textComp.text = piece.m_Name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelected?.Invoke(pieceID);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = m_HoverSelectedSprite;
        transform.DOScale(1.2f, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = m_IdleSprite;
        transform.DOScale(1.0f, 0.3f);
    }
}