using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeShopUI : MonoBehaviour
{
    public RecipeDataComponent myRecipe;

    public event Action<ID> OnSelectRecipe;

    private Button button;
    private TextMeshProUGUI myText;

    private void Awake()
    {
        button = GetComponent<Button>();
        myText = GetComponentInChildren<TextMeshProUGUI>();
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
        OnSelectRecipe?.Invoke(myRecipe.m_PieceID);
    }

    public void SetRecipe(RecipeDataComponent recipe)
    {
        myRecipe = recipe;
        if(myText == null)
            myText = GetComponentInChildren<TextMeshProUGUI>();
        myText.text = recipe.m_RecipeName;
    }
}
