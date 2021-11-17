using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.AssetReferences;
using CQM.Components;

public class CharactersBuilder : MonoBehaviour
{
    [SerializeField] private CharacterReferencesDatabase _characterReferences;

    public List<CharacterComponent> m_CharactersList = new List<CharacterComponent>();
    public List<DialogueCharacterComponent> m_CharacterDialogueList = new List<DialogueCharacterComponent>();
    public List<CharacterWorldPrefabComponent> m_CharacterWorldPrefabComponent = new List<CharacterWorldPrefabComponent>();


    private CharacterComponent c;
    private DialogueCharacterComponent d;
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
    }

    public void CreateCharacter(string idName, string longName, string shortName)
    {
        c = new CharacterComponent();
        d = new DialogueCharacterComponent();
        p = new CharacterWorldPrefabComponent();

        c.m_ID = new ID(idName);
        c.m_ShortName = shortName;
        c.m_FullName = longName;

        c.m_NewspaperSprite = _characterReferences.GetNewspaperSprite(new ID(idName));
        d.m_CharacterImg = _characterReferences.GetDialogueSprite(new ID(idName));

        d.m_ID = new ID(idName);
        p.m_ID = new ID(idName);
    }

    public void SetDescription(string description)
    {
        c.m_Description = description;

        // Finish
        m_CharactersList.Add(c);
        m_CharacterDialogueList.Add(d);
        m_CharacterWorldPrefabComponent.Add(p);
    }
}
