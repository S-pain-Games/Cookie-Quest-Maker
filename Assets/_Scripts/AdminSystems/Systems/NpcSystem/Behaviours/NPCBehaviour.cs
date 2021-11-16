using CQM.Components;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour, IInteractableEntity
{
    public NPCBehaviourData m_NpcData = new NPCBehaviourData();

    private bool m_Interacting = false;

    private Event<ShowDialogueEvtArgs> _showDialogueCmd;
    private Event<ID> _startStoryCmd;
    private Event<ID> _finalizeStoryCmd;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
        _startStoryCmd = evtSys.GetCommandByName<Event<ID>>("story_sys", "start_story");
        _finalizeStoryCmd = evtSys.GetCommandByName<Event<ID>>("story_sys", "finalize_story");
    }

    public void OnInteract()
    {
        if (m_Interacting) return;

        m_Interacting = true;
        if (!m_NpcData.m_AlreadySpokenTo)
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
                m_NpcData.m_Dialogue,
                new ID("meri"),
                () => { DialogueWithNpcFinishedCallback(); }));

            m_NpcData.m_AlreadySpokenTo = true;
        }
        else
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
               new List<string> { m_NpcData.m_AlreadySpokenToDialogue[Random.Range(0, m_NpcData.m_AlreadySpokenToDialogue.Count)] }, // TODO: Fix this atrocity
               new ID("meri"),
               () => m_Interacting = false));
        }
    }

    private void DialogueWithNpcFinishedCallback()
    {
        if (m_NpcData.m_HasToFinalizeAStory)
            _finalizeStoryCmd.Invoke(m_NpcData.m_StoryIDToFinalizeOnInteract);
        if (m_NpcData.m_HasToStartAStory)
        {
            // Please do not write lines this horrible in production code, this is only for debugging
            Debug.Log("Started Story : " + Admin.Global.Components.GetComponentContainer<StoryInfoComponent>()[m_NpcData.m_StoryIDToStartOnInteract].m_StoryData.m_Title);
            _startStoryCmd.Invoke(m_NpcData.m_StoryIDToStartOnInteract);
        }
        m_Interacting = false;
    }
}