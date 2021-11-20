using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDialogueBarrier : MonoBehaviour
{
    [SerializeField] private LastEndingDialogSequence _sequence;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision))
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
}
