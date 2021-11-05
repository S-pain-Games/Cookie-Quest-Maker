using CQM.Databases;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Components
{
    public class NewspaperDataComponent
    {
        public List<int> m_StoriesToShowInNewspaper = new List<int>();

        // Contains a newspaper article that should be shown for a given repercusion ID
        public Dictionary<int, StoryRepNewspaperComponent> m_NewspaperStories = new Dictionary<int, StoryRepNewspaperComponent>();

        public string m_MainTitle;
        public string m_MainBody;
        public Sprite m_MainImg;

        public string m_SecTitle;
        public string m_SecBody;


        public void LoadData(StoryBuilder storyBuilder)
        {
            for (int i = 0; i < storyBuilder.RepercusionNewspaperArticles.Count; i++)
            {
                var news = storyBuilder.RepercusionNewspaperArticles[i];
                m_NewspaperStories.Add(news.m_RepID, news);
            }
        }
    }

    [System.Serializable]
    public class StoryRepNewspaperComponent
    {
        public int m_RepID;
        public string m_Title;
        public string m_Body;
    }
}