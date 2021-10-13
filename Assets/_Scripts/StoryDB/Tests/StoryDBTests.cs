using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class StoryDBTests
{
    private Story _story;

    [SetUp]
    public void Setup()
    {
        string[] guids = AssetDatabase.FindAssets("t:Script StoryDBTests");
        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        path = path.Remove(path.LastIndexOf("/") + 1);
        _story = AssetDatabase.LoadAssetAtPath<Story>(path + "TestStory.asset");
    }

    [Test]
    public void TestExistence()
    {

    }
}
