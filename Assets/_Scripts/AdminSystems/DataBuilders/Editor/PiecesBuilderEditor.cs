using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CQM.Databases;

[CustomEditor(typeof(PieceBuilder))]
public class PiecesBuilderEditor : Editor
{
    private PieceBuilder t;
    private bool showInternal;

    private void Awake()
    {
        t = target as PieceBuilder;
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
