using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debugging;

public class ExtensionsExampleScript : MonoBehaviour
{
    [MethodButton]
    public void Method()
    {
        Debugging.Logger.Log("Works", gameObject);
    }

    [MethodButton]
    public void Method2()
    {
        Debugging.Logger.Log("Works 2", gameObject);
    }
}
