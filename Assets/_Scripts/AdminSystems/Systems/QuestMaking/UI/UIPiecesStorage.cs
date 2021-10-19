using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPiecesStorage : MonoBehaviour
{
    private QuestPiecesStorage _storage = new QuestPiecesStorage();
    private List<UIStoredPiece> m_Pieces = new List<UIStoredPiece>();

    private void OnEnable()
    {
        for (int i = 0; i < m_Pieces.Count; i++)
        {
            m_Pieces[i].OnSelected += UIPieceSelectedHandle;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < m_Pieces.Count; i++)
        {
            m_Pieces[i].OnSelected -= UIPieceSelectedHandle;
        }
    }

    private void UIPieceSelectedHandle(UIStoredPiece obj)
    {

    }
}
