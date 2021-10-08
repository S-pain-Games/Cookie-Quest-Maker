using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a button in the inspector that executes the tagged method (Can't have input parameters)
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class MethodButtonAttribute : Attribute
{

}
