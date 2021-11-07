using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem
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
                if (m_English.TryGetValue(lineKey, out string lineEnglish))
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
        var ids = Admin.Global.ID.localization;

        //Main menu
        m_Spanish.Add(ids.mainmenu_title, "Menú");
        m_English.Add(ids.mainmenu_title, "Menu");
        m_Spanish.Add(ids.mainmenu_button_play, "Jugar");
        m_English.Add(ids.mainmenu_button_play, "Play");
        m_Spanish.Add(ids.mainmenu_button_options, "Opciones");
        m_English.Add(ids.mainmenu_button_options, "Options");
        m_Spanish.Add(ids.mainmenu_button_credits, "Créditos");
        m_English.Add(ids.mainmenu_button_credits, "Credits");

        //Options menu
        m_Spanish.Add(ids.optionsmenu_title, "Opciones");
        m_English.Add(ids.optionsmenu_title, "Options");
        m_Spanish.Add(ids.optionsmenu_text_volume_music, "Volumen de música");
        m_English.Add(ids.optionsmenu_text_volume_music, "Music volume");
        m_Spanish.Add(ids.optionsmenu_text_volume_effects, "Volumen de efectos");
        m_English.Add(ids.optionsmenu_text_volume_effects, "Effects volume");
        m_Spanish.Add(ids.optionsmenu_text_language, "IDIOMA");
        m_English.Add(ids.optionsmenu_text_language, "LANGUAGE");

        //Credits menu
        m_Spanish.Add(ids.creditsmenu_title, "Créditos");
        m_English.Add(ids.creditsmenu_title, "Credits");

        //Dialogue box
        m_Spanish.Add(ids.dialoguebox_text, "Hola");
        m_English.Add(ids.dialoguebox_text, "Hi");
        m_Spanish.Add(ids.dialoguebox_button_next, "Siguiente");
        m_English.Add(ids.dialoguebox_button_next, "Next");


    }
}

public enum Language
{
    Spanish,
    English
}