using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvithBehaviour : MonoBehaviour, IInteractableEntity
{
    private GameEventSystem _eventSystem;
    private Event<GameEventSystem.ShowDialogueEvtArgs> showDialogueEvt;
    private Event<int> startStoryEvt;
    private Event<int> finalizeStoryEvt;

    // HACK
    // HACK
    // HACK
    [SerializeField]
    private UnityEvent onFinishInteract;

    private void Awake()
    {
        _eventSystem = Admin.g_Instance.gameEventSystem;

        var evtIds = Admin.g_Instance.ID.events;
        _eventSystem.DialogueSystemMessaging.GetEvent(evtIds.show_dialogue, out showDialogueEvt);
        _eventSystem.StorySystemMessaging.GetEvent(evtIds.start_story, out startStoryEvt);
        _eventSystem.StorySystemMessaging.GetEvent(evtIds.finalize_story, out finalizeStoryEvt);
    }

    public void OnInteract()
    {

        showDialogueEvt.Invoke(new GameEventSystem.ShowDialogueEvtArgs(
           new List<string>() { "Well well if it isn't the baker here" },
            "Evith",
            () => { DialogueWithNpcFinishedCallback(); }));
    }

    private void DialogueWithNpcFinishedCallback()
    {
        onFinishInteract?.Invoke();
    }
}
