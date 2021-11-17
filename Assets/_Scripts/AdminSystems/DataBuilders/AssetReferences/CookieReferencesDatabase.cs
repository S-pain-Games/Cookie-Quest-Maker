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
        [SerializeField] private List<CookieAssetReference> m_Cookies = new List<CookieAssetReference>();
        [SerializeField] private List<PieceAssetReferences> m_Actions = new List<PieceAssetReferences>();
        [SerializeField] private List<PieceAssetReferences> m_Modifiers = new List<PieceAssetReferences>();
        [SerializeField] private List<PieceAssetReferences> m_Targets = new List<PieceAssetReferences>();
        [SerializeField] private List<PieceAssetReferences> m_Objects = new List<PieceAssetReferences>();

        private Dictionary<ID, PieceAssetReferences> m_PiecesMap = new Dictionary<ID, PieceAssetReferences>();
        private Dictionary<ID, CookieAssetReference> m_CookiesMap = new Dictionary<ID, CookieAssetReference>();


        private void OnEnable()
        {
            m_PiecesMap.Clear();
            m_CookiesMap.Clear();
            AddToHashMap(m_Actions);
            AddToHashMap(m_Modifiers);
            AddToHashMap(m_Targets);
            AddToHashMap(m_Objects);

            void AddToHashMap(List<PieceAssetReferences> references)
            {
                for (int i = 0; i < references.Count; i++)
                {
                    var r = references[i];
                    m_PiecesMap.Add(new ID(r.m_IDName), r);
                }
            }

            for (int i = 0; i < m_Cookies.Count; i++)
            {
                var c = m_Cookies[i];
                m_CookiesMap.Add(new ID(c.m_IDName), c);
            }
        }

        public Sprite GetSimpleSprite(ID cookieID)
        {
            // This is emmmmm, Questionable...
            if (m_PiecesMap.TryGetValue(cookieID, out PieceAssetReferences r))
            {
                return r.m_SimpleSprite;
            }
            else
            {
                return m_CookiesMap[cookieID].m_SimpleSprite;
            }
        }

        public Sprite GetFullSprite(ID cookieID)
        {
            if (m_PiecesMap.TryGetValue(cookieID, out PieceAssetReferences r))
            {
                return r.m_FullCookieSprite;
            }
            else
            {
                return m_CookiesMap[cookieID].m_FullCookieSprite;
            }
        }

        public bool GetHDSprite(ID cookieID, out Sprite sprite)
        {
            sprite = null;
            if (m_CookiesMap.TryGetValue(cookieID, out CookieAssetReference re))
            {
                sprite = re.m_HDSprite;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetShopRecipeSprite(ID cookieID, out Sprite sprite)
        {
            sprite = null;
            if (m_CookiesMap.TryGetValue(cookieID, out CookieAssetReference re))
            {
                sprite = re.m_RecipeSprite;
                return true;
            }
            else
            {
                return false;
            }
        }

        [Serializable]
        public class PieceAssetReferences
        {
            public string m_IDName;
            public Sprite m_SimpleSprite;
            public Sprite m_FullCookieSprite;
            public GameObject m_QuestBuildingPrefab;
        }

        [Serializable]
        public class CookieAssetReference
        {
            public string m_IDName;
            public Sprite m_SimpleSprite;
            public Sprite m_FullCookieSprite;
            public Sprite m_HDSprite;
            public Sprite m_RecipeSprite;
            public GameObject m_QuestBuildingPrefab;
        }
    }
}
