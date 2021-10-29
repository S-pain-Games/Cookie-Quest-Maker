using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIStorySelectionManager : MonoBehaviour
{
    public event Action<int> OnStorySelected;

    // Ui Controls
    [Header("Buttons")]
    [SerializeField] private Button _nextStoryButton;
    [SerializeField] private Button _previousStoryButton;
    [SerializeField] private Button _selectStoryButton;

    // Story View
    [Header("Story Card")]
    [SerializeField] private Image _cardContents;
    [SerializeField] private TextMeshProUGUI _cardTitle;

    private StorySystem storySys;
    private StoryDB storyDB;

    private int currentStoryIndex = 0;
    private int previousTarget = 0;

    private void Awake()
    {
        storySys = Admin.g_Instance.storySystem;
        storyDB = Admin.g_Instance.storyDB;
    }

    private void OnEnable()
    {
        _nextStoryButton.onClick.AddListener(NextStory);
        _previousStoryButton.onClick.AddListener(PreviousStory);
        _selectStoryButton.onClick.AddListener(SelectStory);

        UpdateUI();
    }

    private void OnDisable()
    {
        _nextStoryButton.onClick.RemoveListener(NextStory);
        _previousStoryButton.onClick.RemoveListener(PreviousStory);
        _selectStoryButton.onClick.RemoveListener(SelectStory);
    }

    private void UpdateUI()
    {
        int numOngoingStories = storyDB.m_OngoingStories.Count;
        if (numOngoingStories <= 0)
        {
            _cardContents.sprite = null;
            _cardTitle.text = "Out of Stories";
        }
        else
        {
            currentStoryIndex = Mathf.Clamp(currentStoryIndex, 0, numOngoingStories - 1);
            int storyId = storyDB.m_OngoingStories[currentStoryIndex];
            var data = storyDB.m_StoriesUI[storyId];
            _cardTitle.text = data.m_Title;
        }
    }

    private void NextStory()
    {
        // Increment and wrap around
        currentStoryIndex++;
        if (currentStoryIndex >= storyDB.m_OngoingStories.Count)
            currentStoryIndex = 0;

        UpdateUI();
    }

    private void PreviousStory()
    {
        // Decrement and wrap around
        currentStoryIndex--;
        if (currentStoryIndex < 0)
            currentStoryIndex = storyDB.m_OngoingStories.Count;

        UpdateUI();
    }

    private void SelectStory()
    {
        if (storyDB.m_OngoingStories.Count > 0)
        {
            int storyId = storyDB.m_OngoingStories[currentStoryIndex];
            OnStorySelected?.Invoke(storyId);
        }
    }
}
