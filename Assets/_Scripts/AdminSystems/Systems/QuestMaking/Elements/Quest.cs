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
        List<QuestPieceTagType> highestTagsTypes = new List<QuestPieceTagType>(3);
        foreach (var tagType in values.Keys)
        {
            int tagTypeValue = values[tagType];
            if (tagTypeValue > highestValue)
            {
                highestValue = tagTypeValue;
                highestTagsTypes.Clear();
                highestTagsTypes.Add(tagType);
            }
            else if (tagTypeValue == highestValue)
            {
                highestTagsTypes.Add(tagType);
            }
        }

        if (highestTagsTypes.Count == 1)
            highestTagType = highestTagsTypes[0];
        else
        {
            QuestPiece action = GetPieceOfType(QuestPiece.PieceType.Action);
            bool match = false;

            // We iterate over all the tags of the action (They are sorted from highest value to lowest)
            for (int i = 0; i < action.Tags.Count; i++)
            {
                QuestPieceTagType tagType = action.Tags[i].Type;
                // We check if the tag is the one used 
                for (int j = 0; j < highestTagsTypes.Count; j++)
                {
                    // If we find that the action tag is one
                    // of the highest tags in a tie
                    if (tagType == highestTagsTypes[j])
                    {
                        match = true;
                        highestTagType = tagType;
                        break;
                    }
                }

                if (match)
                    break;
            }

            // If the Action doesn't have any of the highest valued tags
            // we select one of the highest tag types at "Random"
            if (!match)
                highestTagType = highestTagsTypes[0];
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
        QuestPiece actionPiece = GetPieceOfType(QuestPiece.PieceType.Action);
        List<QuestPieceTagType> actionPieceTagTypes = new List<QuestPieceTagType>();

        for (int i = 0; i < actionPiece.Tags.Count; i++)
        {
            actionPieceTagTypes.Add(actionPiece.Tags[i].Type);
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

    public QuestPiece GetPieceOfType(QuestPiece.PieceType pieceType)
    {
        return m_PiecesList.Find((q) => q.Type == pieceType);
    }
}
