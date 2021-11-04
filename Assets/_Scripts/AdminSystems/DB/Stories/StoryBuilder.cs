using CQM.Components;
using System.Collections.Generic;
using UnityEngine;
using PieceType = CQM.Components.QuestPiece.PieceType;
using Tag = CQM.Components.QPTag.TagType;


namespace CQM.Databases
{
    public class StoryBuilder : MonoBehaviour
    {
        public List<StoryInfo> Stories => m_StoriesList;
        public List<StoryRepercusion> Repercusions => m_Repercusions;
        public List<StoryUIData> StoryUI => m_StoryUI;
        public List<StoryRepNewspaperComponent> RepercusionNewspaperArticles => m_RepercusionNewspaperArticles;

        [SerializeField]
        private List<StoryInfo> m_StoriesList = new List<StoryInfo>();
        [SerializeField]
        private List<StoryRepercusion> m_Repercusions = new List<StoryRepercusion>();
        [SerializeField]
        private List<StoryRepNewspaperComponent> m_RepercusionNewspaperArticles = new List<StoryRepNewspaperComponent>();
        [SerializeField]
        private List<StoryUIData> m_StoryUI = new List<StoryUIData>();

        private StoryInfo m_Story;
        private StoryData m_StoryData;
        private BranchOption m_Branch;
        private StoryRepercusion m_Repercusion;

        [SerializeField]
        private List<References> m_References = new List<References>();

        [MethodButton]
        public void LoadDataFromCode()
        {
            m_StoriesList.Clear();
            m_Repercusions.Clear();
            m_StoryUI.Clear();
            m_References.Clear();
            m_RepercusionNewspaperArticles.Clear();

            StartCreatingStory("mayors_wolves", "Mayor's Wolves", new List<string>() { "Intro 1 wolves" });

            CreateRepercusion("wolves_alive", "Wolves Alive", 15);
            AddStoryRepercusionNewspaperArticle("Wolves alive oh god", "In other news");

            StartStoryBranch();
            SetRepercusionToBranch("wolves_alive");
            AddBranchCompletion_NPCDialogue(new List<string>() { "NPC_Dialogue Harm 1" }, Tag.Harm, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() { "Evith mayors wolves Harm dialogue" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() { "Nu mayors wolves Harm dialogue" });

            StartStoryBranch();
            SetRepercusionToBranch("wolves_alive");
            AddBranchCompletion_NPCDialogue(new List<string>() { "NPC_Dialogue Convince 1" }, Tag.Convince, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() { "Evith mayors wolves Convince dialogue" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() { "Nu mayors wolves Convince dialogue" });

            StartStoryBranch();
            SetRepercusionToBranch("wolves_alive");
            AddBranchCompletion_NPCDialogue(new List<string>() { "NPC_Dialogue Help 1" }, Tag.Help, 1, "mayor");
            AddBranchCompletion_EvithDeityDialogue(new List<string>() { "Evith mayors wolves Help dialogue" });
            AddBranchCompletion_NuDeityDialogue(new List<string>() { "Nu mayors wolves Help dialogue" });

            AddStorySelectionUIData("Wolves Party");

            FinishCreatingStory();
        }

        [MethodButton]
        public void SyncReferences()
        {
            for (int i = 0; i < m_References.Count; i++)
            {
                var r = m_References[i];

                for (int j = 0; j < m_StoryUI.Count; j++)
                {
                    if (r.m_ParentStoryID == m_StoryUI[j].m_ParentStoryID)
                        m_StoryUI[j].m_Sprite = r.m_StorySelectionUiSprite;
                }
            }
        }

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

        public void CreateRepercusion(string idName, string repName, int happinessValue)
        {
            var rep = new StoryRepercusion();
            rep.m_ID = idName.GetHashCode();
            rep.m_Name = repName;
            rep.m_ParentStoryID = m_StoryData.m_ID;
            rep.m_Active = false;
            rep.m_Value = happinessValue;
            m_Repercusions.Add(rep);
            m_Repercusion = rep;
        }

        public void SetRepercusionToBranch(string idName)
        {
            StoryRepercusion rep = null;
            for (int i = 0; i < m_Repercusions.Count; i++)
            {
                if (m_Repercusions[i].m_ID == idName.GetHashCode())
                {
                    rep = m_Repercusions[i];
                }
            }

            if (rep == null)
            {
                Debug.LogError($"Repercusion with idName [{idName}] not found when building stories");
            }

            m_Branch.m_Repercusion = rep;
        }

        public void AddStoryRepercusionNewspaperArticle(string title, string body)
        {
            var newsArticle = new StoryRepNewspaperComponent();
            newsArticle.m_RepID = m_Repercusion.m_ID;
            newsArticle.m_Title = title;
            newsArticle.m_Body = body;
            m_RepercusionNewspaperArticles.Add(newsArticle);
        }

        public void AddBranchCompletion_NPCDialogue(List<string> npcResultDialogue, QPTag.TagType tag, int tagValue, string target)
        {
            m_Branch.m_ResultNPCDialogue = npcResultDialogue;

            BranchCondition bCon = new BranchCondition
            {
                m_Tag = tag,
                m_Value = tagValue,
                m_Target = target.GetHashCode()
            };
            m_Branch.m_Condition = bCon;
            m_StoryData.m_BranchOptions.Add(m_Branch);
        }

        public void AddBranchCompletion_EvithDeityDialogue(List<string> dialogue)
        {
            var evithDialogue = new BranchOption.DeitiesStoryDialogue();
            evithDialogue.m_DeityID = 1;
            evithDialogue.m_Dialogue = dialogue;
            m_Branch.m_DeitiesResultDialogue.Add(evithDialogue);
        }

        public void AddBranchCompletion_NuDeityDialogue(List<string> dialogue)
        {
            var evithDialogue = new BranchOption.DeitiesStoryDialogue();
            evithDialogue.m_DeityID = 0;
            evithDialogue.m_Dialogue = dialogue;
            m_Branch.m_DeitiesResultDialogue.Add(evithDialogue);
        }

        public void AddStorySelectionUIData(string title)
        {
            var s = new StoryUIData();
            s.m_Title = title;
            s.m_ParentStoryID = m_StoryData.m_ID;
            m_StoryUI.Add(s);

            var re = new References();
            re.m_ParentStoryID = m_StoryData.m_ID;
            re.m_StoryName = m_StoryData.m_Title;
            m_References.Add(re);
        }


        public void FinishCreatingStory()
        {
            m_StoryData.Build();

            m_Story = new StoryInfo();
            m_Story.m_StoryData = m_StoryData;

            m_StoriesList.Add(m_Story);
        }

        [System.Serializable]
        private class References
        {
            [HideInInspector]
            public int m_ParentStoryID;
            public string m_StoryName;
            public Sprite m_StorySelectionUiSprite;
        }
    }
}