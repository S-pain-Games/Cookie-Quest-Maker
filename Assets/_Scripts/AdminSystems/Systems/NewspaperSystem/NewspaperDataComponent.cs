using System.Collections.Generic;
using UnityEngine;

namespace CQM.Systems
{
    public class NewspaperDataComponent
    {
        public List<int> m_StoriesToShowInNewspaper = new List<int>();

        // Contains a newspaper article that should be shown for a given repercusion
        public Dictionary<int, StoryRepNewspaperComponent> m_NewspaperStories = new Dictionary<int, StoryRepNewspaperComponent>();

        public string m_MainTitle;
        public string m_MainBody;
        public Sprite m_MainImg;

        public string m_SecTitle;
        public string m_SecBody;
    }

    public class StoryRepNewspaperComponent
    {
        public string m_Title;
        public string m_Body;
    }
}