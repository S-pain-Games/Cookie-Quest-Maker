using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Used to add custom controls to the default unity MonoBehaviour inspector
/// </summary>
[CustomEditor(typeof(MonoBehaviour), true)]
public class CustomMonoBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawMethodButtons();
    }

    /// <summary>
    /// Create Buttons to invoke the marked methods in the MonoBehaviour script
    /// </summary>
    private void DrawMethodButtons()
    {
        // We should definitely not be doing this every IMGUI frame, but it works
        MethodInfo[] methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (MethodInfo method in methods)
        {
            if (method.GetCustomAttribute<MethodButtonAttribute>() != null)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(target, null);
                }
            }
        }
    }
}
