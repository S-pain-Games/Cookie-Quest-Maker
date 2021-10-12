using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Word : ScriptableObject
{
    public string WordText { get => _wordText; }
    public WordType Type { get => _type; }

    [SerializeField] private string _wordText = "Unnamed";
    [SerializeField] private WordType _type = WordType.Action;

    public enum WordType
    {
        Action,
        Target
    }

    public List<TagIntensity> TagList = new List<TagIntensity>();
}
