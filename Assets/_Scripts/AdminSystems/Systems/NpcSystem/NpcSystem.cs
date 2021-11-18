using CQM.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Systems
{

    // We calculate the dialogue that the npcs has to say just
    // before each start of the day. We have "Story" NPCs that
    // are always in the tavern and "Random" NPCs that just say
    // random facts or sneaky game tips like
    // (I've heard there will be a surplus of X ingredient in 2 days)
    public class NpcSystem : ISystemEvents
    {
        private ComponentsContainer<StoryInfoComponent> _storyInfoComponents;
        private ComponentsContainer<CharacterComponent> _characterComponents;
        private Singleton_StoriesStateComponent _storiesStateComponent;
        private Singleton_NpcReferencesComponent _npcReferencesComponent;


        public void Initialize(Singleton_NpcReferencesComponent npcReferencesComponent,
                               Singleton_StoriesStateComponent storiesStateComponent,
                               ComponentsContainer<StoryInfoComponent> storyInfoComponents,
                               ComponentsContainer<CharacterComponent> characterComponents,
                               GameEventSystem evtSys)
        {
            _npcReferencesComponent = npcReferencesComponent;
            _storiesStateComponent = storiesStateComponent;
            _characterComponents = characterComponents;
            _storyInfoComponents = storyInfoComponents;

            evtSys.GetCallbackByName<EventVoid>("day_sys", "night_begin").OnInvoked += PopulateDeitiesData;
            // DEV ONLY
            //PopulateNpcsData(0);
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            commands = new EventSys();
            callbacks = new EventSys();
            sysID = new ID("npc_sys");

            // Commands
            commands.AddEvent<int>(new ID("cmd_populate_npcs")).OnInvoked += PopulateNpcsData;
            commands.AddEvent(new ID("populate_deities")).OnInvoked += PopulateDeitiesData;
        }


        // Called just before the start of the day to set the
        // dialogue that each NPC has to say that day
        public void PopulateNpcsData(int maxSecondaryStories)
        {
            // We make a copy because we dont want to remove elements from the storyDB list.
            // The completed stories should be "finalized" when the player has seen the 
            // repercusion dialogue not when the dialogue is assigned to an npc

            List<ID> secondaryStoriesToStart = SelectSecondaryStories(maxSecondaryStories);
            List<ID> availableNPCs = SelectAvailableNPCs();

            List<ID> allStoriesToFinalize = SelectStoriesThatHaveToBeFinalized(_storiesStateComponent.m_CompletedStories, 3);
            List<ID> secondaryStoriesToFinalize = SelectSecondaryStoriesThatHaveToBeFinalized(allStoriesToFinalize);

            bool haveToStartAPrimaryStory = CheckIfShouldStartAPrimaryStory(allStoriesToFinalize);
            bool haveToFinalizeAPrimaryStory = SelectMainStoriesToFinalize(allStoriesToFinalize, out ID finalizeMainStoryID);
            ID primaryStoryToStart = SelectMainStoryToStart();

            for (int i = 0; i < _npcReferencesComponent.m_NpcBehaviour.Count; i++)
            {
                NPCBehaviourData npcData = _npcReferencesComponent.m_NpcBehaviour[i].m_NpcData;

                // Reset Data
                npcData.m_CharacterID = new ID("meri");
                npcData.m_AlreadySpokenTo = false;
                npcData.m_Dialogue.Clear();
                npcData.m_HasToFinalizeAStory = false;
                npcData.m_HasToStartAStory = false;


                if (haveToFinalizeAPrimaryStory)
                {
                    StoryInfoComponent s = _storyInfoComponents[finalizeMainStoryID];

                    npcData.m_CharacterID = new ID(s.m_StoryData.m_QuestGiver);
                    for (int c = 0; c < s.m_QuestBranchResult.m_ResultNPCDialogue.Count; c++)
                        npcData.m_Dialogue.Add(s.m_QuestBranchResult.m_ResultNPCDialogue[c]);

                    npcData.m_HasToFinalizeAStory = true;
                    npcData.m_StoryIDToFinalizeOnInteract = finalizeMainStoryID;

                    haveToFinalizeAPrimaryStory = false;
                    haveToStartAPrimaryStory = false;

                    npcData.m_Dialogue.Add("Menuda Historia...");

                    StartSecondaryStory(secondaryStoriesToStart, npcData);
                }
                else if (haveToStartAPrimaryStory)
                {
                    FinalizeSecondaryStory(secondaryStoriesToFinalize, npcData);

                    StoryInfoComponent s = _storyInfoComponents[primaryStoryToStart];
                    npcData.m_CharacterID = new ID(s.m_StoryData.m_QuestGiver);

                    var introductionDialogue = s.m_StoryData.m_IntroductionDialogue;
                    for (int j = 0; j < introductionDialogue.Count; j++)
                        npcData.m_Dialogue.Add(introductionDialogue[j]);

                    haveToStartAPrimaryStory = false;
                    npcData.m_HasToStartAStory = true;
                    npcData.m_StoryIDToStartOnInteract = primaryStoryToStart;
                }
                else
                {
                    FinalizeSecondaryStory(secondaryStoriesToFinalize, npcData);
                    StartSecondaryStory(secondaryStoriesToStart, npcData);

                    if (npcData.m_Dialogue.Count == 0) npcData.m_Dialogue.Add("What a nice day [NO SECONDARY STORIES]");
                }
            }

            void StartSecondaryStory(List<ID> secondaryStoriesToStart, NPCBehaviourData npcData)
            {
                if (secondaryStoriesToStart.Count > 0)
                {
                    StoryInfoComponent secStoryInfo = _storyInfoComponents[secondaryStoriesToStart[0]];

                    var introductionDialogue = secStoryInfo.m_StoryData.m_IntroductionDialogue;
                    for (int j = 0; j < introductionDialogue.Count; j++)
                        npcData.m_Dialogue.Add(introductionDialogue[j]);

                    npcData.m_HasToStartAStory = true;
                    npcData.m_StoryIDToStartOnInteract = secondaryStoriesToStart[0];
                    secondaryStoriesToStart.RemoveAt(0);
                }
            }

            void FinalizeSecondaryStory(List<ID> secondaryStoriesToFinalize, NPCBehaviourData npcData)
            {
                if (secondaryStoriesToFinalize.Count > 0)
                {
                    StoryInfoComponent secStory = _storyInfoComponents[secondaryStoriesToFinalize[0]];

                    for (int c = 0; c < secStory.m_QuestBranchResult.m_ResultNPCDialogue.Count; c++)
                        npcData.m_Dialogue.Add(secStory.m_QuestBranchResult.m_ResultNPCDialogue[c]);

                    npcData.m_HasToFinalizeAStory = true;
                    npcData.m_StoryIDToFinalizeOnInteract = secondaryStoriesToFinalize[0];

                    secondaryStoriesToFinalize.RemoveAt(0);

                    npcData.m_Dialogue.Add("Menuda Historia...");
                }
            }

            List<ID> SelectSecondaryStoriesThatHaveToBeFinalized(List<ID> allStoriesToFinalize)
            {
                List<ID> secStories = new List<ID>();
                for (int i = 0; i < allStoriesToFinalize.Count; i++)
                {
                    var storyID = allStoriesToFinalize[i];
                    if (_storiesStateComponent.m_AllSecondaryStories.Contains(storyID))
                        secStories.Add(storyID);
                }
                return secStories;
            }

            List<ID> SelectSecondaryStories(int num)
            {
                List<ID> secondaryStories = new List<ID>();
                var l = _storiesStateComponent.m_AvailableSecondaryStoriesToStart;
                var length = Mathf.Min(num, l.Count);
                if (l.Count == 0) return secondaryStories;

                for (int i = 0; i < length; i++)
                {
                    secondaryStories.Add(l[i]);
                }
                return secondaryStories;
            }

            bool SelectMainStoriesToFinalize(List<ID> allStoriesToFinalize, out ID id)
            {
                id = new ID();
                for (int i = 0; i < allStoriesToFinalize.Count; i++)
                {
                    if (IsMainStory(allStoriesToFinalize[i]))
                    {
                        id = allStoriesToFinalize[i];
                        return true;
                    }
                }
                return false;
            }

            bool CheckIfShouldStartAPrimaryStory(List<ID> storiesToFinalize)
            {
                bool should = true;
                for (int i = 0; i < storiesToFinalize.Count; i++)
                {
                    // If one of the missions that have to be finalized is not a secondary mission
                    // that means that we dont have to start a new mission today
                    if (!_storiesStateComponent.m_AllSecondaryStories.Contains(storiesToFinalize[i]))
                    {
                        should = false;
                    }
                }
                return should;
            }

            bool IsMainStory(ID storyID)
            {
                if (!_storiesStateComponent.m_AllSecondaryStories.Contains(storyID))
                    return true;
                else
                    return false;
            }

            List<ID> SelectAvailableNPCs()
            {
                List<ID> l = new List<ID>();
                var charCompList = _characterComponents.GetList();

                for (int i = 0; i < charCompList.Count; i++)
                    l.Add(charCompList[i].m_ID);

                l.Remove(new ID("hio"));
                l.Remove(new ID("nu"));
                l.Remove(new ID("evith"));
                return l;
            }

            ID SelectMainStoryToStart()
            {
                var storiesToStartIds = _storiesStateComponent.m_MainStoriesToStartOrder;
                //int availableStoriesToStart = Mathf.Min(storiesToStartIds.Count, maxStoriesToSelect);
                //List<ID> sStory = new List<ID>();
                //for (int j = 0; j < availableStoriesToStart; j++)
                //{
                //    sStory.Add(storiesToStartIds[j]);
                //}

                //return sStory;
                return storiesToStartIds[0];
            }

            List<ID> SelectStoriesThatHaveToBeFinalized(List<ID> completedStoriesIDList, int maxStoriesToComplete)
            {
                int avaliableCompletedStories = Mathf.Min(completedStoriesIDList.Count, maxStoriesToComplete);
                List<ID> cStoriesIds = new List<ID>(); // Completed Stories Ids
                for (int i = 0; i < avaliableCompletedStories; i++)
                {
                    cStoriesIds.Add(completedStoriesIDList[i]);
                }

                return cStoriesIds;
            }
        }

        public void PopulateDeitiesData()
        {
            List<ID> completedStories = _storiesStateComponent.m_CompletedStories;

            EvithBehaviour evith = _npcReferencesComponent.m_Evith;
            NuBehaviour nu = _npcReferencesComponent.m_Nu;
            evith.m_Dialogue.Clear();
            nu.m_Dialogue.Clear();

            for (int i = 0; i < completedStories.Count; i++)
            {
                BranchOption result = _storyInfoComponents[completedStories[i]].m_QuestBranchResult;

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

        private void OldPopulateMethod(List<ID> storiesToFinalize, List<ID> mainStoriesToStart, int i)
        {
            NPCBehaviourData npcData = _npcReferencesComponent.m_NpcBehaviour[i].m_NpcData;

            // Reset Data
            npcData.m_CharacterID = new ID("meri");
            npcData.m_AlreadySpokenTo = false;
            npcData.m_Dialogue.Clear();
            npcData.m_HasToFinalizeAStory = false;
            npcData.m_HasToStartAStory = false;


            // Try to preappend a result of a completed story
            // only if there are remaining stories to show
            if (storiesToFinalize.Count > 0)
            {
                StoryInfoComponent s = _storyInfoComponents[storiesToFinalize[0]];

                for (int c = 0; c < s.m_QuestBranchResult.m_ResultNPCDialogue.Count; c++)
                    npcData.m_Dialogue.Add(s.m_QuestBranchResult.m_ResultNPCDialogue[c]);

                npcData.m_HasToFinalizeAStory = true;
                npcData.m_StoryIDToFinalizeOnInteract = storiesToFinalize[0];

                storiesToFinalize.RemoveAt(0);

                npcData.m_Dialogue.Add("Menuda Historia...");
            }


            // Append a new story dialogue
            // only if there are new stories to append
            if (mainStoriesToStart.Count > 0)
            {
                StoryInfoComponent s = _storyInfoComponents[mainStoriesToStart[0]];
                npcData.m_CharacterID = new ID(s.m_StoryData.m_QuestGiver);

                var introductionDialogue = s.m_StoryData.m_IntroductionDialogue;
                for (int j = 0; j < introductionDialogue.Count; j++)
                    npcData.m_Dialogue.Add(introductionDialogue[j]);

                npcData.m_HasToStartAStory = true;
                npcData.m_StoryIDToStartOnInteract = mainStoriesToStart[0];
                mainStoriesToStart.RemoveAt(0);
            }
        }
    }
}


namespace CQM.Components
{
    public class NPCBehaviourData
    {
        public ID m_CharacterID;
        // Dialogue lines that the npc has to say
        public List<string> m_Dialogue = new List<string>();
        // The ID of the story that will start when the player interacts with the NPC
        public bool m_HasToStartAStory;
        public ID m_StoryIDToStartOnInteract;
        // ID of the story to finalize on dialogue
        public bool m_HasToFinalizeAStory;
        public ID m_StoryIDToFinalizeOnInteract;

        public bool m_AlreadySpokenTo;
        public List<string> m_AlreadySpokenToDialogue = new List<string> { "I have nothing more to say", "This is a really nice bakery", "Such a nice weather today" };
    }
}
