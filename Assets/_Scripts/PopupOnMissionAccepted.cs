using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOnMissionAccepted : MonoBehaviour
{
    private Event<int> _storyStartedCallback;
    private EventVoid _allStoriesCompletedTodayCallback;

    private Event<PopupData> _showPopupCommand;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _storyStartedCallback = evtSys.GetCallbackByName<Event<int>>("story_sys", "story_started");
        _allStoriesCompletedTodayCallback = evtSys.GetCallbackByName<EventVoid>("day_sys", "all_daily_stories_completed");
        _showPopupCommand = evtSys.GetCommandByName<Event<PopupData>>("popup_sys", "show_popup");
    }

    private void OnEnable()
    {
        _storyStartedCallback.OnInvoked += StoryStartedCallback_OnInvoked;
        _allStoriesCompletedTodayCallback.OnInvoked += AllStoriesCompletedCallback_OnInvoked;
    }

    private void OnDisable()
    {
        _storyStartedCallback.OnInvoked -= StoryStartedCallback_OnInvoked;
        _allStoriesCompletedTodayCallback.OnInvoked -= AllStoriesCompletedCallback_OnInvoked;
    }

    private void StoryStartedCallback_OnInvoked(int storyId)
    {
        PopupData pData = new PopupData();
        pData.m_Text = Admin.Global.Database.Stories.GetStoryComponent<StoryInfo>(storyId).m_StoryData.m_Title + " Story Started";
        pData.m_TimeAlive = 3.0f;
        _showPopupCommand.Invoke(pData);
    }

    private void AllStoriesCompletedCallback_OnInvoked()
    {
        PopupData pData = new PopupData();
        pData.m_Text = "All Stories Completed Today";
        pData.m_TimeAlive = 5.0f;
        _showPopupCommand.Invoke(pData);
    }
}
