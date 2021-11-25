using CQM.Databases;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.AssetReferences;
using CQM.Components;

namespace CQM.DataBuilders
{
    public class IngredientsBuilder : BaseDataBuilder
    {
        [SerializeField] private IngredientsReferencesDatabase _ingredientsReferences;

        [SerializeField] private List<IngredientComponent> m_IngredientsList = new List<IngredientComponent>();

        // Current Components Being Built
        private IngredientComponent i;

        public override void LoadDataFromCode()
        {
            m_IngredientsList.Clear();

            CreateIngredient("masa_de_galletas_encantada", "Masa de galletas encantada", 0, 0, Reputation.GoodCookieReputation);
            CreateIngredient("compota_de_mora_infernal", "Compota de mora infernal", 0, 25, Reputation.EvilCookieReputation);
            CreateIngredient("nucleo_de_cereza_animico", "N�cleo de cereza an�mico", 0, 80, Reputation.GoodCookieReputation);
            CreateIngredient("crema_pastelera_arcana", "Crema pastelera arcana", 10, 10, Reputation.GoodCookieReputation);
            CreateIngredient("vainilla_de_la_iluminacion", "Vainilla de la iluminaci�n", 30, 0, Reputation.EvilCookieReputation);
            CreateIngredient("caramelo_fundido_candiscente", "Caramelo fundido candiscente", 25, 25, Reputation.EvilCookieReputation);
            CreateIngredient("esencia_de_limon_purificadora", "Esencia de lim�n purificadora", 80, 0, Reputation.GoodCookieReputation);
            CreateIngredient("levadura_ancestral_de_la_pereza", "Levadura Ancestral de la pereza", 40, 40, Reputation.EvilCookieReputation);
            CreateIngredient("chocolate_negro_sempiterno", "Chocolate negro Sempiterno", 0, 20, Reputation.EvilCookieReputation);
            CreateIngredient("harina_de_fuerza_titanica", "Harina de fuerza tit�nica", 15, 15, Reputation.GoodCookieReputation);

            CreateIngredient("polvo_impetuoso", "Polvo para hornear impetuoso", 0, 10, Reputation.GoodCookieReputation);
            CreateIngredient("polvo_persuasivo", "Polvo para hornear persuasivo", 5, 5, Reputation.GoodCookieReputation);
            CreateIngredient("polvo_auxilio", "Polvo para hornear del auxilio", 10, 0, Reputation.GoodCookieReputation);
        }

        public override void BuildData(ComponentsDatabase c)
        {
            for (int i = 0; i < m_IngredientsList.Count; i++)
            {
                var ingr = m_IngredientsList[i];
                c.GetComponentContainer<IngredientComponent>().Add(ingr.m_ID, ingr);
            }
        }

        private void CreateIngredient(string idName, string name, int good_price, int evil_price, Reputation repType)
        {
            i = new IngredientComponent();
            i.m_ID = new ID(idName);
            i.m_Name = name;
            i.m_Price_Good = good_price;
            i.m_Price_Evil = evil_price;
            i.m_ReputationTypePrice = repType;
            i.m_Sprite = _ingredientsReferences.GetSprite(i.m_ID);
            m_IngredientsList.Add(i);
        }
    }
}