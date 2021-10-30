using CQM.QuestMaking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LocalizationSystem))]
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

    public CalendarData calendarData;

    // Player Data
    public InventoryData inventoryData;

    // Game Systems
    public GameEventSystem gameEventSystem;
    public GameStateSystem gameStateSystem;
    public LocalizationSystem localizationSystem;
    public CameraSystem camSystem;

    public DaySystem daySystem;
    public StorySystem storySystem;
    public QuestMakingSystem questMakerSystem;
    public CookieMakingSystem cookieMakingSystem;
    public DialogueSystem dialogueSystem;
    public InventorySystem inventorySystem;

    public NpcSystem npcSystem;
    public TownSystem townSystem;
    public CalendarSystem calendarSystem;
    public PopupSystem popupSystem;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        g_Instance = FindObjectOfType<Admin>();
        g_Instance.Initialize();
    }

    private void Initialize()
    {
        // INITIALIZATION ORDER MATTERS

        // Get or create Game Systems
        gameEventSystem = new GameEventSystem();
        gameStateSystem = GetComponent<GameStateSystem>();
        localizationSystem = GetComponent<LocalizationSystem>();

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

        // Initialize Game Systems
        gameEventSystem.Initialize(this);
        gameStateSystem.Initialize(gameEventSystem);
        localizationSystem.LoadData();

        camSystem.Initialize(gameEventSystem);
        daySystem.Initialize(gameEventSystem, dayData);
        storySystem.Initialize(storyDB, gameEventSystem);
        questMakerSystem.Initialize(storySystem, storyDB);
        cookieMakingSystem.Initialize(cookieDB);
        npcSystem.Initialize(storyDB, npcDB);

        townSystem.Initialize(townData);
        calendarSystem.Initialize(calendarData, storyDB);
        inventorySystem.Initialize(inventoryData);

        // Initialize Player Data Containers
        inventoryData.Initialize();
    }
}
