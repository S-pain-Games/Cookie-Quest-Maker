using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOnMissionAccepted : MonoBehaviour
{
    private Event<int> _storyStartedCallback;

    private Event<PopupData> _showPopupCommand;

    private void Awake()
    {
        var evtSys = Admin.g_Instance.gameEventSystem;
        evtSys.StoryCallbacks.GetEvent("on_story_started".GetHashCode(), out _storyStartedCallback);
        evtSys.PopupSystemCommands.GetEvent("show_popup".GetHashCode(), out _showPopupCommand);
    }

    private void OnEnable()
    {
        _storyStartedCallback.OnInvoked += StoryStartedCallback_OnInvoked;
    }

    private void OnDisable()
    {
        _storyStartedCallback.OnInvoked -= StoryStartedCallback_OnInvoked;
    }

    private void StoryStartedCallback_OnInvoked(int storyId)
    {
        PopupData pData = new PopupData();
        pData.m_Text = Admin.g_Instance.storyDB.m_StoriesDB[storyId].m_StoryData.m_Title + " Story Started";
        pData.m_TimeAlive = 3.0f;
        _showPopupCommand.Invoke(pData);
    }
}
