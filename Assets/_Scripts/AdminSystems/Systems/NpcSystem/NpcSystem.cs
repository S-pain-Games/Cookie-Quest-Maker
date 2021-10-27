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

        // DEV ONLY
        PopulateNpcsData();
    }

    // Called just before the start of the day to set the
    // dialogue that each NPC has to say that day
    public void PopulateNpcsData()
    {
        List<int> completedStoriesIDList = _storyDB.m_CompletedStories;
        var storiesDB = Admin.g_Instance.storyDB.m_StoriesDB;

        // We make a copy because we dont want to remove elements from the storyDB list.
        // The completed stories should be "finalized" when the player has seen the 
        // repercusion dialogue not when the dialogue is assigned to an npc

        // In this code we also assume that we might have more stories to show than npcs
        // so we have to "cache" and take into account the remaining unshown stories
        // to show them the next day
        List<int> completedStories = SelectCompletedStoriesToTryToShow(completedStoriesIDList, storiesDB);
        List<int> toStartStories = SelectStoriesToTryToStart();

        for (int i = 0; i < _npcDB.m_NpcBehaviour.Count; i++)
        {
            NPCData npcData = _npcDB.m_NpcBehaviour[i].m_NpcData;

            npcData.m_AlreadySpokenTo = false;
            npcData.m_Dialogue.Clear();

            // Try to preappend a result of a completed story
            // only if there are remaining stories to show
            if (completedStories.Count > 0)
            {
                Story s = _storyDB.m_StoriesDB[completedStories[0]];
                npcData.m_Dialogue.Add(s.m_QuestResult);
                npcData.m_StoryIDToFinalizeOnInteract = completedStories[0];

                completedStories.RemoveAt(0);
                npcData.m_Dialogue.Add("Anyways...");
            }

            // Append a new story dialogue
            // only if there are new stories to append
            if (toStartStories.Count > 0)
            {
                npcData.m_Dialogue.Add(_storyDB.m_StoriesDB[toStartStories[0]].m_StoryData.m_IntroductionPhrase);
                npcData.m_StoryIDToStartOnInteract = toStartStories[0];
                toStartStories.RemoveAt(0);
            }
        }
    }

    private List<int> SelectStoriesToTryToStart()
    {
        var storiesToStartIds = _storyDB.m_StoriesToStart;
        int availableStoriesToStart = Mathf.Min(storiesToStartIds.Count, 3);
        List<int> sStory = new List<int>();
        for (int j = 0; j < availableStoriesToStart; j++)
        {
            sStory.Add(storiesToStartIds[j]);
        }

        return sStory;
    }

    private List<int> SelectCompletedStoriesToTryToShow(List<int> completedStoriesIDList, Dictionary<int, Story> storiesDB)
    {
        int avaliableCompletedStories = Mathf.Min(completedStoriesIDList.Count, 3);
        List<int> cStoriesIds = new List<int>(); // Completed Stories Ids
        for (int i = 0; i < avaliableCompletedStories; i++)
        {
            cStoriesIds.Add(completedStoriesIDList[i]);
        }

        return cStoriesIds;
    }
}

public class NPCData
{
    // Dialogue lines that the npc has to say
    public List<string> m_Dialogue = new List<string>();
    // The ID of the story that will start when the player interacts with the NPC
    public int m_StoryIDToStartOnInteract;
    // ID of the story to finalize on dialogue
    public int m_StoryIDToFinalizeOnInteract;

    public bool m_AlreadySpokenTo;
    public List<string> m_AlreadySpokenToDialogue = new List<string> { "I have nothing more to say" };
}
