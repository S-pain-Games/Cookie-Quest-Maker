using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AgentMouseListener))]
public class CharacterNavMeshAgentHandler : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _interactableEntity;


    private bool _movingTowardsTarget;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _movingTowardsTarget = false;
        _interactableEntity = null;
    }

    //Move towards position
    public void SetTarget(Vector3 target)
    {
        SetupAgent(target);

        _interactableEntity = null;
    }

    //Move towards position, then interact with object
    public void SetTarget(Vector3 target, GameObject interactableEntity)
    {
        SetupAgent(target);
        _interactableEntity = interactableEntity;
    }

    private void SetupAgent(Vector3 target)
    {
        _agent.SetDestination(target);
        _agent.isStopped = false;
        _movingTowardsTarget = true;
    }

    void Update()
    {
        if (!_movingTowardsTarget)
            return;

        if (_agent.pathPending)
            return;

        if (_agent.remainingDistance == 0)
        {
            OnTargetReached();
        }
    }

    private void OnTargetReached()
    {
        Debug.Log("Target reached");
        _agent.isStopped = true;

        //Si el target era interactuable, llamar a su método
        if(_interactableEntity != null)
        {
            _interactableEntity.GetComponent<IInteractableEntity>().OnInteract();
        }

        _interactableEntity = null;
        _movingTowardsTarget = false;

    }

}
