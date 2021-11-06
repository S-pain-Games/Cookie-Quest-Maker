using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsBuilder : MonoBehaviour
{
    [SerializeField] private List<IngredientComponent> m_IngredientsList = new List<IngredientComponent>();

    [SerializeField] private List<IngredientReferences> m_References = new List<IngredientReferences>();

    private IngredientComponent i;

    public void LoadDataFromCode()
    {
        m_IngredientsList.Clear();
        m_References.Clear();

        CreateIngredient("chocolate", "Chocolate");
        CreateIngredient("vanilla", "Vanilla");
        CreateIngredient("cream", "Cream");
    }

    public void ApplyReferences()
    {
        for (int i = 0; i < m_References.Count; i++)
        {
            var reference = m_References[i];
            for (int j = 0; j < m_IngredientsList.Count; j++)
            {
                var ingredient = m_IngredientsList[j];
                if (ingredient.m_ID == reference.m_ID)
                    ingredient.m_Sprite = reference.m_Sprite;
            }
        }
    }

    private void CreateIngredient(string idName, string name)
    {
        i = new IngredientComponent();
        i.m_ID = ID(idName);
        i.m_Name = name;
        m_IngredientsList.Add(i);

        var r = new IngredientReferences();
        r.m_ID = i.m_ID;
        r.m_Name = i.m_Name;
        m_References.Add(r);
    }

    private ID ID(string name)
    {
        return new ID(name);
    }

    [System.Serializable]
    private class IngredientReferences
    {
        [HideInInspector]
        public ID m_ID;
        public string m_Name;
        public Sprite m_Sprite;
    }
}
