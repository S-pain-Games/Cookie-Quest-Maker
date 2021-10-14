using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpAnimatorTester : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private List<AnimationFrames> _animations = new List<AnimationFrames>();
    [SerializeField]
    private int m_CurrentAnimIndex = 0;

    [SerializeField]
    private float m_FrameRate = 5.0f;
    private float m_SecPerFrame = 0.0f;
    private float m_SecSinceLastUpdate = 0.0f;

    private void Update()
    {
        m_SecPerFrame = 1.0f / m_FrameRate;
        m_SecSinceLastUpdate += Time.deltaTime;
        if (m_SecSinceLastUpdate >= m_SecPerFrame)
        {
            _renderer.sprite = _animations[m_CurrentAnimIndex].GetNextSprite();
            m_SecSinceLastUpdate = 0.0f;
        }
    }
}
