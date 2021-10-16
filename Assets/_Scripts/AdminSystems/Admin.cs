using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StorySystem))]
[RequireComponent(typeof(GameStateSystem))]
[RequireComponent(typeof(TownSystem))]
[RequireComponent(typeof(QuestMakerSystem))]
public class Admin : MonoBehaviour
{
    public static Admin g_Instance;

    [HideInInspector]
    public StorySystem storySystem;

    [HideInInspector]
    public GameStateSystem gameStateSystem;

    [HideInInspector]
    public TownSystem townSystem;

    [HideInInspector]
    public QuestMakerSystem questMakerSystem;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        g_Instance = FindObjectOfType<Admin>();
        g_Instance.Initialize();
    }

    private void Initialize()
    {
        storySystem = GetComponent<StorySystem>();
        gameStateSystem = GetComponent<GameStateSystem>();
        questMakerSystem = GetComponent<QuestMakerSystem>();
    }
}
