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

        private bool _firstTime = true;

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
                (id) =>
                {
                    if (!Admin.Global.Components.m_GameStoriesStateComponent.m_AllSecondaryStories.Contains(id))
                        _NewsDataComponent.m_StoriesToShowInNewspaper.Add(id);
                };
            evtSys.GetCallbackByName<EventVoid>("day_sys", "day_ended").OnInvoked +=
                () => UpdateNewspaper();

            UpdateNewspaper();
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
            var n = _NewsDataComponent;
            if (n.m_StoriesToShowInNewspaper.Count > 0)
            {
                StoryInfoComponent s = m_StoryInfoComponents[_NewsDataComponent.m_StoriesToShowInNewspaper[0]];
                ID repId = s.m_QuestBranchResult.m_Repercusion.m_ID;
                var storyNews = _NewsDataComponent.m_NewspaperStories[repId];

                _NewspaperReferencesComponent.mainImg.sprite = Admin.Global.Components.GetComponentContainer<CharacterComponent>().GetComponentByID(storyNews.m_CharacterID).m_NewspaperSprite;
                _NewspaperReferencesComponent.mainTitle.text = storyNews.m_Title;
                _NewspaperReferencesComponent.mainBody.text = storyNews.m_Body;
                n.m_StoriesToShowInNewspaper.RemoveAt(0);
            }
            else
            {
                if (!_firstTime)
                    return;

                //Solo mostrar el primer artículo de periódico el primer día de juego
                //A posteriori, no será necesario volver a mostrar el genérico
                _NewspaperReferencesComponent.mainImg.sprite = Admin.Global.Components.GetComponentContainer<CharacterComponent>().GetComponentByID(new ID("hio")).m_NewspaperSprite;
                _NewspaperReferencesComponent.mainTitle.text = "Nuevo pastelero en el pueblo";
                _NewspaperReferencesComponent.mainBody.text = "Todos los ciudadanos que tengan ganas de dulces podran comprarlos en la nueva pasteleria de Hio";
                _firstTime = false;
            }
        }
    }
}