using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewspaperData
{
    public string MainTitle = "Untitled";
    public List<NewspaperArticle> Articles { get => _articles; }

    [SerializeField] private List<NewspaperArticle> _articles = new List<NewspaperArticle>();

    /// <summary>
    /// Sort by importance
    /// </summary>
    public void SortArticles()
    {
        _articles.Sort((a, b) => a.importance.CompareTo(b.importance));
    }

    /// <summary>
    /// Reset Newspaper and Articles info to default
    /// </summary>
    public void Clear()
    {
        MainTitle = "Untitled";
        for (int i = 0; i < _articles.Count; i++)
        {
            _articles[i].Clear();
        }
    }
}
