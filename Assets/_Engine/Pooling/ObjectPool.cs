using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "Infinitely" expandable object pool
public class ObjectPool<T> where T : new()
{
    private Stack<T> m_Objects;

    public ObjectPool(int startSize)
    {
        m_Objects = new Stack<T>(startSize);
        for (int i = 0; i < startSize; i++)
        {
            m_Objects.Push(new T());
        }
    }

    public T Get()
    {
        if (m_Objects.Count > 0)
        {
            return m_Objects.Pop();
        }
        else
        {
            Logg.Log("Pool ran out of objects, creating a new one");
            return new T();
        }
    }

    public void Return(T obj)
    {
        m_Objects.Push(obj);
    }
}
