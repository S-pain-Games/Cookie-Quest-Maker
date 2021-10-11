using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WordPiece : ScriptableObject
{
    public string Word { get => _word; }
    public WordType Type { get => _type; }

    [SerializeField] private string _word = "Unnamed";
    [SerializeField] private WordType _type = WordType.Action;

    public enum WordType
    {
        Action,
        Target
    }
}
