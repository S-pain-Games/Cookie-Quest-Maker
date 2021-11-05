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
        private GameEventSystem evtSys;
        private Event<GameStateSystem.State> _changeGameStateCmd;

        public EventSys m_evtSys = new EventSys();

        private Event<int> _onStorySelectedCallback;
        private Event<int> _onUsePiece;

        private void Awake()
        {
            _questMakingSys = Admin.Global.Systems.m_QuestMakerSystem;
            evtSys = Admin.Global.EventSystem;
            _changeGameStateCmd = evtSys.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state");

            _storySelection.Initialize(m_evtSys);
            _pieceStorage.Initialize(m_evtSys);
            _questBuilding.Initialize(m_evtSys, _canvas);

            _pieceStorage.AdquireUIEvents();
            _questBuilding.AdquireUIEvents();

            m_evtSys.GetEvent("on_story_selected".GetHashCode(), out _onStorySelectedCallback);
            m_evtSys.GetEvent("on_use_piece".GetHashCode(), out _onUsePiece);
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

        private void OnStorySelected(int storyId)
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
                _changeGameStateCmd.Invoke(GameStateSystem.State.Bakery);
            }
        }

        private void OnPieceSelected(int pieceID)
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