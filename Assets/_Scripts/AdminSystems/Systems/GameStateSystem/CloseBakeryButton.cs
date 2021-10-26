using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBakeryButton : MonoBehaviour
{
    private GameEventSystem _messaging;
    private Event<int> onStoryCompleted;
    private Event<GameStateSystem.State> SetGameState;

    [SerializeField]
    private GameObject _buttonGameObject;
    private Button _button;

    private void Awake()
    {
        var ids = Admin.g_Instance.ID.events;
        _messaging.StoryCallbacks.GetEvent(ids.on_story_completed, out onStoryCompleted);
        _messaging.StoryCallbacks.GetEvent(ids.set_game_state, out SetGameState);

        _button = _buttonGameObject.GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            SetGameState.Invoke(GameStateSystem.State.BakeryNight);
            _buttonGameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        onStoryCompleted.OnInvoked += TryToEnableButton;
    }

    private void OnDisable()
    {
        onStoryCompleted.OnInvoked -= TryToEnableButton;
    }

    private void TryToEnableButton(int storyId)
    {
        _buttonGameObject.SetActive(true);
    }
}
