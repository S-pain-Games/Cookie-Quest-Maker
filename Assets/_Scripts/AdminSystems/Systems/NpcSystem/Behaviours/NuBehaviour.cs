using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class NuBehaviour : MonoBehaviour , IInteractableEntity
{
    public List<string> m_Dialogue = new List<string>();
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
    }

    public void OnInteract()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
            m_Dialogue,
            "Nu",
            null));
    }
}
