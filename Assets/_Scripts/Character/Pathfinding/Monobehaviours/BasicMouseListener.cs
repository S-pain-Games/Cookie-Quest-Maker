using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMouseListener : MonoBehaviour
{
    [SerializeField]
    private TilebasedPathfindingAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _agent.MoveTowardsTargetReceiver(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
