using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu()]
public class AnimationFrames : ScriptableObject
{
    public List<Sprite> Sprites => m_Sprites;

    [SerializeField]
    private List<Sprite> m_Sprites = new List<Sprite>();

    private int m_NextSpriteIndex = 0;

    public Sprite GetNextSprite()
    {
        if (m_NextSpriteIndex >= m_Sprites.Count)
            m_NextSpriteIndex = 0;

        Sprite sp = m_Sprites[m_NextSpriteIndex];
        m_NextSpriteIndex++;

        return sp;
    }
}
