using System;
using System.Collections.Generic;
using UnityEngine;

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
