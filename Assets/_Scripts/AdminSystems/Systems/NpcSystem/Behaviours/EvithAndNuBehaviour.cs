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

    private Event<ID> _soundCmd;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _soundCmd = evtSys.GetCommandByName<Event<ID>>("audio_sys", "play_sound");
    }

    public void OnInteract()
    {
        if (m_Interacting) return;

        m_Interacting = true;
        _button_next_day.SetActive(false);

        _nuBehaviour.ShowDialog();

        //Audio
        var cComp = Admin.Global.Components.GetComponentContainer<CharacterComponent>();
        _soundCmd.Invoke(cComp.GetComponentByID(new ID("nu")).m_AudioID);
    }

    public void OnNuDialogEnded()
    {
        _evithBehaviour.ShowDialog();
        //Audio
        var cComp = Admin.Global.Components.GetComponentContainer<CharacterComponent>();
        _soundCmd.Invoke(cComp.GetComponentByID(new ID("evith")).m_AudioID);
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
