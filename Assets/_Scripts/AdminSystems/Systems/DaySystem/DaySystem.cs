using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem
{
    private GameEventSystem _eventSystem;
    private DayData _dayData;

    private EventVoid _dayStartedCallbacks;
    private EventVoid _dayEndedCallbacks;
    private EventVoid _dailyStoriesCompleted;

    private EventVoid _startNewDayCommand;

    public void Initialize(GameEventSystem eventSystem, DayData dayData)
    {
        // Setup Callbacks and Commands
        var ids = Admin.g_Instance.ID.events;
        _eventSystem = eventSystem;

        _eventSystem.DayCallbacks.GetEvent(ids.on_day_started, out _dayStartedCallbacks);
        _eventSystem.DayCallbacks.GetEvent(ids.on_day_ended, out _dayEndedCallbacks);
        _eventSystem.DayCallbacks.GetEvent(ids.on_daily_stories_completed, out _dailyStoriesCompleted);

        // Subscribe to Events
        _eventSystem.StoryCallbacks.GetEvent(ids.on_story_completed, out Event<int> evt);
        evt.OnInvoked += StoryCompletedCallback;

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
        Admin.g_Instance.npcSystem.PopulateNpcsData();
        Admin.g_Instance.gameStateSystem.SetState(GameStateSystem.State.Bakery);
        _dayStartedCallbacks.Invoke();
    }
}

public class DayData
{
    public int m_StoriesCompletedToday = 0;
}