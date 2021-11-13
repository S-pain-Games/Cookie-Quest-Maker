using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static SpriteAnimatorHio.AnimationsSystem.AnimationID;

public class SpriteAnimatorHio : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AnimationsSystem m_AnimSys;

    private float m_FacingAngle;

    public void Start()
    {
        m_AnimSys.Init();
    }

    public void Update()
    {
        if (_agent.velocity.magnitude > 0.001f)
        {
            float angle = Vector3.SignedAngle(Vector3.right, _agent.velocity, Vector3.forward);
            if (angle < 0)
                angle = 360 + angle;
            m_FacingAngle = angle;

            if (angle <= 45)
                m_AnimSys.SetAnimation(WalkingE);
            else if (angle <= 135)
                m_AnimSys.SetAnimation(WalkingN);
            else if (angle <= 225)
                m_AnimSys.SetAnimation(WalkingW);
            else if (angle <= 315)
                m_AnimSys.SetAnimation(WalkingS);
            if (angle <= 45)
                m_AnimSys.SetAnimation(WalkingE);
        }
        else
        {
            if (m_FacingAngle <= 45)
                m_AnimSys.SetAnimation(IdleE);
            else if (m_FacingAngle <= 135)
                m_AnimSys.SetAnimation(IdleN);
            else if (m_FacingAngle <= 225)
                m_AnimSys.SetAnimation(IdleW);
            else if (m_FacingAngle <= 315)
                m_AnimSys.SetAnimation(IdleS);
            else if (m_FacingAngle <= 360)
                m_AnimSys.SetAnimation(IdleE);
        }


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
            m_CurrentAnimation = m_Anim.m_IdleE;
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

        public void SetAnimation(AnimationID id)
        {
            switch (id)
            {
                case AnimationID.IdleN:
                    m_CurrentAnimation = m_Anim.m_IdleN;
                    break;
                case AnimationID.IdleS:
                    m_CurrentAnimation = m_Anim.m_IdleS;
                    break;
                case AnimationID.IdleE:
                    m_CurrentAnimation = m_Anim.m_IdleE;
                    break;
                case AnimationID.IdleW:
                    m_CurrentAnimation = m_Anim.m_IdleW;
                    break;
                case AnimationID.WalkingN:
                    m_CurrentAnimation = m_Anim.m_WalkingN;
                    break;
                case AnimationID.WalkingS:
                    m_CurrentAnimation = m_Anim.m_WalkingS;
                    break;
                case AnimationID.WalkingE:
                    m_CurrentAnimation = m_Anim.m_WalkingE;
                    break;
                case AnimationID.WalkingW:
                    m_CurrentAnimation = m_Anim.m_WalkingW;
                    break;
                default:
                    break;
            }
        }

        public enum AnimationID
        {
            IdleN,
            IdleS,
            IdleE,
            IdleW,

            WalkingN,
            WalkingS,
            WalkingE,
            WalkingW,
        }

        [System.Serializable]
        private class CharacterAnimations
        {
            public AnimationFrames m_IdleN;
            public AnimationFrames m_IdleS;
            public AnimationFrames m_IdleE;
            public AnimationFrames m_IdleW;

            public AnimationFrames m_WalkingN;
            public AnimationFrames m_WalkingS;
            public AnimationFrames m_WalkingE;
            public AnimationFrames m_WalkingW;
        }
    }
}
