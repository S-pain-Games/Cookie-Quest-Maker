using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem
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

    public void Initialize(GameEventSystem eventSystem, DayData dayData)
    {
        // Setup Callbacks and Commands
        var ids = Admin.g_Instance.ID.events;
        _eventSystem = eventSystem;

        // Initialize own callbacks
        _eventSystem.DayCallbacks.GetEvent(ids.on_day_started, out _dayStartedCallbacks);
        _eventSystem.DayCallbacks.GetEvent(ids.on_day_ended, out _dayEndedCallbacks);
        _eventSystem.DayCallbacks.GetEvent(ids.on_daily_stories_completed, out _dailyStoriesCompleted);

        // Subscribe to Callbacks
        _eventSystem.StoryCallbacks.GetEvent(ids.on_story_completed, out Event<int> evt);
        evt.OnInvoked += StoryCompletedCallback;

        // Initialize external Commands
        _eventSystem.GameStateSystemMessaging.GetEvent(ids.set_game_state, out _setGameStateCommand);
        _eventSystem.NpcSystemCommands.GetEvent("cmd_populate_npcs".GetHashCode(), out _populateNpcsCommand);

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