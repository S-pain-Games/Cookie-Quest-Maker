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

            CreateIngredient("masa_de_galletas_encantada", "Masa de galletas encantada", 0, 0, Karma.GoodKarma);
            CreateIngredient("compota_de_mora_infernal", "Compota de mora infernal", 0, 25, Karma.EvilKarma);
            CreateIngredient("nucleo_de_cereza_animico", "Núcleo de cereza anímico", 0, 80, Karma.GoodKarma);
            CreateIngredient("crema_pastelera_arcana", "Crema pastelera arcana", 10, 10, Karma.GoodKarma);
            CreateIngredient("vainilla_de_la_iluminacion", "Vainilla de la iluminación", 30, 0, Karma.EvilKarma);
            CreateIngredient("caramelo_fundido_candiscente", "Caramelo fundido candiscente", 25, 25, Karma.EvilKarma);
            CreateIngredient("esencia_de_limon_purificadora", "Esencia de limón purificadora", 80, 0, Karma.GoodKarma);
            CreateIngredient("levadura_ancestral_de_la_pereza", "Levadura Ancestral de la pereza", 40, 40, Karma.EvilKarma);
            CreateIngredient("chocolate_negro_sempiterno", "Chocolate negro Sempiterno", 0, 20, Karma.EvilKarma);
            CreateIngredient("harina_de_fuerza_titanica", "Harina de fuerza titánica", 15, 15, Karma.GoodKarma);

            CreateIngredient("polvo_impetuoso", "Polvo para hornear impetuoso", 0, 10, Karma.GoodKarma);
            CreateIngredient("polvo_persuasivo", "Polvo para hornear persuasivo", 5, 5, Karma.GoodKarma);
            CreateIngredient("polvo_auxilio", "Polvo para hornear del auxilio", 10, 0, Karma.GoodKarma);
        }

        public override void BuildData(ComponentsDatabase c)
        {
            for (int i = 0; i < m_IngredientsList.Count; i++)
            {
                var ingr = m_IngredientsList[i];
                c.GetComponentContainer<IngredientComponent>().Add(ingr.m_ID, ingr);
            }
        }

        private void CreateIngredient(string idName, string name, int good_price, int evil_price, Karma repType)
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