using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestToTagScoreCalculator
{
    /*
     * Recibo una serie de entradas con Tags
     * 
     * Desglosar y contabilizar Tags
     * 
     * Devolver el Tag con mayor valor
     * 
     * Dar prioridad al Tag perteneciente al verbo
    */

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

        Debug.Log("Priority Tag: "+ action.m_Tags[0].Tag.TagName+", Value: "+GetTotalValueOfWordTag(action));
        return GenerateTagResult(action.m_Tags[0].Tag, GetTotalValueOfWordTag(action));
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
