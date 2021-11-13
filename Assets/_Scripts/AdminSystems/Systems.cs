﻿using CQM.Components;
using CQM.Databases;
using CQM.Systems;
using System.Collections.Generic;

[System.Serializable]
public class Systems
{
    private GameStateSystem m_GameStateSystem = new GameStateSystem();
    public LocalizationSystem m_LocalizationSystem = new LocalizationSystem();
    private CameraSystem m_CameraSystem = new CameraSystem();

    public QuestMakingSystem m_QuestMakerSystem = new QuestMakingSystem();
    public CookieMakingSystem m_CookieMakingSystem = new CookieMakingSystem();
    public PieceCraftingSystem pieceCraftingSystem = new PieceCraftingSystem(); // NEW
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


    public void InitializeGameState(Singleton_GameStateComponent gameStateComp, GameStateSystem.Singleton_TransitionsComponent transitionsComp)
    {
        m_GameStateSystem.Initialize(gameStateComp, transitionsComp);
    }

    public void InitializeSystems(GameEventSystem eventSystem, ComponentsDatabase d)
    {
        m_CharacterSystem.Initialize(d.m_InputComponent);
        m_DaySystem.Initialize(eventSystem, d.m_DayComponent);
        m_StorySystem.Initialize(d.GetComponentContainer<StoryInfoComponent>(), d.m_OngoingStories, d.m_StoriesToStart, d.m_CompletedStories, d.m_FinalizedStories);
        m_QuestMakerSystem.Initialize();
        m_CookieMakingSystem.Initialize(d.GetComponentContainer<RecipeDataComponent>(), d.m_InventoryComponent);
        pieceCraftingSystem.Initialize(d.GetComponentContainer<RecipeDataComponent>(), d.m_InventoryComponent, d.m_QuestPieceFunctionalComponents); // NEW
        m_NpcSystem.Initialize(d.m_NpcReferencesComponent, d.GetComponentContainer<StoryInfoComponent>(), d.m_StoriesToStart, d.m_CompletedStories, eventSystem);
        m_DialogueSystem.Initialize(d.m_DialogueUIData, d.m_CharacterComponents, d.m_CharacterDialogueComponents, eventSystem);
        m_TownSystem.Initialize(d.m_TownComponent, d.m_LocationsComponents, d.GetComponentContainer<StoryRepercusionComponent>());
        m_CalendarSystem.Initialize(d.m_CalendarComponent);
        m_PopupSystem.Initialize(d.m_Popups);
        m_CameraSystem.Initialize(d.m_Cameras);
        m_InventorySystem.Initialize(d.m_InventoryComponent);
        m_NewspaperSystem.Initialize(d.m_NewspaperRefs, d.m_Newspaper, d.GetComponentContainer<StoryInfoComponent>());
        m_UISystem.Initialize(d.m_UIRefs);
        m_LocalizationSystem.Initialize(d.m_LocalizationComponent);
    }

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
        systems.Add(pieceCraftingSystem); // NEW
        systems.Add(m_NewspaperSystem);
        systems.Add(m_UISystem);

        return systems;
    }

    public void StartGame()
    {
        m_GameStateSystem.StartGame();
    }
}