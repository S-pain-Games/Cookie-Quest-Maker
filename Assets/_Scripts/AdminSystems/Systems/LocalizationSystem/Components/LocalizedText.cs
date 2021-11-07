using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    public string textNameID;
    [SerializeField]
    private TextMeshProUGUI textMesh;

    private LocalizationSystem locSystem;


    private void Awake()
    {
        locSystem = Admin.Global.Systems.m_LocalizationSystem;
    }

    private void OnEnable()
    {
        locSystem.OnLanguageChanged += UpdateTextLine;
        UpdateTextLine();
    }

    private void OnDisable()
    {
        locSystem.OnLanguageChanged -= UpdateTextLine;
    }

    private void UpdateTextLine()
    {
        textMesh.text = locSystem.GetLine(new ID(textNameID));
    }
}
