using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestToTagScoreCalculator
{

    //INPUT: Phrase object
    //OUTPUT: TagResult containing the Tag of Action Word and its added value from all Words.

    //Phrase Examples:
    //              [Attack]    [Mayor]     [Baseball Bat]
    //[Brutally]    [Attack]    [Mayor]     [Baseball Bat]
    //[Adjective]   [Action]    [Target]    [Tool]

    //[Opt]         [Req]       [Req]       [Opt]

    private Dictionary<string, int> tagValueRecount;

    public QuestToTagScoreCalculator()
    {
        tagValueRecount = new Dictionary<string, int>();
    }

    //Recibir conjunto
    public TagResult ParseQuestPhrase(Phrase questPhrase)
    {
        tagValueRecount.Clear();

        //Lo suyo es que la Phrase contenga una lista de Words o algo

        //Required words
        Word action = questPhrase.Action;
        Word target = questPhrase.Target;

        //Optional words
        //Word adjective = questPhrase.Adjective;
        //Word tool = questPhrase.Tool;

        //Contabilizar los tags de las palabras
        RecountTagValuesFromWord(action);
        RecountTagValuesFromWord(target);
        //RecountTagValuesFromWord(adjective);
        //RecountTagValuesFromWord(tool);

        //Seleccionar el tag con prioridad y devolver su valor total
        Tag selectedTag = action.m_Tags[0].Tag;
        int selectedTagTotalValue = GetTotalValueOfWordTag(action);

        Debug.Log("Priority Tag: "+ selectedTag.TagName+", Value: "+ selectedTagTotalValue);
        return GenerateTagResult(selectedTag, selectedTagTotalValue);
    }

    private void RecountTagValuesFromWord(Word w)
    {
        foreach (TagIntensity tagi in w.m_Tags)
        {
            if (!tagValueRecount.ContainsKey(tagi.Tag.TagName))
            {
                tagValueRecount.Add(tagi.Tag.TagName, tagi.Intensity);
            }
            else
            {
                tagValueRecount[tagi.Tag.TagName] += tagi.Intensity;
            }
        }
    }

    private int GetTotalValueOfWordTag(Word w)
    {
        //Dado que un Word puede contener varios tags, asumimos que el principal
        //es el primero de ellos.

        return tagValueRecount[w.m_Tags[0].Tag.TagName];
    }

    public struct TagResult
    {
        public Tag Tag;
        public int Value;
    }

    private TagResult GenerateTagResult(Tag tag, int value)
    {
        TagResult tr = new TagResult
        {
            Tag = tag,
            Value = value
        };
        return tr;
    } 
}
