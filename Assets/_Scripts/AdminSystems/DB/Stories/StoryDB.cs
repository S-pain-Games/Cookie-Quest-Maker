using UnityEngine;
using System.Collections.Generic;

public class StoryDB
{
    // All the Stories Data in the game
    public Dictionary<int, Story> m_StoriesDB = new Dictionary<int, Story>();
    public Dictionary<int, StoryUIData> m_StoriesUI = new Dictionary<int, StoryUIData>();

    // IDs of the stories in the order in which they will be started
    // We could randomize this in the future
    public List<int> m_StoriesToStart = new List<int>();
    public List<int> m_OngoingStories = new List<int>();
    // Stories which were completed with a quest but the player hasnt seen the result yet
    // At the start of the day the system that handles the spawning of the NPCs must assign
    // them 
    public List<int> m_CompletedStories = new List<int>();
    public List<int> m_FinalizedStories = new List<int>();

    // All the repercusions in the game
    public Dictionary<int, StoryRepercusion> m_Repercusions = new Dictionary<int, StoryRepercusion>();

    public void LoadData(StoryDBUnityReferences unityRefs)
    {
        LoadStoryRepercusions();
        LoadStoryUIData(unityRefs);
        LoadStoryData();
        LoadStoriesOrder();
    }

    private void LoadStoriesOrder()
    {
        // Loads the order in which the stories will be
        // presented to the player
        var ids = Admin.g_Instance.ID.stories;
        m_StoriesToStart.Add(ids.mayors_problem);
        m_StoriesToStart.Add(ids.the_birds_and_the_bees);
        m_StoriesToStart.Add(ids.out_of_lactose);
    }

    private void LoadStoryData()
    {
        var ids = Admin.g_Instance.ID.stories;

        Story s = new Story();
        StoryData sData = new StoryData
        {
            m_Title = "The Introductory Madness",
            m_IntroductionPhrase = "Test Story Introduction"
        };

        // Convince Condition
        BranchOption bOpt = new BranchOption
        {
            m_Repercusion = m_Repercusions[Admin.g_Instance.ID.repercusions.center_wolf_dead],
            m_Result = "Test Story Totally Completed Convincingly"
        };
        BranchCondition bCon = new BranchCondition
        {
            m_Tag = QPTag.TagType.Convince,
            m_Value = 1
        };
        bOpt.m_Condition = bCon;
        sData.m_BranchOptions.Add(bOpt);

        // Help Condition
        bOpt = new BranchOption
        {
            m_Repercusion = m_Repercusions[Admin.g_Instance.ID.repercusions.center_wolf_alive],
            m_Result = "Test Story Totally Completed Helpingly"
        };
        bCon = new BranchCondition
        {
            m_Tag = QPTag.TagType.Help,
            m_Value = 1
        };
        bOpt.m_Condition = bCon;
        sData.m_BranchOptions.Add(bOpt);

        // Harm Condition
        bOpt = new BranchOption
        {
            m_Repercusion = m_Repercusions[Admin.g_Instance.ID.repercusions.center_wolf_dead],
            m_Result = "Test Story Totally Completed Harmingly"
        };
        bCon = new BranchCondition
        {
            m_Tag = QPTag.TagType.Harm,
            m_Value = 1
        };
        bOpt.m_Condition = bCon;
        sData.m_BranchOptions.Add(bOpt);

        s.m_StoryData = sData;
        sData.Build();

        m_StoriesDB.Add(ids.test, s);

        // This is for testing only
        m_StoriesDB.Add(ids.mayors_problem, s);
        m_StoriesDB.Add(ids.out_of_lactose, s);
        m_StoriesDB.Add(ids.the_birds_and_the_bees, s);
    }

    private void LoadStoryRepercusions()
    {
        var ids = Admin.g_Instance.ID.repercusions;

        AddRepercusion(ids.center_wolf_dead, 10);
        AddRepercusion(ids.center_wolf_alive, -15);
        AddRepercusion(ids.towncenter_mayor_celebration_happened, 10);
        AddRepercusion(ids.towncenter_mayor_celebration_didnt_happen, -10);
        AddRepercusion(ids.towncenter_in_ruins, -20);
        AddRepercusion(ids.towncenter_not_in_ruins, +20);
    }

    private void AddRepercusion(int id, int value)
    {
        var rep = new StoryRepercusion();
        rep.m_Value = value;
        m_Repercusions.Add(id, rep);
    }

    private void LoadStoryUIData(StoryDBUnityReferences unityRefs)
    {
        var ids = Admin.g_Instance.ID.stories;

        var s = new StoryUIData();
        s.m_Title = "Test Story";
        m_StoriesUI.Add(ids.test, s);

        s = new StoryUIData();
        s.m_Title = "Mayor's Problem";
        m_StoriesUI.Add(ids.mayors_problem, s);

        s = new StoryUIData();
        s.m_Title = "Out of Lactose";
        m_StoriesUI.Add(ids.out_of_lactose, s);

        s = new StoryUIData();
        s.m_Title = "The Birds & Bees";
        m_StoriesUI.Add(ids.the_birds_and_the_bees, s);
    }
}

public class StoryUIData
{
    public Sprite m_Sprite;
    public string m_Title;
}