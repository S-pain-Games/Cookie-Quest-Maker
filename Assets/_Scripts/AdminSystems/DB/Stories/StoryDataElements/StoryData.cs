using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Data that describes the persistent designer-authored state of a story
public class StoryData
{
    public string m_Title = "";
    public List<string> m_IntroductionDialogue = new List<string>();
    public List<BranchOption> m_BranchOptions = new List<BranchOption>();
    public int m_Target; // TODO Add multiple targets

    public void Build()
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
