using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBakeryButton : MonoBehaviour
{
    private StoryDB _storyDb;
    private GameStateSystem _gameStateSystem; //TODO: event system

    [SerializeField]
    private GameObject _buttonGameObject;
    private Button _button;

    private void Awake()
    {
        _storyDb = Admin.g_Instance.storyDB;
        _button = _buttonGameObject.GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            _gameStateSystem.SetState(GameStateSystem.State.BakeryNight);
            _buttonGameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        _storyDb.OnStoryCompleted += TryToEnableButton;
    }

    private void OnDisable()
    {
        _storyDb.OnStoryCompleted -= TryToEnableButton;
    }

    private void TryToEnableButton(int numCompletedMissions)
    {
        if (numCompletedMissions == 3)
        {
            _buttonGameObject.SetActive(true);

        }
    }
}
