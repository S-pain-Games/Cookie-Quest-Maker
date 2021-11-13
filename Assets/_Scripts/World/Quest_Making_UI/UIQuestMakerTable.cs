using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.Databases.UI;
using CQM.Components;

namespace CQM.Gameplay
{
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

        private QuestMakingSystem _questMakingSys;
        private EventVoid _toggleQuestMakingUI;

        public EventSys m_localUIEventSystem = new EventSys();

        private Event<ID> _onStorySelectedCallback;
        private Event<ID> _onUsePiece;

        private void Awake()
        {
            _questMakingSys = Admin.Global.Systems.m_QuestMakerSystem;
            var evtSys = Admin.Global.EventSystem;
            _toggleQuestMakingUI = evtSys.GetCommandByName<EventVoid>("ui_sys", "toggle_quest_making");

            // Initialize all the UI Components (this also registers their events)
            _storySelection.Initialize(m_localUIEventSystem);
            _pieceStorage.Initialize(m_localUIEventSystem);
            _questBuilding.Initialize(m_localUIEventSystem, _canvas);

            // After all the UI Components have registered their events
            // every component that needs them adquires them
            _pieceStorage.AdquireUIEvents();
            _questBuilding.AdquireUIEvents();

            m_localUIEventSystem.GetEvent(new ID("on_story_selected"), out _onStorySelectedCallback);
            m_localUIEventSystem.GetEvent(new ID("on_use_piece"), out _onUsePiece);
        }

        private void OnEnable()
        {
            _questBuilding.OnAddQuestPiece += QuestBuilding_OnAddQuestPiece;
            _questBuilding.OnRemoveQuestPiece += QuestBuilding_OnRemoveQuestPiece;
            _questBuilding.OnFinishQuest += QuestBuilding_OnFinishQuest;

            _onStorySelectedCallback.OnInvoked += OnStorySelected;
            _onUsePiece.OnInvoked += OnPieceSelected;

            EnableStorySelection();
        }

        private void OnDisable()
        {
            _questBuilding.OnAddQuestPiece -= QuestBuilding_OnAddQuestPiece;
            _questBuilding.OnRemoveQuestPiece -= QuestBuilding_OnRemoveQuestPiece;
            _questBuilding.OnFinishQuest -= QuestBuilding_OnFinishQuest;

            _onUsePiece.OnInvoked -= OnPieceSelected;
            _onStorySelectedCallback.OnInvoked -= OnStorySelected;
        }

        private void OnStorySelected(ID storyId)
        {
            EnableQuestBuilding();
            _questMakingSys.SelectStory(storyId);
        }

        private void QuestBuilding_OnRemoveQuestPiece(QuestPieceFunctionalComponent piece)
        {
            _questMakingSys.RemovePiece(piece);
        }

        private void QuestBuilding_OnAddQuestPiece(QuestPieceFunctionalComponent piece)
        {
            _questMakingSys.AddPiece(piece);
        }

        private void QuestBuilding_OnFinishQuest()
        {
            if (_questMakingSys.TryFinishMakingQuest())
            {
                _questBuilding.ConsumeQuest();
                _toggleQuestMakingUI.Invoke();
            }
        }

        private void OnPieceSelected(ID pieceID)
        {
            EnableQuestBuilding();
        }

        public void EnableStorySelection()
        {
            _questBuilding.ClearAllPieces();
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
}