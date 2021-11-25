using CQM.Components;
using CQM.Databases;
using CQM.Systems;
using System.Collections.Generic;

[System.Serializable]
public class Systems
{
    // Core Engine Systems (Layer 0)
    private GameStateSystem m_GameStateSystem = new GameStateSystem();
    public LocalizationSystem m_LocalizationSystem = new LocalizationSystem();
    private CameraSystem m_CameraSystem = new CameraSystem();
    private AudioSystem m_AudioSystem = new AudioSystem();
    private UISystem m_UISystem = new UISystem();
    private PopupSystem m_PopupSystem = new PopupSystem();
    private DialogueSystem m_DialogueSystem = new DialogueSystem();

    // Core Gameplay Systems (Layer 1)
    private StorySystem m_StorySystem = new StorySystem();
    private NpcSystem m_NpcSystem = new NpcSystem();
    private TownSystem m_TownSystem = new TownSystem();
    private DaySystem m_DaySystem = new DaySystem();
    private InventorySystem m_InventorySystem = new InventorySystem();

    // Gameplay Systems (Layer 3)
    public QuestMakingSystem m_QuestMakerSystem = new QuestMakingSystem();
    public CookieMakingSystem m_CookieMakingSystem = new CookieMakingSystem();
    private PieceCraftingSystem m_PieceCraftingSystem = new PieceCraftingSystem();
    private ShopSystem m_ShopSystem = new ShopSystem();

    private CharacterSystem m_CharacterSystem = new CharacterSystem();
    private CalendarSystem m_CalendarSystem = new CalendarSystem();
    private NewspaperSystem m_NewspaperSystem = new NewspaperSystem();
    private RewardsSystem m_RewardsSystem = new RewardsSystem();


    public void InitializeGameState(Singleton_GameStateComponent gameStateComp, GameStateSystem.Singleton_TransitionsComponent transitionsComp)
    {
        m_GameStateSystem.Initialize(gameStateComp, transitionsComp);
    }

    public void InitializeSystems(GameEventSystem eventSystem, ComponentsDatabase d)
    {
        m_CharacterSystem.Initialize(d.m_InputComponent);
        m_DaySystem.Initialize(eventSystem, d.m_DayComponent);
        m_StorySystem.Initialize(d.GetComponentContainer<StoryInfoComponent>(), d.m_GameStoriesStateComponent);
        m_QuestMakerSystem.Initialize();
        m_CookieMakingSystem.Initialize(d.GetComponentContainer<RecipeDataComponent>(), d.m_InventoryComponent);
        m_PieceCraftingSystem.Initialize(d.GetComponentContainer<RecipeDataComponent>(), d.m_InventoryComponent); // NEW
        m_ShopSystem.Initialize(d.GetComponentContainer<RecipeDataComponent>(), d.m_InventoryComponent, d.GetComponentContainer<IngredientComponent>()); // NEW
        m_NpcSystem.Initialize(d.m_NpcReferencesComponent, d.m_GameStoriesStateComponent, d.GetComponentContainer<StoryInfoComponent>(), d.GetComponentContainer<CharacterComponent>(), d.GetComponentContainer<CharacterDialogueComponent>(), eventSystem);
        m_DialogueSystem.Initialize(d.m_DialogueUIData, d.m_CharacterComponents, d.m_CharacterDialogueComponents, eventSystem);
        m_TownSystem.Initialize(d.m_TownComponent, d.m_LocationsComponents, d.GetComponentContainer<StoryRepercusionComponent>());
        m_CalendarSystem.Initialize(d.m_CalendarComponent);
        m_PopupSystem.Initialize(d.m_Popups);
        m_CameraSystem.Initialize(d.m_Cameras);
        m_InventorySystem.Initialize(d.m_InventoryComponent);
        m_NewspaperSystem.Initialize(d.m_NewspaperRefs, d.m_Newspaper, d.GetComponentContainer<StoryInfoComponent>());
        m_UISystem.Initialize(d.m_UIRefs);
        m_LocalizationSystem.Initialize(d.m_LocalizationComponent);
        m_AudioSystem.Initialize(d.m_AudioDataComponent, d.m_AudioClipsComponent);
        m_RewardsSystem.Initialize(d.m_LocationsComponents);
    }

    public List<ISystemEvents> GetAllSystemsWithEvents()
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
        systems.Add(m_PieceCraftingSystem);
        systems.Add(m_ShopSystem);
        systems.Add(m_NewspaperSystem);
        systems.Add(m_UISystem);
        systems.Add(m_AudioSystem);

        // All systems returned here will be initialized by the Event System
        return systems;
    }

    public void StartGame()
    {
        m_GameStateSystem.StartGame();
    }
}