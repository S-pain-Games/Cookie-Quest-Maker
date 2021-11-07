using System;
using System.Collections;
using System.Collections.Generic;

public class LocalizationSystem
{
    // Current Language
    private Language m_Language = Language.Spanish;

    // Used to update all the loaded text on language change
    public event Action OnLanguageChanged;

    private Singleton_LocalizationComponent _locComponent;


    public void Initialize(Singleton_LocalizationComponent locComponent)
    {
        _locComponent = locComponent;
    }

    public void ChangeLanguage(Language newLanguage)
    {
        m_Language = newLanguage;
        OnLanguageChanged?.Invoke();
    }

    public string GetLine(ID lineID)
    {
        switch (m_Language)
        {
            case Language.Spanish:
                if (_locComponent.m_Spanish.TryGetValue(lineID, out string lineSpanish))
                    return lineSpanish;
                else
                    return "NOT LOCALIZED";
            case Language.English:
                if (_locComponent.m_English.TryGetValue(lineID, out string lineEnglish))
                    return lineEnglish;
                else
                    return "NOT LOCALIZED";
            default:
                break;
        }

        return "KEY NOT FOUND";
        throw new ArgumentException("key not found in localization system");
    }
}


public class Singleton_LocalizationComponent
{
    public Dictionary<ID, string> m_Spanish = new Dictionary<ID, string>();
    public Dictionary<ID, string> m_English = new Dictionary<ID, string>();
}

public enum Language
{
    Undefined,
    Spanish,
    English
}