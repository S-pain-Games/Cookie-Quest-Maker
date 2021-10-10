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
        yield return SceneManager.LoadSceneAsync(_persistentScene.SceneIndex, LoadSceneMode.Additive);
        _loadSceneHandle.Dispatch(_startingScene, gameObject);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
