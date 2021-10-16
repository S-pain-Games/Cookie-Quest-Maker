using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Data that describes the persistent designer-authored state of a story
/// </summary>
[CreateAssetMenu()]
public class StoryData : ScriptableObject
{
    public string Title => m_Title;
    public string IntroductionPhrase => m_IntroductionPhrase;
    public QuestPiece Targets => null;

    [SerializeField]
    private string m_Title = "";
    [SerializeField]
    private string m_IntroductionPhrase = "";
    [SerializeField]
    private List<BranchOption> m_BranchOptions = new List<BranchOption>();

    // Check all the options and return the story string result
    // of the one that matches the given input best
    public void Check(QuestPieceTagType tag, int value, out string result)
    {
        result = "";
        bool match = false;
        for (int i = 0; i < m_BranchOptions.Count; i++)
        {
            if (m_BranchOptions[i].Check(tag, value, out result))
            {
                match = true;
                break;
            }
        }

        #region UNITY_EDITOR
#if UNITY_EDITOR
        if (!match)
        {
            throw new ApplicationException("There is an error with the story response structure");
        }
#endif
        #endregion
    }

    private void OnEnable()
    {
        // [Anthony] We have to sort by value descending because
        // we brute force search through all the options
        // and get the first match
        // It is not the most performant but given that this will
        // get executed rarely and we wont have 20+ branch options
        // it is good enough
        m_BranchOptions.Sort();
    }
}
