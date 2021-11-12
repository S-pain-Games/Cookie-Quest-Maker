using CQM.Components;
using UnityEngine;

namespace CQM.Databases
{
    [System.Serializable]
    public class DataBuilders : MonoBehaviour
    {
        public PieceBuilder m_PiecesBuilder;
        public StoryBuilder m_StoryBuilder;
        public CharactersBuilder m_CharactersBuilder;
        public LocalizationBuilder m_LocalizationBuilder;

        public void BuildData(ComponentsDatabase c)
        {
            var storyBuilder = m_StoryBuilder;
            var characterBuilder = m_CharactersBuilder;
            var piecesBuilder = m_PiecesBuilder;
            var localizationBuilder = m_LocalizationBuilder;

            BuildCharacters(c, characterBuilder);
            BuildPieces(c, piecesBuilder);
            BuildStories(c, storyBuilder);
            BuildLocalization(c, localizationBuilder);

            c.m_StoriesToStart.Add(new ID("mayor_problem"));
            c.m_StoriesToStart.Add(new ID("out_of_lactose"));
            c.m_StoriesToStart.Add(new ID("sacred_egg"));

            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("plain_cookie"));
            c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID("attack"));

            c.m_InventoryComponent.m_Ingredients.Add(new InventoryItem(new ID("vanilla"), 1));
            c.m_InventoryComponent.m_Ingredients.Add(new InventoryItem(new ID("chocolate"), 2));
            c.m_InventoryComponent.m_Ingredients.Add(new InventoryItem(new ID("cream"), 1));

            c.m_InventoryComponent.m_GoodCookieReputation += 100;
            c.m_InventoryComponent.m_EvilCookieReputation += 100;


            void BuildCharacters(ComponentsDatabase c, CharactersBuilder characterBuilder)
            {
                var cList = characterBuilder.m_CharactersList;
                for (int i = 0; i < cList.Count; i++)
                {
                    c.m_CharacterComponents.Add(cList[i].m_ID, cList[i]);
                }
                var dList = characterBuilder.m_CharacterDialogueList;
                for (int i = 0; i < cList.Count; i++)
                {
                    c.m_CharacterDialogueComponents.Add(dList[i].m_ID, dList[i]);
                }
            }
            void BuildPieces(ComponentsDatabase c, PieceBuilder piecesBuilder)
            {
                var piecesList = piecesBuilder.m_QuestPieceUIComponent;
                for (int i = 0; i < piecesList.Count; i++)
                {
                    var data = piecesList[i];
                    c.m_QuestPieceUIComponent.Add(data.m_ID, data);
                }
                var functionalQuestPieces = piecesBuilder.m_QuestPieceFunctionalComponents;
                for (int i = 0; i < functionalQuestPieces.Count; i++)
                {
                    var data = functionalQuestPieces[i];
                    c.m_QuestPieceFunctionalComponents.Add(data.m_ID, data);
                }
                var prefabQuestPieces = piecesBuilder.m_QuestPiecePrefabComponent;
                for (int i = 0; i < prefabQuestPieces.Count; i++)
                {
                    var data = prefabQuestPieces[i];
                    c.m_QuestPiecePrefabComponent.Add(data.m_ID, data);
                }
                var recipeData = piecesBuilder.m_RecipeData;
                for (int i = 0; i < recipeData.Count; i++)
                {
                    c.GetComponentContainer<RecipeDataComponent>().Add(recipeData[i].m_ID, recipeData[i]);
                }

                var cookieData = piecesBuilder.m_CookieData;
                for (int i = 0; i < recipeData.Count; i++)
                {
                    c.m_CookieData.Add(cookieData[i].m_ParentID, cookieData[i]);
                }
            }
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
            void BuildLocalization(ComponentsDatabase c, LocalizationBuilder localizationBuilder)
            {
                // TODO: LOAD TEXT FROM BUILDER
                var lText = localizationBuilder.m_LocalizedText;
                for (int i = 0; i < lText.Count; i++)
                {
                    for (int j = 0; j < lText[i].m_Lines.Count; j++)
                    {
                        var line = lText[i].m_Lines[j];

                        switch (line.m_Lang)
                        {
                            case Language.Undefined:
                                Debug.LogError("Undefined language when loading Loc data");
                                break;
                            case Language.Spanish:
                                c.m_LocalizationComponent.m_Spanish.Add(lText[i].m_ID, line.m_Line);
                                break;
                            case Language.English:
                                c.m_LocalizationComponent.m_English.Add(lText[i].m_ID, line.m_Line);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}