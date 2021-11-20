using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceBarrier : MonoBehaviour
{
    [SerializeField] private FirstDayDeitiesScriptedSequence _sequence;
    private Event<PopupData_GenericPopup> _popupCmd;

    private void Awake()
    {
        GameEventSystem evtSys = Admin.Global.EventSystem;
        _popupCmd = evtSys.GetCommandByName<Event<PopupData_GenericPopup>>("popup_sys", "generic_popup");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsPlayer(collision))
            return;

        if (HasTalkedWithMayor())
        {
            collision.GetComponent<CharacterNavMeshAgentHandler>().InterruptAgentMovement();
            _sequence.ExecuteSequence();
            gameObject.SetActive(false);
        }
        else
        {
            _popupCmd.Invoke(new PopupData_GenericPopup { m_Text = "Aun no has hablado con todos tus clientes", m_TimeAlive = 3.0f });
        }
    }

    private bool HasTalkedWithMayor()
    {
        return Admin.Global.Components.m_StoriesStateComponent.m_OngoingStories.Count > 0;
    }

    private bool IsPlayer(Collider2D collision)
    {
        return collision.GetComponent<CharacterNavMeshAgentHandler>() != null;
    }
}
