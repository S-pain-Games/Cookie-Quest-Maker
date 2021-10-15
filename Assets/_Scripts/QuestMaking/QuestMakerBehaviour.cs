using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class QuestMakerBehaviour : MonoBehaviour
{
    [Header("Listening To")]
    [SerializeField]
    private IntEventHandle _onSelectQuest;

    [SerializeField]
    private StorySystemBehaviour storySystemBehaviour;

    [SerializeField]
    private QuestBuilderBehaviour questBuilderBehaviour;

    private QuestMaker m_QuestMaker;

    private void Awake()
    {
        m_QuestMaker = new QuestMaker(storySystemBehaviour.System, questBuilderBehaviour.Builder);
    }

    private void OnEnable()
    {
        m_QuestMaker.OnEnable();
    }

    private void OnDisable()
    {
        m_QuestMaker.OnDisable();
    }

    [MethodButton]
    public void SelectStartQuest()
    {
        m_QuestMaker.SelectStory(0);
    }
}
