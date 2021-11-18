using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.AssetReferences
{
    [CreateAssetMenu]
    public class StoriesReferencesDatabase : ScriptableObject
    {
        [SerializeField] private List<StoryAssetReference> m_Stories = new List<StoryAssetReference>();

        private Dictionary<ID, StoryAssetReference> m_ReferencesMap = new Dictionary<ID, StoryAssetReference>();


        private void OnEnable()
        {
            m_ReferencesMap.Clear();
            AddToHashMap(m_Stories);

            void AddToHashMap(List<StoryAssetReference> references)
            {
                for (int i = 0; i < references.Count; i++)
                {
                    var r = references[i];
                    m_ReferencesMap.Add(new ID(r.m_IDName), r);
                }
            }
        }

        public Sprite GetStorySelectionSprite(ID spriteID)
        {
            return m_ReferencesMap[spriteID].m_Sprite;
        }

        [Serializable]
        public class StoryAssetReference
        {
            public string m_IDName;
            public Sprite m_Sprite;
        }
    }
}