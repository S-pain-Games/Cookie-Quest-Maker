using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class UiSystem : MonoBehaviour
{
    [SerializeField]
    public List<UiView> _views;

    [SerializeField]
    private VoidEventHandle _startQuestMakingUI;
    [SerializeField]
    private UiView _questMakingView;

    [SerializeField]
    private VoidEventHandle _startGameplayUI;
    [SerializeField]
    private UiView _gameplayView;

    private void Start()
    {
        ShowGameplayView();
    }

    private void OnEnable()
    {
        _startQuestMakingUI.OnEvent += ShowQuestMakingView;
        _startGameplayUI.OnEvent += ShowGameplayView;
    }

    private void OnDisable()
    {
        _startQuestMakingUI.OnEvent -= ShowQuestMakingView;
        _startGameplayUI.OnEvent -= ShowGameplayView;
    }

    private void ShowGameplayView()
    {
        ShowOnlyView(_gameplayView);
    }

    private void ShowQuestMakingView()
    {
        ShowOnlyView(_questMakingView);
    }

    private void ShowOnlyView(UiView view)
    {
        HideAllViews();
        view.Enable();
    }

    private void HideAllViews()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            _views[i].Disable();
        }
    }
}
