using System;
using System.Collections.Generic;

// We indicate which data objects are just persistent across the entire game
// and which have runtime data
public class StoryDB
{
    // All the Stories Data in the game (Persistent & Runtime)
    public Dictionary<int, Story> m_StoriesDB = new Dictionary<int, Story>();
    // Story UI Data used in the story selection UI (Persistent)
    public Dictionary<int, StoryUIData> m_StoriesUI = new Dictionary<int, StoryUIData>();

    // All below runtime
    // IDs of the stories in the order in which they will be started
    // We could randomize this in the future
    public List<int> m_StoriesToStart = new List<int>();
    public List<int> m_OngoingStories = new List<int>();
    // Stories which were completed with a quest but the player hasnt seen the result yet
    // At the start of the day the system that handles the spawning of the NPCs must assign them 
    public List<int> m_CompletedStories = new List<int>();
    // Stories that have been completely finished
    public List<int> m_FinalizedStories = new List<int>();

    // All the repercusions in the game (Persistent)
    public Dictionary<int, StoryRepercusion> m_Repercusions = new Dictionary<int, StoryRepercusion>();

    public T GetStoryComponent<T>(int ID) where T : class
    {
        if (m_StoriesDB[ID] is T)
        {
            return m_StoriesDB[ID] as T;
        }
        else if (m_StoriesUI[ID] is T)
        {
            return m_StoriesUI[ID] as T;
        }
        return null;
    }

    public StoryRepercusion GetRepercusion(int ID)
    {
        return m_Repercusions[ID];
    }

    public void LoadData()
    {
        LoadStoryRepercusions();
        LoadStoryUIData();
        LoadStoryData();
        LoadStoriesOrder();
    }

    private void LoadStoriesOrder()
    {
        // Loads the order in which the stories will be
        // presented to the player
        var ids = Admin.Global.ID.stories;
        m_StoriesToStart.Add(ids.mayors_problem);
        //m_StoriesToStart.Add(ids.out_of_lactose);
    }

    private void LoadStoryData()
    {
        var builder = new StoryBuilder(this);
        builder.StartCreatingStory("Mayor's Problem", new List<string>() { "The Towns Center is having a very bad wolves problem",
            "The mayor is obviously not happy about it but 'some' people are really enjoying the mess" });

        builder.AddStoryBranch("center_wolf_dead", "Did you hear what happened ?? apparently conviced mayor.",
            QPTag.TagType.Convince, 1, "mayor".GetHashCode());
        builder.AddStoryBranch("center_wolf_alive", "Apparently yesterday some small creatures tried to approach the Mayor at night, the mayor obviously ran away screaming help.",
            QPTag.TagType.Help, 1, "mayor".GetHashCode());
        builder.AddStoryBranch("center_wolf_alive", "Did you hear that the mayor was attacked just this night?? It is horrible for the town.",
            QPTag.TagType.Harm, 1, "mayor".GetHashCode());

        builder.AddStoryBranch("center_wolf_dead", "Did you hear what happened ?? apparently somebody saw some small creatures talk with the wolves at night and just after that they just left.",
            QPTag.TagType.Convince, 1, "wolves".GetHashCode());
        builder.AddStoryBranch("center_wolf_alive", "Apparently yesterday some small creatures tried to approach the Mayor at night, the mayor obviously ran away screaming help.",
            QPTag.TagType.Help, 1, "wolves".GetHashCode());
        builder.AddStoryBranch("center_wolf_alive", "Did you hear that the wolves were attacked just this night??",
            QPTag.TagType.Harm, 1, "wolves".GetHashCode());

        var story = builder.FinishCreatingAndReturnStory();
        m_StoriesDB.Add("mayors_problem".GetHashCode(), story);
    }

    private void LoadStoryRepercusions()
    {
        var ids = Admin.Global.ID.repercusions;

        AddRepercusion("center_wolf_dead".GetHashCode(), 10);
        AddRepercusion("center_wolf_alive".GetHashCode(), -15);
        AddRepercusion(ids.towncenter_mayor_celebration_happened, 10);
        AddRepercusion(ids.towncenter_mayor_celebration_didnt_happen, -10);
        AddRepercusion(ids.towncenter_in_ruins, -20);
        AddRepercusion(ids.towncenter_not_in_ruins, +20);

        void AddRepercusion(int id, int value)
        {
            var rep = new StoryRepercusion();
            rep.m_Value = value;
            m_Repercusions.Add(id, rep);
        }
    }

    private void LoadStoryUIData()
    {
        var ids = Admin.Global.ID.stories;

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

public class StoryBuilder
{
    private Story m_Story;
    private StoryData m_StoryData;

    private StoryDB _stories;

    public StoryBuilder(StoryDB stories)
    {
        _stories = stories;
    }

    public void StartCreatingStory(string title, List<string> introduction)
    {
        m_StoryData = new StoryData();
        m_StoryData.m_Title = title;
        m_StoryData.m_IntroductionDialogue = introduction;
    }

    public void StartCreatingStory(string title, string introduction)
    {
        m_StoryData = new StoryData();
        m_StoryData.m_Title = title;
        m_StoryData.m_IntroductionDialogue = new List<string>() { introduction };
    }

    public void AddStoryBranch(string repercusion, List<string> result, QPTag.TagType tag, int tagValue, int targetId)
    {
        BranchOption bOpt = new BranchOption
        {
            m_Repercusion = _stories.GetRepercusion(repercusion.GetHashCode()),
            m_Result = result
        };
        BranchCondition bCon = new BranchCondition
        {
            m_Tag = tag,
            m_Value = tagValue,
            m_Target = targetId
        };
        bOpt.m_Condition = bCon;
        m_StoryData.m_BranchOptions.Add(bOpt);
    }

    public void AddStoryBranch(string repercusion, string result, QPTag.TagType tag, int tagValue, int targetId)
    {
        BranchOption bOpt = new BranchOption
        {
            m_Repercusion = _stories.GetRepercusion(repercusion.GetHashCode()),
            m_Result = new List<string>() { result }
        };
        BranchCondition bCon = new BranchCondition
        {
            m_Tag = tag,
            m_Value = tagValue,
            m_Target = targetId
        };
        bOpt.m_Condition = bCon;
        m_StoryData.m_BranchOptions.Add(bOpt);
    }

    public Story FinishCreatingAndReturnStory()
    {
        m_StoryData.Build();

        m_Story = new Story();
        m_Story.m_StoryData = m_StoryData;

        return m_Story;
    }
}