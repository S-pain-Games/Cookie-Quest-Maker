using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CQM.Databases;

[CustomEditor(typeof(TownBuilder))]
public class TownBuilderEditor : Editor
{
    private TownBuilder t;
    private bool showInternal;

    private void Awake()
    {
        t = target as TownBuilder;
    }

    // We could definitely make only one editor with inheritance for all the data builders
    // but oh well wachugonnadu
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Load Data From Script", GUILayout.Height(30)))
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
