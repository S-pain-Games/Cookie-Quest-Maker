using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CQM.Databases;

namespace CQM.Gameplay
{
    public class UIStorySelectionManager : MonoBehaviour
    {
        // Ui Controls
        [Header("Buttons")]
        [SerializeField] private Button _nextStoryButton;
        [SerializeField] private Button _previousStoryButton;
        [SerializeField] private Button _selectStoryButton;

        // Story View
        [Header("Story Card")]
        [SerializeField] private Image _cardContents;
        [SerializeField] private TextMeshProUGUI _cardTitle;

        private StoryDB _storyDB;
        private int _currentStoryIndex = 0;

        private EventSys _evtSys;
        private Event<int> _onStorySelectedCallback;

        public void Initialize(EventSys evtSys)
        {
            _evtSys = evtSys;
            _onStorySelectedCallback = _evtSys.AddEvent<int>("on_story_selected".GetHashCode());
        }

        private void Awake()
        {
            _storyDB = Admin.Global.Database.Stories;
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
            int numOngoingStories = _storyDB.m_OngoingStories.Count;
            if (numOngoingStories <= 0)
            {
                _cardContents.sprite = null;
                _cardTitle.text = "Out of Stories";
            }
            else
            {
                _currentStoryIndex = Mathf.Clamp(_currentStoryIndex, 0, numOngoingStories - 1);
                int storyId = _storyDB.m_OngoingStories[_currentStoryIndex];
                var data = _storyDB.m_StoriesUI[storyId];
                _cardTitle.text = data.m_Title;
            }
        }

        private void NextStory()
        {
            // Increment and wrap around
            _currentStoryIndex++;
            if (_currentStoryIndex >= _storyDB.m_OngoingStories.Count)
                _currentStoryIndex = 0;

            UpdateUI();
        }

        private void PreviousStory()
        {
            // Decrement and wrap around
            _currentStoryIndex--;
            if (_currentStoryIndex < 0)
                _currentStoryIndex = _storyDB.m_OngoingStories.Count;

            UpdateUI();
        }

        private void SelectStory()
        {
            if (_storyDB.m_OngoingStories.Count > 0)
            {
                int storyId = _storyDB.m_OngoingStories[_currentStoryIndex];
                _onStorySelectedCallback.Invoke(storyId);
                //OnStorySelected?.Invoke(storyId);
            }
        }
    }
}