using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestingBehaviour : MonoBehaviour
{
    [SerializeField] private string _clipIDName;
    private Event<ID> _playSoundCmd;


    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _playSoundCmd = evtSys.GetCommandByName<Event<ID>>("audio_sys", "play_sound");
    }

    [MethodButton]
    public void PlaySound()
    {
        _playSoundCmd.Invoke(new ID(_clipIDName));
    }
}
