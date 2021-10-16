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
    public event Action OnStartQuesting;
    public event Action OnStopQuesting;

    [SerializeField]
    private State m_CurrentState = State.Bakery;

    public enum State
    {
        Bakery,
        Questing,
        MainMenu
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
            case State.Questing:
                OnStopQuesting?.Invoke();
                break;
            case State.MainMenu:
                OnStopMainMenu.Invoke();
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
            case State.Questing:
                OnStartQuesting?.Invoke();
                break;
            case State.MainMenu:
                OnStartMainMenu?.Invoke();
                break;
            default:
                break;
        }
    }
}