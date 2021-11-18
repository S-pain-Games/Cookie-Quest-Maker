using CQM.AssetReferences;
using CQM.Components;
using UnityEngine;

namespace CQM.Databases
{
    [System.Serializable]
    public class DataBuilders : MonoBehaviour
    {
        [SerializeField] private PieceBuilder m_PiecesBuilder;
        [SerializeField] private StoryBuilder m_StoryBuilder;
        [SerializeField] private CharactersBuilder m_CharactersBuilder;
        [SerializeField] private IngredientsBuilder m_IngredientsBuilder;
        [SerializeField] private TownBuilder m_TownBuilder;
        [SerializeField] private LocalizationBuilder m_LocalizationBuilder;


        public void BuildData(ComponentsDatabase c)
        {
            m_CharactersBuilder.BuildCharacters(c);
            m_PiecesBuilder.BuildPieces(c);
            m_IngredientsBuilder.BuildPieces(c);
            BuildStories(c, m_StoryBuilder);
            m_LocalizationBuilder.BuildLocalization(c);
            m_TownBuilder.BuildTown(c);

            c.m_StoriesStateComponent.m_MainStoriesToStartOrder.Add(new ID("mayor_problem"));
            c.m_StoriesStateComponent.m_MainStoriesToStartOrder.Add(new ID("out_of_lactose"));
            c.m_StoriesStateComponent.m_MainStoriesToStartOrder.Add(new ID("sacred_egg"));

            c.m_StoriesStateComponent.m_AvailableSecondaryStoriesToStart.Add(new ID("explosive_chocolate"));
            c.m_StoriesStateComponent.m_AvailableSecondaryStoriesToStart.Add(new ID("not_so_dirty_rats"));
            c.m_StoriesStateComponent.m_AvailableSecondaryStoriesToStart.Add(new ID("stingy_taxes"));
            c.m_StoriesStateComponent.m_AvailableSecondaryStoriesToStart.Add(new ID("crazy_cows"));
            c.m_StoriesStateComponent.m_AllSecondaryStories.Add(new ID("explosive_chocolate"));
            c.m_StoriesStateComponent.m_AllSecondaryStories.Add(new ID("not_so_dirty_rats"));
            c.m_StoriesStateComponent.m_AllSecondaryStories.Add(new ID("stingy_taxes"));
            c.m_StoriesStateComponent.m_AllSecondaryStories.Add(new ID("crazy_cows"));

            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("malvavisco_fantasma_tostado"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("attack"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("dialogate"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("assist"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("look"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("stare"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("steal"));

            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("baseball_bat"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("scissors"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("flip_flops"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("cake"));

            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("violently"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("brutally"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("kindly"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("convincingly"));

            c.m_InventoryComponent.m_Ingredients.Add(new InventoryItem(new ID("masa_de_galletas_encantada"), 10));
            c.m_InventoryComponent.m_Ingredients.Add(new InventoryItem(new ID("compota_de_mora_infernal"), 2));
            c.m_InventoryComponent.m_Ingredients.Add(new InventoryItem(new ID("chocolate_negro_sempiterno"), 1));

            c.m_InventoryComponent.m_GoodCookieReputation += 100;
            c.m_InventoryComponent.m_EvilCookieReputation += 100;

            void BuildStories(ComponentsDatabase c, StoryBuilder storyBuilder)
            {
                for (int i = 0; i < storyBuilder.Stories.Count; i++)
                {
                    var story = storyBuilder.Stories[i];
                    c.GetComponentContainer<StoryInfoComponent>().Add(story.m_StoryData.m_ID, story);
                }
                for (int i = 0; i < storyBuilder.Repercusions.Count; i++)
                {
                    var rep = storyBuilder.Repercusions[i];
                    c.GetComponentContainer<StoryRepercusionComponent>().Add(rep.m_ID, rep);
                }
                for (int i = 0; i < storyBuilder.StoryUI.Count; i++)
                {
                    var ui = storyBuilder.StoryUI[i];
                    c.GetComponentContainer<StoryUIDataComponent>().Add(ui.m_ParentStoryID, ui);
                }
                for (int i = 0; i < storyBuilder.RepercusionNewspaperArticles.Count; i++)
                {
                    var news = storyBuilder.RepercusionNewspaperArticles[i];
                    c.m_Newspaper.m_NewspaperStories.Add(news.m_RepID, news);
                }
            }
        }
    }
}