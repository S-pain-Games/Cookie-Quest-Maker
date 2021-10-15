using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu()]
public class Story : ScriptableObject
{
    [SerializeField]
    private string m_IntroductionPhrase = "";

    [SerializeField]
    private List<BranchOption> m_BranchOptions = new List<BranchOption>();

    // Check all the options and return the story string result
    // of the one that matches the given input best
    public void Check(QuestTagType tag, int value, out string result)
    {
        result = "";
        bool match = false;
        for (int i = 0; i < m_BranchOptions.Count; i++)
        {
            if (m_BranchOptions[i].Check(tag, value, out result))
            {
                match = true;
                break;
            }
        }

        #region UNITY_EDITOR
#if UNITY_EDITOR
        if (!match)
        {
            throw new ApplicationException("There is an error with the story response structure");
        }
#endif
        #endregion
    }

    private void OnEnable()
    {
        // [Anthony] We have to sort by value descending because
        // we brute force search through all the options
        // and get the first match
        // It is not the most performant but given that this will
        // get executed rarely and we wont have 20+ branch options
        // it is good enough
        m_BranchOptions.Sort();
    }
}

[Serializable]
public class BranchOption : IComparable<BranchOption>
{
    [SerializeField]
    private BranchCondition m_Condition;

    [SerializeField]
    private string m_Result = "";

    [SerializeField]
    private StoryRepercusion m_Ending;

    public bool Check(QuestTagType tag, int value, out string result)
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

[Serializable]
public class BranchCondition
{
    public QuestTagType Tag { get => m_Tag; }
    public int Value { get => m_Value; }

    [SerializeField]
    private QuestTagType m_Tag;

    [SerializeField]
    private LogicOperation m_LogicOp = LogicOperation.BiggerOrEqual;

    [SerializeField]
    private int m_Value = 1;

    private enum LogicOperation
    {
        BiggerOrEqual,
        Bigger,
        Equal,
    }

    public bool Check(QuestTagType tag, int value)
    {
        if (tag == m_Tag)
        {
            switch (m_LogicOp)
            {
                case LogicOperation.Equal:
                    if (value == m_Value)
                        return true;
                    else
                        return false;
                case LogicOperation.Bigger:
                    if (value > m_Value)
                        return true;
                    else
                        return false;
                case LogicOperation.BiggerOrEqual:
                    if (value >= m_Value)
                        return true;
                    else
                        return false;
                default:
                    return false;
            }
        }
        else
        {
            return false;
        }
    }
}