using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.Components;
using CQM.Databases;
using System;


namespace CQM.DataBuilders
{
    public class TownBuilder : BaseDataBuilder
    {
        [SerializeField] private List<LocationComponent> m_Locations = new List<LocationComponent>();

        // Current Components Being Built
        private LocationComponent l;

        public override void BuildData(ComponentsDatabase c)
        {
            var locComponents = c.GetComponentContainer<LocationComponent>();
            for (int i = 0; i < m_Locations.Count; i++)
            {
                var l = m_Locations[i];
                locComponents.Add(l.m_ID, l);
            }
        }

        public override void LoadDataFromCode()
        {
            m_Locations.Clear();

            //MAYOR
            CreateLocation("mayor_home", "mayor", "Casa de Antonio", "description");
            AddRepercusion("wolves_gone");
            AddRepercusion("wolves_stay");

            AddRepercusion("mayor_assaulted");
            AddRepercusion("mayor_waste");
            AddRepercusion("mayor_robbed");

            AddRepercusion("mayor_alerted");
            AddRepercusion("mayor_relaxed");
            AddRepercusion("mayor_ignore");
            FinishCreatingLocation();

            //MERI
            CreateLocation("meri_rancho", "meri", "Rancho de Meri", "description");
            AddRepercusion("cows_harmed");
            AddRepercusion("cows_saved");

            AddRepercusion("cows_transformed");
            AddRepercusion("cows_back_to_normal");
            AddRepercusion("cows_and_goats_reversed");

            AddRepercusion("wolves_victorious");
            AddRepercusion("rats_victorious");
            AddRepercusion("rats_mounting_wolves");
            FinishCreatingLocation();

            //CANELA
            CreateLocation("canela_home", "canela", "Casa de Canela", "description");
            AddRepercusion("golden_egg_destroyed");
            AddRepercusion("golden_egg_safe");
            AddRepercusion("golden_egg_gone");

            AddRepercusion("artifacts_stolen");
            AddRepercusion("pendant_damaged");
            AddRepercusion("pendant_recovered");
            AddRepercusion("pendant_lost");

            AddRepercusion("living_artifacts_destroyed");
            AddRepercusion("living_artifacts_stopped");
            AddRepercusion("living_artifacts_servants");

            FinishCreatingLocation();

            //MS CHOCOLATE
            CreateLocation("chocolate_home", "miss_chocolate", "Casa de Miss Chocolate", "description");
            AddRepercusion("ms_chocolate_sabotaged");
            AddRepercusion("ms_chocolate_gunpowder");
            AddRepercusion("ms_chocolate_pepper");

            AddRepercusion("cows_poisoned");
            AddRepercusion("cows_experiment_success");
            AddRepercusion("cows_experiment_delayed");

            AddRepercusion("ms_chocolate_experiment_canceled");
            AddRepercusion("ms_chocolate_experiment_success");
            AddRepercusion("ms_chocolate_great_success");

            FinishCreatingLocation();

            //MANTECAS
            CreateLocation("mantecas_home", "mantecas", "Casa del Mantecas", "description");
            AddRepercusion("smart_rats_stay");
            AddRepercusion("smart_rats_gone");
            AddRepercusion("smart_rats_tribute");

            AddRepercusion("ducks_stay");
            AddRepercusion("ducks_gone");
            AddRepercusion("ducks_help");

            AddRepercusion("mantecas_farm_damaged");
            AddRepercusion("mantecas_farm_devastated");
            AddRepercusion("mantecas_farm_revitalized");
            AddRepercusion("mantecas_paranoic");

            FinishCreatingLocation();

            //JOHNNY
            CreateLocation("johnny_home", "johny_setas", "Casa derJohnny", "description");
            AddRepercusion("competition_judges_stoned");
            AddRepercusion("competition_safe");

            AddRepercusion("johnny_sabotaged");
            AddRepercusion("johnny_enhanced");
            AddRepercusion("johnny_neutral");

            AddRepercusion("johnny_trance_interruptded");
            AddRepercusion("johnny_transformed");
            AddRepercusion("johnny_trance_achieved");
            AddRepercusion("johnny_trance_convinced");

            FinishCreatingLocation();
        }

        #region Builder Methods

        private void CreateLocation(string locIDName, string ownerID, string locTitle, string locDescription)
        {
            l = new LocationComponent();
            l.m_Happiness = 0;
            l.m_ID = new ID(locIDName);
            l.m_CharacterOwnerID = new ID(ownerID);
            l.m_LocDesc = locDescription;
            l.m_LocName = locTitle;
        }

        private void AddRepercusion(string repIDName)
        {
            l.m_StoryRepercusionsIDs.Add(new ID(repIDName));
        }

        private void FinishCreatingLocation()
        {
            m_Locations.Add(l);
            l = null;
        }

        #endregion
    }
}