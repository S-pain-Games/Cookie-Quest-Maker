using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsSystemDebugger : MonoBehaviour
{
    [SerializeField] private string m_StoryIDName;

    [MethodButton]
    public void GetRewardOfStory()
    {
        var evtsys = Admin.Global.EventSystem;
        var cmd = evtsys.GetCommandByName<Event<ID>>("rewards_sys", "debug_calculate_reward_for_story");
        cmd.Invoke(new ID(m_StoryIDName));
    }
}
