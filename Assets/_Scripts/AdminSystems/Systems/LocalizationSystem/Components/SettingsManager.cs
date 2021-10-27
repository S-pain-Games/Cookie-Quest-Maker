using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private Button languageEN;

    [SerializeField]
    private Button languageES;

    private LocalizationSystem ls;

    void Awake()
    {
        ls = Admin.g_Instance.localizationSystem;
    }

    private void OnEnable()
    {
        languageEN.onClick.AddListener(SetEnglish);
        languageES.onClick.AddListener(SetSpanish);
    }

    private void OnDisable()
    {
        languageEN.onClick.RemoveListener(SetEnglish);
        languageES.onClick.RemoveListener(SetSpanish);
    }

    public void SetEnglish()
    {
        ls.ChangeLanguage(Language.English);
    }

    public void SetSpanish()
    {
        ls.ChangeLanguage(Language.Spanish);
    }
}
