using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class QuestPiece : ScriptableObject
{
    public string WordText { get => m_PieceName; }
    public PieceType Type { get => m_Type; }
    public List<QuestPieceTag> Tags { get => m_Tags; }

    [SerializeField]
    private string m_PieceName = "Unnamed";
    [SerializeField]
    private PieceType m_Type = PieceType.Action;
    [SerializeField]
    public List<QuestPieceTag> m_Tags = new List<QuestPieceTag>();

    public enum PieceType
    {
        Action,
        Target,
        Modifier,
        Object,
        Cookie
    }
}
