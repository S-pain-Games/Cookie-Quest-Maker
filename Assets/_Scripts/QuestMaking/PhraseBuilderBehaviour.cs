using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WIP
[Serializable]
public class PhraseBuilderBehaviour : MonoBehaviour
{
    [SerializeField] private WordSocketBehaviour _cookieSocket;
    [SerializeField] private WordSocketBehaviour _modifierSocket;
    [SerializeField] private WordSocketBehaviour _actionSocket;
    [SerializeField] private WordSocketBehaviour _targetSocket;
    [SerializeField] private WordSocketBehaviour _objectSocket;

    [MethodButton]
    private void LogCurrentPhrase()
    {
        if (_actionSocket.filled && _targetSocket.filled)
        {
            Logg.Log($"[{_actionSocket.word.WordText}][{_targetSocket.word.WordText}]");
        }
    }

    private Phrase GetPhrase()
    {
        // GC Alloc (Low)
        Phrase phrase = new Phrase();

        if (_cookieSocket.filled)
            phrase.SetWord(_cookieSocket.word);
        if (_modifierSocket.filled)
            phrase.SetWord(_modifierSocket.word);
        if (_actionSocket.filled)
            phrase.SetWord(_actionSocket.word);
        if (_targetSocket.filled)
            phrase.SetWord(_targetSocket.word);
        if (_objectSocket.filled)
            phrase.SetWord(_objectSocket.word);

        return phrase;
    }
}

[Serializable]
public class Phrase
{
    public Word Cookie => m_Cookie;
    public Word Modifier => m_Modifier;
    public Word Action => m_Action;
    public Word Target => m_Target;
    public Word Object => m_Object;

    private List<Word> m_WordsList = new List<Word>();
    private Word m_Cookie;
    private Word m_Modifier;
    private Word m_Action;
    private Word m_Target;
    private Word m_Object;

    // It would probably definitely be easier and better to have a function
    // for each type of word
    public void SetWord(Word word)
    {
        switch (word.Type)
        {
            case Word.WordType.Action:
                SetSpecificWord(m_Action, word);
                break;
            case Word.WordType.Target:
                SetSpecificWord(m_Target, word);
                break;
            case Word.WordType.Modifier:
                SetSpecificWord(m_Modifier, word);
                break;
            case Word.WordType.Object:
                SetSpecificWord(m_Object, word);
                break;
            case Word.WordType.Cookie:
                SetSpecificWord(m_Cookie, word);
                break;
            default:
                break;
        }
    }

    public IReadOnlyList<Word> GetList()
    {
        return m_WordsList;
    }

    private void SetSpecificWord(Word privateWord, Word word)
    {
        // Handles the case where the new word type has already been added
        // It should never happen though
        if (privateWord != null)
            m_WordsList.Remove(privateWord);

        privateWord = word;
        m_WordsList.Add(privateWord);
    }
}
