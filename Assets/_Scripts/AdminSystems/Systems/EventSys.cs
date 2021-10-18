using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSys : MonoBehaviour
{
    private Dictionary<int, object> m_Events = new Dictionary<int, object>();

    public Event<T> AddEvent<T>(string evtName) where T : new()
    {
        Event<T> evt = new Event<T>();
        m_Events.Add(evtName.GetHashCode(), evt);
        return evt;
    }

    public EventVoid AddEvent(string evtName)
    {
        int id = evtName.GetHashCode();

#if UNITY_EDITOR
        if (m_Events.ContainsKey(id))
        {
            Debug.LogError("Collision between event id's");
        }
#endif

        EventVoid evt = new EventVoid();
        m_Events.Add(evtName.GetHashCode(), evt);
        return evt;
    }

    public bool GetEvent<T>(string evtName, out T evt) where T : class
    {
        int id = evtName.GetHashCode();
        if (m_Events.TryGetValue(id, out object evtObj))
        {
            evt = evtObj as T;
            return true;
        }
        else
        {
            evt = null;
            return false;
        }
    }

    public bool GetEvent(string evtName, out EventVoid evt)
    {
        int id = evtName.GetHashCode();
        if (m_Events.TryGetValue(id, out object evtObj))
        {
            evt = evtObj as EventVoid;
            return true;
        }
        else
        {
            evt = null;
            return false;
        }
    }

    private void Start()
    {
        AddEvent<int>("Evt1");

        if (GetEvent("Evt1", out Event<int> evt))
        {
            evt.OnInvoked += (args) => { args += 3; };
        }
    }

    private void Update()
    {
        if (GetEvent("Evt1", out Event<int> evt))
        {
            evt.Invoke(2);
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