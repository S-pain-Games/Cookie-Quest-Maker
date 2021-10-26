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

    // CALLBACKS FROM SYSTEMS
    public EventSys StoryCallbacks = new EventSys();
    public EventSys DayCallbacks = new EventSys();

    public void Initialize()
    {
        var ids = Admin.g_Instance.ID.events;
        StoryCallbacks.AddEvent<int>(ids.on_story_completed);
        StoryCallbacks.AddEvent<int>(ids.on_day_ended);

        StorySystemMessaging.AddEvent<int>(ids.start_story);
        DialogueSystemMessaging.AddEvent<ShowDialogueEvtArgs>(ids.show_dialogue);
    }

    public void LinkCommandEvents(Admin admin)
    {
        var ids = Admin.g_Instance.ID.events;
        StorySystemMessaging.GetEvent(ids.start_story, out Event<int> startStoryEvt);
        startStoryEvt.OnInvoked += admin.storySystem.StartStory;

        DialogueSystemMessaging.GetEvent(ids.show_dialogue, out Event<ShowDialogueEvtArgs> showDialogueEvt);
        showDialogueEvt.OnInvoked += (args) => admin.dialogueSystem.ShowDialogue(args.dialogue, args.charName, args.callback);
    }
}