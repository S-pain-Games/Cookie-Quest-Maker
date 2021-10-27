using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class HappinessTest
{
    private Happiness _happiness;

    [SetUp]
    public void SetUpTest()
    {
        GameObject h = new GameObject();
        _happiness = h.AddComponent<Happiness>();
    }

    [Test]
    public void TestHappiness()
    {
        int randomHappiness = Random.Range(-100, 100);
        _happiness.pointsHappiness(randomHappiness);
        Debug.Log("Happiness to add: " + randomHappiness);
        Debug.Log("New happiness: " + _happiness.currentHappiness);
    }
}
