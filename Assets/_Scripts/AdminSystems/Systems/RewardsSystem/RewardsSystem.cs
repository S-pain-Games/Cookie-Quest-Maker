using CQM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CQM.Systems
{
    public class RewardsSystem : ISystemEvents
    {
        private ComponentsContainer<LocationComponent> _locationComponents;

        public void Initialize(ComponentsContainer<LocationComponent> locations)
        {
            _locationComponents = locations;

            Admin.Global.EventSystem.GetCallbackByName<Event<ID>>("story_sys", "story_finalized").OnInvoked += CalculateAndAddStoryReward;
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            sysID = new ID("rewards_sys");
            commands = new EventSys();
            callbacks = new EventSys();

            commands.AddEvent<ID>(new ID("debug_calculate_reward_for_story")).OnInvoked += CalculateAndAddStoryReward;
        }

        private void CalculateAndAddStoryReward(ID storyID)
        {
            StoryInfoComponent s = Admin.Global.Components.GetComponentContainer<StoryInfoComponent>().GetComponentByID(storyID);
            var invCommands = Admin.Global.EventSystem.GetCommandByName<Event<InventorySys_ChangeReputationEvtArgs>>("inventory_sys", "change_reputation");

            int rewardAmount = 0;
            Karma karmaType;
            int multiplicativeRewardPercentage = 0;
            var storyAssociatedLocation = _locationComponents.GetList().Find(l => l.m_CharacterOwnerID == new ID(s.m_StoryData.m_QuestGiver));
            int globalHappiness = Admin.Global.Components.m_TownComponent.m_GlobalHappiness;

            // Set Base reward amount
            if (!Admin.Global.Components.m_GameStoriesStateComponent.m_AllSecondaryStories.Contains(s.m_StoryData.m_ID))
                rewardAmount = 300;
            else
                rewardAmount = 150;


            if (s.m_QuestRepercusion.m_Value > 0)
            {
                karmaType = Karma.GoodKarma;
                // The story is secondary and doesn't have a localization
                if (storyAssociatedLocation != null)
                    multiplicativeRewardPercentage = storyAssociatedLocation.m_Happiness + 100;
                else
                    multiplicativeRewardPercentage = 100;
                rewardAmount *= multiplicativeRewardPercentage;
                rewardAmount /= 100;
            }
            else
            {
                karmaType = Karma.EvilKarma;
                if (storyAssociatedLocation != null)
                    multiplicativeRewardPercentage = -storyAssociatedLocation.m_Happiness + 100;
                else
                    multiplicativeRewardPercentage = 100;
                rewardAmount *= multiplicativeRewardPercentage;
                rewardAmount /= 100;
            }

            invCommands.Invoke(new InventorySys_ChangeReputationEvtArgs(karmaType, rewardAmount));
        }
    }
}