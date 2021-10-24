using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDBUnityReferences : MonoBehaviour
{
    // References to quest piece prefabs used in the quest building UI
    [SerializeField] public List<QuestPiecePrefabRef> StorySelectionUICards = new List<QuestPiecePrefabRef>();

    [System.Serializable]
    public struct QuestPiecePrefabRef
    {
        public string m_NameID;
        public Sprite m_CardSprite;
    }
}
