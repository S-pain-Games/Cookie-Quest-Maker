using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;

public class SceneSystem : MonoBehaviour
{
    [Header("Listening To")]
    [SerializeField] private SceneDataEventHandle _loadSceneHandle;

    public void LoadScene(SceneData sceneData)
    {
        StartCoroutine(LoadSceneCoroutine(sceneData));
    }

    private IEnumerator LoadSceneCoroutine(SceneData sceneData)
    {
        if (sceneData.Persistent)
            yield return SceneManager.LoadSceneAsync(sceneData.SceneIndex, LoadSceneMode.Additive);
        else
        {
            yield return SceneManager.LoadSceneAsync(sceneData.SceneIndex, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneData.SceneIndex));
        }
    }

    private void OnEnable()
    {
        _loadSceneHandle.OnEvent += LoadScene;
    }

    private void OnDisable()
    {
        _loadSceneHandle.OnEvent -= LoadScene;
    }
}