using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBasedAStarPathfinder : ITileBasedPathfinder
{
    private float _tileSeparation;
    private float _tileOffset;
    private bool _diagonal;

    public TileBasedAStarPathfinder(float tileSeparation, float tileOffset)
    {
        _tileSeparation = tileSeparation;
        _tileOffset = tileOffset;
    }

    public Stack<Vector3> GenerateTiledPathTowardsPosition(Vector3 originPosition, Vector3 targetPosition, bool diagonal)
    {
        _diagonal = diagonal;
        targetPosition = ParsePositionToTileCenter(targetPosition);
        PathfindingNode endNode = GeneratePathTowardsPosition(originPosition, targetPosition);

        return SetupPlanFromNode(endNode);
    }


    private Vector3 ParsePositionToTileCenter(Vector3 position)
    {
        return new Vector3(Mathf.Floor(position.x) + _tileOffset, Mathf.Floor(position.y) + _tileOffset);
    }

    private PathfindingNode GeneratePathTowardsPosition(Vector3 start, Vector3 goal)
    {
        List<PathfindingNode> open = new List<PathfindingNode>();
        List<Vector3> closed = new List<Vector3>();

        PathfindingNode firstNode = new PathfindingNode(start);
        open.Add(firstNode);

        int tries = 1000;

        while (open.Count > 0 && tries > 0)
        {
            PathfindingNode node = open[0];
            open.Remove(node);
            closed.Add(node.position);


            Debug.Log("open: "+open.Count+", closed: "+closed.Count+", intentos: "+tries);
            //Debug.Log(node.f);

            //Debug.Log(node.cell + " contra " + goal);

            if (node.position == goal)
                return node;

            List<Vector3> childs = new List<Vector3>();
            childs.AddRange(GetWalkableNeighboursFromPosition(node.position));

            Debug.Log("Tanda de hijos");

            foreach (Vector3 child in childs)
            {
                //Comprobar si ya hemos pasado por dicho hijo
                if (IsAlreadyInClosedList(child, closed))
                {
                    Debug.Log("Repe");
                    continue;
                }

                Debug.Log("Tú pasas: "+child);

                open.Add(SetupAndCalculateCosts(child, node, goal));
            }

            open = SortListByCost(open);

            tries--;
        }

        Debug.Log("NOPE");

        return null;
    }

    private bool IsAlreadyInClosedList(Vector3 v, List<Vector3> closed)
    {
        foreach (Vector3 p in closed)
        {
            //Debug.Log(p + " == " + v+ ", Dist: "+Vector3.Distance(v, p));

            //if (Vector3.Distance(v, p) < 1f)
            //    return true;

            if (p == v)
                return true;
        }

        return false;
    }

    private List<Vector3> GetWalkableNeighboursFromPosition(Vector3 pos)
    {
        List<Vector3> walkableNeighbours = new List<Vector3>();

        if (IsDirectionWalkable(pos, Vector2.up, _tileSeparation))
            walkableNeighbours.Add(pos + (Vector3.up * _tileSeparation));

        if (IsDirectionWalkable(pos, Vector2.down, _tileSeparation))
            walkableNeighbours.Add(pos + (Vector3.down * _tileSeparation));

        if (IsDirectionWalkable(pos, Vector2.left, _tileSeparation))
            walkableNeighbours.Add(pos + (Vector3.left * _tileSeparation));

        if (IsDirectionWalkable(pos, Vector2.right, _tileSeparation))
            walkableNeighbours.Add(pos + (Vector3.right * _tileSeparation));


        if (_diagonal)
        {
            float diagonalDistance = _tileSeparation * Mathf.Sqrt(2);

            if (IsDirectionWalkable(pos, new Vector2(0.5f, 0.5f), diagonalDistance))
                walkableNeighbours.Add(ParsePositionToTileCenter(pos + (new Vector3(0.5f, 0.5f) * diagonalDistance)));
            
            if (IsDirectionWalkable(pos, new Vector2(0.5f, -0.5f), diagonalDistance))
                walkableNeighbours.Add(ParsePositionToTileCenter(pos + (new Vector3(0.5f, -0.5f) * diagonalDistance)));

            if (IsDirectionWalkable(pos, new Vector2(-0.5f, 0.5f), diagonalDistance))
                walkableNeighbours.Add(ParsePositionToTileCenter(pos + (new Vector3(-0.5f, 0.5f) * diagonalDistance)));

            if (IsDirectionWalkable(pos, new Vector2(-0.5f, -0.5f), diagonalDistance))
                walkableNeighbours.Add(ParsePositionToTileCenter(pos + (new Vector3(-0.5f, -0.5f) * diagonalDistance)));
        }
        
        
        //Debug.Log("vecinos: " + walkableNeighbours.Count);

        return walkableNeighbours;
    }

    private bool IsDirectionWalkable(Vector3 origin, Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);

        return hit ? false : true;
    }

    private PathfindingNode SetupAndCalculateCosts(Vector3 cell, PathfindingNode parent, Vector3 goal)
    {
        PathfindingNode node = new PathfindingNode(cell, parent);
        node.g = parent.g + 1;
        node.h = CalculateDistanceBetweenTwoTiles(cell, goal);
        node.f = node.g + node.h;

        //Debug.Log("Costes: " + node.g + ", " + node.h + ", " + node.f);

        return node;
    }

    /*
    private float CalculateManhattanDistanceBetweenTwoTiles(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(((a.x - b.x) / _tileSeparation) + ((a.y - b.y) / _tileSeparation));
    }
    */

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
        Stack<Vector3> path = new Stack<Vector3>();

        if (node == null)
            return path;


        while (node.parent != null)
        {
            path.Push(node.position);
            node = node.parent;
        }

        return path;
    }
}
