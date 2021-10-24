using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookieMakingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_CookieName;
    [SerializeField] private TextMeshProUGUI txt_CookieDescription;
    [SerializeField] private TextMeshProUGUI txt_CookieStats;

    public void SelectRecipe(int id)
    {
        Admin.g_Instance.cookieMakingSystem.SelectRecipe(id);
        UpdateUI();
    }

    public void CreateCookie()
    {
        Admin.g_Instance.cookieMakingSystem.CreateCookie();
    }

    public void UpdateUI()
    {
        Admin admin = Admin.g_Instance;
        CookieData cookieData;
        if(admin.cookieDB.m_CookieDataDB.TryGetValue(admin.cookieMakingSystem._selectedRecipe, out cookieData))
        {
            txt_CookieName.text = cookieData.m_CookieName;
            txt_CookieDescription.text = cookieData.m_CookieDescription;

        }
        
    }
}
