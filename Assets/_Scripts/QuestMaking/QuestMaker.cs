using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Entry point to the Quest Making Gameplay</para>
/// </summary>
public class QuestMaker
{
    [SerializeField]
    private StorySystem _storySystem;

    [SerializeField]
    private QuestBuilder _questBuilder;

    private Story _currentStory;

    public QuestMaker(StorySystem storySystem, QuestBuilder questBuilder)
    {
        _storySystem = storySystem;
        _questBuilder = questBuilder;
    }

    /// <summary>
    /// Stories Data for the UI
    /// </summary>
    public IReadOnlyList<Story> GetAvailableStories()
    {
        return _storySystem.OngoingStories;
    }

    /// <param name="index">Index of the story in ongoing stories list</param>
    public void SelectStory(int index)
    {
        _currentStory = _storySystem.OngoingStories[index];
    }

    public void OnEnable()
    {
        _questBuilder.OnQuestFinished += OnQuestBuilderCompleted;
    }

    public void OnDisable()
    {
        _questBuilder.OnQuestFinished -= OnQuestBuilderCompleted;
    }

    /// <summary>
    /// Called when the Quest Builder Finishes
    /// <para>Packs the Quest with the Current Story and sends it to the Story System</para>
    /// </summary>
    private void OnQuestBuilderCompleted(Quest quest)
    {
        _storySystem.CompleteQuest(_currentStory.Data, quest);
    }
}
