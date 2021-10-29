using System.Collections.Generic;
using UnityEngine;

public class PopupSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PopupParent;
    [SerializeField]
    private GameObject m_PopupPrefab;

    public void ShowPopup(PopupData popData)
    {
        PopupBehaviour popUp = Object.Instantiate(m_PopupPrefab, m_PopupParent.transform).GetComponent<PopupBehaviour>();
        popUp.Initialize(popData);
    }
}

public struct PopupData
{
    public string m_Text;
    public float m_TimeAlive;
}