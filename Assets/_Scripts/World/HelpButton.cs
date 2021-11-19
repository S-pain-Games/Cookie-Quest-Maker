using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HelpButton : MonoBehaviour
{
    [SerializeField] private GameObject _helpUI;
    private Button b;

    private void Awake()
    {
        b = GetComponent<Button>();
    }

    private void OnEnable()
    {
        b.onClick.AddListener(OpenHelp);
    }

    private void OnDisable()
    {
        b.onClick.RemoveListener(OpenHelp);
    }

    private void OpenHelp()
    {
        _helpUI.SetActive(true);
    }
}
