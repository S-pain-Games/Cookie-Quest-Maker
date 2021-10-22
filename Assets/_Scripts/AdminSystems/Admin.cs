using CQM.QuestMaking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StorySystem))]
[RequireComponent(typeof(GameStateSystem))]
[RequireComponent(typeof(TownSystem))]
[RequireComponent(typeof(QMGameplaySystem))]
public class Admin : MonoBehaviour
{
    public static Admin g_Instance;

    public GameDataIDs ID = new GameDataIDs();

    // Game Data Storage
    public StoryDB storyDB;
    public QuestDB questDB;

    // Game Systems
    public StorySystem storySystem;
    public QMGameplaySystem questMakerSystem;
    public GameStateSystem gameStateSystem;
    public TownSystem townSystem;

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

        // Create DBs
        storyDB = new StoryDB();
        questDB = new QuestDB();

        // Load Data
        storyDB.LoadData();
        questDB.LoadData();

        // Initialize Game Systems References
        storySystem.Initialize(storyDB);
        questMakerSystem.Initialize(storySystem, storyDB);

        // Create Player Systems
        playerPieceStorage = new PlayerPieceStorage();

        // Initialize Player Systems
        playerPieceStorage.Initialize();
    }
}
