using CQM.Components;
using CQM.Databases;
using CQM.DataBuilders;
using System.Collections;
using System.Linq;
using UnityEngine;


public class Admin : MonoBehaviour
{
    public static Admin Global { get; private set; }

    public DataBuilders m_DataBuilders;
    public Systems Systems = new Systems();
    public ComponentsDatabase Components;
    public GameEventSystem EventSystem = new GameEventSystem();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Global = FindObjectOfType<Admin>();
        Global.Initialize();
    }

    public void Initialize()
    {
        // INITIALIZATION ORDER MATTERS
        Components.Initialize();
        m_DataBuilders.BuildData(Components);

        Systems.InitializeGameState(Components.m_GameState, Components.m_TransitionsComponent); // Game State Sys registers events on init
        EventSystem.RegisterSystems(Systems.GetAllSystemsWithEvents());
        EventSystem.Initialize();

        Systems.InitializeSystems(EventSystem, Components);

        Systems.StartGame();
    }
}
