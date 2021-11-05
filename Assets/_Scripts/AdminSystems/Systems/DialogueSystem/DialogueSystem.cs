using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueSystem : ISystemEvents
{
    private DialogueReferences _dialogueData;
    private Dictionary<int, CharacterComponent> _characters;
    private Dictionary<int, DialogueCharacterComponent> _dialogue;

    private EventVoid _enableCharMovementCmd;
    private EventVoid _disableCharMovementCmd;


    public void Initialize(DialogueReferences dialogueRefs,
                           Dictionary<int, CharacterComponent> characters,
                           Dictionary<int, DialogueCharacterComponent> dialogue,
                           GameEventSystem evtSys)
    {
        _dialogueData = dialogueRefs;
        _characters = characters;
        _dialogue = dialogue;

        _enableCharMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        _disableCharMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "disable_movement");
    }

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = "dialogue_sys".GetHashCode();

        // Commands
        var evt = commands.AddEvent<ShowDialogueEvtArgs>("show_dialogue".GetHashCode());
        evt.OnInvoked += (args) => ShowDialogue(args.m_Dialogue, args.m_CharID, args.m_Callback);
        commands.AddEvent("continue_dialogue".GetHashCode()).OnInvoked += NextLine;
    }


    public void ShowDialogue(List<string> dialogue, int characterID, Action callback)
    {
        var data = _dialogueData;
        data.m_Container.gameObject.SetActive(true);
        data.m_CurrentDialogueLines = dialogue;


        data.m_CaracterName.text = _characters[characterID].m_FullName;
        data.m_Container.StartCoroutine(ShowText(data.m_CurrentDialogueLines[data.m_CurrentLineIndex]));
        data.m_CallbackOnDialogueEnd = callback;

        _disableCharMovementCmd.Invoke();
    }

    public IEnumerator ShowText(string dialogueLine)
    {
        int i = 0;
        var wait = new WaitForSeconds(0.02f);
        var d = _dialogueData;
        while (i <= dialogueLine.Length)
        {
            d.m_Line.text = dialogueLine.Substring(0, i);
            yield return wait;
            i++;
        }
    }

    public void NextLine()
    {
        var data = _dialogueData;

        data.m_Container.StopAllCoroutines();

        if (data.m_CurrentLineIndex < data.m_CurrentDialogueLines.Count - 1)
        {
            data.m_CurrentLineIndex++;
            data.m_Container.StartCoroutine(ShowText(data.m_CurrentDialogueLines[data.m_CurrentLineIndex]));
        }
        else
        {
            data.m_Container.gameObject.SetActive(false);
            data.m_CurrentLineIndex = 0;

            _enableCharMovementCmd.Invoke();

            if (data.m_CallbackOnDialogueEnd != null)
            {
                data.m_CallbackOnDialogueEnd.Invoke();
                data.m_CallbackOnDialogueEnd = null;
            }
        }
    }
}


public struct ShowDialogueEvtArgs
{
    public List<string> m_Dialogue;
    public int m_CharID;
    public Action m_Callback;

    public ShowDialogueEvtArgs(List<string> dialogue, int charID, Action callback)
    {
        m_Dialogue = dialogue;
        m_CharID = charID;
        m_Callback = callback;
    }
}


[Serializable]
public class DialogueDB
{
    public DialogueReferences ReferencesAndData => m_DialogueUIData;

    [SerializeField]
    private DialogueReferences m_DialogueUIData = new DialogueReferences();
    private Dictionary<int, List<int>> m_RandomDialogue = new Dictionary<int, List<int>>(); // TODO: Localize


    public void LoadData()
    {
    }
}


[Serializable]
public class DialogueReferences
{
    public DialogueContainer m_Container;

    [Header("Text")]
    public TextMeshProUGUI m_CaracterName;
    public TextMeshProUGUI m_Line;

    [HideInInspector]
    public List<string> m_CurrentDialogueLines;

    [HideInInspector] public int m_CurrentLineIndex = 0;
    public Action m_CallbackOnDialogueEnd;
}

[Serializable]
public class DialogueCharacterComponent
{
    public int m_ID;
    public Sprite m_CharacterImg;
    public Color m_NameColor;
}