using UnityEngine;

public class NPCBehaviour : MonoBehaviour, IInteractableEntity
{
    public NPCData m_NpcData = new NPCData();

    // TODO: Decouple this with an event system
    private DialogueSystem _dialogueSystem;
    private StorySystem _storySystem;

    private void Awake()
    {
        _dialogueSystem = Admin.g_Instance.dialogueSystem;
        _storySystem = Admin.g_Instance.storySystem;
    }

    public void OnInteract()
    {
        if (!m_NpcData.m_AlreadySpokenTo)
        {
            _dialogueSystem.ShowDialogue(m_NpcData.m_Dialogue, "Mamarrachus", () => { StartStoryFromNpcData(); });
            m_NpcData.m_AlreadySpokenTo = true;
        }
        else
        {
            _dialogueSystem.ShowDialogue(m_NpcData.m_AlreadySpokenToDialogue, "Mamarrachus");
        }
    }

    private void StartStoryFromNpcData()
    {
        _storySystem.StartStory(m_NpcData.m_StoryIDToStartOnInteract);

        // Please do not write lines this horrible in production code, this is only for debugging
        Debug.Log("Started Story : " + Admin.g_Instance.storyDB.m_StoriesDB[m_NpcData.m_StoryIDToStartOnInteract].m_StoryData.m_Title);
    }
}