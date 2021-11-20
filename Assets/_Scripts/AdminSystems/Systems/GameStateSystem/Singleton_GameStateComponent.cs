using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Singleton_GameStateComponent : MonoBehaviour
{
    [HideInInspector] public Dictionary<GameStateSystem.State, GameState> m_States = new Dictionary<GameStateSystem.State, GameState>();
    [HideInInspector] public GameState m_State;
    public bool m_GameplayStarted;
    public GameStateSystem.State m_GameplayState;

    [Header("Prefabs")]
    public GameObject m_Bakery;
    public GameObject m_MainMenu;
    public GameObject m_CookieMaking;
    public GameObject m_BakeryNight;
    public GameObject m_EndGame;
}