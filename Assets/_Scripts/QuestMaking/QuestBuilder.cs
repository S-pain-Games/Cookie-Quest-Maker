using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This just builds a quest made of pieces, Every quest must have valid targets 
// that are specified by the story data
// For simplicity this class doesnt check if the quest targets
// are the correct ones for the given story
// all those checks must be made elsewhere
[Serializable]
public class QuestBuilder
{
    // Quest maker listens to this
    public event Action<Quest> OnQuestFinished;

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
        OnQuestFinished.Invoke(m_CurrentQuest);
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

    public void GetValue(out QuestPieceTagType highestTagType, out int highestValue)
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

    public QuestPiece GetTarget()
    {
        // This might be questionable but it works and its easy to change
        return m_PiecesList.Find((q) => q.Type == QuestPiece.PieceType.Target);
    }
}
