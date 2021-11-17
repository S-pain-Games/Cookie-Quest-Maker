using CQM.AssetReferences;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CharacterReferencesDatabase.CharacterReference))]
public class CharacterReferencesPropDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var sLine = EditorGUIUtility.singleLineHeight;
        Rect p = position;
        p.height = sLine;
        EditorGUI.PropertyField(p, property.FindPropertyRelative("m_IDName"));
        p.y += sLine + 1f;
        EditorGUI.PropertyField(p, property.FindPropertyRelative("m_CharacterWorldPrefab"));
        p.y += sLine + 1f;
        p.height = sLine * 9;
        p.width /= 2.0f;
        EditorGUI.ObjectField(p, property.FindPropertyRelative("m_DialogueSprite"), typeof(Sprite), GUIContent.none);
        p.x += p.width;
        EditorGUI.ObjectField(p, property.FindPropertyRelative("m_NewspaperSprite"), typeof(Sprite), GUIContent.none);

        p.y += sLine * 9;
        p.height = sLine;

        var style = new GUIStyle { alignment = TextAnchor.MiddleCenter };
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;
        EditorGUI.LabelField(p, "Newspaper Sprite", style);
        p.x -= p.width;
        EditorGUI.LabelField(p, "Dialogue Sprite", style);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 12 + 2f;
    }
}
