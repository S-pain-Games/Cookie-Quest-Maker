using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersBuilder : MonoBehaviour
{
    public List<CharacterComponent> m_CharactersList = new List<CharacterComponent>();
    public List<DialogueCharacterComponent> m_CharacterDialogueList = new List<DialogueCharacterComponent>();
    public List<CharacterWorldPrefabComponent> m_CharacterWorldPrefabComponent = new List<CharacterWorldPrefabComponent>();

    [SerializeField] private List<CharacterReferences> m_References = new List<CharacterReferences>();

    private CharacterComponent c;
    private DialogueCharacterComponent d;
    private CharacterReferences r;
    private CharacterWorldPrefabComponent p;

    public void BuildCharacters(ComponentsDatabase c)
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

    public void LoadDataFromCode()
    {
        m_CharactersList.Clear();
        m_CharacterDialogueList.Clear();
        m_References.Clear();

        CreateCharacter("hio", "Hio", "Hio");
        SetDescription("She is the owner of the town's farm");

        CreateCharacter("nu", "Nu", "Nu Nu");
        SetDescription("She is the owner of the town's farm");

        CreateCharacter("evith", "Evith", "Evith");
        SetDescription("She is the owner of the town's farm");

        CreateCharacter("meri", "Meri la Leches", "Meri");
        SetDescription("She is the owner of the town's farm");

        CreateCharacter("mayor", "Alcalde, Antonio", "Alcalde");
        SetDescription("She is the owner of the town's farm");

        CreateCharacter("miss_chocolate", "Miss Chocolate", "Ms.Chocolate");
        SetDescription("She is the owner of the town's farm");

        CreateCharacter("canela", "Canela N Rama", "Cane");
        SetDescription("She is the owner of the town's farm");

        CreateCharacter("johny_setas", "Johny, el setas", "Johny");
        SetDescription("She is the owner of the town's farm");

        CreateCharacter("mantecas", "Juanjo, el mantecas", "Juanjo");
        SetDescription("She is the owner of the town's farm");

        CreateCharacter("mamarrachus", "Mamarrachus Generikus", "Mamarrachus");
        SetDescription("She is the owner of the town's farm");
    }

    public void ApplyReferences()
    {
        // This is absurdly inefficient but its an editor tool so its okey
        for (int i = 0; i < m_References.Count; i++)
        {
            var r = m_References[i];

            // Apply Dialogue Character Image References
            for (int j = 0; j < m_CharacterDialogueList.Count; j++)
            {
                var dialogueRef = m_CharacterDialogueList[j];
                if (dialogueRef.m_ID == r.m_ID)
                {
                    dialogueRef.m_CharacterImg = r.m_DialogueSprite;
                }
            }

            for (int j = 0; j < m_CharacterWorldPrefabComponent.Count; j++)
            {
                var prefabRef = m_CharacterWorldPrefabComponent[j];
                if (prefabRef.m_ID == r.m_ID)
                {
                    prefabRef.m_Prefab = r.m_WorldCharacterPrefab;
                }
            }
        }
    }

    public void CreateCharacter(string idName, string longName, string shortName)
    {
        c = new CharacterComponent();
        d = new DialogueCharacterComponent();
        r = new CharacterReferences();
        p = new CharacterWorldPrefabComponent();

        c.m_ID = new ID(idName);
        c.m_ShortName = shortName;
        c.m_FullName = longName;

        d.m_ID = new ID(idName);

        r.m_ID = new ID(idName);
        r.m_CharacterName = longName;

        p.m_ID = new ID(idName);
    }

    public void SetDescription(string description)
    {
        c.m_Description = description;

        // Finish
        m_CharactersList.Add(c);
        m_CharacterDialogueList.Add(d);
        m_References.Add(r);
        m_CharacterWorldPrefabComponent.Add(p);
    }

    [System.Serializable]
    public class CharacterReferences
    {
        [HideInInspector] public ID m_ID;
        public string m_CharacterName;
        public Sprite m_DialogueSprite;
        public GameObject m_WorldCharacterPrefab;
    }
}
