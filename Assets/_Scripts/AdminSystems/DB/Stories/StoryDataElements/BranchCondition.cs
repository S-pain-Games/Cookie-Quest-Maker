using System;
using UnityEngine;

// We should take into account the target
[Serializable]
public class BranchCondition
{
    public QPTag.TagType m_Tag;
    public int m_Value = 1;
}