using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpriteAnimatorNpc.AnimationsSystem.AnimationID;

public class SpriteAnimatorNpc : MonoBehaviour
{
    [SerializeField] private AnimationsSystem m_AnimSys;

    public void Start()
    {
        m_AnimSys.Init();

        m_AnimSys.SetAnimation(IdleSE);
    }

    public void Update()
    {
        m_AnimSys.Update();
    }


    [System.Serializable]
    public class AnimationsSystem
    {
        [SerializeField] private SpriteRenderer _renderer;

        [SerializeField] private CharacterAnimations m_Anim;
        private AnimationFrames m_CurrentAnimation;

        [SerializeField]
        private float m_FrameRate = 5.0f;
        private float m_SecPerFrame = 0.0f;
        private float m_SecSinceLastUpdate = 0.0f;


        public void Init()
        {
            m_CurrentAnimation = m_Anim.m_IdleSE;
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

        // We should definitely use a dictionary for this but oh well
        public void SetAnimation(AnimationID id)
        {
            switch (id)
            {
                case IdleSE:
                    m_CurrentAnimation = m_Anim.m_IdleSE;
                    break;
                default:
                    break;
            }
        }

        public enum AnimationID
        {
            IdleSE
        }

        [System.Serializable]
        private class CharacterAnimations
        {
            public AnimationFrames m_IdleSE;
        }
    }
}
