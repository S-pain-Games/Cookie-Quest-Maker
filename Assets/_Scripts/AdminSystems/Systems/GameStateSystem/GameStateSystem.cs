using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSystem : ISystemEvents
{
    private Singleton_GameStateComponent m_GameState;

    private TransitionsSubSystem m_TransitionsSubSys;


    public void Initialize(Singleton_GameStateComponent gameState,
                           Singleton_TransitionsComponent transitions)
    {
        m_GameState = gameState;
        m_TransitionsSubSys = new TransitionsSubSystem();
        m_TransitionsSubSys.Initialize(transitions);
    }

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        callbacks = new EventSys();
        commands = new EventSys();
        sysID = new ID("game_state_sys");

        var gs = new GameState(callbacks.AddEvent(new ID("bakery_enter")),
            callbacks.AddEvent(new ID("bakery_exit")),
            m_GameState.m_Bakery);
        m_GameState.m_States.Add(State.Bakery, gs);

        gs = new GameState(callbacks.AddEvent(new ID("main_menu_enter")),
            callbacks.AddEvent(new ID("main_menu_exit")),
            m_GameState.m_MainMenu);
        m_GameState.m_States.Add(State.MainMenu, gs);

        gs = new GameState(callbacks.AddEvent(new ID("cookie_making_enter")),
            callbacks.AddEvent(new ID("cookie_making_exit")),
            m_GameState.m_CookieMaking);
        m_GameState.m_States.Add(State.CookieMaking, gs);

        gs = new GameState(callbacks.AddEvent(new ID("bakery_night_enter")),
            callbacks.AddEvent(new ID("bakery_night_exit")),
            m_GameState.m_BakeryNight);
        m_GameState.m_States.Add(State.BakeryNight, gs);

        var evt = commands.AddEvent<State>(new ID("set_game_state"));
        evt.OnInvoked += SetState;
    }

    public void StartGame()
    {
        // Disable All States Except for the starting one (Maybe Bug prone?)
        foreach (GameState state in m_GameState.m_States.Values)
        {
            state.DisableAllGameobjects();
        }
        m_GameState.m_State = m_GameState.m_States[State.MainMenu];
        m_GameState.m_State.OnStateEnter();
    }

    public void SetState(State state)
    {
        m_TransitionsSubSys.TransitionTo(() =>
        {
            m_GameState.m_State.OnStateExit();
            m_GameState.m_State = m_GameState.m_States[state];
            m_GameState.m_State.OnStateEnter();
        });
    }


    public class TransitionsSubSystem
    {
        private Singleton_TransitionsComponent _data;

        public void Initialize(Singleton_TransitionsComponent component)
        {
            _data = component;
        }

        public void TransitionTo(Action midPoint)
        {
            var t = _data.m_FadeTransition;
            t.outCompleted += midPoint;
            t.outCompleted += () => _data.m_FadeTransition.TransitionIn(); ;
            t.inCompleted += () => t.ClearAllCallbacks();
            t.TransitionOut();
        }
    }


    [Serializable]
    public class Singleton_TransitionsComponent
    {
        public TransitionBehaviour m_FadeTransition;
    }


    public enum State
    {
        MainMenu,
        Bakery,
        CookieMaking,
        BakeryNight
    }
}


public class GameState
{
    private EventVoid m_OnStateEnter;
    private EventVoid m_OnStateExit;
    private List<GameObject> m_Prefabs;


    public GameState(EventVoid onStateEnter, EventVoid onStateExit, List<GameObject> prefabs)
    {
        m_OnStateEnter = onStateEnter;
        m_OnStateExit = onStateExit;
        m_Prefabs = prefabs;
    }

    public GameState(EventVoid onStateEnter, EventVoid onStateExit, GameObject prefabs)
    {
        m_OnStateEnter = onStateEnter;
        m_OnStateExit = onStateExit;
        m_Prefabs = new List<GameObject>() { prefabs };
    }


    public void OnStateEnter()
    {
        for (int i = 0; i < m_Prefabs.Count; i++)
        {
            m_Prefabs[i].SetActive(true);
        }
        m_OnStateEnter.Invoke();
    }

    public void OnStateExit()
    {
        m_OnStateExit.Invoke();
        for (int i = 0; i < m_Prefabs.Count; i++)
        {
            m_Prefabs[i].SetActive(false);
        }
    }

    public void DisableAllGameobjects()
    {
        for (int i = 0; i < m_Prefabs.Count; i++)
        {
            m_Prefabs[i].SetActive(false);
        }
    }
}
