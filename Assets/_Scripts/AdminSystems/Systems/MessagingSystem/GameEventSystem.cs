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

    public void Initialize(Admin admin)
    {
        var ids = admin.ID.events;
        StorySystemEvents(ids, admin.storySystem);
        DialogueSystemEvents(ids, admin.dialogueSystem);
        GameStateSystemEvents(ids,admin.gameStateSystem);
        DaySystemEvents(ids, admin.daySystem);
    }

    private void GameStateSystemEvents(IDEvents ids, GameStateSystem gameStateSys)
    {
        // Commands
        var evt = GameStateSystemMessaging.AddEvent<GameStateSystem.State>("change_state".GetHashCode()); // LEFT ONLY IN CASE
        evt.OnInvoked += gameStateSys.SetState;
        evt = GameStateSystemMessaging.AddEvent<GameStateSystem.State>(ids.set_game_state);
        evt.OnInvoked += gameStateSys.SetState;
    }

    private void DialogueSystemEvents(IDEvents ids, DialogueSystem dialogueSys)
    {
        // Commands
        var evt = DialogueSystemMessaging.AddEvent<ShowDialogueEvtArgs>(ids.show_dialogue);
        evt.OnInvoked += (args) => dialogueSys.ShowDialogue(args.dialogue, args.charName, args.callback);
    }

    private void StorySystemEvents(IDEvents ids, StorySystem storySys)
    {
        // Callbacks
        StoryCallbacks.AddEvent<int>(ids.on_story_started);
        StoryCallbacks.AddEvent<int>(ids.on_story_completed);
        StoryCallbacks.AddEvent<int>(ids.on_story_finalized);

        // Commands
        var evt = StorySystemMessaging.AddEvent<int>(ids.start_story);
        evt.OnInvoked += storySys.StartStory;
        evt = StorySystemMessaging.AddEvent<int>(ids.finalize_story);
        evt.OnInvoked += storySys.FinalizeStory;
    }

    private void DaySystemEvents(IDEvents ids, DaySystem daySystem)
    {
        // Callbacks
        DayCallbacks.AddEvent(ids.on_day_started);
        DayCallbacks.AddEvent(ids.on_day_ended);
        DayCallbacks.AddEvent(ids.on_daily_stories_completed);

        // Commands
        var evt = DaySystemCommands.AddEvent(ids.start_new_day);
        evt.OnInvoked += daySystem.StartNewDay;
    }
}