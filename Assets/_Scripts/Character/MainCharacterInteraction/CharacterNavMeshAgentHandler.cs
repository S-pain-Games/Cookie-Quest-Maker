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

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _movingTowardsTarget = false;
        _interactingWithNpc = false;
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
        _interactingWithNpc = _interactableEntity.GetComponent<NPCBehaviour>() != null;
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


        if(_interactingWithNpc && _agent.remainingDistance <= 1)
        {
            OnTargetReached();
        }
        else if (_agent.remainingDistance <= 0.5f)
        {
            OnTargetReached();
        }
    }

    private void OnTargetReached()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;

        //Si el target era interactuable, llamar a su método
        if(_interactableEntity != null)
        {
            _interactableEntity.GetComponent<IInteractableEntity>().OnInteract();
        }

        _interactableEntity = null;
        _movingTowardsTarget = false;
        _interactingWithNpc = false;
    }

}
