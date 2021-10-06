using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDebugDraw : MonoBehaviour
{
    private void Update()
    {
        DebugDraw.DrawRect(new Rect(0, 0, 5, 5));
    }
}
