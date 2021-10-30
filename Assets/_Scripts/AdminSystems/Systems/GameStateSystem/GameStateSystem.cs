using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSystem : MonoBehaviour, ISystemEvents
{
    private Dictionary<State, GameState> m_States = new Dictionary<State, GameState>();
    private GameState m_State;

    [Header("Prefabs")]
    [SerializeField]
    private List<GameObject> m_Bakery;
    [SerializeField]
    private GameObject m_MainMenu;
    [SerializeField]
    private GameObject m_QuestMaking;
    [SerializeField]
    private GameObject m_CookieMaking;
    [SerializeField]
    private List<GameObject> m_BakeryNight;

    private class GameState
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

    public enum State
    {
        MainMenu,
        Bakery,
        QuestMaking,
        CookieMaking,
        BakeryNight
    }

    public void Initialize(GameEventSystem evtSys)
    {
        // Disable All States Except for the starting one (Maybe Bug prone?)
        foreach (GameState state in m_States.Values)
        {
            state.DisableAllGameobjects();
        }
    }

    private void Start()
    {
        m_State = m_States[State.MainMenu];
        m_State.OnStateEnter();
    }

    public void SetState(State state)
    {
        m_State.OnStateExit();
        m_State = m_States[state];
        m_State.OnStateEnter();
    }

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        callbacks = new EventSys();
        commands = new EventSys();
        sysID = "game_state_sys".GetHashCode();

        var gs = new GameState(callbacks.AddEvent("bakery_enter".GetHashCode()),
            callbacks.AddEvent("bakery_exit".GetHashCode()),
            m_Bakery);
        m_States.Add(State.Bakery, gs);

        gs = new GameState(callbacks.AddEvent("main_menu_enter".GetHashCode()),
            callbacks.AddEvent("main_menu_exit".GetHashCode()),
            m_MainMenu);
        m_States.Add(State.MainMenu, gs);

        gs = new GameState(callbacks.AddEvent("quest_making_enter".GetHashCode()),
            callbacks.AddEvent("quest_making_exit".GetHashCode()),
            m_QuestMaking);
        m_States.Add(State.QuestMaking, gs);

        gs = new GameState(callbacks.AddEvent("cookie_making_enter".GetHashCode()),
            callbacks.AddEvent("cookie_making_exit".GetHashCode()),
            m_CookieMaking);
        m_States.Add(State.CookieMaking, gs);

        gs = new GameState(callbacks.AddEvent("bakery_night_enter".GetHashCode()),
            callbacks.AddEvent("bakery_night_exit".GetHashCode()),
            m_BakeryNight);
        m_States.Add(State.BakeryNight, gs);

        var evt = commands.AddEvent<State>("set_game_state".GetHashCode());
        evt.OnInvoked += SetState;
    }
}