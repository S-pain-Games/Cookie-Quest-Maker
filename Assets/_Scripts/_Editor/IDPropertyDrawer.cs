using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ID))]
public class IDPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect p = position;
        p.width -= 20;
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.TextField(p, label, property.FindPropertyRelative("m_NameID").stringValue);
        EditorGUI.EndDisabledGroup();
    }
}
