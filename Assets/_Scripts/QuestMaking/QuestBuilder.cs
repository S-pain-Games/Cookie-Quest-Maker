using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WIP
[Serializable]
public class QuestBuilder
{
    [SerializeField]
    private List<Quest> m_Quests = new List<Quest>();
    [SerializeField]
    private Quest m_CurrentQuest = new Quest();

    public void StartEmptyQuest()
    {
        m_CurrentQuest = new Quest();
    }

    public void AddPiece(QuestPiece piece)
    {
        m_CurrentQuest.AddPiece(piece);
    }

    public void RemovePiece(QuestPiece piece)
    {
        m_CurrentQuest.RemovePiece(piece);
    }

    public void FinishMakingQuest()
    {
        m_Quests.Add(m_CurrentQuest);
    }
}

[Serializable]
public class Quest
{
    public IReadOnlyList<QuestPiece> Pieces => m_PiecesList;
    [SerializeField]
    private List<QuestPiece> m_PiecesList = new List<QuestPiece>();

    public void AddPiece(QuestPiece piece) => m_PiecesList.Add(piece);

    public void RemovePiece(QuestPiece piece) => m_PiecesList.Remove(piece);

    public void GetValue(out QuestTagType highestTagType, out int highestValue)
    {
        Dictionary<QuestTagType, int> values = new Dictionary<QuestTagType, int>();

        // Count all the tags values
        for (int i = 0; i < m_PiecesList.Count; i++)
        {
            List<QuestPieceTag> tags = m_PiecesList[i].Tags;
            for (int j = 0; j < tags.Count; j++)
            {
                QuestPieceTag tag = tags[j];
                if (values.TryGetValue(tags[j].Type, out int currentValue))
                {
                    values[tag.Type] = currentValue += tag.Value;
                }
                else
                {
                    values.Add(tag.Type, tag.Value);
                }
            }
        }

        // Search for the highest valued tag
        highestValue = 0;
        highestTagType = null;
        foreach (var tagType in values.Keys)
        {
            int tagTypeValue = values[tagType];
            if (tagTypeValue > highestValue)
            {
                highestValue = tagTypeValue;
                highestTagType = tagType;
            }
        }
    }
}
