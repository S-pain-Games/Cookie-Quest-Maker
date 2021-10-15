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

    // Saving the type as an enum/int allows us to check the type
    // way faster than using .GetType()
    // This has the disadvantage that we can introduce bugs
    // if we add a quespiece of the action type to a method that
    // expected a questpiece of the target type
    public enum PieceType
    {
        Action,
        Target,
        Modifier,
        Object,
        Cookie
    }
}
