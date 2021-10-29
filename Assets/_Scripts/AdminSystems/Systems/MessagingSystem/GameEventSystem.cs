using CQM.QuestMaking;
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
    public EventSys NpcSystemCommands = new EventSys();
    public EventSys CameraSystemCommands = new EventSys();
    public EventSys InventorySystemCommands = new EventSys();

    // CALLBACKS FROM SYSTEMS
    public EventSys StoryCallbacks = new EventSys();
    public EventSys DayCallbacks = new EventSys();
    public EventSys NpcCallbacks = new EventSys();
    public EventSys GameStateCallbacks = new EventSys();

    public void Initialize(Admin admin)
    {
        var ids = admin.ID.events;
        StorySystemEvents(ids, admin.storySystem);
        DialogueSystemEvents(ids, admin.dialogueSystem);
        GameStateSystemEvents(ids, admin.gameStateSystem);
        DaySystemEvents(ids, admin.daySystem);
        NpcSystemEvents(ids, admin.npcSystem);
        CameraSystemEvents(ids, admin.camSystem);
        InventorySystemEvents(ids, admin.inventorySystem);
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
        StoryCallbacks.AddEvent(ids.on_all_stories_completed);
        StoryCallbacks.AddEvent(ids.on_all_stories_finalized);

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

    private void NpcSystemEvents(IDEvents ids, NpcSystem npcSys)
    {
        // Commands
        var evt = NpcSystemCommands.AddEvent("cmd_populate_npcs".GetHashCode());
        evt.OnInvoked += npcSys.PopulateNpcsData;
    }

    private void CameraSystemEvents(IDEvents ids, CameraSystem camSys)
    {
        var evt = CameraSystemCommands.AddEvent<Transform>("retarget_cmd".GetHashCode());
        evt.OnInvoked += camSys.Retarget;
    }

    private void InventorySystemEvents(IDEvents ids, InventorySystem invSys)
    {
        var evt = InventorySystemCommands.AddEvent<ItemData>("add_cookie".GetHashCode());
        evt.OnInvoked += (args) => invSys.AddCookieToInventory(args.m_ItemID, args.m_Amount);
        evt = InventorySystemCommands.AddEvent<ItemData>("remove_cookie".GetHashCode());
        evt.OnInvoked += (args) => invSys.RemoveCookieFromInventory(args.m_ItemID, args.m_Amount);
    }
}