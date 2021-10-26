using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeShopUI : MonoBehaviour
{
    public RecipeData myRecipe;

    public event Action<int> OnSelectRecipe;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(SelectMyRecipe);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(SelectMyRecipe);
    }

    private void SelectMyRecipe()
    {
        OnSelectRecipe?.Invoke(myRecipe.m_CookieID);
    }
}
