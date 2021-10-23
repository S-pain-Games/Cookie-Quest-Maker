using System;
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
        // Contains all functional quest pieces in the game
        public Dictionary<int, QuestPiece> m_QPiecesDB = new Dictionary<int, QuestPiece>();
        // Contains all the Prefabs to instantiate quest pieces in the Quest Builder UI
        public Dictionary<int, GameObject> m_QuestBuildingPiecesPrefabs = new Dictionary<int, GameObject>();
        // Contains all the UI Data of each quest piece
        public Dictionary<int, UIQuestPieceData> m_UIQuestPieces = new Dictionary<int, UIQuestPieceData>();

        public void LoadData(QuestDBUnityReferences unityReferences)
        {
            var pIds = Admin.g_Instance.ID.pieces;

            LoadQuestPieces(pIds);
            LoadUIQuestPieces(pIds);
            LoadQuestPiecesPrefabs(pIds, unityReferences);
        }

        private void LoadQuestPiecesPrefabs(IDQuestPieces pIds, QuestDBUnityReferences uRef)
        {
            for (int i = 0; i < uRef.QuestBuildingPiecePrefabs.Count; i++)
            {
                var pieceRef = uRef.QuestBuildingPiecePrefabs[i];
                m_QuestBuildingPiecesPrefabs.Add(pieceRef.m_NameID.GetHashCode(), pieceRef.m_Prefab);
            }
        }

        private void LoadUIQuestPieces(IDQuestPieces pIds)
        {
            UIQuestPieceData qpd = new UIQuestPieceData
            {
                sprite = null,
                name = "Mayor",
                description = "A very respected man in town (by some)"
            };
            m_UIQuestPieces.Add(pIds.mayor, qpd);

            qpd = new UIQuestPieceData
            {
                sprite = null,
                name = "Plain Cookie",
                description = "Very plain"
            };
            m_UIQuestPieces.Add(pIds.plain_cookie, qpd);

            qpd = new UIQuestPieceData
            {
                sprite = null,
                name = "Attack",
                description = "Sometimes violence IS the answer -Evil Cookie Goddess"
            };
            m_UIQuestPieces.Add(pIds.attack, qpd);

            qpd = new UIQuestPieceData
            {
                sprite = null,
                name = "Assist",
                description = "Being able to assist someone in need is very gud -Angelic Cookie God"
            };
            m_UIQuestPieces.Add(pIds.assist, qpd);
        }

        private void LoadQuestPieces(IDQuestPieces pIds)
        {
            QuestPiece qp = new QuestPiece();
            qp.m_PieceName = "Mayor";
            qp.m_Type = QuestPiece.PieceType.Target;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Help, m_Value = 1 });
            m_QPiecesDB.Add(pIds.mayor, qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Attack";
            qp.m_Type = QuestPiece.PieceType.Action;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Harm, m_Value = 1 });
            m_QPiecesDB.Add(pIds.attack, qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Assist";
            qp.m_Type = QuestPiece.PieceType.Action;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Help, m_Value = 1 });
            m_QPiecesDB.Add(pIds.assist, qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Plain Cookie";
            qp.m_Type = QuestPiece.PieceType.Cookie;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Help, m_Value = 1 });
            m_QPiecesDB.Add(pIds.plain_cookie, qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Brutally";
            qp.m_Type = QuestPiece.PieceType.Modifier;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Harm, m_Value = 1 });
            m_QPiecesDB.Add(pIds.brutally, qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Kindly";
            qp.m_Type = QuestPiece.PieceType.Modifier;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Help, m_Value = 1 });
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Convince, m_Value = 1 });
            m_QPiecesDB.Add(pIds.kindly, qp);

            qp = new QuestPiece();
            qp.m_PieceName = "Baseball Bat";
            qp.m_Type = QuestPiece.PieceType.Object;
            qp.m_Tags.Add(new QPTag { m_Type = QPTag.TagType.Harm, m_Value = 1 });
            m_QPiecesDB.Add(pIds.baseball_bat, qp);
        }
    }
}