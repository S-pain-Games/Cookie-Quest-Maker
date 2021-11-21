using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MusicEmitter : MonoBehaviour
{
    [SerializeField] private AudioSource _source1;
    [SerializeField] private AudioSource _source2;

    private List<AudioSource> m_Sources = new List<AudioSource>();
    private float m_Volume = 1.0f;

    public void Init()
    {
        _source1.loop = true;
        _source2.loop = true;
        m_Sources.Add(_source1);
        m_Sources.Add(_source2);
    }

    public void PlayMusic(AudioClip clip)
    {
        m_Sources[0].DOFade(0.0f, m_Volume * 1.0f);
        m_Sources[1].volume = 0.0f;
        m_Sources[1].DOFade(m_Volume, m_Volume * 1.0f);
        m_Sources[1].clip = clip;

        // Set new clip start position to previous clip end position
        m_Sources[1].time = Mathf.Min(m_Sources[0].time, m_Sources[1].clip.length - 0.1f);

        var t = m_Sources[1];
        m_Sources.Remove(t);
        m_Sources.Insert(0, t);
        m_Sources[0].Play();
    }

    public void SetVolume(float newValue)
    {
        m_Volume = newValue;

        m_Sources[1].volume = 0.0f;
        m_Sources[1].DOKill(); ;
        m_Sources[0].volume = m_Volume;
        m_Sources[0].DOKill();
    }
}
