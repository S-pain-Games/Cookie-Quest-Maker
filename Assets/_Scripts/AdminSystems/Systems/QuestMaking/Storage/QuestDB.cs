using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores all the created quests in the game
public class QuestDB
{
    public Dictionary<int, QuestData> m_QuestDataDB = new Dictionary<int, QuestData>();
    public Dictionary<int, QuestPiece> m_QPiecesDB = new Dictionary<int, QuestPiece>();

    public QuestDB()
    {
        LoadData();
    }

    public void LoadData()
    {
        QuestPiece qp = new QuestPiece();
        qp.m_PieceName = "Mayor";
        qp.m_Type = QuestPiece.PieceType.Target;
        qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Help, m_Value = 1 });
        m_QPiecesDB.Add(qp.m_PieceName.GetHashCode(), qp);

        qp = new QuestPiece();
        qp.m_PieceName = "Attack";
        qp.m_Type = QuestPiece.PieceType.Action;
        qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Harm, m_Value = 1 });
        m_QPiecesDB.Add(qp.m_PieceName.GetHashCode(), qp);

        qp = new QuestPiece();
        qp.m_PieceName = "Plain Cookie";
        qp.m_Type = QuestPiece.PieceType.Cookie;
        qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Help, m_Value = 1 });
        m_QPiecesDB.Add(qp.m_PieceName.GetHashCode(), qp);
    }
}
