using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This component should be on interactable gameobjects.
/// </summary>
public class Interactable : MonoBehaviour
{
    public event Action OnInteract;

    #region UNITY_EDITOR
#if UNITY_EDITOR
    [SerializeField] private bool showLog;
#endif
    #endregion

    public void Interact(GameObject obj)
    {
        #region UNITY_EDITOR
#if UNITY_EDITOR
        if (showLog)
            Debug.Log(obj.name + " is interacting with: " + gameObject.name, gameObject);
#endif
        #endregion

        OnInteract?.Invoke();
    }
}
