using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.QuestMaking.UI;

// Handles changing between the building and the storage
// and also communication between those 2
public class UIQuestMakerTable : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;

    [Header("Menus")]
    [SerializeField]
    private UIStorySelectionManager _storySelection;
    [SerializeField]
    private UIQuestBuilderManager _questBuilding;
    [SerializeField]
    private UIPieceStorageManager _pieceStorage;

    private QMGameplaySystem _questMakingSys;
    private GameEventSystem _eventSystem;
    private Event<GameStateSystem.State> _changeGameStateEvt;

    private void Awake()
    {
        _questMakingSys = Admin.g_Instance.questMakerSystem;
        _eventSystem = Admin.g_Instance.gameEventSystem;
        _eventSystem.GameStateSystemMessaging.GetEvent("change_state".GetHashCode(), out _changeGameStateEvt);
    }

    private void OnEnable()
    {
        _pieceStorage.OnUsePiece += PieceStorage_OnUsePiece;
        _storySelection.OnStorySelected += StorySelection_OnStorySelected;
        _questBuilding.OnAddQuestPiece += QuestBuilding_OnAddQuestPiece;
        _questBuilding.OnRemoveQuestPiece += QuestBuilding_OnRemoveQuestPiece;
        _questBuilding.OnFinishQuest += QuestBuilding_OnFinishQuest;

        EnableStorySelection();
    }


    private void OnDisable()
    {
        _pieceStorage.OnUsePiece -= PieceStorage_OnUsePiece;
        _storySelection.OnStorySelected -= StorySelection_OnStorySelected;
        _questBuilding.OnAddQuestPiece -= QuestBuilding_OnAddQuestPiece;
        _questBuilding.OnRemoveQuestPiece -= QuestBuilding_OnRemoveQuestPiece;
        _questBuilding.OnFinishQuest -= QuestBuilding_OnFinishQuest;
    }

    private void StorySelection_OnStorySelected(int storyId)
    {
        EnableQuestBuilding();
        _questMakingSys.SelectStory(storyId);
    }

    private void QuestBuilding_OnRemoveQuestPiece(QuestPiece piece)
    {
        _questMakingSys.RemovePiece(piece);
    }

    private void QuestBuilding_OnAddQuestPiece(QuestPiece piece)
    {
        _questMakingSys.AddPiece(piece);
    }

    private void QuestBuilding_OnFinishQuest()
    {
        if (_questMakingSys.TryFinishMakingQuest())
        {
            _changeGameStateEvt.Invoke(GameStateSystem.State.Bakery);
        }
    }

    private void PieceStorage_OnUsePiece(int pieceID)
    {
        EnableQuestBuilding();
        _questBuilding.SpawnPiece(pieceID, _canvas);

    }

    public void EnableStorySelection()
    {
        _questBuilding.gameObject.SetActive(false);
        _storySelection.gameObject.SetActive(true);
    }

    public void EnableQuestBuilding()
    {
        _pieceStorage.gameObject.SetActive(false);
        _questBuilding.gameObject.SetActive(true);
    }

    public void EnablePieceStorage()
    {
        _questBuilding.gameObject.SetActive(false);
        _pieceStorage.gameObject.SetActive(true);
    }
}
