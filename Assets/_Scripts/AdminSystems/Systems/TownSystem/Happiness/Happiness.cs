using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Town's Happiness
/// </summary>
public class Happiness : MonoBehaviour
{
    public int maxHappiness = 100;
    public int currentHappiness = 20;

    #if UNITY_EDITOR
    [SerializeField] private bool showLog;
    #endif

    void Start()
    {
    #if UNITY_EDITOR
        if (showLog)
            Debug.Log("Current happiness: " + currentHappiness);
    #endif
    }

    public void pointsHappiness(int points)
    {
        if (currentHappiness + points <= maxHappiness && currentHappiness + points >= 0)
        {
            currentHappiness += points;
            #if UNITY_EDITOR
            if (showLog)
                Debug.Log("Current happiness: " + currentHappiness);
            #endif
        }
        else if (currentHappiness + points > maxHappiness)
        {
            currentHappiness = maxHappiness;
            #if UNITY_EDITOR
            Debug.Log("Current happiness: " + currentHappiness + ". Happiness overflow!");
            #endif
        }
        else if (currentHappiness + points < 0)
        {
            currentHappiness = 0;
            #if UNITY_EDITOR
            Debug.Log("Current happiness: " + currentHappiness + ". People are depressed.");
            #endif
        }
    }
}
