using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvithBehaviour : MonoBehaviour, IInteractableEntity
{
    public List<string> m_Dialogue = new List<string>();
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;

    // HACK
    [SerializeField]
    private UnityEvent onFinishInteract;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");
    }

    public void OnInteract()
    {
        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(
            m_Dialogue,
            new ID("evith"),
            () => { DialogueWithNpcFinishedCallback(); }));
    }

    private void DialogueWithNpcFinishedCallback()
    {
        onFinishInteract?.Invoke();
    }
}
