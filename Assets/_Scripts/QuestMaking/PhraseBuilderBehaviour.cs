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

public struct Phrase
{
    public Word Action { get => m_Action; }
    public Word Target { get => m_Target; }

    private Word m_Action;
    private Word m_Target;

    public Phrase(Word action, Word target)
    {
        m_Action = action;
        m_Target = target;
    }
}
