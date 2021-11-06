using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : ISystemEvents
{
    private Singleton_DayComponent _dayData;

    // Own Callbacks
    private EventVoid _dayStartedCallbacks;
    private EventVoid _dayEndedCallbacks;
    private EventVoid _nightBeginCallback;
    private EventVoid _dailyStoriesCompleted;

    // External Commands
    private Event<GameStateSystem.State> _setGameStateCommand;
    private EventVoid _populateNpcsCommand;

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("day_sys");

        _dayStartedCallbacks = callbacks.AddEvent(new ID("day_started"));
        _dayEndedCallbacks = callbacks.AddEvent(new ID("day_ended"));
        _nightBeginCallback = callbacks.AddEvent(new ID("night_begin"));
        _dailyStoriesCompleted = callbacks.AddEvent(new ID("all_daily_stories_completed"));

        commands.AddEvent(new ID("start_new_day")).OnInvoked += StartNewDay;
        commands.AddEvent(new ID("begin_night")).OnInvoked += StartNight;
    }

    public void Initialize(GameEventSystem evtSys, Singleton_DayComponent dayData)
    {
        // Subscribe to Callbacks
        Event<ID> evt = evtSys.GetCallback<Event<ID>>(new ID("story_sys"), new ID("story_completed"));
        evt.OnInvoked += StoryCompletedCallback;

        // Initialize external Commands
        _setGameStateCommand = evtSys.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state");
        _populateNpcsCommand = evtSys.GetCommandByName<EventVoid>("npc_sys", "cmd_populate_npcs");

        _dayData = dayData;
    }

    private void StoryCompletedCallback(ID storyId)
    {
        _dayData.m_StoriesCompletedToday += 1;
        if (_dayData.m_StoriesCompletedToday >= 1)
        {
            _dailyStoriesCompleted.Invoke();
        }
    }

    public void StartNight()
    {
        _setGameStateCommand.Invoke(GameStateSystem.State.BakeryNight);
        _nightBeginCallback.Invoke();
    }

    public void StartNewDay()
    {
        _dayEndedCallbacks.Invoke();
        _populateNpcsCommand.Invoke();
        _setGameStateCommand.Invoke(GameStateSystem.State.Bakery);
        _dayStartedCallbacks.Invoke();
    }
}

public class Singleton_DayComponent
{
    public int m_StoriesCompletedToday = 0;
}