using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.AssetReferences
{
    [CreateAssetMenu]
    public class IngredientsReferencesDatabase : ScriptableObject
    {
        [SerializeField] private List<IngredientAssetReference> m_Ingredients = new List<IngredientAssetReference>();

        private Dictionary<ID, IngredientAssetReference> m_ReferencesMap = new Dictionary<ID, IngredientAssetReference>();


        private void OnEnable()
        {
            m_ReferencesMap.Clear();
            AddToHashMap(m_Ingredients);

            void AddToHashMap(List<IngredientAssetReference> references)
            {
                for (int i = 0; i < references.Count; i++)
                {
                    var r = references[i];
                    m_ReferencesMap.Add(new ID(r.m_IDName), r);
                }
            }
        }

        public Sprite GetSprite(ID spriteID)
        {
            return m_ReferencesMap[spriteID].m_Sprite;
        }

        [Serializable]
        public class IngredientAssetReference
        {
            public string m_IDName;
            public Sprite m_Sprite;
        }
    }
}