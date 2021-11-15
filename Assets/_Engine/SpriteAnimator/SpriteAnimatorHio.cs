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

            if (AngleInRange(angle, 0, 20))
                m_AnimSys.SetAnimation(WalkingE);
            else if (AngleInRange(angle, 20, 160))
                m_AnimSys.SetAnimation(WalkingN);
            else if (AngleInRange(angle, 160, 200))
                m_AnimSys.SetAnimation(WalkingW);
            else if (AngleInRange(angle, 200, 260))
                m_AnimSys.SetAnimation(WalkingSW);
            else if (AngleInRange(angle, 260, 280))
                m_AnimSys.SetAnimation(WalkingS);
            else if (AngleInRange(angle, 280, 340))
                m_AnimSys.SetAnimation(WalkingSE);
            else if (angle <= 360)
                m_AnimSys.SetAnimation(WalkingE);
        }
        else
        {
            if (m_FacingAngle <= 20)
                m_AnimSys.SetAnimation(IdleE);
            else if (m_FacingAngle <= 160)
                m_AnimSys.SetAnimation(IdleN);
            else if (m_FacingAngle <= 200)
                m_AnimSys.SetAnimation(IdleW);
            else if (m_FacingAngle <= 260)
                m_AnimSys.SetAnimation(IdleSW);
            else if (m_FacingAngle <= 280)
                m_AnimSys.SetAnimation(IdleE);
            else if (m_FacingAngle <= 340)
                m_AnimSys.SetAnimation(IdleSE);
            else if (m_FacingAngle <= 360)
                m_AnimSys.SetAnimation(IdleE);
        }


        m_AnimSys.Update();
    }

    private bool AngleInRange(float angle, float a, float b)
    {
        if (angle <= b && angle >= a)
            return true;
        else
            return false;
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

        // We should definitely use a dictionary for this but oh well
        public void SetAnimation(AnimationID id)
        {
            switch (id)
            {
                case IdleN:
                    m_CurrentAnimation = m_Anim.m_IdleN;
                    break;
                case IdleS:
                    m_CurrentAnimation = m_Anim.m_IdleS;
                    break;
                case IdleE:
                    m_CurrentAnimation = m_Anim.m_IdleE;
                    break;
                case IdleW:
                    m_CurrentAnimation = m_Anim.m_IdleW;
                    break;
                case IdleSE:
                    m_CurrentAnimation = m_Anim.m_IdleSE;
                    break;
                case IdleSW:
                    m_CurrentAnimation = m_Anim.m_IdleSW;
                    break;
                case WalkingN:
                    m_CurrentAnimation = m_Anim.m_WalkingN;
                    break;
                case WalkingS:
                    m_CurrentAnimation = m_Anim.m_WalkingS;
                    break;
                case WalkingE:
                    m_CurrentAnimation = m_Anim.m_WalkingE;
                    break;
                case WalkingW:
                    m_CurrentAnimation = m_Anim.m_WalkingW;
                    break;
                case WalkingSE:
                    m_CurrentAnimation = m_Anim.m_WalkingSE;
                    break;
                case WalkingSW:
                    m_CurrentAnimation = m_Anim.m_WalkingSW;
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
            IdleSE,
            IdleSW,

            WalkingN,
            WalkingS,
            WalkingSE,
            WalkingE,
            WalkingW,
            WalkingSW
        }

        [System.Serializable]
        private class CharacterAnimations
        {
            public AnimationFrames m_IdleN;
            public AnimationFrames m_IdleS;
            public AnimationFrames m_IdleE;
            public AnimationFrames m_IdleW;
            public AnimationFrames m_IdleSE;
            public AnimationFrames m_IdleSW;

            public AnimationFrames m_WalkingN;
            public AnimationFrames m_WalkingS;
            public AnimationFrames m_WalkingE;
            public AnimationFrames m_WalkingW;
            public AnimationFrames m_WalkingSE;
            public AnimationFrames m_WalkingSW;
        }
    }
}
