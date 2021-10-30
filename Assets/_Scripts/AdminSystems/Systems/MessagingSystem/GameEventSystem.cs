using CQM.QuestMaking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem
{
    private Dictionary<int, EventSys> m_Commands = new Dictionary<int, EventSys>();
    private Dictionary<int, EventSys> m_Callbacks = new Dictionary<int, EventSys>();

    private List<ISystemEvents> m_RegisteredSystems = new List<ISystemEvents>();

    public void RegisterSystem(ISystemEvents sys)
    {
        m_RegisteredSystems.Add(sys);
    }

    public void Initialize()
    {
        for (int i = 0; i < m_RegisteredSystems.Count; i++)
        {
            m_RegisteredSystems[i].RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks);
            m_Commands.Add(sysID, commands);
            m_Callbacks.Add(sysID, callbacks);
        }
    }

    public T GetCommand<T>(int sysID, int cmdID) where T : class
    {
        m_Commands[sysID].GetEvent(cmdID, out T cmd);
        return cmd;
    }

    public T GetCallback<T>(int sysID, int callbackID) where T : class
    {
        m_Callbacks[sysID].GetEvent(callbackID, out T callback);
        return callback;
    }

    public T GetCommandByName<T>(string sysID, string cmdID) where T : class
    {
        m_Commands[sysID.GetHashCode()].GetEvent(cmdID.GetHashCode(), out T cmd);
        return cmd;
    }

    public T GetCallbackByName<T>(string sysID, string callbackID) where T : class
    {
        m_Callbacks[sysID.GetHashCode()].GetEvent(callbackID.GetHashCode(), out T callback);
        return callback;
    }
}

public interface ISystemEvents
{
    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks);
}