using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWalkMovement : MonoBehaviour, IAgentMovement
{
    [SerializeField] private float _movementSpeed = 2;

    private Stack<Vector3> _path;
    private TilebasedPathfindingAgent _agent;
    private bool moving = false;

    private Vector3 nextPositionInPath;
    private float distanceLeftTowardsNextPosition = 0;
    private Vector3 movementDirection;

    void Start()
    {
        _agent = GetComponent<TilebasedPathfindingAgent>();
    }

    public void StartMovingAlongPath(Stack<Vector3> path)
    {
        _path = path;
        moving = true;
        if (_path.Count > 0)
        {
            SetupNextPositionParams();
        }
    }

    void Update()
    {
        if (!moving)
            return;

        UpdateMovementTowardsTarget();
    }

    private void UpdateMovementTowardsTarget()
    {

        if (distanceLeftTowardsNextPosition > 0)
            AdvanceTowardsNextPosition();
        else
        {
            transform.position = nextPositionInPath;

            if (_path.Count == 0)
            {
                PathFinished();
                return;
            }

            SetupNextPositionParams();
        }
    }

    private void SetupNextPositionParams()
    {
        nextPositionInPath = _path.Pop();
        distanceLeftTowardsNextPosition = Vector3.Distance(transform.position, nextPositionInPath);
        movementDirection = (nextPositionInPath - transform.position).normalized;
    }

    private void AdvanceTowardsNextPosition()
    {
        transform.Translate(movementDirection * _movementSpeed * Time.deltaTime);
        distanceLeftTowardsNextPosition -= _movementSpeed * Time.deltaTime;
    }

    private void PathFinished()
    {
        moving = false;
        _agent.CheckIfArrivedAtTarget();
    }

}
