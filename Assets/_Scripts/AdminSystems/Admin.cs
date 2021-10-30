using CQM.QuestMaking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameStateSystem))]

[RequireComponent(typeof(CameraSystem))]
[RequireComponent(typeof(StorySystem))]
[RequireComponent(typeof(QuestMakingSystem))]
[RequireComponent(typeof(CookieMakingSystem))]
[RequireComponent(typeof(NpcSystem))]
[RequireComponent(typeof(PopupSystem))]

[RequireComponent(typeof(QuestDBUnityReferences))]
[RequireComponent(typeof(StoryDBUnityReferences))]
[RequireComponent(typeof(NpcDBUnityReferences))]
public class Admin : MonoBehaviour
{
    public static Admin g_Instance;

    public GameDataIDs ID = new GameDataIDs();

    // Game Data Storage
    public StoryDB storyDB;
    public QuestDB questDB;
    public CookieDB cookieDB;
    public NpcDB npcDB;
    public TownDB townData;
    public DialogueDB dialogueDB;
    public DayData dayData;
    [SerializeField] private QuestDBUnityReferences questDBRef;
    [SerializeField] private StoryDBUnityReferences storyDBRef;
    [SerializeField] private NpcDBUnityReferences npcDBRef;

    [HideInInspector] public CalendarData calendarData;

    // Player Data
    [HideInInspector] public InventoryData inventoryData;

    // Game Systems
    [HideInInspector] public QuestMakingSystem questMakerSystem;
    [HideInInspector] public CookieMakingSystem cookieMakingSystem;
    [HideInInspector] public InventorySystem inventorySystem;
    [HideInInspector] public GameEventSystem gameEventSystem;
    [HideInInspector] public LocalizationSystem localizationSystem;

    private GameStateSystem gameStateSystem;
    private CameraSystem camSystem;
    private DaySystem daySystem;
    private StorySystem storySystem;
    private DialogueSystem dialogueSystem;

    [SerializeField]
    private CharacterSystem characterSystem;
    private NpcSystem npcSystem;
    private TownSystem townSystem;
    private CalendarSystem calendarSystem;
    private PopupSystem popupSystem;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        g_Instance = FindObjectOfType<Admin>();
        g_Instance.Initialize();
    }

    private void Initialize()
    {
        // INITIALIZATION ORDER MATTERS
        InitializeData();
        InitializeSystems();
    }

    private void InitializeSystems()
    {
        // Get or create Game Systems
        gameEventSystem = new GameEventSystem();
        gameStateSystem = GetComponent<GameStateSystem>();
        localizationSystem = new LocalizationSystem();
        camSystem = GetComponent<CameraSystem>();
        daySystem = new DaySystem();
        storySystem = GetComponent<StorySystem>();
        dialogueSystem = FindObjectOfType<DialogueSystem>(true); // This might be questionable
        questMakerSystem = GetComponent<QuestMakingSystem>();
        cookieMakingSystem = GetComponent<CookieMakingSystem>();
        npcSystem = GetComponent<NpcSystem>();
        townSystem = new TownSystem();
        calendarSystem = new CalendarSystem();
        inventorySystem = new InventorySystem();
        popupSystem = GetComponent<PopupSystem>();

        // Initialize Events System
        gameEventSystem.RegisterSystem(gameStateSystem);
        gameEventSystem.RegisterSystem(camSystem);
        gameEventSystem.RegisterSystem(daySystem);
        gameEventSystem.RegisterSystem(storySystem);
        gameEventSystem.RegisterSystem(dialogueSystem);
        gameEventSystem.RegisterSystem(npcSystem);
        gameEventSystem.RegisterSystem(inventorySystem);
        gameEventSystem.RegisterSystem(popupSystem);
        gameEventSystem.RegisterSystem(characterSystem);
        gameEventSystem.Initialize();

        gameStateSystem.Initialize(gameEventSystem);
        localizationSystem.LoadData();
        daySystem.Initialize(gameEventSystem, dayData);
        storySystem.Initialize(storyDB);
        questMakerSystem.Initialize(storySystem, storyDB);
        cookieMakingSystem.Initialize(cookieDB);
        npcSystem.Initialize(storyDB, npcDB, gameEventSystem);
        dialogueSystem.Initialize(gameEventSystem);
        townSystem.Initialize(townData);
        calendarSystem.Initialize(calendarData, storyDB);
        inventorySystem.Initialize(inventoryData);
    }

    private void InitializeData()
    {
        // Create DBs and data objects
        storyDB = new StoryDB();
        questDB = new QuestDB();
        cookieDB = new CookieDB();
        npcDB = new NpcDB();
        dialogueDB = new DialogueDB();
        calendarData = new CalendarData();
        townData = new TownDB();
        dayData = new DayData();

        // Create Player Data Containers
        inventoryData = new InventoryData();

        // Get DBs Unity References Adapters
        questDBRef = GetComponent<QuestDBUnityReferences>();
        storyDBRef = GetComponent<StoryDBUnityReferences>();
        npcDBRef = GetComponent<NpcDBUnityReferences>();

        // Load Data
        storyDB.LoadData(storyDBRef);
        questDB.LoadData(questDBRef);
        townData.LoadData(storyDB);
        cookieDB.LoadData();
        dialogueDB.LoadData();
        npcDB.LoadData(npcDBRef);

        // Initialize Player Data Containers
        inventoryData.Initialize();
    }
}
