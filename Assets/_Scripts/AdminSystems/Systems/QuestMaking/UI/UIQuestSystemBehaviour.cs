using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class UIQuestSystemBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<PieceSocketBehaviour> _sockets = new List<PieceSocketBehaviour>();
    [SerializeField]
    private List<UIQuestPieceBehaviour> _questPieces = new List<UIQuestPieceBehaviour>();

    private QuestMakerSystem _questMakerSystem;

    private void Awake()
    {
        _questMakerSystem = Admin.g_Instance.questMakerSystem;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _sockets.Count; i++)
        {
            _sockets[i].OnPieceAdded += OnPieceAddedHandle;
            _sockets[i].OnPieceRemoved += OnPieceRemovedHandle;
        }

        for (int i = 0; i < _questPieces.Count; i++)
        {
            _questPieces[i].OnSelected += OnPieceSelectedHandle;
            _questPieces[i].OnUnselect += OnPieceUnselectedHandle;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _sockets.Count; i++)
        {
            _sockets[i].OnPieceAdded -= OnPieceAddedHandle;
            _sockets[i].OnPieceRemoved -= OnPieceRemovedHandle;
        }

        for (int i = 0; i < _questPieces.Count; i++)
        {
            _questPieces[i].OnSelected -= OnPieceSelectedHandle;
            _questPieces[i].OnUnselect -= OnPieceUnselectedHandle;
        }
    }

    private void OnPieceAddedHandle(QuestPiece piece)
    {
        _questMakerSystem.AddPiece(piece);
    }

    private void OnPieceRemovedHandle(QuestPiece piece)
    {
        _questMakerSystem.RemovePiece(piece);
    }

    private void OnPieceSelectedHandle(UIQuestPieceBehaviour uiPiece)
    {
        var matchingSocket = _sockets.Find((s) => { return s.RequiredType == uiPiece.Piece.Type; });
        if (!matchingSocket.Filled)
            matchingSocket.OnMatchingPieceSelectedHandle();
    }

    private void OnPieceUnselectedHandle(UIQuestPieceBehaviour uiPiece)
    {
        var matchingSocket = _sockets.Find((s) => { return s.RequiredType == uiPiece.Piece.Type; });
        if (!matchingSocket.Filled)
            matchingSocket.OnMatchingPieceUnselectedHandle();
    }
}
