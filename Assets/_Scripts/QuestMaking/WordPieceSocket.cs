using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPieceSocket : MonoBehaviour
{
    public bool Filled { get => _filled; }

    private bool _filled;
    private WordPiece _currentPiece;
}
