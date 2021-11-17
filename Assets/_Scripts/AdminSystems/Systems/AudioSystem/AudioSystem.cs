using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Systems
{
    public class AudioSystem : ISystemEvents
    {
        private Singleton_AudioDataComponent m_AudioData;
        private Singleton_AudioClipsDataComponent m_AudioClips;

        public void Initialize(Singleton_AudioDataComponent audioData, Singleton_AudioClipsDataComponent clipsData)
        {
            m_AudioData = audioData;
            m_AudioClips = clipsData;
        }

        public void PlayMusic(ID musicID)
        {

        }

        public void PlaySound(ID soundID)
        {
            var source = GameObject.Instantiate(m_AudioData.m_SoundEmitterPrefab).GetComponent<SoundEmitterBehaviour>();
            source.PlaySound(m_AudioClips.m_SoundEffects[soundID]);
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            sysID = new ID("audio_sys");
            commands = new EventSys();
            callbacks = new EventSys();

            commands.AddEvent<ID>(new ID("play_sound")).OnInvoked += PlaySound;
            commands.AddEvent<ID>(new ID("play_music")).OnInvoked += PlayMusic;
        }
    }
}