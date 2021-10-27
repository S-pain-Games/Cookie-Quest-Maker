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

    public event Action OnStartBakeryNight;
    public event Action OnStopBakeryNight;

    [SerializeField]
    private State m_CurrentState = State.Bakery;

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
    private GameObject m_BakeryNight;

    public enum State
    {
        MainMenu,
        Bakery,
        QuestMaking,
        CookieMaking,
        BakeryNight
    }

    private void OnEnable()
    {
        OnStartMainMenu += GameStateSystem_OnStartMainMenu;
        OnStopMainMenu += GameStateSystem_OnStopMainMenu;

        OnStartBakery += GameStateSystem_OnStartBakery;
        OnStopBakery += GameStateSystem_OnStopBakery;

        OnStartQuestMaking += GameStateSystem_OnStartQuestMaking;
        OnStopQuestMaking += GameStateSystem_OnStopQuestMaking;

        OnStartCookieMaking += GameStateSystem_OnStartCookieMaking;
        OnStopCookieMaking += GameStateSystem_OnStopCookieMaking;

        OnStartBakeryNight += GameStateSystem_OnStartBakeryNight;
        OnStopBakeryNight += GameStateSystem_OnStopBakeryNight;

        // We assume we start in the main menu
        m_CurrentState = State.MainMenu;
    }


    private void OnDisable()
    {
        OnStartMainMenu -= GameStateSystem_OnStartMainMenu;
        OnStopMainMenu -= GameStateSystem_OnStopMainMenu;

        OnStartBakery -= GameStateSystem_OnStartBakery;
        OnStopBakery -= GameStateSystem_OnStopBakery;

        OnStartQuestMaking -= GameStateSystem_OnStartQuestMaking;
        OnStopQuestMaking -= GameStateSystem_OnStopQuestMaking;

        OnStartCookieMaking -= GameStateSystem_OnStartCookieMaking;
        OnStopCookieMaking -= GameStateSystem_OnStopCookieMaking;

        OnStartBakeryNight -= GameStateSystem_OnStartBakeryNight;
        OnStopBakeryNight -= GameStateSystem_OnStopBakeryNight;
    }

    public void SetState(State state)
    {
        OnExitCurrentState();
        m_CurrentState = state;
        OnEnterCurrentState();
    }

    private void GameStateSystem_OnStopMainMenu() => m_MainMenu.SetActive(false);
    private void GameStateSystem_OnStartMainMenu() => m_MainMenu.SetActive(true);

    private void GameStateSystem_OnStopCookieMaking() => m_CookieMaking.SetActive(false);
    private void GameStateSystem_OnStartCookieMaking() => m_CookieMaking.SetActive(true);

    private void GameStateSystem_OnStopQuestMaking() => m_QuestMaking.SetActive(false);
    private void GameStateSystem_OnStartQuestMaking() => m_QuestMaking.SetActive(true);


    private void GameStateSystem_OnStopBakeryNight() => m_BakeryNight.SetActive(false);
    private void GameStateSystem_OnStartBakeryNight() => m_BakeryNight.SetActive(true);

    private void GameStateSystem_OnStopBakery()
    {
        //Desactivar todo lo que debería desactivarse


        //Desabilitar listener de movimiento
        m_Bakery[0].GetComponent<AgentMouseListener>().SetListenerEnabled(false);

        //Desactivar escenario
        m_Bakery[1].SetActive(false);
    }

    private void GameStateSystem_OnStartBakery()
    {
        //Activar todo lo que debería activarse

        //Habilitar listener de movimiento
        m_Bakery[0].GetComponent<AgentMouseListener>().SetListenerEnabled(true);

        //Activar escenario
        m_Bakery[1].SetActive(true);
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
            case State.BakeryNight:
                OnStopBakeryNight?.Invoke();
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
            case State.BakeryNight:
                OnStartBakeryNight?.Invoke();
                break;
            default:
                break;
        }
    }
}