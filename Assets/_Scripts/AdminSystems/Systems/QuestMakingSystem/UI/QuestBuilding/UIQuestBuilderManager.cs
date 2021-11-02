using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

namespace CQM.Databases.UI
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
                    var evtSys = Admin.Global.EventSystem;
                    var evt = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_cookie");
                    evt.Invoke(new ItemData(_questPieces[i].Piece.m_ParentID, 1));
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

        public void SpawnPiece(int pieceID, Canvas canvas)
        {
            QuestDB quests = Admin.Global.Database.Quests;
            var questPiece = quests.GetQuestPieceComponent<QuestPiece>(pieceID);
            var uiData = quests.GetQuestPieceComponent<UIQuestPieceData>(pieceID);

            // Destroy existing piece type
            for (int i = 0; i < _questPieces.Count; i++)
            {
                if (_questPieces[i].Piece.m_Type == questPiece.m_Type)
                {
                    // TODO: oh god
                    if (_questPieces[i].Piece.m_Type == QuestPiece.PieceType.Cookie)
                    {
                        var evtSys = Admin.Global.EventSystem;
                        var evt = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "add_cookie");
                        evt.Invoke(new ItemData(_questPieces[i].Piece.m_ParentID, 1));
                    }

                    _questPieces[i].TryToUnsocket(null);
                    Destroy(_questPieces[i].gameObject);
                    _questPieces.RemoveAt(i);
                    i--;
                    break;
                }
            }

            // TODO: oh lord
            if (questPiece.m_Type == QuestPiece.PieceType.Cookie)
            {
                var evtSys = Admin.Global.EventSystem;
                var evt = evtSys.GetCommandByName<Event<ItemData>>("inventory_sys", "remove_cookie");
                evt.Invoke(new ItemData(pieceID, 1));
            }

            // Spawn selected piece from storage in quest builder
            var piecePrefab = Admin.Global.Database.Quests.GetQuestPieceComponent<GameObject>(pieceID);
            var pieceBehaviour = Instantiate(piecePrefab, pieceSpawnPosition).GetComponent<UIQuestPieceBehaviour>();
            pieceBehaviour.Initialize(canvas, uiData, questPiece);

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