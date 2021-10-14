using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class PlayerInteractBehaviour : MonoBehaviour
{
    private List<Interactable> m_InteractablesInRange = new List<Interactable>();

    [SerializeField]
    private VoidEventHandle _onInteractInput;

    private void OnEnable()
    {
        _onInteractInput.OnEvent += OnInteractInputRecieved;
    }

    private void OnDisable()
    {
        _onInteractInput.OnEvent -= OnInteractInputRecieved;
    }

    private void OnInteractInputRecieved()
    {
        if (m_InteractablesInRange.Count > 0)
        {
            m_InteractablesInRange[0].Interact(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable interactable))
        {
            m_InteractablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable interactable))
        {
            m_InteractablesInRange.Remove(interactable);
        }
    }
}
