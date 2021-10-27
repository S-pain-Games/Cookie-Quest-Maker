using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.QuestMaking
{
    [System.Serializable]
    // Storage of all the unlocked pieces and cookies that the player has
    public class PlayerUnlockedPieces
    {
        // List of unlocked pieces IDs for the player
        // TODO: Separate in Cookie/Words/Targets Lists
        public List<int> m_Storage = new List<int>();

        public void Initialize()
        {
            // In the final version we would need to load this from
            // the player save game file

            // Development Init
            // TODO: Init this correctly from file
            var qpDB = Admin.g_Instance.questDB;
            var ids = Admin.g_Instance.ID.pieces;

            m_Storage.Add(ids.plain_cookie);
            m_Storage.Add(ids.attack);
            m_Storage.Add(ids.assist);
            m_Storage.Add(ids.baseball_bat);
            m_Storage.Add(ids.brutally);
            m_Storage.Add(ids.kindly);
            /*
            m_Storage.Add("plain_cookie".GetHashCode());
            m_Storage.Add("plain_cookie_2".GetHashCode());
            */
        }
    }
}