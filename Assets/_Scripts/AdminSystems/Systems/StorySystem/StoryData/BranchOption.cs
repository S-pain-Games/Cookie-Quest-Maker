using System;
using UnityEngine;

[Serializable]
public class BranchOption : IComparable<BranchOption>
{
    [SerializeField]
    private BranchCondition m_Condition;

    [SerializeField]
    private string m_Result = "";

    [SerializeField]
    private StoryRepercusion m_Ending;

    public bool Check(QuestPieceTagType tag, int value, out string result)
    {
        bool match = m_Condition.Check(tag, value);
        if (match)
            result = m_Result;
        else
            result = "";
        return match;
    }

    public int CompareTo(BranchOption obj)
    {
        if (m_Condition.Value >= obj.m_Condition.Value)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
