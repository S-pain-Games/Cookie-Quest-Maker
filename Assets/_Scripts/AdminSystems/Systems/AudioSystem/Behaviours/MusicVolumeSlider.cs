using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public Event<float> _changeMusicVolCmd;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;

        _changeMusicVolCmd = evtSys.GetCommandByName<Event<float>>("audio_sys", "set_music_volume");
        _slider.onValueChanged.AddListener(UpdateMusicVolume); // Not unsubscribing
    }

    private void OnEnable()
    {
        _slider.value = Admin.Global.Components.m_AudioDataComponent.m_MusicSoundVolume;
    }

    public void UpdateMusicVolume(float newValue)
    {
        _changeMusicVolCmd.Invoke(newValue);
    }
}
