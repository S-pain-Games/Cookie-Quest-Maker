using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    // Current Language
    private Language m_Language = Language.Spanish;

    // Used to update all the loaded text on language change
    public event Action OnLanguageChanged;

    // All the localized strings are stored in its own dictionary
    // When you want to add a piece of text to the game create and ID for it.
    // If you want to retrieve the text ask the localization system
    // for the line for the given id
    private Dictionary<int, string> m_Spanish = new Dictionary<int, string>();
    private Dictionary<int, string> m_English = new Dictionary<int, string>();

    public void ChangeLanguage(Language newLanguage)
    {
        m_Language = newLanguage;
        OnLanguageChanged?.Invoke();
    }

    public string GetLine(int lineKey)
    {
        switch (m_Language)
        {
            case Language.Spanish:
                if (m_Spanish.TryGetValue(lineKey, out string lineSpanish))
                    return lineSpanish;
                else
                    return "NOT LOCALIZED";
            case Language.English:
                if (m_Spanish.TryGetValue(lineKey, out string lineEnglish))
                    return lineEnglish;
                else
                    return "NOT LOCALIZED";
            default:
                break;
        }

        return "KEY NOT FOUND";
        throw new System.ArgumentException("key not found in localization system");
    }

    public void LoadData()
    {
        m_Spanish.Add("mainmenu_button_play".GetHashCode(), "Jugar");
        m_English.Add("mainmenu_button_play".GetHashCode(), "Play");
    }

    public enum Language
    {
        Spanish,
        English
    }
}
