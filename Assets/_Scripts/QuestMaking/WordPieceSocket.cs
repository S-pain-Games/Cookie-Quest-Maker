using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPieceSocket : MonoBehaviour
{
    public WordPiece.WordType requiredType = WordPiece.WordType.Action;
    public bool filled = false;
    public WordPiece currentPiece;
}
