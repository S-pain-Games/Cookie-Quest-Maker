using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CQM.Databases;
using CQM.Components;

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

        private List<ID> _ongoingStories;
        private ComponentsContainer<StoryUIDataComponent> _storyUIComponents;

        private int _currentStoryIndex = 0;

        private EventSys _evtSys;
        private Event<ID> _onStorySelectedCallback;


        public void Initialize(EventSys evtSys)
        {
            _evtSys = evtSys;
            _onStorySelectedCallback = _evtSys.AddEvent<ID>(new ID("on_story_selected"));
        }

        private void Awake()
        {
            _ongoingStories = Admin.Global.Components.m_OngoingStories;
            _storyUIComponents = Admin.Global.Components.GetComponentContainer<StoryUIDataComponent>();
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
            int numOngoingStories = _ongoingStories.Count;
            if (numOngoingStories <= 0)
            {
                _cardContents.gameObject.SetActive(false);
                _cardTitle.text = "Out of Stories";
            }
            else
            {
                _cardContents.gameObject.SetActive(true);
                _currentStoryIndex = Mathf.Clamp(_currentStoryIndex, 0, numOngoingStories - 1);
                ID storyId = _ongoingStories[_currentStoryIndex];
                var data = _storyUIComponents[storyId];

                _cardContents.sprite = data.m_Sprite;
                _cardTitle.text = data.m_Title;
            }
        }

        private void NextStory()
        {
            // Increment and wrap around
            _currentStoryIndex++;
            if (_currentStoryIndex >= _ongoingStories.Count)
                _currentStoryIndex = 0;

            UpdateUI();
        }

        private void PreviousStory()
        {
            // Decrement and wrap around
            _currentStoryIndex--;
            if (_currentStoryIndex < 0)
                _currentStoryIndex = _ongoingStories.Count;

            UpdateUI();
        }

        private void SelectStory()
        {
            if (_ongoingStories.Count > 0)
            {
                ID storyId = _ongoingStories[_currentStoryIndex];
                _onStorySelectedCallback.Invoke(storyId);
                //OnStorySelected?.Invoke(storyId);
            }
        }
    }
}