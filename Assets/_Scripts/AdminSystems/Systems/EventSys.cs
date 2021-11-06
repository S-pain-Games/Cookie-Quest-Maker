using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSys
{
    // We have to be careful with boxing and unboxing
    // It "should" work with this design without generating much garbage
    private Dictionary<ID, object> m_Events = new Dictionary<ID, object>();

    public Event<T> AddEvent<T>(ID evtId)
    {
#if UNITY_EDITOR
        if (m_Events.ContainsKey(evtId))
        {
            Debug.LogError("Collision between event id's");
        }
#endif

        Event<T> evt = new Event<T>();
        m_Events.Add(evtId, evt);
        return evt;
    }

    public EventVoid AddEvent(ID evtId)
    {
#if UNITY_EDITOR
        if (m_Events.ContainsKey(evtId))
        {
            Debug.LogError("Collision between event id's");
        }
#endif

        EventVoid evt = new EventVoid();
        m_Events.Add(evtId, evt);
        return evt;
    }

    public bool GetEvent<T>(ID evtId, out T evt) where T : class
    {
        if (m_Events.TryGetValue(evtId, out object evtObj))
        {
            evt = evtObj as T;
            return true;
        }
        else
        {
            evt = null;
            Debug.LogError("Event not found");
            return false;
        }
    }

    public bool GetEvent(ID evtId, out EventVoid evt)
    {
        if (m_Events.TryGetValue(evtId, out object evtObj))
        {
            evt = evtObj as EventVoid;
            return true;
        }
        else
        {
            evt = null;
            Debug.LogError("Event not found");
            return false;
        }
    }
}

public class Event<T>
{
    public event Action<T> OnInvoked;

    public void Invoke(T arg)
    {
        OnInvoked?.Invoke(arg);
    }

    public void LogListeners()
    {
        var delegates = OnInvoked.GetInvocationList();
        foreach (var del in delegates)
        {
            Logg.Log(del.Target.ToString());
        }
    }
}

public class EventVoid
{
    public event Action OnInvoked;

    public void Invoke()
    {
        OnInvoked?.Invoke();
    }
}