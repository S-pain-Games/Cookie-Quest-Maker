using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExampleDebugDraw : MonoBehaviour
{
    public BoxCollider2D bc;
    private void Update()
    {
        DebugDraw.DrawBoxCollider(bc);
        DebugDraw.DrawRect(new Rect(0, 0, 5, 5));
    }
}
