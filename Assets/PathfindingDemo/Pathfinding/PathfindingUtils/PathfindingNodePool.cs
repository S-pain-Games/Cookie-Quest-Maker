using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNodePool
{
    private Stack<PathfindingNode> _nodePool;
    private List<PathfindingNode> _nodeReferences;

    public PathfindingNodePool(int startingSize)
    {
        _nodePool = new Stack<PathfindingNode>();
        _nodeReferences = new List<PathfindingNode>();

        SetupPathfindingNodePool(startingSize);
    }

    private void SetupPathfindingNodePool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            PathfindingNode n = new PathfindingNode();
            _nodePool.Push(n);
            _nodeReferences.Add(n);
        }
    }

    public PathfindingNode RetrieveNode()
    {
        if (_nodePool.Count > 0)
            return _nodePool.Pop();
        else
        {
            PathfindingNode n = new PathfindingNode();
            _nodeReferences.Add(n);
            return n;
        }

    }

    public void CleanAndRefillNodePool()
    {
        Debug.Log("Antes de limpiar: " + _nodePool.Count);
        _nodePool.Clear();

        foreach (PathfindingNode n in _nodeReferences)
        {
            n.ClearInfo();
            _nodePool.Push(n);
        }
        Debug.Log("Después de limpiar: " + _nodePool.Count);
    }
}
