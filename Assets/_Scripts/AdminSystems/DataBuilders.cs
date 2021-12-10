using CQM.AssetReferences;
using CQM.Components;
using CQM.Databases;
using UnityEngine;

namespace CQM.DataBuilders
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
            m_CharactersBuilder.BuildData(c);
            m_PiecesBuilder.BuildData(c);
            m_IngredientsBuilder.BuildData(c);
            m_StoryBuilder.BuildData(c);
            m_LocalizationBuilder.BuildData(c);
            m_TownBuilder.BuildData(c);

            BuildStoriesStartOrder(c);
            BuildSecondaryStoriesAvailable(c);

            UnlockStartingRecipes(c);
            SetupStartingPlayerInventory(c);
        }

        private void SetupStartingPlayerInventory(ComponentsDatabase c)
        {
            AddItemToInventory("masa_de_galletas_encantada", 10);
            AddItemToInventory("compota_de_mora_infernal", 2);
            AddItemToInventory("chocolate_negro_sempiterno", 1);

            // Set Starting Karma
            c.m_InventoryComponent.m_GoodKarma += 100;
            c.m_InventoryComponent.m_EvilKarma += 100;

            void AddItemToInventory(string itemIDName, int amount)
            {
                var i = c.m_InventoryComponent.m_Ingredients;
                i.Add(new InventoryItem(new ID(itemIDName), amount));
            }
        }

        private void UnlockStartingRecipes(ComponentsDatabase c)
        {
            UnlockRecipe("malvavisco_fantasma_tostado");
            UnlockRecipe("attack");
            UnlockRecipe("dialogate");
            UnlockRecipe("assist");
            UnlockRecipe("look");
            UnlockRecipe("stare");
            UnlockRecipe("steal");

            UnlockRecipe("baseball_bat");
            UnlockRecipe("scissors");
            UnlockRecipe("flip_flops");
            UnlockRecipe("cake");

            UnlockRecipe("violently");
            UnlockRecipe("brutally");
            UnlockRecipe("kindly");
            UnlockRecipe("convincingly");

            void UnlockRecipe(string recipeIDName)
            {
                c.m_InventoryComponent.m_UnlockedRecipes.Add(new ID(recipeIDName));
            }
        }

        private void BuildSecondaryStoriesAvailable(ComponentsDatabase c)
        {
            AddSecondaryStory("floral_collect");
            AddSecondaryStory("without_bread");
            AddSecondaryStory("cupcakes_everywhere");
            AddSecondaryStory("forgotten_bell");
            AddSecondaryStory("the_beast");
            AddSecondaryStory("saving_the_wheat");

            void AddSecondaryStory(string storyIDName)
            {
                Singleton_GameStoriesStateComponent s = c.m_GameStoriesStateComponent;
                s.m_AvailableSecondaryStoriesToStart.Add(new ID(storyIDName));
                s.m_AllSecondaryStories.Add(new ID(storyIDName));
            }
        }

        private void BuildStoriesStartOrder(ComponentsDatabase c)
        {
            var l = c.m_GameStoriesStateComponent.m_MainStoriesToStartOrder;

            l.Add(new ID("mayor_problem"));
            l.Add(new ID("out_of_lactose"));
            l.Add(new ID("sacred_egg"));
            l.Add(new ID("explosive_chocolate"));
            l.Add(new ID("not_so_dirty_rats"));
            l.Add(new ID("stingy_taxes"));
            l.Add(new ID("crazy_cows"));
            l.Add(new ID("the_cake_was_not_a_lie"));
            l.Add(new ID("the_lord_of_the_ducks"));
            l.Add(new ID("natural_chocolate_milkshake"));
            l.Add(new ID("the_stolen_pendant"));
            l.Add(new ID("a_regular_day"));
            l.Add(new ID("mushroom_profecy"));
            l.Add(new ID("mayor_worries"));
            l.Add(new ID("old_friends"));
            l.Add(new ID("high_voltage_treatment"));
            l.Add(new ID("full_artifact_panic"));
            l.Add(new ID("fungal_metamorphosis"));
            l.Add(new ID(""));
        }
    }
}