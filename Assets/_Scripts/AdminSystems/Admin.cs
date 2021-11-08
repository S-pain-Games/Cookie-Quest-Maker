using CQM.Components;
using CQM.Databases;
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

    private void Initialize()
    {
        // INITIALIZATION ORDER MATTERS
        m_DataBuilders.BuildData(Components);
        Components.Initialize();

        Systems.InitializeGameState(Components.m_GameState, Components.m_TransitionsComponent); // Game State Sys registers events on init
        EventSystem.RegisterSystems(Systems.GetSystemsEvents());
        EventSystem.Initialize();

        Systems.InitializeSystems(EventSystem, Components);

        Systems.StartGame();
    }
}
