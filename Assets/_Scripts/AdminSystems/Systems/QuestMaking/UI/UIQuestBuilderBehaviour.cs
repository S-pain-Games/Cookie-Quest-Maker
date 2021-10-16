using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class UIQuestBuilderBehaviour : MonoBehaviour
{
    [Header("Broadcasting To")]
    [SerializeField]
    private VoidEventHandle _startMakingQuest;
    [SerializeField]
    private QuestPieceEventHandle _addPiece;
    [SerializeField]
    private QuestPieceEventHandle _removePiece;
    [SerializeField]
    private VoidEventHandle _finishMakingQuest;

    private List<PieceSocketBehaviour> _sockets = new List<PieceSocketBehaviour>();

    private void Awake()
    {
        GetComponentsInChildren(_sockets);
    }

    private void OnEnable()
    {
        _startMakingQuest.Invoke(gameObject);

        for (int i = 0; i < _sockets.Count; i++)
        {
            _sockets[i].OnPieceAdded += OnPieceAdded;
            _sockets[i].OnPieceRemoved += OnPieceRemoved;
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
        _addPiece.Invoke(piece);
    }

    private void OnPieceRemoved(QuestPiece piece)
    {
        _removePiece.Invoke(piece);
    }
}
