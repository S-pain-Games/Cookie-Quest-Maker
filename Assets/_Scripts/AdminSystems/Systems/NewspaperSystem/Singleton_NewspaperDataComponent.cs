using CQM.Databases;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Components
{
    public class Singleton_NewspaperDataComponent
    {
        public List<ID> m_StoriesToShowInNewspaper = new List<ID>();

        // Contains a newspaper article that should be shown for a given repercusion ID
        public Dictionary<ID, StoryRepNewspaperComponent> m_NewspaperStories = new Dictionary<ID, StoryRepNewspaperComponent>();

        public string m_MainTitle;
        public string m_MainBody;
        public Sprite m_MainImg;

        public string m_SecTitle;
        public string m_SecBody;
    }
    

    [System.Serializable]
    public class StoryRepNewspaperComponent
    {
        public ID m_RepID;
        public string m_Title;
        public string m_Body;
    }
}