using System.Collections;
using UnityEngine;

[System.Serializable]
public struct ID
{
#if UNITY_EDITOR
    [SerializeField] private string m_NameID;
#endif
    [SerializeField, HideInInspector] private int m_ID;
    [SerializeField, HideInInspector] private bool m_Initialized;

    public ID(string name)
    {
        m_ID = name.GetHashCode();

#if UNITY_EDITOR
        m_NameID = name;
#endif
        m_Initialized = true;
    }

    public override int GetHashCode()
    {
        return m_ID;
    }

    public bool Initialized() => m_Initialized;

    public static bool operator ==(ID a, ID b)
    {
        if (a.m_ID == b.m_ID)
            return true;
        else
            return false;
    }

    public static bool operator !=(ID a, ID b)
    {
        if (a.m_ID != b.m_ID)
            return true;
        else
            return false;
    }

    public override bool Equals(object obj)
    {
        ID otherID = (ID)obj;
        if (m_ID == otherID.m_ID)
            return true;
        else
            return false;
    }
}
