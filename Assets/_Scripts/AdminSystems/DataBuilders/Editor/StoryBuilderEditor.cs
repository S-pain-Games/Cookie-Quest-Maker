using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CQM.Databases;

[CustomEditor(typeof(StoryBuilder))]
public class StoryBuilderEditor : Editor
{
    private StoryBuilder t;
    private bool showInternal;


    private void Awake()
    {
        t = target as StoryBuilder;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Load Data From Script", GUILayout.Height(40)))
        {
            serializedObject.Update();
            t.LoadDataFromCode();
            EditorUtility.SetDirty(t);
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_References"));

        if (GUILayout.Button("Apply References"))
        {
            t.SyncReferences();
            EditorUtility.SetDirty(t);
        }

        showInternal = EditorGUILayout.Toggle("Show internal data", showInternal);
        if (showInternal)
        {
            DrawDefaultInspector();
        }
    }
}
