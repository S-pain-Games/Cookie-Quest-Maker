using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvithBehaviour : MonoBehaviour, IInteractableEntity
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    // HACK
    // HACK
    // HACK
    [SerializeField]
    private UnityEvent onFinishInteract;

    private void Awake()
    {
        var evtSys = Admin.g_Instance.gameEventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
    }

    public void OnInteract()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
           new List<string>() { "Well well if it isn't the baker here", "Would you like too see my evil cookie recipes" },
            "Evith",
            () => { DialogueWithNpcFinishedCallback(); }));
    }

    private void DialogueWithNpcFinishedCallback()
    {
        onFinishInteract?.Invoke();
    }
}
