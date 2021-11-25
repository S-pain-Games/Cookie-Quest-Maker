using CQM.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class NuBehaviour : MonoBehaviour, IInteractableEntity
{
    private bool m_Interacting = false;
    public List<string> m_MainDialogue = new List<string>();
    public List<List<string>> m_RandomIdleDialogue = new List<List<string>>();
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;


    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");

        var dialogueComponent = Admin.Global.Components.GetComponentContainer<CharacterDialogueComponent>().GetComponentByID(new ID("nu"));

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

        m_Interacting = true;
        if (m_MainDialogue.Count > 0)
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
                                    m_MainDialogue,
                                    new ID("nu"),
                                    DialogueFinished));
        }
        else
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
                                    m_RandomIdleDialogue[UnityEngine.Random.Range(0, m_RandomIdleDialogue.Count)],
                                    new ID("nu"),
                                    DialogueFinished));
        }
    }

    private void DialogueFinished()
    {
        m_Interacting = false;
        m_MainDialogue.Clear();
    }
}
