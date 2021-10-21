using System;
using UnityEngine;

[Serializable]
public class BranchOption : IComparable<BranchOption>
{
    public BranchCondition m_Condition;
    public string m_Result = "";
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
