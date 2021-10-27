using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewspaperArticle
{
    public int importance = 0;
    public string title = "Untitled";
    public string content = "Empty";

    public NewspaperArticle(string title, string content)
    {
        this.title = title;
        this.content = content;
    }

    /// <summary>
    /// Reset all article info to default
    /// </summary>
    public void Clear()
    {
        importance = 0;
        title = "Untitled";
        content = "Empty";
    }
}