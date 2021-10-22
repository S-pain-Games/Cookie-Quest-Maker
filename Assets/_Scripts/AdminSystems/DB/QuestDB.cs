using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.QuestMaking
{
    // Stores all the created quests in the game
    public class QuestDB
    {
        // Contains all quests made in the playthrough
        public Dictionary<int, QuestData> m_QuestDataDB = new Dictionary<int, QuestData>();
        // Contains all quest pieces in the game
        public Dictionary<int, QuestPiece> m_QPiecesDB = new Dictionary<int, QuestPiece>();

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
            qp.m_PieceName = "Assist";
            qp.m_Type = QuestPiece.PieceType.Action;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Help, m_Value = 1 });
            m_QPiecesDB.Add(qp.m_PieceName.GetHashCode(), qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Plain Cookie";
            qp.m_Type = QuestPiece.PieceType.Cookie;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Help, m_Value = 1 });
            m_QPiecesDB.Add(qp.m_PieceName.GetHashCode(), qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Brutally";
            qp.m_Type = QuestPiece.PieceType.Modifier;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Harm, m_Value = 1 });
            m_QPiecesDB.Add(qp.m_PieceName.GetHashCode(), qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Kindly";
            qp.m_Type = QuestPiece.PieceType.Modifier;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Help, m_Value = 1 });
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Convince, m_Value = 1 });
            m_QPiecesDB.Add(qp.m_PieceName.GetHashCode(), qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Baseball Bat";
            qp.m_Type = QuestPiece.PieceType.Object;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Harm, m_Value = 1 });
            m_QPiecesDB.Add(qp.m_PieceName.GetHashCode(), qp);
        }
    }
}