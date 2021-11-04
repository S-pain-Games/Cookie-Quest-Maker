using System.Collections;
using System.Collections.Generic;
using CQM.Components;

namespace CQM.Systems
{
    public class NewspaperSystem : ISystemEvents
    {
        private NewspaperReferencesComponent _newspaperReferences;
        private NewspaperDataComponent _newsData;

        public void Initialize(NewspaperReferencesComponent refs, NewspaperDataComponent data)
        {
            _newspaperReferences = refs;
            _newsData = data;
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
            // Collect town data and update newspaper acordingly
        }
    }
}