using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.QuestMaking
{
    [System.Serializable]
    // Storage of all the unlocked pieces and cookies that the player has
    public class PlayerPieceStorage
    {
        // List of unlocked pieces for the player
        public List<QuestPiece> m_Storage = new List<QuestPiece>();

        public void Initialize()
        {
            // Development Init
            var qpDB = Admin.g_Instance.questDB;
            m_Storage.Add(qpDB.m_QPiecesDB["Attack".GetHashCode()]);
            m_Storage.Add(qpDB.m_QPiecesDB["Assist".GetHashCode()]);
        }
    }
}