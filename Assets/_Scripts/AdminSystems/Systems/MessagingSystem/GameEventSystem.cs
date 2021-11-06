using CQM.Databases;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem
{
    private Dictionary<ID, EventSys> m_Commands = new Dictionary<ID, EventSys>();
    private Dictionary<ID, EventSys> m_Callbacks = new Dictionary<ID, EventSys>();

    private List<ISystemEvents> m_RegisteredSystems = new List<ISystemEvents>();

    public void RegisterSystem(ISystemEvents sys)
    {
        m_RegisteredSystems.Add(sys);
    }

    public void RegisterSystems(List<ISystemEvents> sys)
    {
        for (int i = 0; i < sys.Count; i++)
        {
            RegisterSystem(sys[i]);
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < m_RegisteredSystems.Count; i++)
        {
            m_RegisteredSystems[i].RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks);
            m_Commands.Add(sysID, commands);
            m_Callbacks.Add(sysID, callbacks);
        }
    }

    public T GetCommand<T>(ID sysID, ID cmdID) where T : class
    {
        m_Commands[sysID].GetEvent(cmdID, out T cmd);
        return cmd;
    }

    public T GetCallback<T>(ID sysID, ID callbackID) where T : class
    {
        m_Callbacks[sysID].GetEvent(callbackID, out T callback);
        return callback;
    }

    public T GetCommandByName<T>(string sysID, string cmdID) where T : class
    {
        m_Commands[new ID(sysID)].GetEvent(new ID(cmdID), out T cmd);
        return cmd;
    }

    public T GetCallbackByName<T>(string sysID, string callbackID) where T : class
    {
        m_Callbacks[new ID(sysID)].GetEvent(new ID(callbackID), out T callback);
        return callback;
    }
}

public interface ISystemEvents
{
    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks);
}