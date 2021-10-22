using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractableEntity
{

    public void OnInteract()
    {
        Debug.Log("INTERACCIÓN CON HORNO");
    }
}
