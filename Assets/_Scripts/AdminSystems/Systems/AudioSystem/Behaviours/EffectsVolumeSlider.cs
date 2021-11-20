using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public Event<float> _changeEffectsVolCmd;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;

        _changeEffectsVolCmd = evtSys.GetCommandByName<Event<float>>("audio_sys", "set_effects_volume");
        _slider.onValueChanged.AddListener(UpdateEffectsVolume); // Not unsubscribing
    }

    private void OnEnable()
    {
        _slider.value = Admin.Global.Components.m_AudioDataComponent.m_SoundEffectsVolume;
    }

    public void UpdateEffectsVolume(float newValue)
    {
        _changeEffectsVolCmd.Invoke(newValue);
    }
}
