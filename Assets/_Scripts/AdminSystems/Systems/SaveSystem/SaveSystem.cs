using CQM.Components;
using CQM.Databases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


// Save System for WebGL by : https://amalgamatelabs.com/Blog/1/data_persistence
public class SaveSystem : ISystemEvents
{
    ComponentsDatabase c;

    [DllImport("__Internal")] private static extern void SyncFiles();
    [DllImport("__Internal")] private static extern void WindowAlert(string message);


    public void Initialize(ComponentsDatabase c)
    {
        this.c = c;
    }

    public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
    {
        sysID = new ID("save_sys");
        commands = new EventSys();
        callbacks = new EventSys();

        commands.AddEvent(new ID("save")).OnInvoked += Save;
    }

    public void Save()
    {
        // Create Save Game Data Object
        SaveGameData data = new SaveGameData();
        data.m_InventoryComponent = c.m_InventoryComponent;
        data.m_Repercusions = c.GetComponentContainer<StoryRepercusionComponent>();
        data.m_StoryInfoComponents = c.m_StoriesInfo;
        data.m_StoriesState = c.m_GameStoriesStateComponent;
        data.m_TownComponent = c.m_TownComponent;

        // Save it
        string dataPath = string.Format("{0}/CQMSave.dat", Application.persistentDataPath);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream;

        try
        {
            if (File.Exists(dataPath))
            {
                File.WriteAllText(dataPath, string.Empty);
                fileStream = File.Open(dataPath, FileMode.Open);
            }
            else
            {
                fileStream = File.Create(dataPath);
            }

            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                SyncFiles();
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Save: " + e.Message);
        }
    }

    public void Load()
    {
        SaveGameData gameDetails = null;
        string dataPath = string.Format("{0}/CQMSave.dat", Application.persistentDataPath);

        try
        {
            if (File.Exists(dataPath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(dataPath, FileMode.Open);

                gameDetails = (SaveGameData)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Load: " + e.Message);
        }

        if (gameDetails != null)
        {
            c.m_GameStoriesStateComponent = gameDetails.m_StoriesState;
            c.m_InventoryComponent = gameDetails.m_InventoryComponent;
            c.m_TownComponent = gameDetails.m_TownComponent;
            c.m_StoriesInfo = gameDetails.m_StoryInfoComponents;
            c.m_Repercusions = gameDetails.m_Repercusions;
        }
    }

    private void PlatformSafeMessage(string message)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            WindowAlert(message);
        else
            Debug.Log(message);
    }

}


[Serializable]
public class SaveGameData
{
    // This is far from good but it will work
    public Singleton_GameStoriesStateComponent m_StoriesState;
    public Singleton_InventoryComponent m_InventoryComponent;
    public Singleton_TownComponent m_TownComponent;
    public ComponentsContainer<StoryInfoComponent> m_StoryInfoComponents;
    public ComponentsContainer<StoryRepercusionComponent> m_Repercusions;
}