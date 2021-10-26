using CQM.QuestMaking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StorySystem))]
[RequireComponent(typeof(GameStateSystem))]
[RequireComponent(typeof(QMGameplaySystem))]
[RequireComponent(typeof(QuestDBUnityReferences))]
[RequireComponent(typeof(StoryDBUnityReferences))]
[RequireComponent(typeof(NpcDBUnityReferences))]
[RequireComponent(typeof(CookieMakingSystem))]
[RequireComponent(typeof(LocalizationSystem))]
[RequireComponent(typeof(NpcSystem))]
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
    [SerializeField] private QuestDBUnityReferences questDBRef;
    [SerializeField] private StoryDBUnityReferences storyDBRef;
    [SerializeField] private NpcDBUnityReferences npcDBRef;

    public CalendarData calendarData;

    // Player Data
    public PlayerUnlockedPieces playerPieceStorage;
    public PlayerBakingIngredients playerBakingIngredients;
    public PlayerReputation playerReputation;

    // Game Systems
    public GameEventSystem gameEventSystem;
    public StorySystem storySystem;
    public QMGameplaySystem questMakerSystem;
    public GameStateSystem gameStateSystem;
    public TownSystem townSystem;
    public CookieMakingSystem cookieMakingSystem;
    public LocalizationSystem localizationSystem;
    public CalendarSystem calendarSystem;
    public NpcSystem npcSystem;
    public DialogueSystem dialogueSystem;

    public ReputationSystem reputationSystem;
    public IngredientsSystem ingredientsSystem;

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
        storySystem = GetComponent<StorySystem>();
        gameStateSystem = GetComponent<GameStateSystem>();
        questMakerSystem = GetComponent<QMGameplaySystem>();
        cookieMakingSystem = GetComponent<CookieMakingSystem>();
        localizationSystem = GetComponent<LocalizationSystem>();
        townSystem = new TownSystem();
        calendarSystem = new CalendarSystem();
        reputationSystem = new ReputationSystem();
        ingredientsSystem = new IngredientsSystem();
        npcSystem = GetComponent<NpcSystem>();
        dialogueSystem = FindObjectOfType<DialogueSystem>(true); // This might be questionable

        // Create DBs and data objects
        storyDB = new StoryDB();
        questDB = new QuestDB();
        cookieDB = new CookieDB();
        npcDB = new NpcDB();
        dialogueDB = new DialogueDB();
        calendarData = new CalendarData();
        townData = new TownDB();

        // Create Player Data Containers
        playerPieceStorage = new PlayerUnlockedPieces();
        playerReputation = new PlayerReputation();
        playerBakingIngredients = new PlayerBakingIngredients();

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
        gameEventSystem.Initialize();
        gameEventSystem.LinkCommandEvents(this);
        storySystem.Initialize(storyDB, gameEventSystem);
        questMakerSystem.Initialize(storySystem, storyDB);
        cookieMakingSystem.Initialize(cookieDB);
        npcSystem.Initialize(storyDB, npcDB);
        localizationSystem.LoadData();

        townSystem.Initialize(townData);
        calendarSystem.Initialize(calendarData, storyDB);
        reputationSystem.Initialize(playerReputation);
        ingredientsSystem.Initialize(playerBakingIngredients);

        // Initialize Player Data Containers
        playerPieceStorage.Initialize();
    }
}
