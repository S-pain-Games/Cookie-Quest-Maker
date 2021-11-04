using CQM.Components;
using System.Collections.Generic;


namespace CQM.Databases
{
    public class StoryBuilder
    {
        public List<Story> Stories => m_StoriesList;

        private List<Story> m_StoriesList = new List<Story>();

        private Story m_Story;
        private StoryData m_StoryData;
        private BranchOption m_Branch;

        public void StartCreatingStory(string idName, string title, List<string> introductionDialogue)
        {
            m_StoryData = new StoryData();
            m_StoryData.m_ID = idName.GetHashCode();

            m_StoryData.m_Title = title;
            m_StoryData.m_IntroductionDialogue = introductionDialogue;
        }

        public void StartStoryBranch()
        {
            m_Branch = new BranchOption();
        }

        public void AddRepercusionToBranch(string idName, string repName, int happinessValue)
        {
            var rep = new StoryRepercusion();
            rep.m_ID = idName.GetHashCode();
            rep.m_Name = repName;
            rep.m_ParentStoryID = m_StoryData.m_ID;
            rep.m_Active = false;
            rep.m_Value = happinessValue;
            m_Branch.m_Repercusion = rep;
        }

        public void AddBranchCompletion_NPCDialogue(List<string> npcResultDialogue, QPTag.TagType tag, int tagValue, int targetId)
        {
            m_Branch.m_ResultNPCDialogue = npcResultDialogue;

            BranchCondition bCon = new BranchCondition
            {
                m_Tag = tag,
                m_Value = tagValue,
                m_Target = targetId
            };
            m_Branch.m_Condition = bCon;
            m_StoryData.m_BranchOptions.Add(m_Branch);
        }

        public void FinishCreatingStory()
        {
            m_StoryData.Build();

            m_Story = new Story();
            m_Story.m_StoryData = m_StoryData;

            m_StoriesList.Add(m_Story);
        }
    }
}