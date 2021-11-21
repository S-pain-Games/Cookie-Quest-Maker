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

            m_AudioData.m_MusicEmitter.Init();
            PlayMusic(new ID("main_menu_theme"));
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            sysID = new ID("audio_sys");
            commands = new EventSys();
            callbacks = new EventSys();

            commands.AddEvent<ID>(new ID("play_sound")).OnInvoked += PlaySound;
            commands.AddEvent<ID>(new ID("play_music")).OnInvoked += PlayMusic;
            commands.AddEvent<float>(new ID("set_music_volume")).OnInvoked += (args) => { m_AudioData.m_MusicSoundVolume = args; UpdateVolume(); };
            commands.AddEvent<float>(new ID("set_effects_volume")).OnInvoked += (args) => { m_AudioData.m_SoundEffectsVolume = args; };
        }

        public void PlayMusic(ID musicID)
        {
            m_AudioData.m_MusicEmitter.PlayMusic(m_AudioClips.m_Music[musicID]);
        }

        public void PlaySound(ID soundID)
        {
            var source = GameObject.Instantiate(m_AudioData.m_SoundEmitterPrefab).GetComponent<SoundEmitterBehaviour>();
            source.PlaySound(m_AudioClips.m_SoundEffects[soundID], m_AudioData.m_SoundEffectsVolume);
        }

        public void UpdateVolume()
        {
            m_AudioData.m_MusicEmitter.SetVolume(m_AudioData.m_MusicSoundVolume);
        }

    }
}