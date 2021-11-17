using CQM.UI.QuestMakingTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIPieceTypeButtonAnimations : MonoBehaviour
{
    private UIPieceTypeSelectionButton _typeButton;

    private void Awake()
    {
        _typeButton = GetComponent<UIPieceTypeSelectionButton>();
    }

    private void OnEnable()
    {
        _typeButton.OnSelected += OnSelected;
        _typeButton.OnUnselected += OnUnselected;
    }

    private void OnDisable()
    {
        _typeButton.OnSelected -= OnSelected;
        _typeButton.OnUnselected -= OnUnselected;
    }

    private void OnUnselected()
    {
        transform.DOKill();
        transform.DOScale(1.0f, 0.3f);
    }

    private void OnSelected()
    {
        transform.DOKill();
        transform.DOScale(1.5f, 0.3f).OnComplete(() => transform.DOScale(1.5f, 0.3f).SetLoops(-1, LoopType.Yoyo));
    }
}
