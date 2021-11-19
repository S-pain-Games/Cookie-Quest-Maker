using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShowDescriptionButton : MonoBehaviour
{
    [SerializeField] private GameObject _descriptionUI;
    private Button b;

    private void Awake()
    {
        b = GetComponent<Button>();
    }

    private void OnEnable()
    {
        b.onClick.AddListener(OpenDescriptionWindow);
    }

    private void OnDisable()
    {
        b.onClick.RemoveListener(OpenDescriptionWindow);
    }

    private void OpenDescriptionWindow()
    {
        _descriptionUI.SetActive(true);
    }
}
