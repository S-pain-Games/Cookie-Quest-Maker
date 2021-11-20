using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingNPC : MonoBehaviour, IInteractableEntity
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;
    [SerializeField] string _characterName;

    private ID _charId;

    private void Start()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");

        _charId = new ID(_characterName);
    }


    public void OnInteract()
    {
       
    }
}
