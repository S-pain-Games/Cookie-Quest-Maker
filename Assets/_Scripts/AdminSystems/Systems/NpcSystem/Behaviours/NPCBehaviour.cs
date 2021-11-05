using CQM.Components;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour, IInteractableEntity
{
    public NPCBehaviourData m_NpcData = new NPCBehaviourData();

    private bool m_Interacting = false;

    private Event<ShowDialogueEvtArgs> _showDialogueCmd;
    private Event<int> _startStoryCmd;
    private Event<int> finalizeStoryEvt;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
        _startStoryCmd = evtSys.GetCommandByName<Event<int>>("story_sys", "start_story");
        finalizeStoryEvt = evtSys.GetCommandByName<Event<int>>("story_sys", "finalize_story");
    }

    public void OnInteract()
    {
        if (m_Interacting) return;

        m_Interacting = true;
        if (!m_NpcData.m_AlreadySpokenTo)
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
                m_NpcData.m_Dialogue,
                "Mamarrachus",
                () => { DialogueWithNpcFinishedCallback(); }));

            m_NpcData.m_AlreadySpokenTo = true;
        }
        else
        {
            _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
               new List<string> { m_NpcData.m_AlreadySpokenToDialogue[Random.Range(0, m_NpcData.m_AlreadySpokenToDialogue.Count)] }, // TODO: Fix this atrocity
               "Mamarrachus",
               () => m_Interacting = false));
        }
    }

    private void DialogueWithNpcFinishedCallback()
    {
        if (m_NpcData.m_HasToFinalizeAStory)
            finalizeStoryEvt.Invoke(m_NpcData.m_StoryIDToFinalizeOnInteract);
        if (m_NpcData.m_HasToStartAStory)
        {
            // Please do not write lines this horrible in production code, this is only for debugging
            Debug.Log("Started Story : " + Admin.Global.Database.Stories.GetStoryComponent<StoryInfoComponent>(m_NpcData.m_StoryIDToStartOnInteract).m_StoryData.m_Title);
            _startStoryCmd.Invoke(m_NpcData.m_StoryIDToStartOnInteract);
        }
        m_Interacting = false;
    }
}