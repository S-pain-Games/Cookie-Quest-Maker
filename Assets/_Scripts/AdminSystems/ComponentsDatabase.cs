using CQM.Components;
using System.Collections.Generic;
using UnityEngine;
using static GameStateSystem;

namespace CQM.Databases
{
    [System.Serializable]
    public class ComponentsDatabase : MonoBehaviour
    {
        [SerializeField]
        private DataBuilders _dataBuilders;

        private List<object> m_Components = new List<object>();

        // All the Stories Data in the game (Persistent & Runtime)
        public ComponentsContainer<StoryInfoComponent> m_StoriesInfo = new ComponentsContainer<StoryInfoComponent>();
        // Story UI Data used in the story selection UI (Persistent)
        public ComponentsContainer<StoryUIDataComponent> m_StoriesUI = new ComponentsContainer<StoryUIDataComponent>();
        // All the repercusions in the game (Persistent)
        public ComponentsContainer<StoryRepercusionComponent> m_Repercusions = new ComponentsContainer<StoryRepercusionComponent>();


        // Contains all the cookie recipes
        public ComponentsContainer<RecipeDataComponent> m_RecipeData = new ComponentsContainer<RecipeDataComponent>();
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
        public Singleton_PopupComponent m_Popups = new Singleton_PopupComponent();
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
        public Singleton_InventoryComponent m_InventoryComponent; // INIT?



        public ComponentsContainer<T> GetComponentContainer<T>() where T : class
        {
            for (int i = 0; i < m_Components.Count; i++)
            {
                if (m_Components[i] is ComponentsContainer<T> c)
                    return c;
            }
            return null;
        }


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



        public void LoadData()
        {
            m_Components.Add(m_StoriesInfo);
            m_Components.Add(m_StoriesUI);
            m_Components.Add(m_Repercusions);
            m_Components.Add(m_RecipeData);
            m_Components.Add(m_CookieData);
            m_Components.Add(m_QuestPieceFunctionalComponents);
            m_Components.Add(m_QuestPieceUIComponent);
            m_Components.Add(m_QuestPiecePrefabComponent);
            m_Components.Add(m_CharacterComponents);
            m_Components.Add(m_CharacterDialogueComponents);
            m_Components.Add(m_LocationsComponents);


            var storyBuilder = _dataBuilders.m_StoryBuilder;
            var characterBuilder = _dataBuilders.m_CharactersBuilder;
            var piecesBuilder = _dataBuilders.m_PiecesBuilder;


            var cList = characterBuilder.m_CharactersList;
            for (int i = 0; i < cList.Count; i++)
            {
                m_CharacterComponents.Add(cList[i].m_ID, cList[i]);
            }
            var dList = characterBuilder.m_CharacterDialogueList;
            for (int i = 0; i < cList.Count; i++)
            {
                m_CharacterDialogueComponents.Add(dList[i].m_ID, dList[i]);
            }


            var piecesList = piecesBuilder.m_QuestPieceUIComponent;
            for (int i = 0; i < piecesList.Count; i++)
            {
                var data = piecesList[i];
                m_QuestPieceUIComponent.Add(data.m_ID, data);
            }
            var functionalQuestPieces = piecesBuilder.m_QuestPieceFunctionalComponents;
            for (int i = 0; i < functionalQuestPieces.Count; i++)
            {
                var data = functionalQuestPieces[i];
                m_QuestPieceFunctionalComponents.Add(data.m_ID, data);
            }
            var prefabQuestPieces = piecesBuilder.m_QuestPiecePrefabComponent;
            for (int i = 0; i < prefabQuestPieces.Count; i++)
            {
                var data = prefabQuestPieces[i];
                m_QuestPiecePrefabComponent.Add(data.m_ID, data);
            }
            var recipeData = piecesBuilder.m_RecipeData;
            for (int i = 0; i < recipeData.Count; i++)
            {
                m_RecipeData.Add(recipeData[i].m_ID, recipeData[i]);
            }

            var cookieData = piecesBuilder.m_CookieData;
            for (int i = 0; i < recipeData.Count; i++)
            {
                m_CookieData.Add(cookieData[i].m_ParentID, cookieData[i]);
            }


            for (int i = 0; i < storyBuilder.Stories.Count; i++)
            {
                var story = storyBuilder.Stories[i];
                m_StoriesInfo.Add(story.m_StoryData.m_ID, story);
            }
            for (int i = 0; i < storyBuilder.Repercusions.Count; i++)
            {
                var rep = storyBuilder.Repercusions[i];
                m_Repercusions.Add(rep.m_ID, rep);
            }
            for (int i = 0; i < storyBuilder.StoryUI.Count; i++)
            {
                var ui = storyBuilder.StoryUI[i];
                m_StoriesUI.Add(ui.m_ParentStoryID, ui);
            }
            for (int i = 0; i < storyBuilder.RepercusionNewspaperArticles.Count; i++)
            {
                var news = storyBuilder.RepercusionNewspaperArticles[i];
                m_Newspaper.m_NewspaperStories.Add(news.m_RepID, news);
            }

            m_StoriesToStart.Add(new ID("mayors_wolves"));


            m_InventoryComponent.m_UnlockedRecipes.Add(new ID("plain_cookie"));
            m_InventoryComponent.m_UnlockedRecipes.Add(new ID("attack"));

            m_InventoryComponent.m_Ingredients.Add(new InventoryItem(new ID("vanilla"), 1));
            m_InventoryComponent.m_Ingredients.Add(new InventoryItem(new ID("chocolate"), 2));
            m_InventoryComponent.m_Ingredients.Add(new InventoryItem(new ID("cream"), 1));

            m_InventoryComponent.m_GoodCookieReputation += 100;
            m_InventoryComponent.m_EvilCookieReputation += 100;
        }
    }


    [System.Serializable]
    public class DataBuilders
    {
        public PieceBuilder m_PiecesBuilder;
        public StoryBuilder m_StoryBuilder;
        public CharactersBuilder m_CharactersBuilder;
    }
}