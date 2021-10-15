using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySystemBehaviour : MonoBehaviour
{
    public StorySystem System => m_StoryStateSystem;

    [SerializeField]
    private StorySystem m_StoryStateSystem = new StorySystem();



    #region UNITY_EDITOR
#if UNITY_EDITOR
    [SerializeField]
    private StoryData t_StoryData;

    [MethodButton]
    private void StartStory()
    {
        m_StoryStateSystem.StartStory(t_StoryData);
    }
#endif
    #endregion
}
