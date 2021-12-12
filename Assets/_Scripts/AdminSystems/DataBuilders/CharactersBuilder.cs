using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.AssetReferences;
using CQM.Components;

namespace CQM.DataBuilders
{
    public class CharactersBuilder : BaseDataBuilder
    {
        [SerializeField] private CharacterReferencesDatabase _characterReferences;

        [SerializeField] private List<CharacterComponent> m_CharactersList = new List<CharacterComponent>();
        [SerializeField] private List<CharacterDialogueComponent> m_CharacterDialogueList = new List<CharacterDialogueComponent>();
        [SerializeField] private List<CharacterWorldPrefabComponent> m_CharacterWorldPrefabComponent = new List<CharacterWorldPrefabComponent>();

        // Current Components Being Built
        private CharacterComponent c;
        private CharacterDialogueComponent d;
        private CharacterWorldPrefabComponent p;


        public override void BuildData(ComponentsDatabase c)
        {
            var cList = m_CharactersList;
            for (int i = 0; i < cList.Count; i++)
            {
                c.m_CharacterComponents.Add(cList[i].m_ID, cList[i]);
            }
            var dList = m_CharacterDialogueList;
            for (int i = 0; i < cList.Count; i++)
            {
                c.m_CharacterDialogueComponents.Add(dList[i].m_ID, dList[i]);
            }
        }

        public override void LoadDataFromCode()
        {
            m_CharactersList.Clear();
            m_CharacterDialogueList.Clear();
            m_CharacterWorldPrefabComponent.Clear();

            CreateCharacter("hio", "Hio", "Hio");
            SetDescription("Dueño de una pastelería y protagonista en esta historia.");
            AddRandomDialogue(new List<string>() { "¿No se suponía que yo era mudo?" });
            FinishCharacter();

            CreateCharacter("nu", "Nu", "Nu Nu");
            SetDescription("Ángel de las Galletas.");
            AddRandomDialogue(new List<string>() { "Ocurra lo que ocurra, jamás confíes en Evith." });
            AddRandomDialogue(new List<string>() { "Este pueblo parece un buen sitio para vivir. Si no fuera por la miriada de problemas." });
            SetAudio(new ID("voice_nu"));
            FinishCharacter();

            CreateCharacter("evith", "Evith", "Evith");
            SetDescription("Demonio de las Galletas.");
            AddRandomDialogue(new List<string>() { "Nu intentará lavarte la cabeza con sus sermones baratos." });
            AddRandomDialogue(new List<string>() { "Algún día seré reconocida como la reina de este mundo." });
            SetAudio(new ID("voice_evith"));
            FinishCharacter();

            CreateCharacter("meri", "Meri la Leches", "Meri");
            SetDescription("Ganadera entusiasta de las vacas.");
            AddRandomDialogue(new List<string>() { "Las vacas son la alegría de mi vida. Será mejor que nadie se meta con ellas." });
            AddRandomDialogue(new List<string>() { "¡No estoy obsesionada con las vacas! También me gustan los becerros y los bueyes.", "¡¿Por qué me miras así de raro?! ¿Son distintos, no?" });
            SetAudio(new ID("voice_meri"));
            FinishCharacter();

            CreateCharacter("mayor", "Alcalde, Antonio", "Alcalde");
            SetDescription("El muy tacaño alcalde del pueblo.");
            AddRandomDialogue(new List<string>() { "A este pueblo lo que le falta son más bancos donde sentarse." });
            AddRandomDialogue(new List<string>() { "¿El puerto? ¡Va de maravilla! Quizás el próximo lustro dispondremos de alguna barca." });
            SetAudio(new ID("voice_mayor"));
            FinishCharacter();

            CreateCharacter("miss_chocolate", "Miss Chocolate", "Ms.Chocolate");
            SetDescription("Excéntrica chocolatera.");
            AddRandomDialogue(new List<string>() { "El chocolate es el alimento más intenso que existe." });
            AddRandomDialogue(new List<string>() { "Dedico todo mi ser a buscar nuevas formas de crear chocolate." });
            SetAudio(new ID("voice_miss"));
            FinishCharacter();

            CreateCharacter("canela", "Canela N Rama", "Cane");
            SetDescription("Coleccionista de artefactos antiguos.");
            AddRandomDialogue(new List<string>() { "Mi colección se ha vuelto tan famosa que mucha gente viene de fuera para verla." });
            AddRandomDialogue(new List<string>() { "Este pueblo no tiene nada de nada. Pero al menos se puede vivir en tranquilidad... a veces." });
            SetAudio(new ID("voice_canela"));
            FinishCharacter();

            CreateCharacter("johny_setas", "Johny, el setas", "Johny");
            SetDescription("Un alquimista muy peculiar.");
            AddRandomDialogue(new List<string>() { "¡Esta pastelería que tienes es la leche, colega!" });
            AddRandomDialogue(new List<string>() { "Me gusta todo lo que tenga que ver con los hongos.", "Pero no me importaría dedicarme a la panadería algún día." });
            SetAudio(new ID("voice_johnny"));
            FinishCharacter();

            CreateCharacter("mantecas", "Juanjo, el mantecas", "Juanjo");
            SetDescription("El agricultor malhumorado");
            AddRandomDialogue(new List<string>() { "Mis hortalizas son las mejores del mundo entero.", "¡Será por eso que no paran de molestarme las plagas!" });
            AddRandomDialogue(new List<string>() { "¡No estoy siempre de mal humor! ¡De hecho hoy estoy feliz! ¡¿No se me nota en la cara?!" });
            SetAudio(new ID("voice_mantecas"));
            FinishCharacter();

            CreateCharacter("narrator", "narrator", "narrator");
            SetDescription("narrator");
            FinishCharacter();
        }

        #region Builder Methods

        private void CreateCharacter(string idName, string longName, string shortName)
        {
            c = new CharacterComponent();
            d = new CharacterDialogueComponent();
            p = new CharacterWorldPrefabComponent();

            c.m_ID = new ID(idName);
            c.m_ShortName = shortName;
            c.m_FullName = longName;

            c.m_NewspaperSprite = _characterReferences.GetNewspaperSprite(new ID(idName));
            c.m_CharacterWorldPrefab = _characterReferences.GetWorldPrefab(new ID(idName));
            d.m_CharacterImg = _characterReferences.GetDialogueSprite(new ID(idName));

            d.m_ID = new ID(idName);
            p.m_ID = new ID(idName);
        }

        private void SetDescription(string description)
        {
            c.m_Description = description;
        }

        private void SetAudio(ID audioID)
        {
            c.hasAudio = true;
            c.m_AudioID = audioID;
        }

        private void AddRandomDialogue(List<string> dialogue)
        {
            SerializableList<string> serList = new SerializableList<string>();
            for (int i = 0; i < dialogue.Count; i++)
            {
                serList.Add(dialogue[i]);
            }
            d.m_IdleRandomDialogue.Add(serList);
        }

        private void FinishCharacter()
        {
            m_CharactersList.Add(c);
            m_CharacterDialogueList.Add(d);
            m_CharacterWorldPrefabComponent.Add(p);
        }

        #endregion
    }
}