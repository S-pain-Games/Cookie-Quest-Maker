using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We calculate the dialogue that the npcs has to say just
// before each start of the day. We have "Story" NPCs that
// are always in the tavern and "Random" NPCs that just say
// random facts or sneaky game tips like
// (I've heard there will be a surplus of X ingredient in 2 days)
public class NpcSystem : MonoBehaviour
{
    public StoryDB _storyDB;
    public NpcDB _npcDB;

    public void Initialize(StoryDB storyDb, NpcDB npcDB)
    {
        _storyDB = storyDb;
        _npcDB = npcDB;
    }

    // Called just before the start of the day to set the
    // dialogue that each NPC has to say that day
    public void PopulateNpcsData()
    {
        List<int> storiesIDList = _storyDB.m_CompletedStories;

        // WE ASSUME that the number of completed stories
        // the previous day its equal or less than the number of "Story" NPCs
        for (int i = 0; i < storiesIDList.Count; i++)
        {
            Story s = _storyDB.m_StoriesDB[storiesIDList[i]];
            NPCData npcData = _npcDB.m_NpcBehaviour[i].m_NpcData;

            npcData.m_AlreadySpokenTo = false;
            npcData.m_Dialogue.Clear();
            npcData.m_Dialogue.Add(s.m_QuestResult);

            if (_storyDB.m_StoriesToStart.Count >= i + 1)
            {
                npcData.m_Dialogue.Add("Anyways...");
                int storyId = _storyDB.m_StoriesToStart[i];
                Story nextStory = _storyDB.m_StoriesDB[storyId];

                npcData.m_Dialogue.Add(nextStory.m_StoryData.m_IntroductionPhrase);
                npcData.m_StoryID = storyId;
            }
        }
    }
}

public class NpcDB
{
    // Populated in the inspector
    public List<NPCBehaviour> m_NpcBehaviour = new List<NPCBehaviour>();
}

public class NPCData
{
    // Contains information like the dialogue that it has to say
    public List<string> m_Dialogue = new List<string>();
    // The ID of the story that will start when the player interacts with the NPC
    public int m_StoryID;
    public bool m_AlreadySpokenTo;
    public List<string> m_AlreadySpokenToDialogue = new List<string>();
}

public class NPCBehaviour : MonoBehaviour
{
    public NPCData m_NpcData = new NPCData();
}