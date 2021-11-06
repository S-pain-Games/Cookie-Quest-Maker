using System.Collections;
using System.Collections.Generic;
using CQM.Components;
using CQM.Databases;
using UnityEngine;

namespace CQM.Systems
{
    public class NewspaperSystem : ISystemEvents
    {
        private Singleton_NewspaperReferencesComponent _NewspaperReferencesComponent;
        private Singleton_NewspaperDataComponent _NewsDataComponent;
        private ComponentsContainer<StoryInfoComponent> m_StoryInfoComponents;

        public void Initialize(Singleton_NewspaperReferencesComponent newspaperReferencesComponent,
                               Singleton_NewspaperDataComponent newspaperDataComponent,
                               ComponentsContainer<StoryInfoComponent> storyInfoComponents)
        {
            _NewspaperReferencesComponent = newspaperReferencesComponent;
            _NewsDataComponent = newspaperDataComponent;
            m_StoryInfoComponents = storyInfoComponents;

            Debug.Assert(m_StoryInfoComponents != null);
            Debug.Assert(_NewsDataComponent != null);
            Debug.Assert(_NewspaperReferencesComponent != null);

            var evtSys = Admin.Global.EventSystem;
            evtSys.GetCallbackByName<Event<ID>>("story_sys", "story_finalized").OnInvoked +=
                (id) => _NewsDataComponent.m_StoriesToShowInNewspaper.Add(id);
            evtSys.GetCallbackByName<EventVoid>("day_sys", "day_ended").OnInvoked +=
                () => UpdateNewspaper();
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            sysID = new ID("newspaper_sys");
            commands = new EventSys();
            callbacks = new EventSys();

            commands.AddEvent(new ID("update_newspaper")).OnInvoked += UpdateNewspaper;
        }

        public void UpdateNewspaper()
        {
            for (int i = 0; i < _NewsDataComponent.m_StoriesToShowInNewspaper.Count; i++)
            {
                StoryInfoComponent s = m_StoryInfoComponents[_NewsDataComponent.m_StoriesToShowInNewspaper[i]];
                ID repId = s.m_QuestBranchResult.m_Repercusion.m_ID;
                var storyNews = _NewsDataComponent.m_NewspaperStories[repId];

                _NewspaperReferencesComponent.mainTitle.text = storyNews.m_Title;
                _NewspaperReferencesComponent.mainBody.text = storyNews.m_Body;
            }
        }
    }
}