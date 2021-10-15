using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

[RequireComponent(typeof(Interactable))]
public class QuestMakingTableBehaviour : MonoBehaviour
{
    [SerializeField]
    private VoidEventHandle _openQuestMakingTable;
    private Interactable _interactable;

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();
    }

    private void OnEnable()
    {
        _interactable.OnInteract += OnInteract;
    }

    private void OnDisable()
    {
        _interactable.OnInteract -= OnInteract;
    }

    private void OnInteract()
    {
        _openQuestMakingTable.Invoke(gameObject);
    }
}
