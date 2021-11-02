using System.Collections.Generic;
using UnityEngine;

public class TownSystem
{
    private TownDB _data;
    private StoryDB _stories;

    public void Initialize(TownDB data, StoryDB stories)
    {
        _data = data;
        _stories = stories;
    }

    public void CalculateTownHappiness()
    {
        var repDb = _stories.m_Repercusions;
        List<Location> locList = _data.m_LocationsList;
        int globalHappiness = 0;
        for (int i = 0; i < locList.Count; i++)
        {
            int locHappiness = 0;
            for (int j = 0; j < locList[i].m_StoryRepercusionsIDs.Count; j++)
            {
                StoryRepercusion rep = repDb[locList[i].m_StoryRepercusionsIDs[j]];
                if (rep.m_Active)
                    locHappiness += rep.m_Value;
            }
            locList[i].m_Happiness = locHappiness;
            globalHappiness += locHappiness;
        }
        _data.m_GlobalHappiness = globalHappiness;
    }

    public void SetBuildingRepercusion(int repercusionID, bool activated)
    {
        List<Location> locList = _data.m_LocationsList;
        for (int i = 0; i < locList.Count; i++)
        {
            if (locList[i].m_StoryRepercusionsIDs[i] == repercusionID)
            {

            }
        }
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
        var townIds = Admin.Global.ID.townLocations;
        var repIds = Admin.Global.ID.repercusions;
        var rep = storyDB.m_Repercusions;

        Location loc = new Location();
        loc.m_LocName = "Town Center";
        loc.m_LocDesc = "Desc";
        loc.m_StoryRepercusionsIDs.Add(repIds.center_wolf_alive);
        loc.m_StoryRepercusionsIDs.Add(repIds.center_wolf_dead);
        loc.m_StoryRepercusionsIDs.Add(repIds.towncenter_in_ruins);
        loc.m_StoryRepercusionsIDs.Add(repIds.towncenter_not_in_ruins);
        loc.m_StoryRepercusionsIDs.Add(repIds.towncenter_mayor_celebration_happened);
        loc.m_StoryRepercusionsIDs.Add(repIds.towncenter_mayor_celebration_didnt_happen);

        m_Locations.Add(townIds.town_center, loc);
        m_LocationsList.Add(loc);
    }
}

public class Location
{
    public string m_LocName;
    public string m_LocDesc;

    public int m_Happiness;
    public List<int> m_StoryRepercusionsIDs = new List<int>();
}