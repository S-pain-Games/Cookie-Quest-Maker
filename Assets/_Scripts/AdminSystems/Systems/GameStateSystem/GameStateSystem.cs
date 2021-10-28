using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSystem : MonoBehaviour
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
        var gs = new GameState(evtSys.GameStateCallbacks.AddEvent("on_bakery_enter".GetHashCode()),
            evtSys.GameStateCallbacks.AddEvent("on_bakery_exit".GetHashCode()),
            m_Bakery);
        m_States.Add(State.Bakery, gs);

        gs = new GameState(evtSys.GameStateCallbacks.AddEvent("on_main_menu_enter".GetHashCode()),
            evtSys.GameStateCallbacks.AddEvent("on_main_menu_exit".GetHashCode()),
            m_MainMenu);
        m_States.Add(State.MainMenu, gs);

        gs = new GameState(evtSys.GameStateCallbacks.AddEvent("on_quest_making_enter".GetHashCode()),
            evtSys.GameStateCallbacks.AddEvent("on_quest_making_exit".GetHashCode()),
            m_QuestMaking);
        m_States.Add(State.QuestMaking, gs);

        gs = new GameState(evtSys.GameStateCallbacks.AddEvent("on_cookie_making_enter".GetHashCode()),
            evtSys.GameStateCallbacks.AddEvent("on_cookie_making_exit".GetHashCode()),
            m_CookieMaking);
        m_States.Add(State.CookieMaking, gs);

        gs = new GameState(evtSys.GameStateCallbacks.AddEvent("on_bakery_night_enter".GetHashCode()),
            evtSys.GameStateCallbacks.AddEvent("on_bakery_night_exit".GetHashCode()),
            m_BakeryNight);
        m_States.Add(State.BakeryNight, gs);
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
}