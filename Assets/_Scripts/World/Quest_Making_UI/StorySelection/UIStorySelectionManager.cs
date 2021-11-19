using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CQM.Databases;
using CQM.Components;


namespace CQM.UI.QuestMakingTable
{
    public class UIStorySelectionManager : MonoBehaviour
    {
        // UI Events
        public event Action<ID> OnStorySelected;
        public event Action OnExit;

        // UI Controls
        [Header("Buttons")]
        [SerializeField] private Button _nextStoryButton;
        [SerializeField] private Button _previousStoryButton;
        [SerializeField] private Button _selectStoryButton;
        [SerializeField] private Button _exitButton;

        // Story View
        [Header("Story Card")]
        [SerializeField] private Image _selectedStoryImage;
        [SerializeField] private TextMeshProUGUI _selectedStoryTitle;
        [SerializeField] private Image _storyIconImage;

        [SerializeField] private Sprite _mainStoryIconSprite;
        [SerializeField] private Sprite _secondaryStoryIconSprite;

        //Story Description
        [SerializeField] private TextMeshProUGUI _selectedStoryDescription;

        // Game Data
        private List<ID> _ongoingStories;
        private ComponentsContainer<StoryUIDataComponent> _storyUIComponents;

        // UI Data
        private int _currentStoryIndex = 0;


        private void Awake()
        {
            _ongoingStories = Admin.Global.Components.m_StoriesStateComponent.m_OngoingStories;
            _storyUIComponents = Admin.Global.Components.GetComponentContainer<StoryUIDataComponent>();
        }

        private void OnEnable()
        {
            _nextStoryButton.onClick.AddListener(NextStory);
            _previousStoryButton.onClick.AddListener(PreviousStory);
            _selectStoryButton.onClick.AddListener(SelectStory);
            _exitButton.onClick.AddListener(Exit);

            UpdateSelectedStoryUI();
        }

        private void OnDisable()
        {
            _nextStoryButton.onClick.RemoveListener(NextStory);
            _previousStoryButton.onClick.RemoveListener(PreviousStory);
            _selectStoryButton.onClick.RemoveListener(SelectStory);
            _exitButton.onClick.RemoveListener(Exit);
        }


        private void UpdateSelectedStoryUI()
        {
            int numOngoingStories = _ongoingStories.Count;
            if (numOngoingStories <= 0)
            {
                _selectedStoryImage.gameObject.SetActive(false);
                _selectedStoryTitle.text = "Ninguna Historia Disponible";

                _storyIconImage.gameObject.SetActive(false);
            }
            else
            {
                _currentStoryIndex = Mathf.Clamp(_currentStoryIndex, 0, numOngoingStories - 1);
                ID storyId = _ongoingStories[_currentStoryIndex];
                var data = _storyUIComponents[storyId];

                _selectedStoryImage.gameObject.SetActive(true);
                _selectedStoryImage.sprite = data.m_Sprite;
                _selectedStoryTitle.text = data.m_Title;

                UpdateQuestIconImage(storyId);

                _selectedStoryDescription.SetText(Admin.Global.Components.GetComponentContainer<StoryInfoComponent>().GetComponentByID(storyId).m_StoryData.m_Description);
                //string desc = Admin.Global.Components.GetComponentContainer<StoryInfoComponent>().GetComponentByID(storyId).m_StoryData.m_Description;
            }
        }

        private void UpdateQuestIconImage(ID storyId)
        {
            _storyIconImage.gameObject.SetActive(true);

            if (IsSecondaryStory(storyId))
                _storyIconImage.sprite = _secondaryStoryIconSprite;
            else
                _storyIconImage.sprite = _mainStoryIconSprite;
        }

        private bool IsSecondaryStory(ID storyId)
        {
            return Admin.Global.Components.m_StoriesStateComponent.m_AllSecondaryStories.Contains(storyId);
        }

        private void NextStory()
        {
            // Increment and wrap around
            _currentStoryIndex++;
            if (_currentStoryIndex >= _ongoingStories.Count)
                _currentStoryIndex = 0;

            UpdateSelectedStoryUI();
        }

        private void PreviousStory()
        {
            // Decrement and wrap around
            _currentStoryIndex--;
            if (_currentStoryIndex < 0)
                _currentStoryIndex = _ongoingStories.Count;

            UpdateSelectedStoryUI();
        }

        private void SelectStory()
        {
            if (_ongoingStories.Count > 0)
            {
                ID storyId = _ongoingStories[_currentStoryIndex];
                OnStorySelected.Invoke(storyId);
            }
        }

        private void Exit()
        {
            OnExit.Invoke();
        }
    }
}