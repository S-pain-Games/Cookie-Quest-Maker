using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlaySoundOnButton : MonoBehaviour
{
    [SerializeField] private string _clipIDName;
    private Event<ID> _playSoundCmd;
    private Button _button;


    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _playSoundCmd = evtSys.GetCommandByName<Event<ID>>("audio_sys", "play_sound");
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlaySound);
    }

    public void PlaySound()
    {
        _playSoundCmd.Invoke(new ID(_clipIDName));
    }
}
