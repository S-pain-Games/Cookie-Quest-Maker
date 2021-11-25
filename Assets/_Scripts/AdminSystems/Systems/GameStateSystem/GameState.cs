using System.Collections.Generic;
using UnityEngine;

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
