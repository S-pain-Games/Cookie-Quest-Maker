using System;
using UnityEngine;
// We should take into account the target
[Serializable]
public class BranchCondition
{
    public QuestPiece Target { get => m_Target; }
    public QuestPieceTagType Tag { get => m_Tag; }
    public int Value { get => m_Value; }

    [SerializeField]
    public QuestPiece m_Target;

    [SerializeField]
    private QuestPieceTagType m_Tag;

    [SerializeField]
    private LogicOperation m_LogicOp = LogicOperation.BiggerOrEqual;

    [SerializeField]
    private int m_Value = 1;

    private enum LogicOperation
    {
        BiggerOrEqual,
        //Bigger,
        //Equal,
    }

    public bool Check(QuestPieceTagType tag, int value)
    {
        if (tag == m_Tag)
        {
            switch (m_LogicOp)
            {
                //case LogicOperation.Equal:
                //    if (value == m_Value)
                //        return true;
                //    else
                //        return false;
                //case LogicOperation.Bigger:
                //    if (value > m_Value)
                //        return true;
                //    else
                //        return false;
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