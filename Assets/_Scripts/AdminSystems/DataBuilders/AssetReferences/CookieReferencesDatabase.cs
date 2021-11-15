using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace CQM.AssetReferences
{
    [CreateAssetMenu]
    public class CookieReferencesDatabase : ScriptableObject
    {
        [SerializeField] private List<CookieAssetReferences> m_Cookies = new List<CookieAssetReferences>();
        [SerializeField] private List<CookieAssetReferences> m_Actions = new List<CookieAssetReferences>();
        [SerializeField] private List<CookieAssetReferences> m_Modifiers = new List<CookieAssetReferences>();
        [SerializeField] private List<CookieAssetReferences> m_Targets = new List<CookieAssetReferences>();
        [SerializeField] private List<CookieAssetReferences> m_Objects = new List<CookieAssetReferences>();

        private Dictionary<ID, CookieAssetReferences> m_ReferencesMap = new Dictionary<ID, CookieAssetReferences>();

        private void OnEnable()
        {
            m_ReferencesMap.Clear();
            AddToHashMap(m_Cookies);
            AddToHashMap(m_Actions);
            AddToHashMap(m_Modifiers);
            AddToHashMap(m_Targets);
            AddToHashMap(m_Objects);

            void AddToHashMap(List<CookieAssetReferences> references)
            {
                for (int i = 0; i < references.Count; i++)
                {
                    var r = references[i];
                    m_ReferencesMap.Add(new ID(r.m_IDName), r);
                }
            }
        }

        public Sprite GetSimpleSprite(ID spriteID)
        {
            return m_ReferencesMap[spriteID].m_SimpleSprite;
        }

        public Sprite GetFullSprite(ID spriteID)
        {
            return m_ReferencesMap[spriteID].m_FullCookieSprite;
        }

        [Serializable]
        public class CookieAssetReferences
        {
            public string m_IDName;
            public Sprite m_SimpleSprite;
            public Sprite m_FullCookieSprite;
            public GameObject m_QuestBuildingPrefab;
        }
    }
}
