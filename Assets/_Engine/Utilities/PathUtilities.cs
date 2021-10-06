using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PathUtilities
{
    /// <returns>If the Object is in "Assets/Folder/Object" then it returns "Assets/Folder/"</returns>
    public static string GetPath(ScriptableObject so)
    {
        string path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(so));
        path = path.Remove(path.LastIndexOf("/") + 1);
        return path;
    }

    /// <inheritdoc cref="GetPath(ScriptableObject)"/>
    public static string GetPath(MonoBehaviour mb)
    {
        string path = AssetDatabase.GetAssetPath(MonoScript.FromMonoBehaviour(mb));
        path = path.Remove(path.LastIndexOf("/") + 1);
        return path;
    }
}
