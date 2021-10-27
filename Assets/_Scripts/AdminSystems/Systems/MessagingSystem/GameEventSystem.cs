using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem
{
    public struct ShowDialogueEvtArgs
    {
        public List<string> dialogue;
        public string charName;
        public Action callback;

        public ShowDialogueEvtArgs(List<string> dialogue, string charName, Action callback)
        {
            this.dialogue = dialogue;
            this.charName = charName;
            this.callback = callback;
        }
    }

    // COMMANDS TO SYSTEMS
    public EventSys StorySystemMessaging = new EventSys();
    public EventSys DialogueSystemMessaging = new EventSys();
    public EventSys GameStateSystemMessaging = new EventSys();
    public EventSys DaySystemCommands = new EventSys();

    // CALLBACKS FROM SYSTEMS
    public EventSys StoryCallbacks = new EventSys();
    public EventSys DayCallbacks = new EventSys();

    public void Initialize()
    {
        var ids = Admin.g_Instance.ID.events;
        // CALLBACKS
        // The int is the storyId
        StoryCallbacks.AddEvent<int>(ids.on_story_started);
        StoryCallbacks.AddEvent<int>(ids.on_story_completed);
        StoryCallbacks.AddEvent<int>(ids.on_story_finalized);

        // INITIALIZE
        StorySystemMessaging.AddEvent<int>(ids.start_story);
        StorySystemMessaging.AddEvent<int>(ids.finalize_story);
        DialogueSystemMessaging.AddEvent<ShowDialogueEvtArgs>(ids.show_dialogue);
        GameStateSystemMessaging.AddEvent<GameStateSystem.State>("change_state".GetHashCode());
    }

    public void LinkCommandEvents(Admin admin)
    {
        var ids = Admin.g_Instance.ID.events;
        {
            StorySystemMessaging.GetEvent(ids.start_story, out Event<int> evt);
            evt.OnInvoked += admin.storySystem.StartStory;
        }
        {
            StorySystemMessaging.GetEvent(ids.finalize_story, out Event<int> evt);
            evt.OnInvoked += admin.storySystem.FinalizeStory;
        }
        {
            DialogueSystemMessaging.GetEvent(ids.show_dialogue, out Event<ShowDialogueEvtArgs> evt);
            evt.OnInvoked += (args) => admin.dialogueSystem.ShowDialogue(args.dialogue, args.charName, args.callback);
        }
        {
            GameStateSystemMessaging.GetEvent("change_state".GetHashCode(), out Event<GameStateSystem.State> evt);
            evt.OnInvoked += (args) => admin.gameStateSystem.SetState(args);
        }
    }
}