using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerSwapperZoneNPC : MonoBehaviour
{
    /*[SerializeField]
    private SpriteRenderer[] _affectedObjects;*/

    [SerializeField]
    private int _layerIncreaseOnEnter;

    [SerializeField]
    private int _layerDecreaseOnExit;

    [SerializeField]
    private SpriteRenderer hioRenderer; 

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
        //transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder += newLayer;

        /*foreach (SpriteRenderer sr in _affectedObjects)
            sr.sortingOrder += newLayer;*/

        hioRenderer.sortingOrder += newLayer;
    }
}
