using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

[TestFixture]
public class QuestMakingTests
{
    private StorySystem m_StoryStateSystem;
    private QuestMaker m_QuestMaker;
    private QuestBuilder m_QuestBuilder;

    [OneTimeSetUp]
    public void Initialize()
    {
        m_StoryStateSystem = new StorySystem();
        m_QuestMaker = new QuestMaker();
        m_QuestBuilder = new QuestBuilder();
    }

    [Test]
    public void Full()
    {

    }
}
