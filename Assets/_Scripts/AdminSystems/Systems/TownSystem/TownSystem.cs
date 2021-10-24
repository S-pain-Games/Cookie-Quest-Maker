using System.Collections.Generic;
using UnityEngine;

public class TownSystem
{
    private TownDB _data;

    public void Initialize(TownDB data)
    {
        _data = data;
    }

    public void CalculateHappiness()
    {
        List<Location> locList = _data.m_LocationsList;
        int globalHappiness = 0;
        for (int i = 0; i < locList.Count; i++)
        {
            int locHappiness = 0;
            for (int j = 0; j < locList[i].m_StoryRepercusions.Count; j++)
            {
                StoryRepercusion rep = locList[i].m_StoryRepercusions[j];
                if (rep.m_Active)
                    locHappiness += rep.m_Value;
            }
            locList[i].m_Happiness = locHappiness;
            globalHappiness += locHappiness;
        }
        _data.m_GlobalHappiness = globalHappiness;
    }
}

[SerializeField]
public class TownDB
{
    public int m_GlobalHappiness;
    public Dictionary<int, Location> m_Locations = new Dictionary<int, Location>();
    public List<Location> m_LocationsList = new List<Location>();

    public void LoadData(StoryDB storyDB)
    {
        var townIds = Admin.g_Instance.ID.townLocations;
        var repIds = Admin.g_Instance.ID.repercusions;
        var rep = storyDB.m_Repercusions;

        Location loc = new Location();
        loc.m_LocName = "Town Center";
        loc.m_LocDesc = "Desc";
        loc.m_StoryRepercusions.Add(rep[repIds.center_wolf_alive]);
        loc.m_StoryRepercusions.Add(rep[repIds.center_wolf_dead]);
        loc.m_StoryRepercusions.Add(rep[repIds.towncenter_in_ruins]);
        loc.m_StoryRepercusions.Add(rep[repIds.towncenter_not_in_ruins]);
        loc.m_StoryRepercusions.Add(rep[repIds.towncenter_mayor_celebration_happened]);
        loc.m_StoryRepercusions.Add(rep[repIds.towncenter_mayor_celebration_didnt_happen]);

        m_Locations.Add(townIds.town_center, loc);
        m_LocationsList.Add(loc);
    }
}

public class Location
{
    public string m_LocName;
    public string m_LocDesc;

    public int m_Happiness;
    public List<StoryRepercusion> m_StoryRepercusions = new List<StoryRepercusion>();
}