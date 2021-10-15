using UnityEngine;
using System.Collections;


public class UiView : MonoBehaviour
{
    private GameObject _view;

    private void Awake()
    {
        // Assumes we only have 1 children
        _view = transform.GetChild(0).gameObject;
    }

    public void Enable()
    {
        _view.SetActive(true);
    }

    public void Disable()
    {
        _view.SetActive(false);
    }
}
