using UnityEngine;

public class GameStateSystem : MonoBehaviour
{
    public State CurrentState => m_CurrentState;
    private State m_CurrentState = State.BakeryGameplay;

    public enum State
    {
        BakeryGameplay,
        QuestMaking
    }

    public void EnableBakery()
    {

    }

    public void EnableQuestMaking()
    {

    }
}
