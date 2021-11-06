using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersBuilder : MonoBehaviour
{
    public List<CharacterComponent> m_CharactersList = new List<CharacterComponent>();
    public List<DialogueCharacterComponent> m_CharacterDialogueList = new List<DialogueCharacterComponent>();

    [SerializeField] private List<CharacterReferences> m_References = new List<CharacterReferences>();

    private CharacterComponent c;
    private DialogueCharacterComponent d;
    private CharacterReferences r;

    public void LoadDataFromCode()
    {
        m_CharactersList.Clear();
        m_CharacterDialogueList.Clear();
        m_References.Clear();

        CreateCharacter("mery", "Mery la Leches", "Mery");
        SetDescription("She is the owner of the town's farm");
        SetCharacterDialogueColor(Color.red);

        CreateCharacter("evith", "Evith", "Evith");
        SetDescription("She is the owner of the town's farm");
        SetCharacterDialogueColor(Color.red);

        CreateCharacter("mamarrachus", "Mamarrachus Generikus", "Mamarrachus");
        SetDescription("She is the owner of the town's farm");
        SetCharacterDialogueColor(Color.red);

        CreateCharacter("nu", "Nu", "Nu Nu");
        SetDescription("She is the owner of the town's farm");
        SetCharacterDialogueColor(Color.red);
    }

    public void ApplyReferences()
    {
        for (int i = 0; i < m_References.Count; i++)
        {
            var r = m_References[i];
            for (int j = 0; j < m_CharacterDialogueList.Count; j++)
            {
                var d = m_CharacterDialogueList[j];
                if (d.m_ID == r.m_ID)
                {
                    d.m_CharacterImg = r.m_DialogueSprite;
                }
            }
        }
    }

    public void CreateCharacter(string idName, string longName, string shortName)
    {
        c = new CharacterComponent();
        d = new DialogueCharacterComponent();
        r = new CharacterReferences();

        c.m_ID = new ID(idName);
        c.m_ShortName = shortName;
        c.m_FullName = longName;

        d.m_ID = new ID(idName);

        r.m_ID = new ID(idName);
    }

    public void SetDescription(string description)
    {
        c.m_Description = description;
    }

    public void SetCharacterDialogueColor(Color color)
    {
        d.m_NameColor = color;

        m_CharactersList.Add(c);
        m_CharacterDialogueList.Add(d);
        m_References.Add(r);
    }

    [System.Serializable]
    public class CharacterReferences
    {
        public ID m_ID;
        public string m_CharacterName;
        public Sprite m_DialogueSprite;
    }

    public int ID(string name)
    {
        return name.GetHashCode();
    }
}
