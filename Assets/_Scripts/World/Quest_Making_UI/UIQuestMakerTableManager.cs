using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.Databases.UI;
using CQM.Components;


namespace CQM.UI.QuestMakingTable
{
    // Handles changing between the building and the storage
    // and also communication between those 2
    public class UIQuestMakerTableManager : MonoBehaviour
    {
        [SerializeField] private QuestMakerTableState m_State = new QuestMakerTableState();

        [SerializeField] private Canvas _canvas;

        [Header("Menus")]
        [SerializeField] private UIStorySelectionManager _storySelection;
        [SerializeField] private UIQuestBuilderManager _questBuilding;
        [SerializeField] private UIPieceStorageManager _pieceStorage;

        private QuestMakingSystem _questMakingSys; // Should use events
        private EventVoid _toggleQuestMakingUI;

        private void Awake()
        {
            _questMakingSys = Admin.Global.Systems.m_QuestMakerSystem;
            var evtSys = Admin.Global.EventSystem;
            _toggleQuestMakingUI = evtSys.GetCommandByName<EventVoid>("ui_sys", "toggle_quest_making");

            _pieceStorage.Initialize(m_State);

            _questBuilding.Initialize(m_State, _canvas);
        }

        private void OnEnable()
        {
            _questBuilding.OnAddQuestPiece += AddPieceToQuestHandle;
            _questBuilding.OnRemoveQuestPiece += RemovePieceFromQuestHandle;
            _questBuilding.OnFinishQuest += FinishQuestHandle;
            _questBuilding.OnExit += EnableStorySelection;
            _questBuilding.OnOpenStorage += EnablePieceStorage;

            _pieceStorage.OnPickPiece += PieceTakenOutOfStorageHandle;
            _pieceStorage.OnExit += EnableQuestBuilding;

            _storySelection.OnStorySelected += StorySelection_OnStorySelected;
            _storySelection.OnExit += ExitTableHandle;

            EnableStorySelection();
        }

        private void OnDisable()
        {
            _questBuilding.OnAddQuestPiece -= AddPieceToQuestHandle;
            _questBuilding.OnRemoveQuestPiece -= RemovePieceFromQuestHandle;
            _questBuilding.OnFinishQuest -= FinishQuestHandle;
            _questBuilding.OnExit -= EnableStorySelection;
            _questBuilding.OnOpenStorage -= EnablePieceStorage;

            _pieceStorage.OnPickPiece -= PieceTakenOutOfStorageHandle;
            _pieceStorage.OnExit -= EnableQuestBuilding;

            _storySelection.OnStorySelected -= StorySelection_OnStorySelected;
            _storySelection.OnExit -= ExitTableHandle;
        }


        private void StorySelection_OnStorySelected(ID storyId)
        {
            EnableQuestBuilding();
            m_State.m_SelectedStoryID = storyId;
            _questMakingSys.SelectStory(storyId);
        }


        private void RemovePieceFromQuestHandle(QuestPieceFunctionalComponent piece)
        {
            m_State.m_PiecesInUse.Remove(piece.m_ID);
            _questMakingSys.RemovePiece(piece);
        }

        private void AddPieceToQuestHandle(QuestPieceFunctionalComponent piece)
        {
            m_State.m_PiecesInUse.Add(piece.m_ID);
            _questMakingSys.AddPiece(piece);
        }

        private void FinishQuestHandle()
        {
            if (_questMakingSys.TryFinishMakingQuest())
            {
                _questBuilding.ConsumeQuest();
                _toggleQuestMakingUI.Invoke();
            }
        }

        private void ExitTableHandle()
        {
            _toggleQuestMakingUI.Invoke();
        }

        private void PieceTakenOutOfStorageHandle(ID pieceID)
        {
            EnableQuestBuilding();
            _questBuilding.SpawnPiece(pieceID);
        }


        // Enable / Disable UI's

        public void EnableStorySelection()
        {
            _questBuilding.gameObject.SetActive(false);
            _storySelection.gameObject.SetActive(true);
        }

        public void EnableQuestBuilding()
        {
            _storySelection.gameObject.SetActive(false);
            _pieceStorage.gameObject.SetActive(false);
            _questBuilding.gameObject.SetActive(true);
        }

        public void EnablePieceStorage()
        {
            _questBuilding.gameObject.SetActive(false);
            _pieceStorage.gameObject.SetActive(true);
        }
    }


    [System.Serializable]
    public class QuestMakerTableState
    {
        public ID m_SelectedStoryID;
        public List<ID> m_PiecesInUse = new List<ID>();
    }
}