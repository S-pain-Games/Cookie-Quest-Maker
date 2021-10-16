using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StorySystem))]
[RequireComponent(typeof(GameStateSystem))]
[RequireComponent(typeof(TownSystem))]
public class Admin : MonoBehaviour
{
    public static Admin g_Instance;

    [HideInInspector]
    public StorySystem storySystem;

    [HideInInspector]
    public GameStateSystem gameStateSystem;

    [HideInInspector]
    public TownSystem townSystem;

    private void Awake()
    {
        g_Instance = this;
        storySystem = GetComponent<StorySystem>();
        gameStateSystem = GetComponent<GameStateSystem>();
    }
}
