using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Admin : MonoBehaviour
{
    public static Admin Global { get; private set; }

    public Database Database { get => _database; }
    public Systems Systems { get => _systems; }
    public GameEventSystem EventSystem { get => _eventSystem; }

    [SerializeField] private Systems _systems = new Systems();
    [SerializeField] private Database _database;
    private GameEventSystem _eventSystem;


    public GameDataIDs ID = new GameDataIDs();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Global = FindObjectOfType<Admin>();
        Global.Initialize();
    }

    private void Initialize()
    {
        // INITIALIZATION ORDER MATTERS
        _database.LoadData();

        _eventSystem = new GameEventSystem();

        _systems.m_GameStateSystem.Initialize(Database.GameState); // Game State Sys registers events on init
        _eventSystem.RegisterSystems(_systems.GetSystemsEvents());
        _eventSystem.Initialize();

        _systems.InitializeSystems(EventSystem, Database);

        _systems.m_GameStateSystem.StartGame();
    }
}

[System.Serializable]
public class Systems
{
    public GameStateSystem m_GameStateSystem = new GameStateSystem();
    public LocalizationSystem m_LocalizationSystem = new LocalizationSystem();
    public CameraSystem m_CameraSystem = new CameraSystem();

    public QuestMakingSystem m_QuestMakerSystem = new QuestMakingSystem();
    public CookieMakingSystem m_CookieMakingSystem = new CookieMakingSystem();
    public StorySystem m_StorySystem = new StorySystem();

    public DialogueSystem m_DialogueSystem = new DialogueSystem();
    public NpcSystem m_NpcSystem = new NpcSystem();
    public PopupSystem m_PopupSystem = new PopupSystem();
    public CharacterSystem m_CharacterSystem = new CharacterSystem();
    public InventorySystem m_InventorySystem = new InventorySystem();

    public DaySystem m_DaySystem = new DaySystem();
    public TownSystem m_TownSystem = new TownSystem();
    public CalendarSystem m_CalendarSystem = new CalendarSystem();


    public List<ISystemEvents> GetSystemsEvents()
    {
        List<ISystemEvents> systems = new List<ISystemEvents>();
        systems.Add(m_GameStateSystem);
        systems.Add(m_PopupSystem);
        systems.Add(m_CameraSystem);
        systems.Add(m_DaySystem);
        systems.Add(m_StorySystem);
        systems.Add(m_DialogueSystem);
        systems.Add(m_NpcSystem);
        systems.Add(m_InventorySystem);
        systems.Add(m_CharacterSystem);
        systems.Add(m_CookieMakingSystem);

        return systems;
    }

    public void InitializeSystems(GameEventSystem eventSystem, Database database)
    {
        m_CharacterSystem.Initialize(database.Player.Input);
        m_LocalizationSystem.LoadData();
        m_DaySystem.Initialize(eventSystem, database.World.CurrentDay);
        m_StorySystem.Initialize(database.Stories);
        m_QuestMakerSystem.Initialize(m_StorySystem, database.Stories); // TODO -> Remove Story System dependency
        m_CookieMakingSystem.Initialize(database.Cookies);
        m_NpcSystem.Initialize(database.Stories, database.Npcs, eventSystem);
        m_DialogueSystem.Initialize(database.Dialogues.SceneElements, eventSystem);
        m_TownSystem.Initialize(database.Town);
        m_CalendarSystem.Initialize(database.World.Calendar, database.Stories);
        m_PopupSystem.Initialize(database.Popups);
        m_CameraSystem.Initialize(database.Cameras);
        m_InventorySystem.Initialize(database.Player.Inventory);
    }
}

public class WorldDB
{
    public DayData CurrentDay { get; private set; }
    public CalendarData Calendar { get; private set; }


    public WorldDB()
    {
        CurrentDay = new DayData();
        Calendar = new CalendarData();
    }
}

[System.Serializable]
public class PlayerDB
{
    public InventoryData Inventory { get => m_Inventory; }
    public InputComponent Input { get => m_InputComponent; }

    private InputComponent m_InputComponent = new InputComponent();
    private InventoryData m_Inventory;

    public void LoadData()
    {
        m_Inventory = new InventoryData();
        m_Inventory.Initialize();

        // Automatically initialize to avoid inspector pain
        m_InputComponent.m_Character = Object.FindObjectsOfType<AgentMouseListener>(true).ToList();
    }
}