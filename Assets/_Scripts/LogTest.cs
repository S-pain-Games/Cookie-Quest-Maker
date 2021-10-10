using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debugging;

public class LogTest : MonoBehaviour
{
    void Update()
    {
        Debugging.Logg.Log("Message", priority: 3, filterName: "Test");
    }
}
