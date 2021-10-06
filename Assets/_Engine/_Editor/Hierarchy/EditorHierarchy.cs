using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class EditorHierarchy
{
    static EditorHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= OnGUI;
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect rect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (gameObject)
        {
            if (gameObject.name.Contains("header_"))
            {
                DrawHeader(rect, gameObject.name.Replace("header_", ""));
            }
            else
            {
                DrawDefault(rect, gameObject);
            }
        }
    }

    private static void DrawHeader(Rect rect, string name)
    {
        EditorGUI.DrawRect(rect, Color.black);
        rect.x += 1;
        rect.y += 1;
        rect.width -= 2;
        rect.height -= 2;

        float darkGray = 0.35f;
        EditorGUI.DrawRect(rect, new Color(darkGray, darkGray, darkGray));

        GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
        float c = 0.96f;
        style.normal.textColor = new Color(c, c, c);
        style.alignment = TextAnchor.MiddleCenter;
        EditorGUI.LabelField(rect, name, style);
    }

    private static void DrawDefault(Rect rect, GameObject gameObject)
    {
        //DrawIcon(rect);
        DrawToggleActive(rect, gameObject);
    }

    private static void DrawIcon(Rect rect)
    {
        // [TO-DO] Implement a way to select custom icons
        Rect cursor = rect;
        cursor.x += cursor.width - 40;
        cursor.width = 20;
        GUIContent scriptIcon = EditorGUIUtility.IconContent("cs Script Icon");
        EditorGUI.LabelField(cursor, scriptIcon);
    }

    private static void DrawToggleActive(Rect rect, GameObject gameObject)
    {
        rect.x += rect.width - 20;
        rect.width = 20;
        bool toggled = EditorGUI.Toggle(rect, gameObject.activeSelf);
        if (toggled != gameObject.activeSelf)
            gameObject.SetActive(toggled);
    }
}
