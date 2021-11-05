using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortingSystem
{
    private SortedSpritesComponent _data;

    public void Update(float timeStep)
    {
    }
}

public class SortedSpritesComponent
{
    public List<Sprite> m_PlayerSprite;
    public List<SortedSprite> m_Sprites;
}
