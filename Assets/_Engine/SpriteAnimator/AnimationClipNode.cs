using UnityEngine;
using System.Collections;


[System.Serializable]
public class AnimationClipNode
{
    public AnimationFrames AnimationClip => m_AnimationFrames;
    [SerializeField]
    private AnimationFrames m_AnimationFrames;
}