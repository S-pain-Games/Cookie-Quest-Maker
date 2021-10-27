using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
    private Action _callbackOnDialogueEnd;

    // TODO: Add behaviour to stack dialogues
    public void ShowDialogue(List<string> dialogue, string characterName, Action callback)
    {
        _dialogueBoxContainer.SetActive(true);
        _dialogueLines = dialogue;

        _characterNameComp.text = characterName;
        _lineTextComp.text = _dialogueLines[_lineIndex];
        _callbackOnDialogueEnd = callback;
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

            if (_callbackOnDialogueEnd != null)
            {
                _callbackOnDialogueEnd.Invoke();
                _callbackOnDialogueEnd = null;
            }
        }
    }
}

public class DialogueDB
{
    // TODO: Complete this
    // Contains all the random dialogue lines id's (for localization) said by secondary non-story NPCs
    public Dictionary<int, List<int>> m_RandomDialogue = new Dictionary<int, List<int>>(); // List?

    public void LoadData()
    {
    }
}