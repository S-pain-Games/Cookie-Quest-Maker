using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


public class EditorStart : MonoBehaviour
{
#if UNITY_EDITOR
    // Used to load the systems scene in editor to easily start the game in any scene
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ColdStart()
    {
        // If the current scene is not the boot scene
        if (!SceneManager.GetSceneByName("Bootloader").IsValid())
        {
            Logg.Log("Setting up hot game start", "Editor");
            SceneManager.LoadScene("Persistent_Systems", LoadSceneMode.Additive);
        }
    }
#endif
}
