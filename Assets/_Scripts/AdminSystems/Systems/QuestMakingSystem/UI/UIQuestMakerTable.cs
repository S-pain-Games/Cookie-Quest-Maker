using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.QuestMaking.UI;

// Handles changing between the building and the storage
// and also communication between those 2
public class UIQuestMakerTable : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private UIStorySelectionManager storySelection;
    [SerializeField]
    private UIQuestBuilderManager questBuilding;
    [SerializeField]
    private UIPieceStorageManager pieceStorage;

    private QMGameplaySystem questMakingSys;

    private void Awake()
    {
        questMakingSys = Admin.g_Instance.questMakerSystem;
    }

    private void OnEnable()
    {
        pieceStorage.OnUsePiece += PieceStorage_OnUsePiece;
        storySelection.OnStorySelected += StorySelection_OnStorySelected;
        questBuilding.OnAddQuestPiece += QuestBuilding_OnAddQuestPiece;
        questBuilding.OnRemoveQuestPiece += QuestBuilding_OnRemoveQuestPiece;
    }

    private void OnDisable()
    {
        pieceStorage.OnUsePiece -= PieceStorage_OnUsePiece;
        storySelection.OnStorySelected -= StorySelection_OnStorySelected;
        questBuilding.OnAddQuestPiece -= QuestBuilding_OnAddQuestPiece;
        questBuilding.OnRemoveQuestPiece -= QuestBuilding_OnRemoveQuestPiece;
    }

    private void StorySelection_OnStorySelected(int storyId)
    {
        EnableQuestBuilding();
        questMakingSys.SelectStory(storyId);
    }

    private void QuestBuilding_OnRemoveQuestPiece(QuestPiece piece)
    {
        questMakingSys.RemovePiece(piece);
    }

    private void QuestBuilding_OnAddQuestPiece(QuestPiece piece)
    {
        questMakingSys.AddPiece(piece);
    }

    private void PieceStorage_OnUsePiece(int pieceID)
    {
        EnableQuestBuilding();

        // Spawn selected piece from storage in quest builder
        var piecePrefab = Admin.g_Instance.questDB.m_QuestBuildingPiecesPrefabs[pieceID];
        var pieceBehaviour = Instantiate(piecePrefab, questBuilding.pieceSpawnPosition).GetComponent<UIQuestPieceBehaviour>();
        pieceBehaviour.Initialize(canvas, pieceID);
    }

    public void EnableStorySelection()
    {
        questBuilding.gameObject.SetActive(false);
        storySelection.gameObject.SetActive(true);
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
}
