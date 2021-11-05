using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : ISystemEvents
{
    private DayComponent _dayData;

    // Own Callbacks
    private EventVoid _dayStartedCallbacks;
    private EventVoid _dayEndedCallbacks;
    private EventVoid _nightBeginCallback;
    private EventVoid _dailyStoriesCompleted;

    // External Commands
    private Event<GameStateSystem.State> _setGameStateCommand;
    private EventVoid _populateNpcsCommand;

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = "day_sys".GetHashCode();

        _dayStartedCallbacks = callbacks.AddEvent("day_started".GetHashCode());
        _dayEndedCallbacks = callbacks.AddEvent("day_ended".GetHashCode());
        _nightBeginCallback = callbacks.AddEvent("night_begin".GetHashCode());
        _dailyStoriesCompleted = callbacks.AddEvent("all_daily_stories_completed".GetHashCode());

        commands.AddEvent("start_new_day".GetHashCode()).OnInvoked += StartNewDay;
        commands.AddEvent("begin_night".GetHashCode()).OnInvoked += StartNight;
    }

    public void Initialize(GameEventSystem evtSys, DayComponent dayData)
    {
        // Subscribe to Callbacks
        Event<int> evt = evtSys.GetCallback<Event<int>>("story_sys".GetHashCode(), "story_completed".GetHashCode());
        evt.OnInvoked += StoryCompletedCallback;

        // Initialize external Commands
        _setGameStateCommand = evtSys.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state");
        _populateNpcsCommand = evtSys.GetCommandByName<EventVoid>("npc_sys", "cmd_populate_npcs");

        _dayData = dayData;
    }

    private void StoryCompletedCallback(int storyId)
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

public class DayComponent
{
    public int m_StoriesCompletedToday = 0;
}