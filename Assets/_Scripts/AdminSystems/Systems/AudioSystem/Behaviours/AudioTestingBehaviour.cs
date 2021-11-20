using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestingBehaviour : MonoBehaviour
{
    [SerializeField] private string _clipIDName;
    private Event<ID> _playSoundCmd;

    [SerializeField] private string _musicIDName;
    private Event<ID> _playMusicCmd;


    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _playSoundCmd = evtSys.GetCommandByName<Event<ID>>("audio_sys", "play_sound");
        _playMusicCmd = evtSys.GetCommandByName<Event<ID>>("audio_sys", "play_music");
    }

    [MethodButton]
    public void PlaySound()
    {
        _playSoundCmd.Invoke(new ID(_clipIDName));
    }

    [MethodButton]
    public void PlayMusic()
    {
        _playMusicCmd.Invoke(new ID(_musicIDName));
    }
}
