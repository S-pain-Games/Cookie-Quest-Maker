using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstNightSequenceBarrier : MonoBehaviour
{
    [SerializeField] private FirstNightDeitiesScriptedSequence _sequence;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (IsPlayer(collision) && IsNightState())
        {
            collision.GetComponent<CharacterNavMeshAgentHandler>().InterruptAgentMovement();

            _sequence.ExecuteSequence();
            gameObject.SetActive(false);
        }
    }

    private bool IsPlayer(Collider2D collision)
    {
        return collision.GetComponent<CharacterNavMeshAgentHandler>() != null;
    }

    private bool IsNightState()
    {
        return Admin.Global.Components.m_GameState.m_BakeryNight.activeSelf;
    }
}
