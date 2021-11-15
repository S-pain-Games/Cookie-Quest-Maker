using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerSwapperZone : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] _affectedObjects;

    [SerializeField]
    private int _targetLayerOnEnter;

    [SerializeField]
    private int _targetLayerOnExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Algo ha entrado");

        if (collision.GetComponent<CharacterNavMeshAgentHandler>() != null)
        {
            Debug.Log("Cambiooo");
            SetAffectedObjectsLayerToValue(_targetLayerOnEnter);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Algo ha entrado");

        if (collision.GetComponent<CharacterNavMeshAgentHandler>() != null)
        {
            Debug.Log("Cambiooo");
            SetAffectedObjectsLayerToValue(_targetLayerOnExit);
        }
    }

    private void SetAffectedObjectsLayerToValue(int newLayer)
    {
        foreach (SpriteRenderer sr in _affectedObjects)
            sr.sortingOrder = newLayer;
    }
}
