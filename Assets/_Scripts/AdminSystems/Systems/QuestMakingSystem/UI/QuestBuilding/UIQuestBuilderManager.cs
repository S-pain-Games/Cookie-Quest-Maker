using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

namespace CQM.QuestMaking.UI
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
                Destroy(_questPieces[i].gameObject); // Pooling?
            }
            _questPieces.Clear();
        }

        public void SpawnPiece(int pieceID, Canvas canvas)
        {
            // Spawn selected piece from storage in quest builder
            var piecePrefab = Admin.g_Instance.questDB.m_QuestBuildingPiecesPrefabs[pieceID];
            var pieceBehaviour = Instantiate(piecePrefab, pieceSpawnPosition).GetComponent<UIQuestPieceBehaviour>();
            pieceBehaviour.Initialize(canvas, pieceID);

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