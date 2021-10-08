using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[CreateAssetMenu()]
public class SceneData : ScriptableObject
{
    public int SceneIndex { get => _sceneID; }
    public bool Persistent { get => _persistent; }

    [SerializeField] private int _sceneID = -1;
    [SerializeField] private bool _persistent = false;

#if UNITY_EDITOR
    [SerializeField] private Object _sceneAsset;

    private void OnValidate()
    {
        if (_sceneAsset as SceneAsset != null)
        {
            _sceneID = SceneUtility.GetBuildIndexByScenePath(AssetDatabase.GetAssetPath(_sceneAsset));
        }
        else
        {
            _sceneID = -1;
            _sceneAsset = null;
        }
    }
#endif
}
