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

    [HideInInspector]
    public StorySystem storySystem;
    public StoryDB storyDB = new StoryDB();

    [HideInInspector]
    public QMGameplaySystem questMakerSystem;
    public QuestDB questDB = new QuestDB();

    [HideInInspector]
    public GameStateSystem gameStateSystem;

    [HideInInspector]
    public TownSystem townSystem;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        g_Instance = FindObjectOfType<Admin>();
        g_Instance.Initialize();
    }

    private void Initialize()
    {
        storySystem = GetComponent<StorySystem>();
        storySystem.storyDB = storyDB;

        gameStateSystem = GetComponent<GameStateSystem>();

        questMakerSystem = GetComponent<QMGameplaySystem>();
        questMakerSystem.storySystem = storySystem;
        questMakerSystem.storyDB = storyDB;
    }
}
