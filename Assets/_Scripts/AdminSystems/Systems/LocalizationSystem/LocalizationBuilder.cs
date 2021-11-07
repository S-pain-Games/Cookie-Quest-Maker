﻿using System;
using System.Collections.Generic;
using UnityEngine;
using static Language;

public class LocalizationBuilder : MonoBehaviour
{
    public List<LocalizedData> m_LocalizedText = new List<LocalizedData>();

    private ID _currentID;
    private LocalizedData _localData;


    [MethodButton]
    public void LoadDataFromCode()
    {
        m_LocalizedText.Clear();
        _localData = null;

        //Main menu
        SelectID("mainmenu_title");
        SetLocal(Spanish, "Menú");
        SetLocal(English, "Menu");

        SelectID("mainmenu_button_play");
        SetLocal(Spanish, "Jugar");
        SetLocal(English, "Play");

        SelectID("mainmenu_button_options");
        SetLocal(Spanish, "Opciones");
        SetLocal(English, "Options");

        SelectID("mainmenu_button_credits");
        SetLocal(Spanish, "Créditos");
        SetLocal(English, "Credits");

        //Options menu
        SelectID("optionsmenu_title");
        SetLocal(Spanish, "Opciones");
        SetLocal(English, "Options");

        SelectID("optionsmenu_text_volume_music");
        SetLocal(Spanish, "Volumen de música");
        SetLocal(English, "Music volume");

        SelectID("optionsmenu_text_volume_effects");
        SetLocal(Spanish, "Volumen de efectos");
        SetLocal(English, "Effects volume");

        SelectID("optionsmenu_text_language");
        SetLocal(Spanish, "IDIOMA");
        SetLocal(English, "LANGUAGE");

        //Credits menu
        SelectID("creditsmenu_title");
        SetLocal(Spanish, "Créditos");
        SetLocal(English, "Credits");

        //Dialogue box
        SelectID("dialoguebox_text");
        SetLocal(Spanish, "Hola");
        SetLocal(English, "Hi");

        SelectID("dialoguebox_button_next");
        SetLocal(Spanish, "Siguiente");
        SetLocal(English, "Next");

        FinishBuilding();
    }


    private void SelectID(string idName)
    {
        if (_localData != null)
            m_LocalizedText.Add(_localData);

        _currentID = new ID(idName);
        _localData = new LocalizedData();
        _localData.m_ID = _currentID;
    }

    private void SetLocal(Language l, string line)
    {
        _localData.m_Lines.Add(new LocalizedLine { m_Lang = l, m_Line = line });
    }

    private void FinishBuilding()
    {
        m_LocalizedText.Add(_localData);
    }

    [Serializable]
    public class LocalizedData
    {
        public ID m_ID;
        public List<LocalizedLine> m_Lines = new List<LocalizedLine>();
    }


    [Serializable]
    public class LocalizedLine
    {
        public Language m_Lang;
        public string m_Line;
    }
}
