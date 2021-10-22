using System.Collections.Generic;
using UnityEngine;

// Processes quest data
public class QuestSystem
{
    public void GetOverallTag(List<QuestPiece> pieces, out QPTag.TagType highestTagType, out int highestValue)
    {
        Dictionary<QPTag.TagType, int> values = CountAllTags(pieces);

        // Search for the highest valued tag
        highestValue = 0;
        highestTagType = QPTag.TagType.Convince; // Counts as null
        List<QPTag.TagType> highestTagsTypes = new List<QPTag.TagType>(3);
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
            QuestPiece action = pieces.Find((q) => q.m_Type == QuestPiece.PieceType.Action);
            bool match = false;

            // We iterate over all the tags of the action (They are sorted from highest value to lowest)
            for (int i = 0; i < action.m_Tags.Count; i++)
            {
                QPTag.TagType tagType = action.m_Tags[i].m_Type;
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

    private Dictionary<QPTag.TagType, int> CountAllTags(List<QuestPiece> questPieces)
    {
        Dictionary<QPTag.TagType, int> values = new Dictionary<QPTag.TagType, int>();

        // Count all the tags values
        for (int i = 0; i < questPieces.Count; i++)
        {
            List<QPTag> tags = questPieces[i].m_Tags;
            for (int j = 0; j < tags.Count; j++)
            {
                QPTag tag = tags[j];
                if (values.TryGetValue(tags[j].m_Type, out int currentValue))
                {
                    values[tag.m_Type] = currentValue += tag.m_Value;
                }
                else
                {
                    values.Add(tag.m_Type, tag.m_Value);
                }
            }
        }

        return values;
    }

    // [TO-DO] FIX ORDER BY ERROR AFTER DOD REDESIGN
    // =================================================
    // NEW
    // =================================================

    public void GetOverallTagV2(List<QuestPiece> pieces, out QPTag.TagType highestTagType, out int highestValue)
    {
        Dictionary<QPTag.TagType, int> valuesDict = CountAllTags(pieces);

        // Search for the highest valued tag
        highestValue = 0;
        highestTagType = QPTag.TagType.Convince; // Counts as null


        //TO DO: ASEGURARSE DE QUE, EFECTIVAMENTE, LA LISTA ESTÉ ORDENADA
        //var sortedDict = from entry in valuesDict orderby entry.Value descending select entry;
        //List<KeyValuePair<QPTag.TagType, int>> sortedList = sortedDict.ToList();

        //KeyValuePair<QPTag.TagType, int> selected;

        ////¿Sólo hay un elemento en la lista?
        //if (sortedList.Count == 1)
        //{
        //    selected = sortedList[0];
        //}
        ////¿Hay empate entre la primera y segunda posición en la lista?
        //else if (sortedList[0].Value == sortedList[1].Value)
        //{
        //    List<KeyValuePair<QPTag.TagType, int>> tieTagValues = GetTieTagValuesList(sortedList);
        //    int selectedTagIndex = SelectTagIndexFromList(pieces, tieTagValues);
        //    selected = tieTagValues[selectedTagIndex];
        //}
        //else
        //{
        //    selected = sortedList[0];
        //}

        ////Tag y valor seleccionado
        //highestValue = selected.Value;
        //highestTagType = selected.Key;
    }

    private List<KeyValuePair<QPTag.TagType, int>> GetTieTagValuesList(List<KeyValuePair<QPTag.TagType, int>> sortedList)
    {
        List<KeyValuePair<QPTag.TagType, int>> tieValues = new List<KeyValuePair<QPTag.TagType, int>>();

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

    private int SelectTagIndexFromList(List<QuestPiece> pieces, List<KeyValuePair<QPTag.TagType, int>> tieTagValues)
    {
        List<int> actionTagsIndex = GetActionTagValuesList(pieces, tieTagValues);
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
    private List<int> GetActionTagValuesList(List<QuestPiece> pieces, List<KeyValuePair<QPTag.TagType, int>> tieList)
    {
        QuestPiece actionPiece = GetPieceOfType(pieces, QuestPiece.PieceType.Action);
        List<QPTag.TagType> actionPieceTagTypes = new List<QPTag.TagType>();

        for (int i = 0; i < actionPiece.m_Tags.Count; i++)
        {
            actionPieceTagTypes.Add(actionPiece.m_Tags[i].m_Type);
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

    public QuestPiece GetPieceOfType(List<QuestPiece> pieces, QuestPiece.PieceType pieceType)
    {
        return pieces.Find((q) => q.m_Type == pieceType);
    }
}