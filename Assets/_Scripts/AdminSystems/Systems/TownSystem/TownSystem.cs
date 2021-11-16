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


        public void Initialize(Singleton_TownComponent townComponent,
                               ComponentsContainer<LocationComponent> locationComponents,
                               ComponentsContainer<StoryRepercusionComponent> repercusionsComponents)
        {
            _townComponent = townComponent;
            _locationComponents = locationComponents;
            _repercusionsComponents = repercusionsComponents;
        }

        public void CalculateTownHappiness()
        {
            List<LocationComponent> locList = _locationComponents.GetList();

            int globalHappiness = 0;
            for (int i = 0; i < locList.Count; i++)
            {
                int locHappiness = 0;
                for (int j = 0; j < locList[i].m_StoryRepercusionsIDs.Count; j++)
                {
                    StoryRepercusionComponent rep = _repercusionsComponents[locList[i].m_StoryRepercusionsIDs[j]];
                    if (rep.m_Active)
                        locHappiness += rep.m_Value;
                }
                locList[i].m_Happiness = locHappiness;
                globalHappiness += locHappiness;
            }
            _townComponent.m_GlobalHappiness = globalHappiness;
        }

        public void SetBuildingRepercusion(ID repercusionID, bool activated)
        {
            List<LocationComponent> locList = _locationComponents.GetList();
            for (int i = 0; i < locList.Count; i++)
            {
                if (locList[i].m_StoryRepercusionsIDs[i] == repercusionID)
                {

                }
            }
        }
    }
}


namespace CQM.Components
{
    [SerializeField]
    public class Singleton_TownComponent
    {
        public int m_GlobalHappiness;
    }


    [System.Serializable]
    public class LocationComponent
    {
        public ID m_ID;

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
