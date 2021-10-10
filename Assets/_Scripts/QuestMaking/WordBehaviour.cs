using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Debugging;

public class WordBehaviour : MonoBehaviour
{
    public WordPiece Piece { get => _piece; }

    [SerializeField] private WordPiece _piece;
}
