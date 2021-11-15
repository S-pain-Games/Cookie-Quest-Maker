using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerSwapperZone : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] _affectedObjects;

    [SerializeField]
    private int _layerIncreaseOnEnter;

    [SerializeField]
    private int _layerDecreaseOnExit;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterNavMeshAgentHandler>() != null)
        {
            SetAffectedObjectsLayerToValue(_layerIncreaseOnEnter);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterNavMeshAgentHandler>() != null)
        {
            SetAffectedObjectsLayerToValue(_layerDecreaseOnExit);
        }
    }

    private void SetAffectedObjectsLayerToValue(int newLayer)
    {
        foreach (SpriteRenderer sr in _affectedObjects)
            sr.sortingOrder += newLayer;
    }
}
