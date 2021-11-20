using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstNightSequenceBarrier : MonoBehaviour
{
    [SerializeField] private FirstNightDeitiesScriptedSequence _sequence;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Admin.Global.Components.m_GameState.m_BakeryNight.activeSelf)
        {
            _sequence.ExecuteSequence();
            gameObject.SetActive(false);
        }
    }
}
