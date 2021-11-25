using CQM.Components;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CQM.Systems
{
    public class TownSystem
    {
        private Singleton_TownComponent _townComponent;
        private ComponentsContainer<LocationComponent> _locationComponents;
        private ComponentsContainer<StoryRepercusionComponent> _repercusionsComponents;

        private Event<ID> _onStoryCompleted;


        public void Initialize(Singleton_TownComponent townComponent,
                               ComponentsContainer<LocationComponent> locationComponents,
                               ComponentsContainer<StoryRepercusionComponent> repercusionsComponents)
        {
            _townComponent = townComponent;
            _locationComponents = locationComponents;
            _repercusionsComponents = repercusionsComponents;

            _onStoryCompleted = Admin.Global.EventSystem.GetCallbackByName<Event<ID>>("story_sys", "story_completed");
            _onStoryCompleted.OnInvoked += (storyID) =>
            {
                // This could be in the story system but for now we can leave it here
                StoryInfoComponent s = Admin.Global.Components.GetComponentContainer<StoryInfoComponent>().GetComponentByID(storyID);
                if (!Admin.Global.Components.m_GameStoriesStateComponent.m_AllSecondaryStories.Contains(storyID))
                    SetRepercusionState(s.m_QuestRepercusion.m_ID, true);
            };
        }

        private void SetRepercusionState(ID repercusionID, bool activated)
        {
            _repercusionsComponents.GetComponentByID(repercusionID).m_Active = true;
            CalculateTownHappiness();
        }

        private void CalculateTownHappiness()
        {
            List<LocationComponent> locList = _locationComponents.GetList();

            int globalHappiness = 0;
            for (int i = 0; i < locList.Count; i++)
            {
                CalculateLocationHappiness(locList[i]);
                globalHappiness += locList[i].m_Happiness;
            }
            _townComponent.m_GlobalHappiness = globalHappiness;
        }

        private void CalculateLocationHappiness(LocationComponent location)
        {
            int locHappiness = 0;
            for (int i = 0; i < location.m_StoryRepercusionsIDs.Count; i++)
            {
                StoryRepercusionComponent rep = _repercusionsComponents[location.m_StoryRepercusionsIDs[i]];
                if (rep.m_Active)
                    locHappiness += rep.m_Value;
            }
            location.m_Happiness = locHappiness;
        }
    }
}


namespace CQM.Components
{
    [System.Serializable]
    public class Singleton_TownComponent
    {
        public int m_GlobalHappiness;
    }


    [System.Serializable]
    public class LocationComponent
    {
        public ID m_ID;
        public ID m_CharacterOwnerID;

        public string m_LocName;
        public string m_LocDesc;

        public int m_Happiness;
        public List<ID> m_StoryRepercusionsIDs = new List<ID>();
    }


    [System.Serializable]
    public class TownUI
    {
        public TextMeshProUGUI _happiness;
    }
}
