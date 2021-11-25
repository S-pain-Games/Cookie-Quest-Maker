using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class KarmaCounter : MonoBehaviour
{
    [SerializeField] private Reputation m_KarmaType;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        Admin.Global.EventSystem.GetCallbackByName<EventVoid>("inventory_sys", "reputation_changed").OnInvoked += UpdateUI; ;
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (m_KarmaType == Reputation.GoodCookieReputation)
        {
            _text.text = Admin.Global.Components.m_InventoryComponent.m_GoodKarma.ToString();
            PopAnimation();
        }
        else
        {
            _text.text = Admin.Global.Components.m_InventoryComponent.m_EvilKarma.ToString();
            PopAnimation();
        }


        void PopAnimation()
        {
            transform.DOKill();
            transform.DOScale(1.2f, 0.3f).OnComplete(() => transform.DOScale(1.0f, 0.3f));
        }
    }
}