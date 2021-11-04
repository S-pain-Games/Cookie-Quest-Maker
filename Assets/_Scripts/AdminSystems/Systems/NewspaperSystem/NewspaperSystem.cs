using System.Collections;
using System.Collections.Generic;
using CQM.Components;
using CQM.Databases;
using UnityEngine;

namespace CQM.Systems
{
    public class NewspaperSystem : ISystemEvents
    {
        private NewspaperReferencesComponent w_newspaperReferences;
        private NewspaperDataComponent rw_newsData;
        private StoryDB r_story;

        public void Initialize(NewspaperReferencesComponent refs,
                               NewspaperDataComponent data,
                               StoryDB stories)
        {
            w_newspaperReferences = refs;
            rw_newsData = data;
            r_story = stories;

            Debug.Assert(r_story != null);
            Debug.Assert(rw_newsData != null);
            Debug.Assert(w_newspaperReferences != null);

            var evtSys = Admin.Global.EventSystem;
            evtSys.GetCallbackByName<Event<int>>("story_sys", "story_finalized").OnInvoked +=
                (id) =>
                {
                    rw_newsData.m_StoriesToShowInNewspaper.Add(id);
                    UpdateNewspaper();
                };
        }

        public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
        {
            sysID = "newspaper_sys".GetHashCode();
            commands = new EventSys();
            callbacks = new EventSys();

            commands.AddEvent("update_newspaper".GetHashCode()).OnInvoked += UpdateNewspaper;
        }

        public void UpdateNewspaper()
        {
            for (int i = 0; i < rw_newsData.m_StoriesToShowInNewspaper.Count; i++)
            {
                StoryInfo s = r_story.m_Stories[rw_newsData.m_StoriesToShowInNewspaper[i]];
                int repId = s.m_QuestBranchResult.m_Repercusion.m_ID;
                var storyNews = rw_newsData.m_NewspaperStories[repId];

                w_newspaperReferences.mainTitle.text = storyNews.m_Title;
                w_newspaperReferences.mainBody.text = storyNews.m_Body;
            }
        }
    }
}