using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOnMissionAccepted : MonoBehaviour
{
    private Event<ID> _storyStartedCallback;
    private EventVoid _allStoriesCompletedTodayCallback;

    private Event<PopupData_MissionStarted> _showPopupCommand;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _storyStartedCallback = evtSys.GetCallbackByName<Event<ID>>("story_sys", "story_started");
        _allStoriesCompletedTodayCallback = evtSys.GetCallbackByName<EventVoid>("day_sys", "all_daily_stories_completed");
        _showPopupCommand = evtSys.GetCommandByName<Event<PopupData_MissionStarted>>("popup_sys", "primary_mission_started");
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

    private void StoryStartedCallback_OnInvoked(ID storyId)
    {
        PopupData_MissionStarted pData = new PopupData_MissionStarted();
        pData.m_MissionTitle = Admin.Global.Components.GetComponentContainer<StoryInfoComponent>()[storyId].m_StoryData.m_Title;
        pData.m_TimeAlive = 3.0f;
        _showPopupCommand.Invoke(pData);
    }

    private void AllStoriesCompletedCallback_OnInvoked()
    {
        PopupData_MissionStarted pData = new PopupData_MissionStarted();
        pData.m_MissionTitle = "All Stories Completed Today";
        pData.m_TimeAlive = 5.0f;
        _showPopupCommand.Invoke(pData);
    }
}
