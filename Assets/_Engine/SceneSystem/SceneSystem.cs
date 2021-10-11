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

    // We assume that we wont try to load multiple scenes at the same time
    private IEnumerator LoadSceneCoroutine(SceneData sceneData)
    {
        Logg.Log($"Start Loading Scene [{sceneData.name}]", "Scene System");

        if (sceneData.Persistent)
        {
            yield return SceneManager.LoadSceneAsync(sceneData.SceneIndex, LoadSceneMode.Additive);

            Logg.Log($"Loading Persistent [{sceneData.name}] Finished", "Scene System");
        }
        else
        {
            yield return SceneManager.LoadSceneAsync(sceneData.SceneIndex, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneData.SceneIndex));

            Logg.Log($"Loading [{sceneData.name}] Finished", "Scene System");
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