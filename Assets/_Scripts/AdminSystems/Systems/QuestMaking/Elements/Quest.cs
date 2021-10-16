using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    public List<QuestPiece> Pieces => m_PiecesList;

    [SerializeField]
    private List<QuestPiece> m_PiecesList = new List<QuestPiece>();

    public void AddPiece(QuestPiece piece) => m_PiecesList.Add(piece);

    public void RemovePiece(QuestPiece piece) => m_PiecesList.Remove(piece);

    public void GetOverallTag(out QuestPieceTagType highestTagType, out int highestValue)
    {
        Dictionary<QuestPieceTagType, int> values = CountAllTags();

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

    private Dictionary<QuestPieceTagType, int> CountAllTags()
    {
        Dictionary<QuestPieceTagType, int> values = new Dictionary<QuestPieceTagType, int>();

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

        return values;
    }

    public QuestPiece GetTarget()
    {
        // This might be questionable but it works and its easy to change
        return m_PiecesList.Find((q) => q.Type == QuestPiece.PieceType.Target);
    }
}
