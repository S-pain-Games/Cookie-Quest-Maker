using CQM.Components;
using CQM.Databases;
using CQM.Systems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Admin : MonoBehaviour
{
    public static Admin Global { get; private set; }

    public Systems Systems = new Systems();
    public ComponentsDatabase Components;
    public GameEventSystem EventSystem = new GameEventSystem();

    public GameDataIDsUSECAREFULLY ID = new GameDataIDsUSECAREFULLY();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Global = FindObjectOfType<Admin>();
        Global.Initialize();
    }

    private void Initialize()
    {
        // INITIALIZATION ORDER MATTERS
        Components.LoadData();

        Systems.InitializeGameState(Components.m_GameState, Components.m_TransitionsComponent); // Game State Sys registers events on init
        EventSystem.RegisterSystems(Systems.GetSystemsEvents());
        EventSystem.Initialize();

        Systems.InitializeSystems(EventSystem, Components);

        Systems.StartGame();
    }
}


[System.Serializable]
public class Systems
{
    private GameStateSystem m_GameStateSystem = new GameStateSystem();
    public LocalizationSystem m_LocalizationSystem = new LocalizationSystem();
    private CameraSystem m_CameraSystem = new CameraSystem();

    public QuestMakingSystem m_QuestMakerSystem = new QuestMakingSystem();
    public CookieMakingSystem m_CookieMakingSystem = new CookieMakingSystem();
    private StorySystem m_StorySystem = new StorySystem();

    private DialogueSystem m_DialogueSystem = new DialogueSystem();
    private NpcSystem m_NpcSystem = new NpcSystem();
    private PopupSystem m_PopupSystem = new PopupSystem();
    private CharacterSystem m_CharacterSystem = new CharacterSystem();
    public InventorySystem m_InventorySystem = new InventorySystem();

    private DaySystem m_DaySystem = new DaySystem();
    private TownSystem m_TownSystem = new TownSystem();
    private CalendarSystem m_CalendarSystem = new CalendarSystem();
    private NewspaperSystem m_NewspaperSystem = new NewspaperSystem();

    private UISystem m_UISystem = new UISystem();



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
        systems.Add(m_NewspaperSystem);
        systems.Add(m_UISystem);

        return systems;
    }

    public void InitializeGameState(Singleton_GameStateComponent gameStateComp, GameStateSystem.Singleton_TransitionsComponent transitionsComp)
    {
        m_GameStateSystem.Initialize(gameStateComp, transitionsComp);
    }

    public void InitializeSystems(GameEventSystem eventSystem, ComponentsDatabase d)
    {
        m_CharacterSystem.Initialize(d.m_InputComponent);
        m_LocalizationSystem.LoadData();
        m_DaySystem.Initialize(eventSystem, d.m_DayComponent);
        m_StorySystem.Initialize(d.m_StoriesInfo, d.m_OngoingStories, d.m_StoriesToStart, d.m_CompletedStories, d.m_FinalizedStories);
        m_QuestMakerSystem.Initialize();
        m_CookieMakingSystem.Initialize(d.m_RecipeData, d.m_InventoryComponent);
        m_NpcSystem.Initialize(d.m_NpcReferencesComponent, d.m_StoriesInfo, d.m_StoriesToStart, d.m_CompletedStories, eventSystem);
        m_DialogueSystem.Initialize(d.m_DialogueUIData, d.m_CharacterComponents, d.m_CharacterDialogueComponents, eventSystem);
        m_TownSystem.Initialize(d.m_TownComponent, d.m_LocationsComponents, d.m_Repercusions);
        m_CalendarSystem.Initialize(d.m_CalendarComponent);
        m_PopupSystem.Initialize(d.m_Popups);
        m_CameraSystem.Initialize(d.m_Cameras);
        m_InventorySystem.Initialize(d.m_InventoryComponent);
        m_NewspaperSystem.Initialize(d.m_NewspaperRefs, d.m_Newspaper, d.m_StoriesInfo);
        m_UISystem.Initialize(d.m_UIRefs);
    }

    public void StartGame()
    {
        m_GameStateSystem.StartGame();
    }
}