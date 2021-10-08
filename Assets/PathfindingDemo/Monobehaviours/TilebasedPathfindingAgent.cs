using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilebasedPathfindingAgent : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private float _distanceBetweenTiles;
    [SerializeField] private float _tileOffset = 0.5f;
    [SerializeField] private float _movementSpeed = 2;

    [SerializeField] private PathFindingMethod _pathMethod;
    [SerializeField] private MovementMethod _moveMethod;
    [SerializeField] private bool _diagonalMovement;

    [Header("Hill Climb")]
    [SerializeField] private int _searchDepth;

    private bool pathGenerated = false;
    private ITileBasedPathfinder _pathFinder;
    private Stack<Vector3> _path;

    enum PathFindingMethod
    {
        AStar,
        HillClimbing
    }

    enum MovementMethod
    {
        Jump,
        Walk
    }

    void Start()
    {
        if(_pathMethod == PathFindingMethod.AStar)
            _pathFinder = new TileBasedAStarPathfinder(_distanceBetweenTiles, _tileOffset);
        else if(_pathMethod == PathFindingMethod.HillClimbing)
            _pathFinder = new TileBasedHillClimbingPathfinder(_distanceBetweenTiles, _tileOffset, _searchDepth);
    }

    public void MoveTowardsTarget(Vector3 targetPosition)
    {
        if (pathGenerated)
        {
            Debug.Log("Interrupting current path");
            _path.Clear();
            pathGenerated = false;
        }

        _path = _pathFinder.GenerateTiledPathTowardsPosition(transform.position, targetPosition, _diagonalMovement);

        if (_path.Count > 0)
            pathGenerated = true;
        else
            Debug.Log("No se ha podido encontrar un camino hacia " + targetPosition);
    }

    void Update()
    {
        if (!pathGenerated)
            return;

        if(_moveMethod == MovementMethod.Jump)
            JumpTowardsNextPosition();
        else if(_moveMethod == MovementMethod.Walk)
        {
            if (!movingTowardsTarget)
                StartMovingTowardsPosition();
            else
                UpdateMovementTowardsTarget();
        }
    }

    // ===========================================================================================
    //  JUMP MOVEMENT
    // ===========================================================================================

    float timer = 0.5f;
    private void JumpTowardsNextPosition()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && _path.Count > 0)
        {
            transform.position = _path.Pop();
            timer = 0.5f;
        }
    }

    // ===========================================================================================
    //  WALK MOVEMENT
    // ===========================================================================================

    private bool movingTowardsTarget = false;
    private Vector3 nextPositionInPath;
    private float distanceLeftTowardsNextPosition = 0;
    private Vector3 movementDirection;

    private void StartMovingTowardsPosition()
    {
        if (_path.Count > 0)
        {
            SetupNextPositionParams();
            movingTowardsTarget = true;
        }
    }

    private void UpdateMovementTowardsTarget()
    {
        
        if(distanceLeftTowardsNextPosition > 0)
            AdvanceTowardsNextPosition();
        else
        {
            transform.position = nextPositionInPath;

            if (_path.Count == 0){
                ArrivedAtTarget();
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

    // ===========================================================================================
    //  ARRIVED AT TARGET
    // ===========================================================================================

    private void ArrivedAtTarget()
    {
        Debug.Log("He llegado al objetivo");
        movingTowardsTarget = false;
        pathGenerated = false;
    }
   
}
