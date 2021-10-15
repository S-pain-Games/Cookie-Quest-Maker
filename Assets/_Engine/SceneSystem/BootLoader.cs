using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;

public class BootLoader : MonoBehaviour
{
    [SerializeField] private SceneDataEventHandle _loadSceneHandle;
    [SerializeField] private SceneData _persistentScene;
    [SerializeField] private SceneData _startingScene;

    private void Start()
    {
        StartCoroutine(Bootloader());
    }

    private IEnumerator Bootloader()
    {
        Logg.Log($"Starting Bootloader", "Bootloader", gameObject);

        yield return SceneManager.LoadSceneAsync(_persistentScene.SceneIndex, LoadSceneMode.Additive);
        _loadSceneHandle.Invoke(_startingScene, gameObject);
        Logg.Log($"Persistent Engine Scene Loaded and Load Starting Scene Event Dispatched", "Bootloader", gameObject);

        Logg.Log($"Starting to Unload Bootloader", "Bootloader", gameObject);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
