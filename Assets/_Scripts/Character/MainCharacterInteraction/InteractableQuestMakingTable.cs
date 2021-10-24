using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableQuestMakingTable : MonoBehaviour, IInteractableEntity
{

    public void OnInteract()
    {
        Debug.Log("Interacción con mesa de misiones");

        Admin.g_Instance.gameStateSystem.SetState(GameStateSystem.State.QuestMaking);
    }
}
