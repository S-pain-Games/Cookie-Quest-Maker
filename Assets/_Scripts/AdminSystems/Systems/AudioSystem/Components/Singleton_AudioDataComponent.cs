using UnityEngine;

namespace CQM.Components
{
    public class Singleton_AudioDataComponent : MonoBehaviour
    {
        public float m_MusicSoundVolume = 1.0f;
        public float m_SoundEffectsVolume = 1.0f;

        public GameObject m_SoundEmitterPrefab;
        public MusicEmitter m_MusicEmitter;
    }
}