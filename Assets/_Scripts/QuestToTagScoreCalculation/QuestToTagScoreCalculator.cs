using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestToTagScoreCalculator
{

    //INPUT: Phrase object
    //OUTPUT: TagResult containing the highest Tag value added from all Words in Phrase.

    //Phrase Examples:
    //              [Attack]    [Mayor]     [Baseball Bat]
    //[Brutally]    [Attack]    [Mayor]     [Baseball Bat]
    //[Modifier]    [Action]    [Target]    [Object]

    //[Opt]         [Req]       [Req]       [Opt]
    /*
    private Dictionary<TagIntensity, int> tagValueRecount;

    public QuestToTagScoreCalculator()
    {
        tagValueRecount = new Dictionary<TagIntensity, int>();
    }

    //Recibir conjunto
    public TagResult ParseQuestPhrase(Phrase questPhrase)
    {
        tagValueRecount.Clear();

        //Contabilizar los tags de las palabras
        RecountTagValuesFromWord(questPhrase.Action);
        RecountTagValuesFromWord(questPhrase.Target);

        //Optional words
        if (questPhrase.Modifier != null)
            RecountTagValuesFromWord(questPhrase.Modifier);

        if (questPhrase.Modifier != null)
            RecountTagValuesFromWord(questPhrase.Object);

        return GenerateTagResult(questPhrase.Action);
    }

    private void RecountTagValuesFromWord(Word w)
    {
        foreach (TagIntensity tagi in w.m_Tags)
        {
            if (!tagValueRecount.ContainsKey(tagi))
            {
                tagValueRecount.Add(tagi, tagi.Intensity);
            }
            else
            {
                tagValueRecount[tagi] += tagi.Intensity;
            }
        }
    }

    private TagResult GenerateTagResult(Word action)
    {

        //TO DO: ASEGURARSE DE QUE EFECTIVAMENTE LA LISTA ESTÁ ORDENADA

        tagValueRecount.OrderByDescending(key => key.Value);

        var sortedDict = from entry in tagValueRecount orderby entry.Value descending select entry;
        List<KeyValuePair<TagIntensity, int>> sortedList = sortedDict.ToList();

        KeyValuePair<TagIntensity, int> selected;

        //¿Sólo hay un elemento en la lista?
        if (sortedList.Count == 1)
        {
            selected = sortedList[0];
        }
        //¿Hay empate entre la primera y segunda posición en la lista?
        else if (sortedList[0].Value == sortedList[1].Value)
        {
            List<KeyValuePair<TagIntensity, int>> tieTagValues = GetTieTagValuesList(sortedList);
            int selectedTagIndex = SelectTagIndexFromList(tieTagValues, action);
            selected = sortedList[selectedTagIndex];
        }
        else
        {
            selected = sortedList[0];
        }

        return SetupTagResultWithTagAndValue(selected.Key.Tag, selected.Value);
    }

    private List<KeyValuePair<TagIntensity, int>> GetTieTagValuesList(List<KeyValuePair<TagIntensity, int>> sortedList)
    {
        List<KeyValuePair<TagIntensity, int>> tieValues = new List<KeyValuePair<TagIntensity, int>>();

        int highestValue = sortedList[0].Value;

        for (int i = 0; i < sortedList.Count; i++)
        {
            //Recorrer la lista añadiendo los Tags con el mismo valor
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

    private int SelectTagIndexFromList(List<KeyValuePair<TagIntensity, int>> tieTagValues, Word action)
    {
        List<int> actionTags = GetActionTagValuesList(tieTagValues, action);
        if (actionTags.Count == 1)
        {
            return actionTags[0];
        }
        else if (actionTags.Count >= 1)
        {
            //Seleccionar aleatoriamente entre las acciones
            return actionTags[Random.Range(0, actionTags.Count - 1)];
        }
        else
        {
            //Seleccionar aleatoriamente de la lista general
            return Random.Range(0, tieTagValues.Count - 1);
        }
    }

    private List<int> GetActionTagValuesList(List<KeyValuePair<TagIntensity, int>> tieList, Word action)
    {
        List<int> actionTags = new List<int>();

        for (int i = 0; i < tieList.Count; i++)
        {
            if (action.m_Tags.Contains(tieList[i].Key))
            {
                actionTags.Add(i);
            }
        }

        return actionTags;
    }


    private TagResult SetupTagResultWithTagAndValue(Tag tag, int value)
    {
        return new TagResult
        {
            Tag = tag,
            Value = value
        };
    }

    public struct TagResult
    {
        public Tag Tag;
        public int Value;
    }
    */
}
