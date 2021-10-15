using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDBSystem : MonoBehaviour
{
    [SerializeField]
    private QuestBuilder m_PhraseBuilder;

    [SerializeField]
    private StoryData m_Story;

    [SerializeField]
    private QuestPieceTagType m_Tag;

    [SerializeField]
    private int m_Value;

    [MethodButton]
    public void GetTestStory()
    {
        GetStoryPhrase(m_Story, m_Tag, m_Value);
    }

    public void GetStoryPhrase(StoryData story, QuestPieceTagType tag, int value)
    {
        story.Check(tag, value, out string result);
        Logg.Log(result, "StoryDB");
    }
}
