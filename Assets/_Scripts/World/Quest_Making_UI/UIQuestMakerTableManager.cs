using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            _questBuilding.OnAddQuestPiece += AddPieceToQuest;
            _questBuilding.OnRemoveQuestPiece += RemovePieceFromQuest;
            _questBuilding.OnFinishQuest += FinishQuestMaking;
            _questBuilding.OnExit += EnableStorySelection;
            _questBuilding.OnOpenStorage += EnablePieceStorage;

            _pieceStorage.OnSelectPieceFromStorage += TakePieceOutOfStorage;
            _pieceStorage.OnExit += EnableQuestBuilding;

            _storySelection.OnStorySelected += SelectStory;
            _storySelection.OnExit += ExitTable;

            EnableStorySelection();
        }

        private void OnDisable()
        {
            _questBuilding.OnAddQuestPiece -= AddPieceToQuest;
            _questBuilding.OnRemoveQuestPiece -= RemovePieceFromQuest;
            _questBuilding.OnFinishQuest -= FinishQuestMaking;
            _questBuilding.OnExit -= EnableStorySelection;
            _questBuilding.OnOpenStorage -= EnablePieceStorage;

            _pieceStorage.OnSelectPieceFromStorage -= TakePieceOutOfStorage;
            _pieceStorage.OnExit -= EnableQuestBuilding;

            _storySelection.OnStorySelected -= SelectStory;
            _storySelection.OnExit -= ExitTable;
        }


        private void SelectStory(ID storyId)
        {
            EnableQuestBuilding();
            m_State.m_SelectedStoryID = storyId;
            _questMakingSys.SelectStory(storyId);


            // Clear all the pieces only if a different story has been selected
            var s = m_State;
            if (s.m_PreviousStoryID != s.m_SelectedStoryID)
            {
                _questBuilding.ReturnAllPiecesToStorage();
            }
            s.m_PreviousStoryID = s.m_SelectedStoryID;
        }

        private void RemovePieceFromQuest(QuestPieceFunctionalComponent piece)
        {
            m_State.m_PiecesInUse.Remove(piece);
        }

        private void AddPieceToQuest(QuestPieceFunctionalComponent piece)
        {
            m_State.m_PiecesInUse.Add(piece);
        }

        private void FinishQuestMaking()
        {
            for (int i = 0; i < m_State.m_PiecesInUse.Count; i++)
            {
                var p = m_State.m_PiecesInUse[i];
                _questMakingSys.AddPiece(p);
            }

            if (_questMakingSys.TryFinishMakingQuest())
            {
                for (int i = 0; i < m_State.m_PiecesInUse.Count; i++)
                {
                    var p = m_State.m_PiecesInUse[i];
                    _questMakingSys.RemovePiece(p);
                }

                _questBuilding.ConsumeAllPieces();
                _toggleQuestMakingUI.Invoke();
            }
            else
            {
                for (int i = 0; i < m_State.m_PiecesInUse.Count; i++)
                {
                    var p = m_State.m_PiecesInUse[i];
                    _questMakingSys.RemovePiece(p);
                }
            }
        }

        private void ExitTable()
        {
            _toggleQuestMakingUI.Invoke();
        }

        private void TakePieceOutOfStorage(ID pieceID)
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
        public ID m_PreviousStoryID;
        public ID m_SelectedStoryID;
        public List<QuestPieceFunctionalComponent> m_PiecesInUse = new List<QuestPieceFunctionalComponent>();
    }
}