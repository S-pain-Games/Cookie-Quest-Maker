using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AgentMouseListener))]
public class CharacterNavMeshAgentHandler : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Vector3 _targetToReach;
    private GameObject _interactableEntity;


    private bool _movingTowardsTarget;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _movingTowardsTarget = false;
    }

    public void SetTarget(Vector3 target)
    {
        _agent.SetDestination(target);
        _targetToReach = target;

        _agent.isStopped = false;
        _movingTowardsTarget = true;
    }

    public void SetTarget(Vector3 target, GameObject interactableEntity)
    {
        _agent.SetDestination(target);
        _targetToReach = target;

        _agent.isStopped = false;
        _movingTowardsTarget = true;

        _interactableEntity = interactableEntity;
    }

    private float _lastVelocity;

    void Update()
    {
        if (!_movingTowardsTarget)
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
