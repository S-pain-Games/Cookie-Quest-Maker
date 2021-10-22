using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.QuestMaking.UI;

// Handles changing between the building and the storage
// and also communication between those 2
public class UIQuestMakerTable : MonoBehaviour
{
    [SerializeField]
    private UIQuestBuilderManager questBuilding;
    [SerializeField]
    private UIPieceStorageManager pieceStorage;

    private void OnEnable()
    {
        pieceStorage.OnUsePiece += PieceStorage_OnUsePiece;
    }

    private void OnDisable()
    {
        pieceStorage.OnUsePiece -= PieceStorage_OnUsePiece;
    }

    private void PieceStorage_OnUsePiece(QuestPiece obj)
    {
        EnableQuestBuilding();
    }

    public void EnableQuestBuilding()
    {
        pieceStorage.gameObject.SetActive(false);
        questBuilding.gameObject.SetActive(true);
    }

    public void EnablePieceStorage()
    {
        questBuilding.gameObject.SetActive(false);
        pieceStorage.gameObject.SetActive(true);
    }

    public void SpawnPieceInQuestBuilding(int pieceID)
    {
        Logg.Log("Spawn Piece " + pieceID);
    }
}
