using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileBasedPathfinder
{
    Stack<Vector3> GenerateTiledPathTowardsPosition(Vector3 originPosition, Vector3 targetPosition, bool diagonalMovement);
}
