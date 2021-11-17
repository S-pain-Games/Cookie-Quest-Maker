using CQM.Databases;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.AssetReferences;

public class IngredientsBuilder : MonoBehaviour
{

    [SerializeField] private IngredientsReferencesDatabase _ingredientsReferences;

    [SerializeField] private List<IngredientComponent> m_IngredientsList = new List<IngredientComponent>();

    private IngredientComponent i;

    public void LoadDataFromCode()
    {
        m_IngredientsList.Clear();

        CreateIngredient("masa_de_galletas_encantada", "Masa de galletas encantada", 0, Reputation.GoodCookieReputation);
        CreateIngredient("compota_de_mora_infernal", "Compota de mora infernal", 0, Reputation.EvilCookieReputation);
        CreateIngredient("nucleo_de_cereza_animico", "Núcleo de cereza anímico", 0, Reputation.GoodCookieReputation);
        CreateIngredient("crema_pastelera_arcana", "Crema pastelera arcana", 0, Reputation.GoodCookieReputation);
        CreateIngredient("vainilla_de_la_iluminacion", "Vainilla de la iluminación", 0, Reputation.EvilCookieReputation);
        CreateIngredient("caramelo_fundido_candiscente", "Caramelo fundido candiscente", 0, Reputation.EvilCookieReputation);
        CreateIngredient("esencia_de_limon_purificadora", "Esencia de limón purificadora", 0, Reputation.GoodCookieReputation);
        CreateIngredient("levadura_ancestral_de_la_pereza", "Levadura Ancestral de la pereza", 0, Reputation.EvilCookieReputation);
        CreateIngredient("chocolate_negro_sempiterno", "Chocolate negro Sempiterno", 0, Reputation.EvilCookieReputation);
        CreateIngredient("harina_de_fuerza_titanica", "Harina de fuerza titánica", 0, Reputation.GoodCookieReputation);
    }

    public void BuildPieces(ComponentsDatabase c)
    {
        for (int i = 0; i < m_IngredientsList.Count; i++)
        {
            var ingr = m_IngredientsList[i];
            c.GetComponentContainer<IngredientComponent>().Add(ingr.m_ID, ingr);
        }
    }

    private void CreateIngredient(string idName, string name, int price, Reputation repType)
    {
        i = new IngredientComponent();
        i.m_ID = new ID(idName);
        i.m_Name = name;
        i.m_Price = price;
        i.m_ReputationTypePrice = repType;
        i.m_Sprite = _ingredientsReferences.GetSprite(i.m_ID);
        m_IngredientsList.Add(i);
    }
}
