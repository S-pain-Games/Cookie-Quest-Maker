using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debugging;

[System.Serializable]
public class PhraseBuilderBehaviour : MonoBehaviour
{
    [SerializeField] private WordSocketBehaviour _actionSocket;
    [SerializeField] private WordSocketBehaviour _targetSocket;

    [MethodButton]
    private void LogCurrentPhrase()
    {
        if (_actionSocket.filled && _targetSocket.filled)
        {
            Logg.Log($"[{_actionSocket.piece.WordText}][{_targetSocket.piece.WordText}]");
        }
    }
}
