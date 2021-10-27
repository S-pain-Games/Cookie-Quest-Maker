using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(CharacterNavMeshAgentHandler))]
public class AgentMouseListener : MonoBehaviour
{

    [SerializeField]
    private bool _enabledListener;

    private CharacterNavMeshAgentHandler _agent;


    void Start()
    {
        _agent = GetComponent<CharacterNavMeshAgentHandler>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _enabledListener)
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPosition, Vector2.zero);

        GameObject interactableObject = null;
        bool clickedOnWalkable = false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.GetComponent<IInteractableEntity>() != null)
            {
                //Debug.Log("He clickado en " + hit.transform.gameObject.name);
                interactableObject = hit.transform.gameObject;
            }
            else if(hit.transform.name == "Walkable")
            {
                clickedOnWalkable = true;
            }
        }


        if (interactableObject != null)
            _agent.SetTarget(mouseWorldPosition, interactableObject);
        else if (clickedOnWalkable)
            _agent.SetTarget(mouseWorldPosition);

    }

    public bool IsListenerEnabled()
    {
        return _enabledListener;
    }

    public void SetListenerEnabled(bool state)
    {
        _enabledListener = state;
    }

}
