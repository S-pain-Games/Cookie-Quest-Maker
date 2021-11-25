using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOnMissionAccepted : MonoBehaviour
{
    private Event<ID> _storyStartedCallback;
    private EventVoid _allStoriesCompletedTodayCallback;

    private Event<PopupData_MissionStarted> _showPrimaryStoryPopup;
    private Event<PopupData_MissionStarted> _showSecondaryStoryPopup;
    private Event<PopupData_GenericPopup> _showGenericStoryPopup;

    private void Awake()
    {
        var evtSys = Admin.Global.EventSystem;
        _storyStartedCallback = evtSys.GetCallbackByName<Event<ID>>("story_sys", "story_started");
        _allStoriesCompletedTodayCallback = evtSys.GetCallbackByName<EventVoid>("day_sys", "all_daily_stories_completed");
        _showPrimaryStoryPopup = evtSys.GetCommandByName<Event<PopupData_MissionStarted>>("popup_sys", "primary_mission_started");
        _showSecondaryStoryPopup = evtSys.GetCommandByName<Event<PopupData_MissionStarted>>("popup_sys", "secondary_mission_started");
        _showGenericStoryPopup = evtSys.GetCommandByName<Event<PopupData_GenericPopup>>("popup_sys", "generic_popup");
    }

    private void OnEnable()
    {
        _storyStartedCallback.OnInvoked += ShowPopupOfStory;
        _allStoriesCompletedTodayCallback.OnInvoked += ShowAllStoriesCompletedPopup;
    }

    private void OnDisable()
    {
        _storyStartedCallback.OnInvoked -= ShowPopupOfStory;
        _allStoriesCompletedTodayCallback.OnInvoked -= ShowAllStoriesCompletedPopup;
    }

    private void ShowPopupOfStory(ID storyId)
    {
        PopupData_MissionStarted pData = new PopupData_MissionStarted();
        var compDatabase = Admin.Global.Components;
        List<ID> secondaryStories = compDatabase.m_GameStoriesStateComponent.m_AllSecondaryStories;

        pData.m_MissionTitle = compDatabase.GetComponentContainer<StoryInfoComponent>()[storyId].m_StoryData.m_Title;
        pData.m_TimeAlive = 2.0f;

        // Check if its a primary or a secondary story
        if (secondaryStories.Contains(storyId))
            _showSecondaryStoryPopup.Invoke(pData);
        else
            _showPrimaryStoryPopup.Invoke(pData);
    }

    private void ShowAllStoriesCompletedPopup()
    {
        PopupData_GenericPopup pData = new PopupData_GenericPopup();
        pData.m_Text = "Has completado todas las historias por hoy";
        pData.m_TimeAlive = 5.0f;
        _showGenericStoryPopup.Invoke(pData);
    }
}
