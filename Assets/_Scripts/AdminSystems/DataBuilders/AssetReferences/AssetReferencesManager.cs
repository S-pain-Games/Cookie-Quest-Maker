using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.AssetReferences
{
    public class AssetReferencesManager : MonoBehaviour
    {
        [SerializeField] private CookieReferencesDatabase m_SpriteReferences;

        private Dictionary<Type, object> m_References = new Dictionary<Type, object>();

        public T GetReferences<T>() where T : class
        {
            return m_References[typeof(T)] as T;
        }
    }
}