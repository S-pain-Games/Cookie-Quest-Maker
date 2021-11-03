using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.Databases;


namespace CQM.Systems
{

    // We calculate the dialogue that the npcs has to say just
    // before each start of the day. We have "Story" NPCs that
    // are always in the tavern and "Random" NPCs that just say
    // random facts or sneaky game tips like
    // (I've heard there will be a surplus of X ingredient in 2 days)
    public class NpcSystem : ISystemEvents
    {
        private StoryDB _storyDB;
        private NpcData _npcData;
        private Event<PopupData> _showPopupCmd;

        public void Initialize(StoryDB storyDb, NpcData npcData, GameEventSystem evtSys)
        {
            _storyDB = storyDb;
            _npcData = npcData;

            _showPopupCmd = evtSys.GetCommandByName<Event<PopupData>>("popup_sys", "show_popup");
            evtSys.GetCallbackByName<EventVoid>("day_sys", "night_begin").OnInvoked += PopulateDeitiesData;
            // DEV ONLY
            PopulateNpcsData();
        }

        public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
        {
            commands = new EventSys();
            callbacks = new EventSys();
            sysID = "npc_sys".GetHashCode();

            // Commands
            commands.AddEvent("cmd_populate_npcs".GetHashCode()).OnInvoked += PopulateNpcsData;
            commands.AddEvent("populate_deities".GetHashCode()).OnInvoked += PopulateDeitiesData;
        }

        // Called just before the start of the day to set the
        // dialogue that each NPC has to say that day
        public void PopulateNpcsData()
        {
            // We make a copy because we dont want to remove elements from the storyDB list.
            // The completed stories should be "finalized" when the player has seen the 
            // repercusion dialogue not when the dialogue is assigned to an npc

            // In this code we also assume that we might have more stories to show than npcs
            // so we have to "cache" and take into account the remaining unshown stories
            // to show them the next day
            List<int> finalizableStories = SelectStoriesThatHaveToBeFinalized(_storyDB.m_CompletedStories, 3);
            List<int> storiesToStart = SelectStoriesThatShouldBeStarted(3);

            for (int i = 0; i < _npcData.m_NpcBehaviour.Count; i++)
            {
                NPCBehaviourData npcData = _npcData.m_NpcBehaviour[i].m_NpcData;

                npcData.m_AlreadySpokenTo = false;
                npcData.m_Dialogue.Clear();

                // Try to preappend a result of a completed story
                // only if there are remaining stories to show
                if (finalizableStories.Count > 0)
                {
                    Story s = _storyDB.m_StoriesDB[finalizableStories[0]];

                    for (int c = 0; c < s.m_QuestBranchResult.m_ResultNPCDialogue.Count; c++)
                    {
                        npcData.m_Dialogue.Add(s.m_QuestBranchResult.m_ResultNPCDialogue[c]);
                    }

                    npcData.m_HasToFinalizeAStory = true;
                    npcData.m_StoryIDToFinalizeOnInteract = finalizableStories[0];

                    finalizableStories.RemoveAt(0);
                    npcData.m_Dialogue.Add("Anyways...");
                }
                else
                {
                    npcData.m_HasToFinalizeAStory = false;
                }


                // Append a new story dialogue
                // only if there are new stories to append
                if (storiesToStart.Count > 0)
                {
                    var introductionDialogue = _storyDB.m_StoriesDB[storiesToStart[0]].m_StoryData.m_IntroductionDialogue;
                    for (int j = 0; j < introductionDialogue.Count; j++)
                    {
                        npcData.m_Dialogue.Add(introductionDialogue[j]);
                    }

                    npcData.m_HasToStartAStory = true;
                    npcData.m_StoryIDToStartOnInteract = storiesToStart[0];
                    storiesToStart.RemoveAt(0);
                }
                else
                {
                    npcData.m_HasToStartAStory = false;
                    //_showPopupCmd.Invoke(new PopupData { m_Text = "You have completed all stories, thank you for playing the alpha", m_TimeAlive = 999999999 });
                }
            }
        }

        public void PopulateDeitiesData()
        {
            List<int> completedStories = _storyDB.m_CompletedStories;

            EvithBehaviour evith = _npcData.m_Evith;
            NuBehaviour nu = _npcData.m_Nu;
            evith.m_Dialogue.Clear();
            nu.m_Dialogue.Clear();

            for (int i = 0; i < completedStories.Count; i++)
            {
                BranchOption result = _storyDB.m_StoriesDB[completedStories[i]].m_QuestBranchResult;

                for (int j = 0; j < result.m_DeitiesResultDialogue.Count; j++)
                {
                    BranchOption.DeitiesStoryDialogue dialogue = result.m_DeitiesResultDialogue[j];
                    if (dialogue.m_DeityID == 0)
                    {
                        for (int k = 0; k < dialogue.m_Dialogue.Count; k++)
                        {
                            nu.m_Dialogue.Add(dialogue.m_Dialogue[k]);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < dialogue.m_Dialogue.Count; k++)
                        {
                            evith.m_Dialogue.Add(dialogue.m_Dialogue[k]);
                        }
                    }
                }
            }
        }

        private List<int> SelectStoriesThatShouldBeStarted(int maxStoriesToSelect)
        {
            var storiesToStartIds = _storyDB.m_StoriesToStart;
            int availableStoriesToStart = Mathf.Min(storiesToStartIds.Count, maxStoriesToSelect);
            List<int> sStory = new List<int>();
            for (int j = 0; j < availableStoriesToStart; j++)
            {
                sStory.Add(storiesToStartIds[j]);
            }

            return sStory;
        }

        private List<int> SelectStoriesThatHaveToBeFinalized(List<int> completedStoriesIDList, int maxStoriesToComplete)
        {
            int avaliableCompletedStories = Mathf.Min(completedStoriesIDList.Count, maxStoriesToComplete);
            List<int> cStoriesIds = new List<int>(); // Completed Stories Ids
            for (int i = 0; i < avaliableCompletedStories; i++)
            {
                cStoriesIds.Add(completedStoriesIDList[i]);
            }

            return cStoriesIds;
        }
    }
}

namespace CQM.Components
{
    public class NPCBehaviourData
    {
        // Dialogue lines that the npc has to say
        public List<string> m_Dialogue = new List<string>();
        // The ID of the story that will start when the player interacts with the NPC
        public bool m_HasToStartAStory;
        public int m_StoryIDToStartOnInteract;
        // ID of the story to finalize on dialogue
        public bool m_HasToFinalizeAStory;
        public int m_StoryIDToFinalizeOnInteract;

        public bool m_AlreadySpokenTo;
        public List<string> m_AlreadySpokenToDialogue = new List<string> { "I have nothing more to say", "This is a really nice bakery", "Such a nice weather today" };
    }
}
