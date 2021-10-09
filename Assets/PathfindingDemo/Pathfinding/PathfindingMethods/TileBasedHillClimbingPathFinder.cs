using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBasedHillClimbingPathFinder : ITileBasedPathfinder
{
    private float _tileSeparation;
    private float _tileOffset;
    private int _maxDepth;
    private bool _diagonal;

    private PathfindingNodePool _nodePool;

    private List<PathfindingNode> _open;
    private List<Vector3> _closed;
    private List<Vector3> _childs;
    private List<Vector3> _walkableNeighbours;
    private Stack<Vector3> _path;

    private Vector3[] _movementDirections;

    public TileBasedHillClimbingPathFinder(float tileSeparation, float tileOffset, int maxDepth, int nodePoolSize)
    {
        _tileSeparation = tileSeparation;
        _tileOffset = tileOffset;
        _maxDepth = maxDepth;

        _nodePool = new PathfindingNodePool(nodePoolSize);

        _open = new List<PathfindingNode>();
        _closed = new List<Vector3>();
        _childs = new List<Vector3>();
        _walkableNeighbours = new List<Vector3>();
        _path = new Stack<Vector3>();

        SetupMovementDirections();
    }

    private void SetupMovementDirections()
    {
        _movementDirections = new Vector3[8];

        _movementDirections[0] = Vector3.up;
        _movementDirections[1] = Vector3.down;
        _movementDirections[2] = Vector3.left;
        _movementDirections[3] = Vector3.right;

        _movementDirections[4] = new Vector3(0.5f, 0.5f);
        _movementDirections[5] = new Vector3(-0.5f, 0.5f);
        _movementDirections[6] = new Vector3(0.5f, -0.5f);
        _movementDirections[7] = new Vector3(-0.5f, -0.5f);
    }

    public Stack<Vector3> GenerateTiledPathTowardsPosition(Vector3 originPosition, Vector3 targetPosition, bool diagonal)
    {
        _diagonal = diagonal;

        originPosition = ParsePositionToTileCenter(originPosition);
        targetPosition = ParsePositionToTileCenter(targetPosition);

        //Debug.Log("Origin: "+originPosition+ ", Target: "+ targetPosition);

        PathfindingNode n = _nodePool.RetrieveNode();
        n.position = originPosition;
        n.h = CalculateDistanceBetweenTwoTiles(originPosition, targetPosition);

        PathfindingNode iterationResult = GeneratePathTowardsPosition(n, targetPosition);

        return RetrieveCalculatedPath(iterationResult);
    }
    

    private Stack<Vector3> RetrieveCalculatedPath(PathfindingNode result)
    {
        Stack<Vector3> path = SetupPlanFromNode(result);
        _nodePool.CleanAndRefillNodePool();
       
        return path;
    }

    private Vector3 ParsePositionToTileCenter(Vector3 position)
    {
        return new Vector3(Mathf.Floor(position.x) + _tileOffset, Mathf.Floor(position.y) + _tileOffset, 0);
    }

    private PathfindingNode GeneratePathTowardsPosition(PathfindingNode firstNode, Vector3 goal)
    {
        _open.Clear();
        _closed.Clear();

        _open.Add(firstNode);

        int tries = 30;

        PathfindingNode best = _open[0];

        while (_open.Count > 0 && tries > 0)
        {
            if (best.h < _open[0].h)
                best = _open[0];

            PathfindingNode node = _open[0];
            _open.Remove(node);
            _closed.Add(node.position);

            if (node.k > _maxDepth)
            {
                Debug.Log("Fin del tramo");
                return node;
            }

            Debug.Log("node: "+node.position+", open: " + _open.Count + ", closed: " + _closed.Count + ", intentos: " + tries);

            if (node.position == goal)
                return node;
            
            _childs.AddRange(GetWalkableNeighboursFromPosition(node.position));

            foreach (Vector3 child in _childs)
            {
                if (IsAlreadyInClosedList(child, _closed))
                    continue;
                
                _open.Add(SetupAndCalculateCosts(child, node, goal));
            }

            _childs.Clear();
            _walkableNeighbours.Clear();
            _open = SortListByCost(_open);

            tries--;
        }

        Debug.Log("No se ha encontrado un camino, devolviendo la última mejor ruta");

        return best;
    }

    private bool IsAlreadyInClosedList(Vector3 v, List<Vector3> closed)
    {
        foreach (Vector3 p in closed)
        {
            //if (p == v)
            //    return true;

            if (Vector3.Distance(p, v) < _tileSeparation)
                return true;
        }

        return false;
    }

    private List<Vector3> GetWalkableNeighboursFromPosition(Vector3 pos)
    {
        float diagonalDistance = _tileSeparation * Mathf.Sqrt(2);

        for (int i = 0; i < _movementDirections.Length; i++)
        {
            if (i < 4 && IsDirectionWalkable(pos, _movementDirections[i], _tileSeparation))
                _walkableNeighbours.Add(pos + (_movementDirections[i] * _tileSeparation));
            else if (_diagonal && IsDirectionWalkable(pos, _movementDirections[i], diagonalDistance))
                _walkableNeighbours.Add(ParsePositionToTileCenter(pos + (_movementDirections[i] * diagonalDistance)));
            else if (!_diagonal && i > 3)
                break;
        }

        //Debug.Log("Vecinos: "+_walkableNeighbours.Count);

        return _walkableNeighbours;
    }

    private bool IsDirectionWalkable(Vector3 origin, Vector3 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);

        return hit ? false : true;
    }

    private PathfindingNode SetupAndCalculateCosts(Vector3 cell, PathfindingNode parent, Vector3 goal)
    {
        PathfindingNode node = _nodePool.RetrieveNode();
        node.position = cell;
        node.parent = parent;

        node.g = parent.g + 1;
        node.h = CalculateDistanceBetweenTwoTiles(cell, goal);
        node.f = node.g + node.h;

        node.k = parent.k + 1;

        //Debug.Log("Costes: " + node.g + ", " + node.h + ", " + node.f);

        return node;
    }

    private float CalculateDistanceBetweenTwoTiles(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }

    private List<PathfindingNode> SortListByCost(List<PathfindingNode> lista)
    {
        int pos = 0;
        PathfindingNode sel;

        while (pos < lista.Count)
        {
            sel = lista[pos];

            for (int i = pos; i < lista.Count; i++)
            {   //Buscar el Nodo con menor coste hacia N. desde pos hasta el final de la lista.
                PathfindingNode num = lista[i];
                if (num.f <= sel.f)
                    sel = num;
            }

            lista.Remove(sel);  //Intercambiamos los nodos de posición
            lista.Insert(pos, sel);

            pos++;
        }
        return lista;
    }

    private Stack<Vector3> SetupPlanFromNode(PathfindingNode node)
    {
        _path.Clear();

        if (node == null)
            return _path;

        while (node.parent != null)
        {
            PathfindingNode parent = node.parent;
            _path.Push(node.position);
            node = parent;
        }

        return _path;
    }
}
