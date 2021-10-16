using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

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
        highestTagType = QuestPieceTagType.Convince; // Counts as null
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

    // =================================================
    // NEW
    // =================================================

    public void GetOverallTagV2(out QuestPieceTagType highestTagType, out int highestValue)
    {
        Dictionary<QuestPieceTagType, int> valuesDict = CountAllTags();

        // Search for the highest valued tag
        highestValue = 0;
        highestTagType = QuestPieceTagType.Convince; // Counts as null


        //TO DO: ASEGURARSE DE QUE, EFECTIVAMENTE, LA LISTA ESTÉ ORDENADA
        var sortedDict = from entry in valuesDict orderby entry.Value descending select entry;
        List<KeyValuePair<QuestPieceTagType, int>> sortedList = sortedDict.ToList();

        KeyValuePair<QuestPieceTagType, int> selected;

        //¿Sólo hay un elemento en la lista?
        if (sortedList.Count == 1)
        {
            selected = sortedList[0];
        }
        //¿Hay empate entre la primera y segunda posición en la lista?
        else if (sortedList[0].Value == sortedList[1].Value)
        {
            List<KeyValuePair<QuestPieceTagType, int>> tieTagValues = GetTieTagValuesList(sortedList);
            int selectedTagIndex = SelectTagIndexFromList(tieTagValues);
            selected = tieTagValues[selectedTagIndex];
        }
        else
        {
            selected = sortedList[0];
        }

        //Tag y valor seleccionado
        highestValue = selected.Value;
        highestTagType = selected.Key;
    }


    private List<KeyValuePair<QuestPieceTagType, int>> GetTieTagValuesList(List<KeyValuePair<QuestPieceTagType, int>> sortedList)
    {
        List<KeyValuePair<QuestPieceTagType, int>> tieValues = new List<KeyValuePair<QuestPieceTagType, int>>();

        int highestValue = sortedList[0].Value;

        //Recorrer la lista añadiendo los Tags con el mismo valor
        for (int i = 0; i < sortedList.Count; i++)
        {
            if (sortedList[i].Value == highestValue)
            {
                tieValues.Add(sortedList[i]);
            }
            else
            {
                break;
            }
        }

        return tieValues;
    }

    private int SelectTagIndexFromList(List<KeyValuePair<QuestPieceTagType, int>> tieTagValues)
    {
        List<int> actionTagsIndex = GetActionTagValuesList(tieTagValues);
        if (actionTagsIndex.Count == 1)
        {
            return actionTagsIndex[0];
        }
        else if (actionTagsIndex.Count >= 1)
        {
            //Seleccionar aleatoriamente entre las acciones
            return actionTagsIndex[Random.Range(0, actionTagsIndex.Count - 1)];
        }
        else
        {
            //Seleccionar aleatoriamente de la lista general
            return Random.Range(0, tieTagValues.Count - 1);
        }
    }

    //Obtener una lista de índices marcando los QuestPieceTagType que formen parte de Acción
    private List<int> GetActionTagValuesList(List<KeyValuePair<QuestPieceTagType, int>> tieList)
    {
        QuestPiece actionPiece = GetTarget(QuestPiece.PieceType.Action);
        List<QuestPieceTagType> actionPieceTagTypes = new List<QuestPieceTagType>();

        for(int i = 0; i < actionPiece.m_Tags.Count; i++)
        {
            actionPieceTagTypes.Add(actionPiece.m_Tags[i].Type);
        }

        List<int> actionTags = new List<int>();

        for (int i = 0; i < tieList.Count; i++)
        {
            if (actionPieceTagTypes.Contains(tieList[i].Key))
            {
                actionTags.Add(i);
            }
        }

        return actionTags;
    }

    public QuestPiece GetTarget(QuestPiece.PieceType pieceType)
    {
        return m_PiecesList.Find((q) => q.Type == pieceType);
    }
}
