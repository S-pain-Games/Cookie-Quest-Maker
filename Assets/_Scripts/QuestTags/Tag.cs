using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TagIntensity
{
    public Tag Tag { get => _tag; }
    [SerializeField] private Tag _tag;

    public int Intensity { get => _intensity; }
    [SerializeField] private int _intensity;
}

[CreateAssetMenu()]
public class Tag : ScriptableObject
{
    public string TagName { get => _tagName; }
    [SerializeField] private string _tagName = "Unnamed";
}
