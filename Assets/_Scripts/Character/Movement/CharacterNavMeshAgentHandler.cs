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
    private bool _interactingWithNpc;
    private bool _movingTowardsObject;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _movingTowardsTarget = false;
        _movingTowardsObject = false;
        _interactingWithNpc = false;
        _interactableEntity = null;
    }

    //Move towards position
    public void SetTarget(Vector3 target)
    {
        SetupAgent(target);
        _interactableEntity = null;
        _movingTowardsObject = false;
    }

    //Move towards position, then interact with object
    public void SetTarget(Vector3 target, GameObject interactableEntity)
    {
        SetupAgent(target);
        _interactableEntity = interactableEntity;
        _interactingWithNpc = _interactableEntity.GetComponent<NPCBehaviour>() != null;
        _movingTowardsTarget = true;
        _movingTowardsObject = true;
    }

    private void SetupAgent(Vector3 target)
    {
        _agent.SetDestination(target);
        _agent.isStopped = false;
        _movingTowardsTarget = true;
    }

    void Update()
    {
        if (!_movingTowardsTarget || _agent.pathPending)
            return;


        //Moving towards Interactable Object
        if (_movingTowardsObject)
        {
            if (_interactingWithNpc && _agent.remainingDistance <= 1)
            {
                OnTargetReached();
            }
            else if (_agent.remainingDistance <= 0.5f)
            {
                OnTargetReached();
            }
        }
        else if(_agent.remainingDistance == 0)
        {
            OnTargetReached();
        }
    }

    private void OnTargetReached()
    {
        StopAgentMovement();

        //Si el target era interactuable, llamar a su método
        if(_interactableEntity != null)
        {
            _interactableEntity.GetComponent<IInteractableEntity>().OnInteract();
        }

        ResetAgentTarget();
    }

    public void InterruptAgentMovement()
    {
        StopAgentMovement();
        ResetAgentTarget();
    }

    private void StopAgentMovement()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }

    private void ResetAgentTarget()
    {
        _interactableEntity = null;
        _movingTowardsTarget = false;
        _interactingWithNpc = false;
    }
}
