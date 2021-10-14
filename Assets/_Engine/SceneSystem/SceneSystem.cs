using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;

public class SceneSystem : MonoBehaviour
{
    [Header("Listening To")]
    [SerializeField]
    private SceneDataEventHandle _loadSceneHandle;
    [SerializeField]
    private Scene _currentMainScene;

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
            // Unload existing main scene
            if (_currentMainScene.IsValid())
            {
                yield return SceneManager.UnloadSceneAsync(_currentMainScene);
            }

            // Load new scene
            yield return SceneManager.LoadSceneAsync(sceneData.SceneIndex, LoadSceneMode.Additive);

            // Load new scene
            _currentMainScene = SceneManager.GetSceneByBuildIndex(sceneData.SceneIndex);
            SceneManager.SetActiveScene(_currentMainScene);

            Logg.Log($"Loading [{sceneData.name}] Finished", "Scene System");
        }
    }

#if UNITY_EDITOR
    // This way of loading might be very bad but its only in the editor so its not that bad
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ColdStart()
    {
        // If the current scene is not the boot scene
        if (!SceneManager.GetSceneByName("Bootloader").IsValid())
        {
            Logg.Log("Setting up hot game start", "Editor");
            SceneManager.LoadSceneAsync("Persistent_Systems", LoadSceneMode.Additive).completed += (a) =>
            {
                FindObjectOfType<SceneSystem>()._currentMainScene = SceneManager.GetActiveScene();
            };
        }
    }
#endif

    private void OnEnable()
    {
        _loadSceneHandle.OnEvent += LoadScene;
    }

    private void OnDisable()
    {
        _loadSceneHandle.OnEvent -= LoadScene;
    }
}