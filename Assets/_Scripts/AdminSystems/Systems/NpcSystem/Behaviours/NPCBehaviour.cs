using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour, IInteractableEntity
{
    public NPCData m_NpcData = new NPCData();

    private bool m_Interacting = false;

    private GameEventSystem _eventSystem;
    private Event<GameEventSystem.ShowDialogueEvtArgs> showDialogueEvt;
    private Event<int> startStoryEvt;
    private Event<int> finalizeStoryEvt;

    private void Awake()
    {
        _eventSystem = Admin.g_Instance.gameEventSystem;

        var evtIds = Admin.g_Instance.ID.events;
        _eventSystem.DialogueSystemMessaging.GetEvent(evtIds.show_dialogue, out showDialogueEvt);
        _eventSystem.StorySystemMessaging.GetEvent(evtIds.start_story, out startStoryEvt);
        _eventSystem.StorySystemMessaging.GetEvent(evtIds.finalize_story, out finalizeStoryEvt);
    }

    public void OnInteract()
    {
        if (m_Interacting) return;

        m_Interacting = true;
        if (!m_NpcData.m_AlreadySpokenTo)
        {
            showDialogueEvt.Invoke(new GameEventSystem.ShowDialogueEvtArgs(
                m_NpcData.m_Dialogue,
                "Mamarrachus",
                () => { DialogueWithNpcFinishedCallback(); }));

            m_NpcData.m_AlreadySpokenTo = true;
        }
        else
        {
            showDialogueEvt.Invoke(new GameEventSystem.ShowDialogueEvtArgs(
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
            Debug.Log("Started Story : " + Admin.g_Instance.storyDB.m_StoriesDB[m_NpcData.m_StoryIDToStartOnInteract].m_StoryData.m_Title);
            startStoryEvt.Invoke(m_NpcData.m_StoryIDToStartOnInteract);
        }
        m_Interacting = false;
    }
}