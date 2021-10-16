using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilebasedPathfindingAgent : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private float _distanceBetweenTiles;
    [SerializeField] private float _tileOffset = 0.5f;
    [SerializeField] private int _maxAttemps;
    [SerializeField] private int _nodePoolSize = 50;
    [SerializeField] private int _loopAttemps = 100;
    private int _triesLeft;

    [SerializeField] private PathFindingMethod _pathMethod;
    [SerializeField] private bool _diagonalMovement;

    [Header("Hill Climb")]
    [SerializeField] private int _searchDepth;

    private bool pathGenerated = false;

    private ITileBasedPathfinder _pathFinder;
    private ITileBasedPathfinder _pathFinderA;
    private ITileBasedPathfinder _pathFinderB;

    private IAgentMovement _agentMovement;

    private Stack<Vector3> _path;

    enum PathFindingMethod
    {
        AStar,
        HillClimbing,
    }

    void Start()
    {
        _pathFinderA = new TileBasedAStarPathfinder(_distanceBetweenTiles, _tileOffset);
        _pathFinderB = new TileBasedHillClimbingPathFinder(_distanceBetweenTiles, _tileOffset, _searchDepth, _nodePoolSize, _loopAttemps);

        _path = new Stack<Vector3>();

        _agentMovement = GetComponent<IAgentMovement>();
    }

    private Vector3 _currentTarget;


    public void MoveTowardsTargetReceiver(Vector3 targetPosition)
    {
        targetPosition = ParsePositionToTileCenter(targetPosition);

        if (targetPosition == _currentTarget)
            return;

        _triesLeft = _maxAttemps;


        MoveTowardsTarget(targetPosition);
    }

    private void MoveTowardsTarget(Vector3 targetPosition)
    {
        _currentTarget = targetPosition;

        if (pathGenerated)
        {
            Debug.Log("Interrupting current path");
            _path.Clear();
            pathGenerated = false;
        }

        _path = GeneratePath(targetPosition);

        Debug.Log("Path lenght: "+_path.Count);

        if (_path.Count > 0)
            _agentMovement.StartMovingAlongPath(_path);
        else
            Debug.Log("No se ha podido encontrar un camino hacia " + targetPosition);
    }

    private Vector3 ParsePositionToTileCenter(Vector3 position)
    {
        return new Vector3(Mathf.Floor(position.x) + _tileOffset, Mathf.Floor(position.y) + _tileOffset, 0);
    }

    private Stack<Vector3> GeneratePath(Vector3 targetPosition)
    {
        if (_pathMethod == PathFindingMethod.AStar)
            return _pathFinderA.GenerateTiledPathTowardsPosition(transform.position, targetPosition, _diagonalMovement);
        else if (_pathMethod == PathFindingMethod.HillClimbing)
            return _pathFinderB.GenerateTiledPathTowardsPosition(transform.position, targetPosition, _diagonalMovement);

        return null;
    }

    // ===========================================================================================
    //  ARRIVED AT TARGET
    // ===========================================================================================

    public void CheckIfArrivedAtTarget()
    {
        if (Vector3.Distance(transform.position, _currentTarget) < _distanceBetweenTiles)
        {
            ArrivedAtTarget();
        }
        else
        {
            //No he alcanzado el objetivo, necesito que me generen otro camino que seguir
            Debug.Log("Pido otro camino");

            _triesLeft--;

            if (_triesLeft > 0)
                MoveTowardsTarget(_currentTarget);
            else
                ArrivedAtTarget();
        }
    }

    private void ArrivedAtTarget()
    {
        Debug.Log("He llegado al objetivo");
        pathGenerated = false;
    }
   
}
