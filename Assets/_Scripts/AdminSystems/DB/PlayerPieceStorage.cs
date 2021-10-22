using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.QuestMaking
{
    [System.Serializable]
    // Storage of all the unlocked pieces and cookies that the player has
    public class PlayerPieceStorage
    {
        // List of unlocked pieces IDs for the player
        public List<int> m_Storage = new List<int>();

        public void Initialize()
        {
            // In the final version we would need to load this from
            // the player save game file

            // Development Init
            var qpDB = Admin.g_Instance.questDB;
            m_Storage.Add("attack".GetHashCode());
            m_Storage.Add("assist".GetHashCode());
            m_Storage.Add("plain_cookie".GetHashCode());
        }
    }
}