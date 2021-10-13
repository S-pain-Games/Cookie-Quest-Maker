using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Word : ScriptableObject
{
    public string WordText { get => m_WordText; }
    public WordType Type { get => m_Type; }
    public List<TagIntensity> Tags { get => m_Tags; }

    [SerializeField]
    private string m_WordText = "Unnamed";

    [SerializeField]
    private WordType m_Type = WordType.Action;

    [SerializeField]
    public List<TagIntensity> m_Tags = new List<TagIntensity>();

    public enum WordType
    {
        Action,
        Target
    }
}
