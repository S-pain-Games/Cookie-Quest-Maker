using CQM.Components;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace CQM.Databases
{
    // We indicate which data objects are just persistent across the entire game
    // and which have runtime data
    public class StoryDB
    {
        // All the Stories Data in the game (Persistent & Runtime)
        public Dictionary<int, StoryInfoComponent> m_StoriesInfo = new Dictionary<int, StoryInfoComponent>();
        // Story UI Data used in the story selection UI (Persistent)
        public Dictionary<int, StoryUIDataComponent> m_StoriesUI = new Dictionary<int, StoryUIDataComponent>();
        // All the repercusions in the game (Persistent)
        public Dictionary<int, StoryRepercusionComponent> m_Repercusions = new Dictionary<int, StoryRepercusionComponent>();


        // All below runtime
        // IDs of the stories in the order in which they will be started
        public List<int> m_StoriesToStart = new List<int>();
        public List<int> m_OngoingStories = new List<int>();
        // Stories which were completed with a quest but the player hasnt seen the result yet
        // At the start of the day the system that handles the spawning of the NPCs must assign them 
        public List<int> m_CompletedStories = new List<int>();
        // Stories that have been completely finished
        public List<int> m_FinalizedStories = new List<int>();


        public T GetStoryComponent<T>(int ID) where T : class
        {
            if (m_StoriesInfo[ID] is T)
            {
                return m_StoriesInfo[ID] as T;
            }
            else if (m_StoriesUI[ID] is T)
            {
                return m_StoriesUI[ID] as T;
            }
            return null;
        }

        public StoryRepercusionComponent GetRepercusion(int ID)
        {
            return m_Repercusions[ID];
        }

        public void LoadData(StoryBuilder storyBuilder)
        {
            for (int i = 0; i < storyBuilder.Stories.Count; i++)
            {
                var story = storyBuilder.Stories[i];
                m_StoriesInfo.Add(story.m_StoryData.m_ID, story);
            }
            for (int i = 0; i < storyBuilder.Repercusions.Count; i++)
            {
                var rep = storyBuilder.Repercusions[i];
                m_Repercusions.Add(rep.m_ID, rep);
            }
            for (int i = 0; i < storyBuilder.StoryUI.Count; i++)
            {
                var ui = storyBuilder.StoryUI[i];
                m_StoriesUI.Add(ui.m_ParentStoryID, ui);
            }
            LoadStoriesOrder();
        }

        private void LoadStoriesOrder()
        {
            // Loads the order in which the stories will be
            // presented to the player
            m_StoriesToStart.Add("mayors_wolves".GetHashCode());
        }
    }
}