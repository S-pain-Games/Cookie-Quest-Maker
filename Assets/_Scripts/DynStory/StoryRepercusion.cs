using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class StoryRepercusion : ScriptableObject
{
    public bool Archieved { get => m_Archieved; }
    public int Value { get => m_Value; }

    // Is the given repercusion true
    // Are there still wolves in the town
    [SerializeField]
    private bool m_Archieved = false;

    // The value that this repercusion has to the
    // total value of "happiness" of the location
    // if it has been archieved
    [SerializeField]
    private int m_Value = 0;
}