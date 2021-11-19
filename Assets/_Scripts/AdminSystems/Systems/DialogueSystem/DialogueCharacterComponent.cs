using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace CQM.Components
{
    [Serializable]
    public class DialogueCharacterComponent
    {
        public ID m_ID;
        public Sprite m_CharacterImg;
        public List<SerializableList<string>> m_IdleRandomDialogue = new List<SerializableList<string>>();
    }

    // Unity cant Serialize a List<List<T>> by default
    [Serializable]
    public class SerializableList<T>
    {
        [SerializeField] private List<T> m_List = new List<T>();

        public T this[int index] { get => m_List[index]; set => m_List[index] = value; }
        public int Count => m_List.Count;

        public void Add(T value)
        {
            m_List.Add(value);
        }

        public void Clear()
        {
            m_List.Clear();
        }
    }
}