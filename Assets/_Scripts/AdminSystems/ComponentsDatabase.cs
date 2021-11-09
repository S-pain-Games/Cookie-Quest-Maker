using CQM.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using static GameStateSystem;

namespace CQM.Databases
{
    [System.Serializable]
    public class ComponentsDatabase : MonoBehaviour
    {
        // All the Stories Data in the game (Persistent & Runtime)
        private ComponentsContainer<StoryInfoComponent> m_StoriesInfo = new ComponentsContainer<StoryInfoComponent>();
        // Story UI Data used in the story selection UI (Persistent)
        private ComponentsContainer<StoryUIDataComponent> m_StoriesUI = new ComponentsContainer<StoryUIDataComponent>();
        // All the repercusions in the game (Persistent)
        private ComponentsContainer<StoryRepercusionComponent> m_Repercusions = new ComponentsContainer<StoryRepercusionComponent>();


        // Contains all the cookie recipes
        private ComponentsContainer<RecipeDataComponent> m_RecipeData = new ComponentsContainer<RecipeDataComponent>();
        // Contains all the data for the cookies
        public ComponentsContainer<CookieDataComponent> m_CookieData = new ComponentsContainer<CookieDataComponent>();

        // Contains all the quest piece data
        public ComponentsContainer<QuestPieceFunctionalComponent> m_QuestPieceFunctionalComponents = new ComponentsContainer<QuestPieceFunctionalComponent>();
        public ComponentsContainer<UIQuestPieceComponent> m_QuestPieceUIComponent = new ComponentsContainer<UIQuestPieceComponent>();
        public ComponentsContainer<QuestPiecePrefabComponent> m_QuestPiecePrefabComponent = new ComponentsContainer<QuestPiecePrefabComponent>();

        public ComponentsContainer<CharacterComponent> m_CharacterComponents = new ComponentsContainer<CharacterComponent>();
        public ComponentsContainer<DialogueCharacterComponent> m_CharacterDialogueComponents = new ComponentsContainer<DialogueCharacterComponent>();

        public ComponentsContainer<LocationComponent> m_LocationsComponents = new ComponentsContainer<LocationComponent>();


        // Singleton Components
        public Singleton_GameStateComponent m_GameState;
        public Singleton_PopupReferencesComponent m_Popups = new Singleton_PopupReferencesComponent();
        public Singleton_CameraDataComponent m_Cameras = new Singleton_CameraDataComponent();
        public Singleton_NewspaperDataComponent m_Newspaper = new Singleton_NewspaperDataComponent();
        public Singleton_NewspaperReferencesComponent m_NewspaperRefs;
        public Singleton_UIReferencesComponent m_UIRefs;
        public Singleton_TransitionsComponent m_TransitionsComponent;
        public Singleton_NpcReferencesComponent m_NpcReferencesComponent = new Singleton_NpcReferencesComponent();
        public Singleton_DayComponent m_DayComponent = new Singleton_DayComponent();
        public Singleton_CalendarComponent m_CalendarComponent = new Singleton_CalendarComponent();
        public Singleton_DialogueReferencesComponent m_DialogueUIData = new Singleton_DialogueReferencesComponent();
        public Singleton_TownComponent m_TownComponent = new Singleton_TownComponent();

        public Singleton_InputComponent m_InputComponent = new Singleton_InputComponent();
        public Singleton_InventoryComponent m_InventoryComponent;

        public Singleton_LocalizationComponent m_LocalizationComponent = new Singleton_LocalizationComponent();

        /*
         * DATA VIEWS
         */
        public ComponentsContainer<QuestDataComponent> m_CompletedQuestData = new ComponentsContainer<QuestDataComponent>();

        // IDs of the stories in the order in which they will be started
        public List<ID> m_StoriesToStart = new List<ID>();
        public List<ID> m_OngoingStories = new List<ID>();
        // Stories which were completed with a quest but the player hasnt seen the result yet
        // At the start of the day the system that handles the spawning of the NPCs must assign them 
        public List<ID> m_CompletedStories = new List<ID>();
        // Stories that have been completely finished
        public List<ID> m_FinalizedStories = new List<ID>();


        private Dictionary<Type, object> m_ComponentContainers = new Dictionary<Type, object>();
        private Dictionary<Type, object> m_SingletonComponents = new Dictionary<Type, object>();


        public void Initialize()
        {
            m_ComponentContainers.Add(typeof(StoryInfoComponent), m_StoriesInfo);
            m_ComponentContainers.Add(typeof(StoryUIDataComponent), m_StoriesUI);
            m_ComponentContainers.Add(typeof(StoryRepercusionComponent), m_Repercusions);

            m_ComponentContainers.Add(typeof(RecipeDataComponent), m_RecipeData);
            m_ComponentContainers.Add(typeof(CookieDataComponent), m_CookieData);

            m_ComponentContainers.Add(typeof(QuestPieceFunctionalComponent), m_QuestPieceFunctionalComponents);
            m_ComponentContainers.Add(typeof(UIQuestPieceComponent), m_QuestPieceUIComponent);
            m_ComponentContainers.Add(typeof(QuestPiecePrefabComponent), m_QuestPiecePrefabComponent);

            m_ComponentContainers.Add(typeof(CharacterComponent), m_CharacterComponents);
            m_ComponentContainers.Add(typeof(DialogueCharacterComponent), m_CharacterDialogueComponents);

            m_ComponentContainers.Add(typeof(LocationComponent), m_LocationsComponents);
        }

        public ComponentsContainer<T> GetComponentContainer<T>()
        {
            return m_ComponentContainers[typeof(T)] as ComponentsContainer<T>;
        }

        // TODO:
        //public T GetSingletonComponent<T>() where T : class
        //{
        //    return m_SingletonComponents[typeof(T)] as T;
        //}
    }
}