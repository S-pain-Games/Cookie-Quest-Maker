using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class StoryDBTests
{
    private Story _story;
    private QuestTagType _tag1;
    private QuestTagType _tag2;
    private QuestTagType _tag3;

    [SetUp]
    public void Setup()
    {
        string[] guids = AssetDatabase.FindAssets("t:Script StoryDBTests");
        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        path = path.Remove(path.LastIndexOf("/") + 1);
        _story = AssetDatabase.LoadAssetAtPath<Story>(path + "TestStory.asset");
        _tag1 = AssetDatabase.LoadAssetAtPath<QuestTagType>(path + "TestTags/xTest_Tag1.asset");
        _tag2 = AssetDatabase.LoadAssetAtPath<QuestTagType>(path + "TestTags/xTest_Tag2.asset");
        _tag3 = AssetDatabase.LoadAssetAtPath<QuestTagType>(path + "TestTags/xTest_Tag3.asset");
    }

    [Test]
    public void Search_Tag1_1()
    {
        _story.Check(_tag1, 1, out string result);
        Assert.That(result, Is.EqualTo("T1-1"));
    }

    [Test]
    public void Search_Tag1_3()
    {
        _story.Check(_tag1, 3, out string result);
        Assert.That(result, Is.EqualTo("T1-3"));
    }

    [Test]
    public void Search_Tag2_1()
    {
        _story.Check(_tag2, 2, out string result);
        Assert.That(result, Is.EqualTo("T2-1"));
    }

    [Test]
    public void Search_Tag2_2()
    {
        _story.Check(_tag2, 2, out string result);
        Assert.That(result, Is.EqualTo("T2-1"));
    }

    [Test]
    public void Search_Tag2_3()
    {
        _story.Check(_tag2, 3, out string result);
        Assert.That(result, Is.EqualTo("T2-3"));
    }

    [Test]
    public void Search_Tag2_4()
    {
        _story.Check(_tag2, 4, out string result);
        Assert.That(result, Is.EqualTo("T2-3"));
    }

    [Test]
    public void Search_Tag3_1()
    {
        _story.Check(_tag3, 1, out string result);
        Assert.That(result, Is.EqualTo("T3-1"));
    }
}
