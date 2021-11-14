using CQM.Databases;
using System;
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

        CreateIngredient("masa_de_galletas_encantada", "Masa de galletas encantada");
        CreateIngredient("compota_de_mora_infernal", "Compota de mora infernal");
        CreateIngredient("nucleo_de_cereza_animico", "Núcleo de cereza anímico");
        CreateIngredient("crema_pastelera_arcana", "Crema pastelera arcana");
        CreateIngredient("vainilla_de_la_iluminacion", "Vainilla de la iluminación");
        CreateIngredient("caramelo_fundido_candiscente", "Caramelo fundido candiscente");
        CreateIngredient("esencia_de_limon_purificadora", "Esencia de limón purificadora");
        CreateIngredient("levadura_ancestral_de_la_pereza", "Levadura Ancestral de la pereza");
        CreateIngredient("chocolate_negro_sempiterno", "Chocolate negro Sempiterno");
        CreateIngredient("harina_de_fuerza_titanica", "Harina de fuerza titánica");
    }

    public void BuildPieces(ComponentsDatabase c)
    {
        for (int i = 0; i < m_IngredientsList.Count; i++)
        {
            var ingr = m_IngredientsList[i];
            c.GetComponentContainer<IngredientComponent>().Add(ingr.m_ID, ingr);
        }
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
        i.m_ID = new ID(idName);
        i.m_Name = name;
        m_IngredientsList.Add(i);

        var r = new IngredientReferences();
        r.m_ID = i.m_ID;
        r.m_Name = i.m_Name;
        m_References.Add(r);
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
