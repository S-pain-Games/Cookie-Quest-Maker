using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WordPiece : ScriptableObject
{
    public string Word { get => _word; }

    [SerializeField] private string _word = "Unnamed";
}
