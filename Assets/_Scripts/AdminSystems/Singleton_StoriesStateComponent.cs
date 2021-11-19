using System.Collections.Generic;

namespace CQM.Components
{
    [System.Serializable]
    public class Singleton_StoriesStateComponent
    {
        // IDs of the stories in the order in which they will be started
        public List<ID> m_MainStoriesToStartOrder = new List<ID>();
        public List<ID> m_AvailableSecondaryStoriesToStart = new List<ID>();

        public List<ID> m_AllSecondaryStories = new List<ID>();

        public List<ID> m_OngoingStories = new List<ID>();
        // Stories which were completed with a quest but the player hasnt seen the result yet
        // At the start of the day the system that handles the spawning of the NPCs must assign them 
        public List<ID> m_CompletedStories = new List<ID>();
        // Stories that have been completely finished
        public List<ID> m_FinalizedPrimaryStories = new List<ID>();
    }
}