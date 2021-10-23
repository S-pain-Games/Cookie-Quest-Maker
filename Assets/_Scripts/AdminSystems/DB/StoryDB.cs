using UnityEngine;
using System.Collections.Generic;

public class StoryDB
{
    // All the Stories in the game
    public Dictionary<int, Story> m_StoriesDB = new Dictionary<int, Story>();
    // [TO-DO] add an adapter to this like the one for the questDB
    public Dictionary<int, StoryUIData> m_StoriesUI = new Dictionary<int, StoryUIData>();

    public List<int> m_OngoingStories = new List<int>();
    public List<int> m_CompletedStories = new List<int>();

    // All the repercusions in the game
    public Dictionary<int, StoryRepercusion> m_Repercusions = new Dictionary<int, StoryRepercusion>();

    public void LoadData()
    {
        LoadStoryRepercusions();
        LoadStoryUIData();
        LoadStoryData();
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
        var rep = new StoryRepercusion();
        m_Repercusions.Add(Admin.g_Instance.ID.repercusions.center_wolf_dead, rep);
        m_Repercusions.Add(Admin.g_Instance.ID.repercusions.center_wolf_alive, rep);
    }

    private void LoadStoryUIData()
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