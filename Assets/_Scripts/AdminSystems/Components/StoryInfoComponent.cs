using System.Collections.Generic;
using UnityEngine;
using System;

namespace CQM.Components
{
    //Runtime representation of a ongoing/completed story and its state
    //Currently its only created by the Story System when a new story its started
    //Unstarted stories dont have this representation

    [System.Serializable]
    public class StoryInfoComponent
    {
        public StoryData m_StoryData; // Persistent Story Data
        public State m_State = State.NotStarted;


        public QuestDataComponent m_QuestData; // The quest that was created to complete the story
        public BranchOption m_QuestBranchResult; // The final result of the story given the quest
        public StoryRepercusionComponent m_QuestRepercusion = null;

        public enum State
        {
            NotStarted,
            InProgress,
            Completed
        }
    }


    // Data that describes the persistent designer-authored state of a story
    [Serializable]
    public class StoryData
    {
        public ID m_ID;
        public string m_Title = "";
        public List<string> m_IntroductionDialogue = new List<string>();
        public List<BranchOption> m_BranchOptions = new List<BranchOption>();

        // We cache all the targets on build
        public List<ID> m_AllPossibleTargets = new List<ID>();

        public void Build()
        {
            // [Anthony] We have to sort by value descending because
            // we brute force search through all the options
            // and get the first match
            // It is not the most performant but given that this will
            // get executed rarely and we wont have 20+ branch options
            // it is good enough
            m_BranchOptions.Sort();

            m_AllPossibleTargets.Clear();
            for (int i = 0; i < m_BranchOptions.Count; i++)
            {
                ID targetId = m_BranchOptions[i].m_Condition.m_Target;
                if (!m_AllPossibleTargets.Contains(targetId))
                {
                    m_AllPossibleTargets.Add(targetId);
                }
            }
        }
    }


    [Serializable]
    public class BranchOption : IComparable<BranchOption>
    {
        public BranchCondition m_Condition;

        // The dialogue that the customer will say the next day
        public List<string> m_ResultNPCDialogue = new List<string>();

        // The dialogue the deities will say at night after completing a story
        public List<DeitiesStoryDialogue> m_DeitiesResultDialogue = new List<DeitiesStoryDialogue>();

        public StoryRepercusionComponent m_Repercusion;

        public int CompareTo(BranchOption obj)
        {
            if (m_Condition.m_Value >= obj.m_Condition.m_Value)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        [System.Serializable]
        public class DeitiesStoryDialogue
        {
            public int m_DeityID = 0; // 0 -> Good Deity | 1 -> Evil Deity
            public List<string> m_Dialogue = new List<string>();
        }
    }


    // We should take into account the target
    [Serializable]
    public class BranchCondition
    {
        public ID m_Target;
        public QPTag.TagType m_Tag;
        public int m_Value = 1;
    }
}