using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIQuestPieceBehaviour))]
[RequireComponent(typeof(UIDraggable))]
[RequireComponent(typeof(UIPressable))]
public class UIQuestPieceEffects : MonoBehaviour
{
    private UIQuestPieceBehaviour _behaviour;
    private UIDraggable _draggable;
    private UIPressable _pressable;

    private void Awake()
    {
        _behaviour = GetComponent<UIQuestPieceBehaviour>();
        _draggable = GetComponent<UIDraggable>();
        _pressable = GetComponent<UIPressable>();
    }

    private void OnEnable()
    {
        _behaviour.OnSocketCorrectly += OnSocketedCorrectly;
        _behaviour.OnSocketFailed += OnSocketedFailed;
        _behaviour.OnUnsocketed += OnUnsocketed;
    }

    private void OnUnsocketed()
    {
    }

    private void OnSocketedFailed()
    {
    }

    private void OnSocketedCorrectly(Vector3 pos)
    {
        transform.position = pos;
    }
}
