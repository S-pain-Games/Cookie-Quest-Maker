using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles changing between the building and the storage
// and also communication between those 2
public class UIQuestMakerTable : MonoBehaviour
{
    [SerializeField]
    private GameObject questBuilding;
    [SerializeField]
    private GameObject pieceStorage;

    public void EnableQuestBuilding()
    {
        pieceStorage.SetActive(false);
        questBuilding.SetActive(true);
    }

    public void EnablePieceStorage()
    {
        questBuilding.SetActive(false);
        pieceStorage.SetActive(true);
    }

    public void SpawnPieceInQuestBuilding(int pieceID)
    {
        Logg.Log("Spawn Piece " + pieceID);
    }
}
