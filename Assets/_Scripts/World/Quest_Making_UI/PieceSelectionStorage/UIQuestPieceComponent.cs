using System;
using UnityEngine;

[Serializable]
public class UIQuestPieceComponent
{
    public Sprite m_SimpleSprite;
    public Sprite m_CookiePieceSprite;
    public Sprite m_HDSprite;
    public Sprite m_ShopRecipeSprite;
    public string m_Name;
    public string m_Description;

    [HideInInspector] public ID m_ID;
}