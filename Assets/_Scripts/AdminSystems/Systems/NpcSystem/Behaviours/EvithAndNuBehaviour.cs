using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvithAndNuBehaviour : MonoBehaviour, IInteractableEntity
{
    private bool m_Interacting = false;

    [SerializeField] private UnityEvent _onFinishInteract;
    [SerializeField] private GameObject _button_next_day;

    [SerializeField] private EvithBehaviour _evithBehaviour;
    [SerializeField] private NuBehaviour _nuBehaviour;

    public void OnInteract()
    {
        if (m_Interacting) return;

        m_Interacting = true;
        _button_next_day.SetActive(false);

        _nuBehaviour.ShowDialog();
    }

    public void OnNuDialogEnded()
    {
        _evithBehaviour.ShowDialog();
    }

    public void OnEvithDialogEnded()
    {
        DialoguesFinished();
    }


    private void DialoguesFinished()
    {
        m_Interacting = false;
        _onFinishInteract?.Invoke();
    }

}
