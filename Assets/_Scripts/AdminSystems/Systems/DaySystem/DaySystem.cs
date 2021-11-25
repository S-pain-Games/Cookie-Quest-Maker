using CQM.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : ISystemEvents
{
    private Singleton_DayComponent _dayData;

    // Own Callbacks
    private EventVoid _dayStartedCallbacks;
    private EventVoid _tutorialDayStartedCallbacks;
    private EventVoid _dayEndedCallbacks;
    private EventVoid _nightBeginCallback;
    private EventVoid _dailyStoriesCompleted;

    // External Commands
    private Event<GameStateSystem.State> _setGameStateCommand;
    private Event<int> _populateNpcsCommand;


    public void Initialize(GameEventSystem evtSys, Singleton_DayComponent dayData)
    {
        // Subscribe to Callbacks
        Event<ID> evt = evtSys.GetCallback<Event<ID>>(new ID("story_sys"), new ID("story_completed"));
        evt.OnInvoked += StoryCompletedCallback;

        // Initialize external Commands
        _setGameStateCommand = evtSys.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state");
        _populateNpcsCommand = evtSys.GetCommandByName<Event<int>>("npc_sys", "cmd_populate_npcs");

        _dayData = dayData;
    }

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("day_sys");

        _dayStartedCallbacks = callbacks.AddEvent(new ID("day_started"));
        _dayEndedCallbacks = callbacks.AddEvent(new ID("day_ended"));
        _nightBeginCallback = callbacks.AddEvent(new ID("night_begin"));
        _dailyStoriesCompleted = callbacks.AddEvent(new ID("all_daily_stories_completed"));

        _tutorialDayStartedCallbacks = callbacks.AddEvent(new ID("tutorial_day_started"));

        commands.AddEvent(new ID("start_new_day")).OnInvoked += StartNormalDay;
        commands.AddEvent(new ID("start_tutorial_day")).OnInvoked += StartTutorialDay;
        commands.AddEvent(new ID("begin_night")).OnInvoked += StartNight;
    }


    private void StoryCompletedCallback(ID storyId)
    {
        _dayData.m_StoriesCompletedToday += 1;
        if (_dayData.m_StoriesCompletedToday >= _dayData.m_StoriesToCompleteInADay)
        {
            _dailyStoriesCompleted.Invoke();
        }
    }

    private void StartNight()
    {
        _setGameStateCommand.Invoke(GameStateSystem.State.BakeryNight);
        _nightBeginCallback.Invoke();
    }

    private void StartNormalDay()
    {
        _dayEndedCallbacks.Invoke();
        _populateNpcsCommand.Invoke(3);
        _dayData.m_StoriesCompletedToday = 0;
        _dayData.m_StoriesToCompleteInADay = 3;
        _setGameStateCommand.Invoke(GameStateSystem.State.Bakery);
        _dayStartedCallbacks.Invoke();
    }

    private void StartTutorialDay()
    {
        _dayEndedCallbacks.Invoke();
        _populateNpcsCommand.Invoke(0);
        _dayData.m_StoriesToCompleteInADay = 1;
        _setGameStateCommand.Invoke(GameStateSystem.State.Bakery);
        _dayStartedCallbacks.Invoke();
        _tutorialDayStartedCallbacks.Invoke();
    }
}

public class Singleton_DayComponent
{
    public int m_StoriesCompletedToday = 0;
    public int m_StoriesToCompleteInADay = 3;
}