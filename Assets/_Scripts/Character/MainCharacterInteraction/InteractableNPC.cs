using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractableEntity
{
    public void OnInteract()
    {
        Debug.Log("Interacci�n con "+name);
    }

}
