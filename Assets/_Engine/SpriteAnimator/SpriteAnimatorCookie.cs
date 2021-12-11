using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpriteAnimatorCookie.CookieAnimationsSystem.AnimationID;

public class SpriteAnimatorCookie : MonoBehaviour
{
    [SerializeField] private CookieAnimationsSystem m_AnimSys;

    public void Start()
    {
        m_AnimSys.Init();

        m_AnimSys.SetAnimation();
    }

    public void Update()
    {
        m_AnimSys.Update();
    }


    [System.Serializable]
    public class CookieAnimationsSystem
    {
        [SerializeField] private SpriteRenderer _renderer;

        [SerializeField] private CookieAnimation m_Anim;
        private AnimationFrames m_CurrentAnimation;

        [SerializeField]
        private float m_FrameRate = 5.0f;
        private float m_SecPerFrame = 0.0f;
        private float m_SecSinceLastUpdate = 0.0f;


        public void Init()
        {
            m_CurrentAnimation = m_Anim.m_Idle;
        }

        public void Update()
        {
            m_SecPerFrame = 1.0f / m_FrameRate;
            m_SecSinceLastUpdate += Time.deltaTime;
            if (m_SecSinceLastUpdate >= m_SecPerFrame)
            {
                _renderer.sprite = m_CurrentAnimation.GetNextSprite();
                m_SecSinceLastUpdate = 0.0f;
            }
        }

        public void SetAnimation()
        {
            m_CurrentAnimation = m_Anim.m_Idle;
        }

        public enum AnimationID
        {
            Idle
        }

        [System.Serializable]
        private class CookieAnimation
        {
            public AnimationFrames m_Idle;
        }
    }
}
