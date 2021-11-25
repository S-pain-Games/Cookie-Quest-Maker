using CQM.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using static GameStateSystem;

namespace CQM.Databases
{
    [Serializable]
    public class ComponentsDatabase : MonoBehaviour
    {
        [Header("Batch Components")]
        // All the Stories Data in the game (Persistent & Runtime)
        [SerializeField] private ComponentsContainer<StoryInfoComponent> m_StoriesInfo = new ComponentsContainer<StoryInfoComponent>();
        // Story UI Data used in the story selection UI (Persistent)
        [SerializeField] private ComponentsContainer<StoryUIDataComponent> m_StoriesUI = new ComponentsContainer<StoryUIDataComponent>();
        // All the repercusions in the game (Persistent)
        [SerializeField] private ComponentsContainer<StoryRepercusionComponent> m_Repercusions = new ComponentsContainer<StoryRepercusionComponent>();

        // Contains all the cookie recipes
        [SerializeField] private ComponentsContainer<RecipeDataComponent> m_RecipeData = new ComponentsContainer<RecipeDataComponent>();
        // Contains all the data for the cookies
        [SerializeField] public ComponentsContainer<CookieDataComponent> m_CookieData = new ComponentsContainer<CookieDataComponent>();

        // Contains all the quest piece data
        [SerializeField] public ComponentsContainer<QuestPieceFunctionalComponent> m_QuestPieceFunctionalComponents = new ComponentsContainer<QuestPieceFunctionalComponent>();
        [SerializeField] public ComponentsContainer<QuestPieceUIComponent> m_QuestPieceUIComponent = new ComponentsContainer<QuestPieceUIComponent>();
        [SerializeField] public ComponentsContainer<QuestPiecePrefabComponent> m_QuestPiecePrefabComponent = new ComponentsContainer<QuestPiecePrefabComponent>();

        [SerializeField] public ComponentsContainer<CharacterComponent> m_CharacterComponents = new ComponentsContainer<CharacterComponent>();
        [SerializeField] public ComponentsContainer<CharacterDialogueComponent> m_CharacterDialogueComponents = new ComponentsContainer<CharacterDialogueComponent>();

        [SerializeField] public ComponentsContainer<LocationComponent> m_LocationsComponents = new ComponentsContainer<LocationComponent>();

        [SerializeField] private ComponentsContainer<IngredientComponent> m_IngredientsComponents = new ComponentsContainer<IngredientComponent>();


        // Singleton Components
        [Header("Singleton Components")]
        public Singleton_GameStateComponent m_GameState;
        public Singleton_PopupReferencesComponent m_Popups;
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
        public Singleton_AudioDataComponent m_AudioDataComponent;
        public Singleton_AudioClipsDataComponent m_AudioClipsComponent;
        public Singleton_GameStoriesStateComponent m_GameStoriesStateComponent = new Singleton_GameStoriesStateComponent();


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
            m_ComponentContainers.Add(typeof(QuestPieceUIComponent), m_QuestPieceUIComponent);
            m_ComponentContainers.Add(typeof(QuestPiecePrefabComponent), m_QuestPiecePrefabComponent);

            m_ComponentContainers.Add(typeof(CharacterComponent), m_CharacterComponents);
            m_ComponentContainers.Add(typeof(CharacterDialogueComponent), m_CharacterDialogueComponents);

            m_ComponentContainers.Add(typeof(LocationComponent), m_LocationsComponents);
            m_ComponentContainers.Add(typeof(IngredientComponent), m_IngredientsComponents);
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