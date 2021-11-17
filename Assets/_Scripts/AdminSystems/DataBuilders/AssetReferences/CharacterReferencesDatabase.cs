using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CQM.AssetReferences
{
    [CreateAssetMenu]
    public class CharacterReferencesDatabase : ScriptableObject
    {
        [SerializeField] private List<CharacterReference> m_Characters = new List<CharacterReference>();

        private Dictionary<ID, CharacterReference> m_ReferencesMap = new Dictionary<ID, CharacterReference>();

        public void OnEnable()
        {
            m_ReferencesMap.Clear();

            for (int i = 0; i < m_Characters.Count; i++)
            {
                var c = m_Characters[i];
                m_ReferencesMap.Add(new ID(c.m_IDName), c);
            }
        }

        public Sprite GetDialogueSprite(ID id)
        {
            return m_ReferencesMap[id].m_DialogueSprite;
        }

        public Sprite GetNewspaperSprite(ID id)
        {
            return m_ReferencesMap[id].m_NewspaperSprite;
        }

        [System.Serializable]
        public class CharacterReference
        {
            public string m_IDName;
            public Sprite m_DialogueSprite;
            public Sprite m_NewspaperSprite;
        }
    }
}