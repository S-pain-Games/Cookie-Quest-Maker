using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFurnace : MonoBehaviour, IInteractableEntity
{

    public void OnInteract()
    {
        Debug.Log("Interacción con horno");
    }

}
