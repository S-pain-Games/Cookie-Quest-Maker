using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAllocator: MonoBehaviour
{
    private Vector3[] _spawnPositions;

    private List<Vector3> _availableSpawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        SetupSpawnPositionList();
        SetupSupportLists();
    }

    private void SetupSpawnPositionList()
    {
        Transform t = GameObject.Find("Npc spawn locations").transform;
        _spawnPositions = new Vector3[t.childCount];

        for (int i = 0; i < t.childCount; i++)
        {
            _spawnPositions[i] = t.GetChild(i).position;
        }
    }

    private void SetupSupportLists()
    {
        _availableSpawnPositions = new List<Vector3>();
        _availableSpawnPositions.AddRange(_spawnPositions);
    }

    public Vector3 GetNextAvailablePosition()
    {
        Vector3 selectedPosition = _availableSpawnPositions[0];
        _availableSpawnPositions.Remove(selectedPosition);

        return selectedPosition;
    }

    public Vector3 GetRandomAvailablePosition()
    {
        Vector3 selectedPosition = _availableSpawnPositions[Random.Range(0, _availableSpawnPositions.Count - 1)];
        _availableSpawnPositions.Remove(selectedPosition);

        return selectedPosition;
    }


    public void ResetLists()
    {
        _availableSpawnPositions.Clear();
        _availableSpawnPositions.AddRange(_spawnPositions);
    }
}
