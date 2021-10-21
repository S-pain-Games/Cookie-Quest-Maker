using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseListener : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    

    private void OnClick()
    {
        //Comprobar si se ha clickeado en en el escenario caminable:

        Debug.Log("He clickado");

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("He clickado en algo");
            if (hit.transform.name == "walkable")
            {
                Debug.Log("Allé que voy");
                agent.SetDestination(hit.point);
            }
        }
    }


}
