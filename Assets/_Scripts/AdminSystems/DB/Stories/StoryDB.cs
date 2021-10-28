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
        //m_StoriesToStart.Add(ids.test);
        m_StoriesToStart.Add(ids.mayors_problem);
        m_StoriesToStart.Add(ids.out_of_lactose);
        //m_StoriesToStart.Add(ids.the_birds_and_the_bees);
    }

    private void LoadStoryData()
    {
        var ids = Admin.g_Instance.ID.stories;
        var repIds = Admin.g_Instance.ID.repercusions;
        var piecesIds = Admin.g_Instance.ID.pieces;

        Story s = new Story();
        StoryData sData = new StoryData
        {
            m_Title = "The Introductory Madness",
            m_IntroductionDialogue = new List<string>() { "This is an introduction quest sory no text available" }
        };
        AddBranchToStoryData_DEPRECATED_DONT_USE(sData, repIds.center_wolf_dead, "Test Story Totally Completed Convincingly", QPTag.TagType.Convince, 1);
        AddBranchToStoryData_DEPRECATED_DONT_USE(sData, repIds.center_wolf_alive, "Test Story Totally Completed Helpingly", QPTag.TagType.Help, 1);
        AddBranchToStoryData_DEPRECATED_DONT_USE(sData, repIds.center_wolf_dead, "Test Story Totally Completed Harmingly", QPTag.TagType.Harm, 1);
        s.m_StoryData = sData;
        sData.m_Target = piecesIds.mayor;
        sData.Build();
        //m_StoriesDB.Add(ids.test, s);

        s = new Story();
        sData = new StoryData
        {
            m_Title = "Mayor's Problem",
            m_IntroductionDialogue = new List<string>() { "The Towns Center is having a very bad wolves problem", "The mayor is obviously not happy about it but 'some' people are really enjoying the mess" }
        };
        AddBranchToStoryData_DEPRECATED_DONT_USE(sData, repIds.center_wolf_dead, "Did you hear what happened?? apparently somebody saw some small creatures talk with the wolves at night and just after that they just left.", QPTag.TagType.Convince, 1);
        AddBranchToStoryData_DEPRECATED_DONT_USE(sData, repIds.center_wolf_alive, "Apparently yesterday some small creatures tried to approach the Mayor at night, the mayor obviously ran away screaming help.", QPTag.TagType.Help, 1);
        AddBranchToStoryData_DEPRECATED_DONT_USE(sData, repIds.center_wolf_dead, "Did you hear that the mayor was attacked just this night?? It is horrible for the town.", QPTag.TagType.Harm, 1);
        s.m_StoryData = sData;
        sData.m_Target = piecesIds.mayor;
        sData.Build();
        m_StoriesDB.Add(ids.mayors_problem, s);

        s = new Story();
        sData = new StoryData
        {
            m_Title = "Out of Lactose",
            m_IntroductionDialogue = new List<string>() { "Did you hear that Molly is having problems with her cows? apparently they aren't producing any milk, such a shame i wont be able to buy milk for my morning cereal." }
        };
        AddBranchToStoryData_DEPRECATED_DONT_USE(sData, repIds.center_wolf_dead, "Some weird being were like 'speaking?' or something with the cows? this really sounds crazy but now the cows produce milk so i dont see any problems.", QPTag.TagType.Convince, 1);
        AddBranchToStoryData_DEPRECATED_DONT_USE(sData, repIds.center_wolf_alive, "Apparently yesterday some small creatures tried to approach Molly at the night.", QPTag.TagType.Help, 1);
        AddBranchToStoryData_DEPRECATED_DONT_USE(sData, repIds.center_wolf_dead, "Molly's cows were actually kidnaped, she is devastated.", QPTag.TagType.Harm, 1);
        s.m_StoryData = sData;
        sData.m_Target = piecesIds.molly;
        sData.Build();
        m_StoriesDB.Add(ids.out_of_lactose, s);


        m_StoriesDB.Add(ids.the_birds_and_the_bees, s);
    }

    private void AddBranchToStoryData_DEPRECATED_DONT_USE(StoryData sData, int repercusionID, string result, QPTag.TagType tag, int tagValue)
    {
        BranchOption bOpt = new BranchOption
        {
            m_Repercusion = m_Repercusions[repercusionID],
            m_Result = new List<string>() { result }
        };
        BranchCondition bCon = new BranchCondition
        {
            m_Tag = tag,
            m_Value = tagValue
        };
        bOpt.m_Condition = bCon;
        sData.m_BranchOptions.Add(bOpt);
    }

    private void AddBranchToStoryData(StoryData sData, int repercusionID, List<string> result, QPTag.TagType tag, int tagValue)
    {
        BranchOption bOpt = new BranchOption
        {
            m_Repercusion = m_Repercusions[repercusionID],
            m_Result = result
        };
        BranchCondition bCon = new BranchCondition
        {
            m_Tag = tag,
            m_Value = tagValue
        };
        bOpt.m_Condition = bCon;
        sData.m_BranchOptions.Add(bOpt);
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
