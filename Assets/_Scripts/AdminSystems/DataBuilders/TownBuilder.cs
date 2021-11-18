using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.Components;

public class TownBuilder : MonoBehaviour
{
    [SerializeField] private List<LocationComponent> m_Locations = new List<LocationComponent>();

    private LocationComponent l;

    public void LoadDataFromCode()
    {
        m_Locations.Clear();

        CreateLocation("mayor_home", "Town Center", "description");
        AddRepercusion("wolves_gone");
        AddRepercusion("wolves_stay");
        AddRepercusion("mayor_assaulted");
        AddRepercusion("mayor_waste");
        AddRepercusion("mayor_robbed");
        FinishCreatingLocation();

        CreateLocation("meri_rancho", "Town Center", "description");
        AddRepercusion("cows_harmed");
        AddRepercusion("cows_saved");
        AddRepercusion("cows_transformed");
        AddRepercusion("cows_back_to_normal");
        AddRepercusion("cows_and_goats_reversed");
        FinishCreatingLocation();

        CreateLocation("canela_home", "Town Center", "description");
        AddRepercusion("golden_egg_destroyed");
        AddRepercusion("golden_egg_safe");
        AddRepercusion("golden_egg_gone");

        AddRepercusion("artifacts_stolen");
        AddRepercusion("pendant_damaged");
        AddRepercusion("pendant_recovered");
        AddRepercusion("pendant_lost");
        FinishCreatingLocation();

        CreateLocation("chocolate_home", "Town Center", "description");
        AddRepercusion("ms_chocolate_sabotaged");
        AddRepercusion("ms_chocolate_gunpowder");
        AddRepercusion("ms_chocolate_pepper");
        AddRepercusion("cows_poisoned");
        AddRepercusion("cows_experiment_success");
        AddRepercusion("cows_experiment_delayed");
        FinishCreatingLocation();

        CreateLocation("mantecas_home", "Town Center", "description");
        AddRepercusion("smart_rats_stay");
        AddRepercusion("smart_rats_gone");
        AddRepercusion("smart_rats_tribute");
        AddRepercusion("ducks_stay");
        AddRepercusion("ducks_gone");
        AddRepercusion("ducks_help");
        FinishCreatingLocation();

        CreateLocation("johnny_home", "Town Center", "description");
        AddRepercusion("competition_judges_stoned");
        AddRepercusion("competition_safe");
        FinishCreatingLocation();
    }

    private void CreateLocation(string locIDName, string locTitle, string locDescription)
    {
        l = new LocationComponent();
        l.m_Happiness = 0;
        l.m_ID = new ID(locIDName);
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
}
