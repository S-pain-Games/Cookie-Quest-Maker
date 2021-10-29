using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterNavMeshAgentHandler))]
public class AgentMouseListener : MonoBehaviour
{
    [SerializeField]
    private bool _enabledListener;

    [Header("Game State System Callbacks")]
    [SerializeField] private string _enableCallbackID;
    [SerializeField] private string _disableCallbackID;

    private CharacterNavMeshAgentHandler _agent;

    private GameEventSystem _evtSys;
    private EventVoid _onBakeryStateEnter;
    private EventVoid _onBakeryStateExit;

    private void Awake()
    {
        _agent = GetComponent<CharacterNavMeshAgentHandler>();
        _evtSys = Admin.g_Instance.gameEventSystem;
        _evtSys.GameStateCallbacks.GetEvent(_enableCallbackID.GetHashCode(), out _onBakeryStateEnter);
        _evtSys.GameStateCallbacks.GetEvent(_disableCallbackID.GetHashCode(), out _onBakeryStateExit);
    }

    private void OnEnable()
    {
        _onBakeryStateEnter.OnInvoked += EnableMouseInput;
        _onBakeryStateExit.OnInvoked += DisableMouseInput;
    }

    private void OnDisable()
    {
        _onBakeryStateEnter.OnInvoked -= EnableMouseInput;
        _onBakeryStateExit.OnInvoked -= DisableMouseInput;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _enabledListener)
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPosition.z = 0;

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
            else if (hit.transform.name == "Walkable")
            {
                clickedOnWalkable = true;
            }
        }


        if (interactableObject != null)
            _agent.SetTarget(mouseWorldPosition, interactableObject);
        else if (clickedOnWalkable)
            _agent.SetTarget(mouseWorldPosition);

    }

    private void EnableMouseInput() => _enabledListener = true;
    private void DisableMouseInput() => _enabledListener = false;
}