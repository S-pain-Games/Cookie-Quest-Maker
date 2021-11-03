using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CQM.Components
{

    // Data that describes the persistent designer-authored state of a story
    public class StoryData
    {
        public string m_Title = "";
        public List<string> m_IntroductionDialogue = new List<string>();
        public List<BranchOption> m_BranchOptions = new List<BranchOption>();
        public List<int> m_Targets = new List<int>(); // TODO Add multiple targets

        public void Build()
        {
            // [Anthony] We have to sort by value descending because
            // we brute force search through all the options
            // and get the first match
            // It is not the most performant but given that this will
            // get executed rarely and we wont have 20+ branch options
            // it is good enough
            m_BranchOptions.Sort();

            m_Targets.Clear();
            for (int i = 0; i < m_BranchOptions.Count; i++)
            {
                int targetId = m_BranchOptions[i].m_Condition.m_Target;
                if (!m_Targets.Contains(targetId))
                {
                    m_Targets.Add(targetId);
                }
            }
        }
    }

    [Serializable]
    public class BranchOption : IComparable<BranchOption>
    {
        public BranchCondition m_Condition;

        public List<string> m_Result = new List<string>();
        public StoryRepercusion m_Repercusion;

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
    }

    // We should take into account the target
    [Serializable]
    public class BranchCondition
    {
        public int m_Target;
        public QPTag.TagType m_Tag;
        public int m_Value = 1;
    }

}