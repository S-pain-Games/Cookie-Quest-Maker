using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSystem : MonoBehaviour
{
    public State CurrentState => m_CurrentState;

    public event Action OnStartMainMenu;
    public event Action OnStopMainMenu;
    public event Action OnStartBakery;
    public event Action OnStopBakery;
    public event Action OnStartQuestMaking;
    public event Action OnStopQuestMaking;
    public event Action OnStartCookieMaking;
    public event Action OnStopCookieMaking;

    [SerializeField]
    private State m_CurrentState = State.Bakery;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject m_Bakery;
    [SerializeField]
    private GameObject m_MainMenu;
    [SerializeField]
    private GameObject m_QuestMaking;
    [SerializeField]
    private GameObject m_CookieMaking;

    public enum State
    {
        MainMenu,
        Bakery,
        QuestMaking,
        CookieMaking
    }

    public void SetState(State state)
    {
        OnExitCurrentState();
        m_CurrentState = state;
        OnEnterCurrentState();
    }

    private void OnExitCurrentState()
    {
        switch (m_CurrentState)
        {
            case State.Bakery:
                OnStopBakery?.Invoke();
                break;
            case State.QuestMaking:
                OnStopQuestMaking?.Invoke();
                break;
            case State.MainMenu:
                OnStopMainMenu?.Invoke();
                break;
            case State.CookieMaking:
                OnStopCookieMaking?.Invoke();
                break;
            default:
                break;
        }
    }

    private void OnEnterCurrentState()
    {
        switch (m_CurrentState)
        {
            case State.Bakery:
                OnStartBakery?.Invoke();
                break;
            case State.QuestMaking:
                OnStartQuestMaking?.Invoke();
                break;
            case State.MainMenu:
                OnStartMainMenu?.Invoke();
                break;
            case State.CookieMaking:
                OnStartCookieMaking?.Invoke();
                break;
            default:
                break;
        }
    }
}