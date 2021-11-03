using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
using CQM.Components;
using CQM.Databases;

namespace CQM.Gameplay
{
    public class UIQuestBuilderManager : MonoBehaviour
    {
        public event Action<QuestPiece> OnAddQuestPiece;
        public event Action<QuestPiece> OnRemoveQuestPiece;
        public event Action OnFinishQuest;

        public RectTransform pieceSpawnPosition;
        [SerializeField]
        private Button finishQuestButton;

        [SerializeField]
        private List<UIPieceSocketBehaviour> _sockets = new List<UIPieceSocketBehaviour>();
        [SerializeField]
        private List<UIQuestPieceBehaviour> _questPieces = new List<UIQuestPieceBehaviour>();

        private Event<ItemData> _addPieceCmd;
        private Event<ItemData> _removePieceCmd;

        private EventSys _evtSys;
        private Event<int> _onUsePiece;
        private Canvas _canvas;

        public void Initialize(EventSys evtSys, Canvas canvas)
        {
            _evtSys = evtSys;
            _canvas = canvas;
        }

        public void AdquireUIEvents()
        {
            _evtSys.GetEvent("on_use_piece".GetHashCode(), out _onUsePiece);
            _onUsePiece.OnInvoked += SpawnPiece;
        }

        private void Awake()
        {
            var evtSys = Admin.Global.EventSystem;
            _addPieceCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_piece");
            _removePieceCmd = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "remove_piece");
        }

        private void OnEnable()
        {
            // Register Sockets Events
            for (int i = 0; i < _sockets.Count; i++)
            {
                _sockets[i].OnPieceSocketed += OnPieceSocketedHandle;
                _sockets[i].OnPieceUnsocketed += OnPieceUnsocketedHandle;
            }

            // Register Pieces Events
            for (int i = 0; i < _questPieces.Count; i++)
            {
                InitializeQuestPieceBehaviour(_questPieces[i]);
            }

            finishQuestButton.onClick.AddListener(OnFinishQuestButtonClicked);
        }

        private void OnDisable()
        {
            UnregisterPiecesAndSocketsEvents();
            finishQuestButton.onClick.RemoveListener(OnFinishQuestButtonClicked);
        }

        private void UnregisterPiecesAndSocketsEvents()
        {
            // Unregister Socket Events
            for (int i = 0; i < _sockets.Count; i++)
            {
                _sockets[i].OnPieceSocketed -= OnPieceSocketedHandle;
                _sockets[i].OnPieceUnsocketed -= OnPieceUnsocketedHandle;
            }

            // Unregister Pieces Events
            for (int i = 0; i < _questPieces.Count; i++)
            {
                _questPieces[i].OnSelected -= OnPieceSelectedBroadcast;
                _questPieces[i].OnUnselect -= OnPieceUnselectedBroadcast;
            }
        }

        public void ClearAllPieces()
        {
            UnregisterPiecesAndSocketsEvents();
            for (int i = 0; i < _sockets.Count; i++)
            {
                _sockets[i].Clear();
            }
            for (int i = 0; i < _questPieces.Count; i++)
            {
                // TODO: >:[
                if (_questPieces[i].Piece.m_Type == QuestPiece.PieceType.Cookie)
                {
                    _addPieceCmd.Invoke(new ItemData(_questPieces[i].Piece.m_ParentID, 1));
                }

                Destroy(_questPieces[i].gameObject); // Pooling?
            }
            _questPieces.Clear();
        }

        public void ConsumeQuest()
        {
            UnregisterPiecesAndSocketsEvents();
            for (int i = 0; i < _sockets.Count; i++)
            {
                _sockets[i].Clear();
            }
            for (int i = 0; i < _questPieces.Count; i++)
            {
                Destroy(_questPieces[i].gameObject); // Pooling?
            }
            _questPieces.Clear();
        }

        public void SpawnPiece(int pieceID)
        {
            PiecesDB quests = Admin.Global.Database.Pieces;
            var questPiece = quests.GetQuestPieceComponent<QuestPiece>(pieceID);
            var uiData = quests.GetQuestPieceComponent<UIQuestPieceData>(pieceID);

            // Destroy existing piece type
            for (int i = 0; i < _questPieces.Count; i++)
            {
                if (_questPieces[i].Piece.m_Type == questPiece.m_Type)
                {
                    _addPieceCmd.Invoke(new ItemData(_questPieces[i].Piece.m_ParentID, 1));

                    _questPieces[i].TryToUnsocket(null);
                    Destroy(_questPieces[i].gameObject);
                    _questPieces.RemoveAt(i);
                    break;
                }
            }

            _removePieceCmd.Invoke(new ItemData(pieceID, 1));

            // Spawn selected piece from storage in quest builder
            var piecePrefab = Admin.Global.Database.Pieces.GetQuestPieceComponent<GameObject>(pieceID);
            var pieceBehaviour = Instantiate(piecePrefab, pieceSpawnPosition).GetComponent<UIQuestPieceBehaviour>();
            pieceBehaviour.Initialize(_canvas, uiData, questPiece);

            _questPieces.Add(pieceBehaviour);
            InitializeQuestPieceBehaviour(pieceBehaviour);
        }

        private void OnFinishQuestButtonClicked()
        {
            OnFinishQuest?.Invoke();
        }

        private void OnPieceSocketedHandle(QuestPiece piece)
        {
            OnAddQuestPiece?.Invoke(piece);
        }

        private void OnPieceUnsocketedHandle(QuestPiece piece)
        {
            OnRemoveQuestPiece?.Invoke(piece);
        }

        private void OnPieceSelectedBroadcast(UIQuestPieceBehaviour uiPiece)
        {
            var matchingSocket = _sockets.Find((s) => { return s.RequiredType == uiPiece.Piece.m_Type; });
            if (!matchingSocket.m_Filled)
                matchingSocket.OnMatchingPieceSelectedHandle();
        }

        private void OnPieceUnselectedBroadcast(UIQuestPieceBehaviour uiPiece)
        {
            var matchingSocket = _sockets.Find((s) => { return s.RequiredType == uiPiece.Piece.m_Type; });
            if (!matchingSocket.m_Filled)
                matchingSocket.OnMatchingPieceUnselectedHandle();
        }

        private void InitializeQuestPieceBehaviour(UIQuestPieceBehaviour pieceBehaviour)
        {
            pieceBehaviour.OnSelected += OnPieceSelectedBroadcast;
            pieceBehaviour.OnUnselect += OnPieceUnselectedBroadcast;
        }
    }
}