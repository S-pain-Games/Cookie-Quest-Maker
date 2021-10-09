using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentJumpMovement : MonoBehaviour, IAgentMovement 
{
    [SerializeField] private float _timeBetweenJumps = 0.5f;
    private float _currentTimer;

    private Stack<Vector3> _path;
    private TilebasedPathfindingAgent _agent;
    private bool moving = false;

    private void Start()
    {
        _agent = GetComponent<TilebasedPathfindingAgent>();
        _currentTimer = _timeBetweenJumps;
    }

    public void StartMovingAlongPath(Stack<Vector3> path)
    {
        _path = path;
        _currentTimer = _timeBetweenJumps;
        moving = true;
    }

   
    private void Update()
    {
        if (!moving)
            return;

        JumpTowardsNextPosition();
    }


    private void JumpTowardsNextPosition()
    {
        _currentTimer -= Time.deltaTime;
        if (_currentTimer < 0)
        {
            transform.position = _path.Pop();
            _currentTimer = _timeBetweenJumps;

            if(_path.Count == 0)
            {
                PathFinished();
            }
        }
    }

    void PathFinished()
    {
        moving = false;
        _agent.CheckIfArrivedAtTarget();
    }
}
