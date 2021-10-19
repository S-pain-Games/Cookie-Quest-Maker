using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestPieceTagType
{
    // It might be a horrible idea to serialize an enum
    Harm,
    Convince,
    Help
}

[Serializable]
public class QuestPieceTag
{
    public QuestPieceTagType Type { get => m_TagType; }
    [SerializeField] private QuestPieceTagType m_TagType;

    public int Value { get => m_Value; }
    [SerializeField] private int m_Value;
}