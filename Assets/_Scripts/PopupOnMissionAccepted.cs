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
        _storyStartedCallback = evtSys.GetCallbackByName<Event<int>>("story_sys", "story_started");
        _showPopupCommand = evtSys.GetCommandByName<Event<PopupData>>("popup_sys", "show_popup");
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
