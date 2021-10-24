using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookieMakingSystem : MonoBehaviour
{
    private CookieDB _cookieDB; 
    public int _selectedRecipe = -1;

    public void Initialize(CookieDB cookieDB)
    {
        _cookieDB = cookieDB;
    }

    public void SelectRecipe(int recipeId)
    {
        _selectedRecipe = recipeId;
    }

    public void CreateCookie()
    {
        if(_selectedRecipe != -1)
        {
            Admin.g_Instance.playerPieceStorage.m_Storage.Add(_selectedRecipe);
        }
    }
}
