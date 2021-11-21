using System.Collections.Generic;
using UnityEngine;

namespace CQM.Components
{
    [CreateAssetMenu()]
    public class Singleton_AudioClipsDataComponent : ScriptableObject
    {
        [SerializeField] private List<AudioClipInspector> m_AudioClips = new List<AudioClipInspector>();
        [SerializeField] private List<AudioClipInspector> m_MusicClips = new List<AudioClipInspector>();

        public Dictionary<ID, AudioClip> m_SoundEffects = new Dictionary<ID, AudioClip>();
        public Dictionary<ID, AudioClip> m_Music = new Dictionary<ID, AudioClip>();


        private void OnEnable()
        {
            m_SoundEffects.Clear();
            m_Music.Clear();

            for (int i = 0; i < m_AudioClips.Count; i++)
            {
                var c = m_AudioClips[i];
                m_SoundEffects.Add(new ID(c.m_IDName), c.m_Clip);
            }
            for (int i = 0; i < m_MusicClips.Count; i++)
            {
                var c = m_MusicClips[i];
                m_Music.Add(new ID(c.m_IDName), c.m_Clip);
            }
        }

        [System.Serializable]
        private class AudioClipInspector
        {
            public string m_IDName;
            public AudioClip m_Clip;
        }
    }
}