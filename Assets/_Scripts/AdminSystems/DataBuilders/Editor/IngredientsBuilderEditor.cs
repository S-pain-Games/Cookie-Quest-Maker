using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IngredientsBuilder))]
public class IngredientsBuilderEditor : Editor
{
    private IngredientsBuilder t;
    private bool showInternal;

    private void Awake()
    {
        t = target as IngredientsBuilder;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Load Data From Script", GUILayout.Height(40)))
        {
            serializedObject.Update();
            t.LoadDataFromCode();
            EditorUtility.SetDirty(t);
        }

        showInternal = EditorGUILayout.Toggle("Show internal data", showInternal);
        if (showInternal)
        {
            DrawDefaultInspector();
        }
    }
}
