using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhraseBuilderBehaviour : MonoBehaviour
{
    [SerializeField] private WordSocketBehaviour m_ActionSocket;
    [SerializeField] private WordSocketBehaviour m_TargetSocket;

    [MethodButton]
    private void LogCurrentPhrase()
    {
        if (m_ActionSocket.filled && m_TargetSocket.filled)
        {
            Logg.Log($"[{m_ActionSocket.word.WordText}][{m_TargetSocket.word.WordText}]");
        }
    }

    private Phrase GetPhrase()
    {
        return new Phrase(m_ActionSocket.word, m_TargetSocket.word);
    }
}

[System.Serializable]
public struct Phrase
{
    public Word Modifier => m_Modifier;
    public Word Action => m_Action;
    public Word Target => m_Target;
    public Word Object => m_Object;

    private Word m_Modifier;
    private Word m_Action;
    private Word m_Target;
    private Word m_Object;

    public Phrase(Word action, Word target)
    {
        m_Modifier = null;
        m_Action = action;
        m_Target = target;
        m_Object = null;
    }

    public List<Word> ToList()
    {
        List<Word> words = new List<Word>();

        // These null checks on unity objects are really expensive
        // We should use a bitmask
        if (m_Action != null)
            words.Add(m_Action);
        if (m_Target != null)
            words.Add(m_Target);

        return words;
    }
}
