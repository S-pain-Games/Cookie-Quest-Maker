using System;
using UnityEngine;

[Serializable]
public class UIQuestPieceComponent
{
    public Sprite m_Sprite;
    public Sprite m_QuestBuildingSprite;
    public string m_Name;
    public string m_Description;

    [HideInInspector] public ID m_ID;
}