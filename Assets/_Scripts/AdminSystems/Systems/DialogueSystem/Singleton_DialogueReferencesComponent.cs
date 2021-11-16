using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace CQM.Components
{
    [Serializable]
    public class Singleton_DialogueReferencesComponent
    {
        public DialogueContainer m_Container;

        [Header("Text")]
        public TextMeshProUGUI m_CaracterName;
        public TextMeshProUGUI m_Line;

        [HideInInspector]
        public List<string> m_CurrentDialogueLines;

        [HideInInspector] public int m_CurrentLineIndex = 0;
        public Action m_CallbackOnDialogueEnd;

        public Image m_CharacterImage;
    }
}