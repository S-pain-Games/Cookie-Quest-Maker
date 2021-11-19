using CQM.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.AssetReferences;
using CQM.Components;

public class CharactersBuilder : BaseDataBuilder
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

    public override void LoadDataFromCode()
    {
        m_CharactersList.Clear();
        m_CharacterDialogueList.Clear();
        m_CharacterWorldPrefabComponent.Clear();

        CreateCharacter("hio", "Hio", "Hio");
        SetDescription("Dueño de una pastelería y protagonista en esta historia.");
        AddRandomDialogue(new List<string>() { "Hio frase", "frase1Continuacion" });
        FinishCharacter();

        CreateCharacter("nu", "Nu", "Nu Nu");
        SetDescription("Ángel de las Galletas.");
        AddRandomDialogue(new List<string>() { "Nu frase", "frase1Continuacion" });
        FinishCharacter();

        CreateCharacter("evith", "Evith", "Evith");
        SetDescription("Demonio de las Galletas.");
        AddRandomDialogue(new List<string>() { "Evith frase", "frase1Continuacion" });
        FinishCharacter();

        CreateCharacter("meri", "Meri la Leches", "Meri");
        SetDescription("Ganadera entusiasta de las vacas.");
        AddRandomDialogue(new List<string>() { "Leches frase", "frase1Continuacion" });
        FinishCharacter();

        CreateCharacter("mayor", "Alcalde, Antonio", "Alcalde");
        SetDescription("El muy tacaño alcalde del pueblo.");
        AddRandomDialogue(new List<string>() { "Alcalde frase", "frase1Continuacion" });
        FinishCharacter();

        CreateCharacter("miss_chocolate", "Miss Chocolate", "Ms.Chocolate");
        SetDescription("Excéntrica chocolatera.");
        AddRandomDialogue(new List<string>() { "Miss frase", "frase1Continuacion" });
        FinishCharacter();

        CreateCharacter("canela", "Canela N Rama", "Cane");
        SetDescription("Coleccionista de artefactos antiguos.");
        AddRandomDialogue(new List<string>() { "Rama frase", "frase1Continuacion" });
        FinishCharacter();

        CreateCharacter("johny_setas", "Johny, el setas", "Johny");
        SetDescription("Un alquimista muy peculiar.");
        AddRandomDialogue(new List<string>() { "setas fr", "frase1Continuacion" });
        FinishCharacter();

        CreateCharacter("mantecas", "Juanjo, el mantecas", "Juanjo");
        SetDescription("El agricultor malhumorado");
        AddRandomDialogue(new List<string>() { "Juanjo frase", "frase1Continuacion" });
        FinishCharacter();
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
        c.m_CharacterWorldPrefab = _characterReferences.GetWorldPrefab(new ID(idName));
        d.m_CharacterImg = _characterReferences.GetDialogueSprite(new ID(idName));

        d.m_ID = new ID(idName);
        p.m_ID = new ID(idName);
    }

    public void SetDescription(string description)
    {
        c.m_Description = description;
    }

    public void AddRandomDialogue(List<string> dialogue)
    {
        SerializableList<string> serList = new SerializableList<string>();
        for (int i = 0; i < dialogue.Count; i++)
        {
            serList.Add(dialogue[i]);
        }
        d.m_IdleRandomDialogue.Add(serList);
    }

    public void FinishCharacter()
    {
        m_CharactersList.Add(c);
        m_CharacterDialogueList.Add(d);
        m_CharacterWorldPrefabComponent.Add(p);
    }
}
