using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : ISystemEvents
{
    private GameEventSystem _eventSystem;
    private DayData _dayData;

    // Own Callbacks
    private EventVoid _dayStartedCallbacks;
    private EventVoid _dayEndedCallbacks;
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
        _dailyStoriesCompleted = callbacks.AddEvent("all_daily_stories_completed".GetHashCode());

        var evt = commands.AddEvent("start_new_day".GetHashCode());
        evt.OnInvoked += StartNewDay;
    }

    public void Initialize(GameEventSystem eventSystem, DayData dayData)
    {
        // Setup Callbacks and Commands
        var ids = Admin.g_Instance.ID.events;
        _eventSystem = eventSystem;

        // Subscribe to Callbacks
        Event<int> evt = _eventSystem.GetCallback<Event<int>>("story_sys".GetHashCode(), "story_completed".GetHashCode());
        evt.OnInvoked += StoryCompletedCallback;

        // Initialize external Commands
        _setGameStateCommand = _eventSystem.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state");
        _populateNpcsCommand = _eventSystem.GetCommandByName<EventVoid>("npc_sys", "cmd_populate_npcs");

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

    public void StartNewDay()
    {
        _dayEndedCallbacks.Invoke();
        _populateNpcsCommand.Invoke();
        _setGameStateCommand.Invoke(GameStateSystem.State.Bakery);
        _dayStartedCallbacks.Invoke();
    }
}

public class DayData
{
    public int m_StoriesCompletedToday = 0;
}