using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CQM.Components;

public class DialogueSystem : ISystemEvents
{
    private Singleton_DialogueReferencesComponent _dialogueData;
    private ComponentsContainer<CharacterComponent> _characters;
    private ComponentsContainer<CharacterDialogueComponent> _dialogue;

    private EventVoid _enableCharMovementCmd;
    private EventVoid _disableCharMovementCmd;


    public void Initialize(Singleton_DialogueReferencesComponent dialogueRefs,
                           ComponentsContainer<CharacterComponent> characters,
                           ComponentsContainer<CharacterDialogueComponent> dialogue,
                           GameEventSystem evtSys)
    {
        _dialogueData = dialogueRefs;
        _characters = characters;
        _dialogue = dialogue;

        _enableCharMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "enable_movement");
        _disableCharMovementCmd = evtSys.GetCommandByName<EventVoid>("character_sys", "disable_movement");
    }

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = new ID("dialogue_sys");

        // Commands
        var evt = commands.AddEvent<ShowDialogueEvtArgs>(new ID("show_dialogue"));
        evt.OnInvoked += (args) => ShowDialogue(args.m_Dialogue, args.m_CharID, args.m_Callback);
        commands.AddEvent(new ID("continue_dialogue")).OnInvoked += NextLine;
    }


    public void ShowDialogue(List<string> dialogue, ID characterID, Action callback)
    {
        var data = _dialogueData;
        data.m_Container.gameObject.SetActive(true);
        data.m_CurrentDialogueLines = dialogue;

        Sprite characterSprite = _dialogue[characterID].m_CharacterImg;
        if (characterSprite != null)
            data.m_CharacterImage.sprite = characterSprite;
        else
            data.m_CharacterImage.sprite = null;

        data.m_Container.StartCoroutine(ShowText(data.m_CurrentDialogueLines[data.m_CurrentLineIndex]));
        data.m_CallbackOnDialogueEnd = callback;

        _disableCharMovementCmd.Invoke();
    }

    public IEnumerator ShowText(string dialogueLine)
    {
        int i = 0;
        var wait = new WaitForSeconds(0.015f);
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

        if (data.m_Line.text != data.m_CurrentDialogueLines[data.m_CurrentLineIndex])
        {
            data.m_Line.text = data.m_CurrentDialogueLines[data.m_CurrentLineIndex];
        }
        else if (data.m_CurrentLineIndex < data.m_CurrentDialogueLines.Count - 1)
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
                // We do this to allow a chain of dialogues
                var callback = data.m_CallbackOnDialogueEnd;
                data.m_CallbackOnDialogueEnd = null;
                callback.Invoke();
            }
        }
    }
}


public struct ShowDialogueEvtArgs
{
    public List<string> m_Dialogue;
    public ID m_CharID;
    public Action m_Callback;

    public ShowDialogueEvtArgs(List<string> dialogue, ID charID, Action callback)
    {
        m_Dialogue = dialogue;
        m_CharID = charID;
        m_Callback = callback;
    }
}
