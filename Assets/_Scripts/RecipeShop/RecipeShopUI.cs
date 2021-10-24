using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeShopUI : MonoBehaviour
{
    public RecipeData myRecipe;

    public event Action<int> OnSelectRecipe;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SelectMyRecipe);
    }

    private void SelectMyRecipe()
    {
        OnSelectRecipe?.Invoke(myRecipe.m_CookieID);
    }
}
