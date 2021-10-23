using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CQM.QuestMaking.UI
{
    public class UIQuestBuilderManager : MonoBehaviour
    {
        public RectTransform pieceSpawnPosition;

        [SerializeField]
        private List<UIPieceSocketBehaviour> _sockets = new List<UIPieceSocketBehaviour>();
        [SerializeField]
        private List<UIQuestPieceBehaviour> _questPieces = new List<UIQuestPieceBehaviour>();

        private QMGameplaySystem _questMakerSystem;

        private void Awake()
        {
            _questMakerSystem = Admin.g_Instance.questMakerSystem;
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
                _questPieces[i].OnSelected += OnPieceSelectedBroadcast;
                _questPieces[i].OnUnselect += OnPieceUnselectedBroadcast;
            }
        }

        private void OnDisable()
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

        [MethodButton]
        private void GetSocketsAndPieces()
        {
            GetComponentsInChildren(true, _sockets);
            GetComponentsInChildren(true, _questPieces);
        }

        private void OnPieceSocketedHandle(QuestPiece piece)
        {
            _questMakerSystem.AddPiece(piece);
        }

        private void OnPieceUnsocketedHandle(QuestPiece piece)
        {
            _questMakerSystem.RemovePiece(piece);
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
    }
}