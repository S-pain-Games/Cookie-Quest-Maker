using CQM.QuestMaking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StorySystem))]
[RequireComponent(typeof(GameStateSystem))]
[RequireComponent(typeof(TownSystem))]
[RequireComponent(typeof(QMGameplaySystem))]
[RequireComponent(typeof(QuestDBUnityReferences))]
<<<<<<< HEAD
[RequireComponent(typeof(CookieMakingSystem))]
=======
[RequireComponent(typeof(LocalizationSystem))]
>>>>>>> f8f052379887edc517764749e799018f0091f6a9
public class Admin : MonoBehaviour
{
    public static Admin g_Instance;

    public GameDataIDs ID = new GameDataIDs();

    // Game Data Storage
    public StoryDB storyDB;
    public QuestDB questDB;
    public CookieDB cookieDB;
    [SerializeField] private QuestDBUnityReferences questDBRef;

    // Game Systems
    public StorySystem storySystem;
    public QMGameplaySystem questMakerSystem;
    public GameStateSystem gameStateSystem;
    public TownSystem townSystem;
<<<<<<< HEAD
    public CookieMakingSystem cookieMakingSystem;
=======
    public LocalizationSystem localizationSystem;
>>>>>>> f8f052379887edc517764749e799018f0091f6a9

    // Player Systems
    public PlayerPieceStorage playerPieceStorage;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        g_Instance = FindObjectOfType<Admin>();
        g_Instance.Initialize();
    }

    private void Initialize()
    {
        // INITIALIZATION ORDER MATTERS

        // Get Game Systems Components
        storySystem = GetComponent<StorySystem>();
        gameStateSystem = GetComponent<GameStateSystem>();
        questMakerSystem = GetComponent<QMGameplaySystem>();
<<<<<<< HEAD
        cookieMakingSystem = GetComponent<CookieMakingSystem>();
=======
        localizationSystem = GetComponent<LocalizationSystem>();
>>>>>>> f8f052379887edc517764749e799018f0091f6a9

        // Create DBs
        storyDB = new StoryDB();
        questDB = new QuestDB();
        cookieDB = new CookieDB();

        // Get DBs Unity References Adapters
        questDBRef = GetComponent<QuestDBUnityReferences>();

        // Load Data
        storyDB.LoadData();
        questDB.LoadData(questDBRef);
        cookieDB.LoadData();

        // Initialize Game Systems References
        storySystem.Initialize(storyDB);
        questMakerSystem.Initialize(storySystem, storyDB);
        localizationSystem.LoadData();

        // Create Player Systems
        playerPieceStorage = new PlayerPieceStorage();

        // Initialize Player Systems
        playerPieceStorage.Initialize();
    }
}
