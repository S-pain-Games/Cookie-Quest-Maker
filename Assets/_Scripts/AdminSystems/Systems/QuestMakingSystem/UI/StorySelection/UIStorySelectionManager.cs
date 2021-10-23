using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStorySelectionManager : MonoBehaviour
{
    [SerializeField] private Button _nextStoryButton;
    [SerializeField] private Button _previousStoryButton;
    [SerializeField] private Image _cardContents;

    private StorySystem storySys;
    private StoryDB storyDB;

    private void Awake()
    {
        storySys = Admin.g_Instance.storySystem;
        storyDB = Admin.g_Instance.storyDB;
    }

    private void OnEnable()
    {
        _nextStoryButton.onClick.AddListener(NextStory);
        _previousStoryButton.onClick.AddListener(PreviousStory);
    }

    private void OnDisable()
    {
        _nextStoryButton.onClick.RemoveListener(NextStory);
        _previousStoryButton.onClick.RemoveListener(PreviousStory);
    }

    private void UpdateUI()
    {
        _cardContents.sprite =
    }

    private void NextStory()
    {

    }

    private void PreviousStory()
    {

    }
}
