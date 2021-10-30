using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode
{
    public Vector3 position { get; set; }
    public float g { get; set; }
    public float h { get; set; }
    public float f { get; set; }

    public int k { get; set; }

    public PathfindingNode parent { get; set; }

    public PathfindingNode()
    {
        this.parent = null;

        f = 0;
        g = 1;
        h = 0;

        k = 1;
    }

    public PathfindingNode(Vector3 position)
    {
        this.position = position;
        this.parent = null;

        f = 0;
        g = 1;
        h = 0;

        k = 1;
    }

    public PathfindingNode(Vector3 position, PathfindingNode parent)
    {
        this.position = position;
        this.parent = parent;

        f = 0;
        g = 1;
        h = 0;

        k = parent.k++;
    }

    public void ClearInfo()
    {
        this.position = Vector3.zero;
        this.parent = null;
        f = 0;
        g = 1;
        h = 0;

        k = 1;
    }

}

