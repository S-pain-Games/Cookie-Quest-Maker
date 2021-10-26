using UnityEngine;

public class NPCBehaviour : MonoBehaviour, IInteractableEntity
{
    public NPCData m_NpcData = new NPCData();

    private GameEventSystem _messagingSystem;
    private Event<GameEventSystem.ShowDialogueEvtArgs> showDialogueEvt;
    private Event<int> startStoryEvt;

    private void Awake()
    {
        _messagingSystem = Admin.g_Instance.gameEventSystem;

        var evtIds = Admin.g_Instance.ID.events;
        _messagingSystem.DialogueSystemMessaging.GetEvent(evtIds.show_dialogue, out showDialogueEvt);
        _messagingSystem.StorySystemMessaging.GetEvent(evtIds.start_story, out startStoryEvt);
    }

    public void OnInteract()
    {
        if (!m_NpcData.m_AlreadySpokenTo)
        {

            showDialogueEvt.Invoke(new GameEventSystem.ShowDialogueEvtArgs(
                m_NpcData.m_Dialogue,
                "Mamarrachus",
                () => { StartStoryFromNpcData(); }));

            m_NpcData.m_AlreadySpokenTo = true;
        }
        else
        {
            showDialogueEvt.Invoke(new GameEventSystem.ShowDialogueEvtArgs(
               m_NpcData.m_AlreadySpokenToDialogue,
               "Mamarrachus",
               null));
        }
    }

    private void StartStoryFromNpcData()
    {
        startStoryEvt.Invoke(m_NpcData.m_StoryIDToStartOnInteract);

        // Please do not write lines this horrible in production code, this is only for debugging
        Debug.Log("Started Story : " + Admin.g_Instance.storyDB.m_StoriesDB[m_NpcData.m_StoryIDToStartOnInteract].m_StoryData.m_Title);
    }
}