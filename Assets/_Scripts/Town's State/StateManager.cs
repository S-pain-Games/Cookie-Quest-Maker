using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * DEPRECATED
 * 
 */

public class StateManager : MonoBehaviour
{
    [SerializeField]
    private List<LocationState> Locations;

    private Happiness happy = new Happiness();

    #if UNITY_EDITOR
    [SerializeField]
    private bool showLog = false;
    #endif

    //Points to add to Happiness
    private int newHappiness = 0;

    private void CalculateNewHappiness()
    {
        foreach(LocationState lc in Locations)
        {
            lc.CalculateLocationState();
            newHappiness += lc.locationvalue;

#if UNITY_EDITOR
            if (showLog)
                Debug.Log("Value added from location: " + lc.locationvalue);
#endif
        }
    }

    private void setHappiness()
    {
        happy.pointsHappiness(newHappiness);

#if UNITY_EDITOR
        if (showLog)
            Debug.Log("Happiness to add: " + newHappiness);
#endif
    }

    [MethodButton]
    public void StartCalculation()
    {
        CalculateNewHappiness();
        setHappiness();
    }

}
