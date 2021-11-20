using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MusicEmitter : MonoBehaviour
{
    [SerializeField] private AudioSource _source1;
    [SerializeField] private AudioSource _source2;

    private List<AudioSource> m_Sources = new List<AudioSource>();

    public void Init()
    {
        _source1.loop = true;
        _source2.loop = true;
        m_Sources.Add(_source1);
        m_Sources.Add(_source2);
    }

    public void PlayMusic(AudioClip clip)
    {
        m_Sources[0].DOFade(0.0f, 10.0f);
        m_Sources[1].volume = 0.0f;
        m_Sources[1].DOFade(1.0f, 10.0f);
        m_Sources[1].clip = clip;

        var t = m_Sources[1];
        m_Sources.Remove(t);
        m_Sources.Insert(0, t);
        m_Sources[0].Play();
    }
}
