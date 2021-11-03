using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * DEPRECATED
 * 
 */



[CreateAssetMenu()]
public class LocationState : ScriptableObject
{
    [SerializeField]
    private List<StoryRepercusion> StoryRepercusionList;

    public int locationvalue = 0;

    public void CalculateLocationState()
    {
        foreach (StoryRepercusion _sr in StoryRepercusionList)
        {
            if (_sr.m_Active)
            {
                locationvalue += _sr.m_Value;
            }
        }
    }
}
