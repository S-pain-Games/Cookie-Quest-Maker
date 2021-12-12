using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterComponent
{
    public ID m_ID;
    public string m_FullName;
    public string m_ShortName;
    public string m_Description;

    public bool hasAudio = false;
    public ID m_AudioID = new ID("voice_mayor");

    // This should be in other component but wachugonadu no time
    public Sprite m_NewspaperSprite;
    public GameObject m_CharacterWorldPrefab;
}
