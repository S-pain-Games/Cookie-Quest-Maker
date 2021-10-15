using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class QuestPieceTagType : ScriptableObject
{
    public string TagName { get => m_TagName; }
    [SerializeField] private string m_TagName = "Unnamed";
}

[Serializable]
public class QuestPieceTag
{
    public QuestPieceTagType Type { get => m_TagType; }
    [SerializeField] private QuestPieceTagType m_TagType;

    public int Value { get => m_Value; }
    [SerializeField] private int m_Value;
}