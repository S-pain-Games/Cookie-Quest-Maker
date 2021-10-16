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
            _sockets[i].OnPieceAdded += OnPieceAdded;
            _sockets[i].OnPieceRemoved += OnPieceRemoved;
        }

        for (int i = 0; i < _questPieces.Count; i++)
        {
            _questPieces[i].OnSelected += OnPieceSelected;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _sockets.Count; i++)
        {
            _sockets[i].OnPieceAdded -= OnPieceAdded;
            _sockets[i].OnPieceRemoved -= OnPieceRemoved;
        }


    }

    private void OnPieceAdded(QuestPiece piece)
    {
        _questMakerSystem.AddPiece(piece);
    }

    private void OnPieceRemoved(QuestPiece piece)
    {
        _questMakerSystem.RemovePiece(piece);
    }

    private void OnPieceSelected(QuestPiece piece)
    {

    }
}
