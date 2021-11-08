using CQM.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIPieceTypeSelectionButton : MonoBehaviour
{
    public event Action<QuestPieceFunctionalComponent.PieceType> OnButtonClicked;

    [SerializeField]
    private QuestPieceFunctionalComponent.PieceType m_PieceType;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickHandle);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickHandle);
    }

    private void OnClickHandle()
    {
        OnButtonClicked?.Invoke(m_PieceType);
    }
}
