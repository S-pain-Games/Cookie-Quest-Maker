using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _dialogueBoxContainer;

    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI _characterNameComp;
    [SerializeField]
    private TextMeshProUGUI _lineTextComp;

    [SerializeField]
    private List<string> _dialogueLines;

    private int _lineIndex = 0;

    public void ShowDialogue(List<string> dialogue, string characterName)
    {
        _dialogueLines = dialogue;
        _characterNameComp.text = characterName;

        _lineTextComp.text = string.Empty;
        _lineTextComp.text = _dialogueLines[_lineIndex];
    }

    public void NextLine()
    {
        if (_lineIndex < _dialogueLines.Count - 1)
        {
            _lineIndex++;
            _lineTextComp.text = _dialogueLines[_lineIndex];
        }
        else
        {
            _dialogueBoxContainer.SetActive(false);
            _lineIndex = 0;
        }
    }
}

public class DialogueDB
{
    // Contains all the random dialogue lines id's (for localization) said by secondary non-story NPCs
    public Dictionary<int, List<int>> m_RandomDialogue = new Dictionary<int, List<int>>(); // List?

    public void LoadData()
    {
    }
}