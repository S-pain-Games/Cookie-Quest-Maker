using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
}
