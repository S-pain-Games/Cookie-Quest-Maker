using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

[TestFixture]
public class NewspaperTests
{
    private NewspaperController _newspaperController;
    private NewspaperData _newspaperData;

    [SetUp]
    public void SetupTest()
    {
        GameObject g = new GameObject();
        _newspaperController = g.AddComponent<NewspaperController>();

        _newspaperData = new NewspaperData();
        for (int i = 0; i < 10; i++)
        {
            _newspaperData.Articles.Add(new NewspaperArticle($"Article_{i}", $"Content_{i}"));
        }
    }

    [Test]
    public void ClearsToDefaultTitle()
    {
        _newspaperData.MainTitle = "Random";
        _newspaperData.Clear();
        Assert.That(_newspaperData.MainTitle, Is.EqualTo("Untitled"));
    }

    [Test]
    public void ClearsArticlesToDefault()
    {
        _newspaperData.Articles.Add(new NewspaperArticle("Random", "Randomer"));
        _newspaperData.Clear();

        for (int i = 0; i < _newspaperData.Articles.Count; i++)
        {
            Assert.That(_newspaperData.Articles[i].importance, Is.EqualTo(0));
            Assert.That(_newspaperData.Articles[i].title, Is.EqualTo("Untitled"));
            Assert.That(_newspaperData.Articles[i].content, Is.EqualTo("Empty"));
        }
    }
}
