using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestPiece
{
    public string m_PieceName = "Unnamed";
    public PieceType m_Type = PieceType.Action;
    public List<QPTag> m_Tags = new List<QPTag>();

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