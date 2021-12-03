using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvithBehaviour : MonoBehaviour, IInteractableEntity
{
    private bool m_Interacting = false;
    public List<string> m_MainDialogue = new List<string>();
    public List<List<string>> m_RandomIdleDialogue = new List<List<string>>();
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    // HACK
    [SerializeField]
    private UnityEvent onFinishInteract;

    [SerializeField] private GameObject button_next_day;


    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");

        var dialogueComponent = Admin.Global.Components.GetComponentContainer<CharacterDialogueComponent>().GetComponentByID(new ID("evith"));

        // This should be a static function
        m_RandomIdleDialogue.Clear();
        var serializedDialogue = dialogueComponent.m_IdleRandomDialogue;
        for (int j = 0; j < serializedDialogue.Count; j++)
        {
            List<string> characterRandomDialogueTemp = new List<string>(); // Pooling
            for (int k = 0; k < serializedDialogue[j].Count; k++)
            {
                SerializableList<string> l = serializedDialogue[j];
                characterRandomDialogueTemp.Add(l[k]);
            }
            m_RandomIdleDialogue.Add(characterRandomDialogueTemp);
        }
    }

    public void OnInteract()
    {
        if (m_Interacting) return;

        //Deactivate next day button
        button_next_day.SetActive(false);

        m_Interacting = true;
        if (m_MainDialogue.Count > 0)
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
                                    m_MainDialogue,
                                    new ID("evith"),
                                    DialogueFinished));
        }
        else
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
                                    m_RandomIdleDialogue[UnityEngine.Random.Range(0, m_RandomIdleDialogue.Count)],
                                    new ID("evith"),
                                    DialogueFinished));
        }
    }

    public void ShowDialog()
    {
        if (m_MainDialogue.Count > 0)
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(m_MainDialogue, new ID("evith"), DialogueFinished));
        else
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs( m_RandomIdleDialogue[UnityEngine.Random.Range(0, m_RandomIdleDialogue.Count)],new ID("evith"), DialogueFinished));
    }

    /*private void DialogueFinished()
    {
        m_Interacting = false;
        onFinishInteract?.Invoke();
        m_MainDialogue.Clear();
    }*/

    private void DialogueFinished()
    {
        m_MainDialogue.Clear();
        transform.parent.GetComponent<EvithAndNuBehaviour>().OnEvithDialogEnded();
    }
}
