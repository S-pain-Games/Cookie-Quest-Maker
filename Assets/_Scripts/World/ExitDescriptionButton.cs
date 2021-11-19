using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitDescriptionButton : MonoBehaviour
{
    [SerializeField] private GameObject _descriptionUI;
    private Button b;

    private void Awake()
    {
        b = GetComponent<Button>();
    }

    private void OnEnable()
    {
        b.onClick.AddListener(CloseDescriptionMenu);
    }

    private void OnDisable()
    {
        b.onClick.RemoveListener(CloseDescriptionMenu);
    }

    private void CloseDescriptionMenu()
    {
        _descriptionUI.SetActive(false);
    }
}