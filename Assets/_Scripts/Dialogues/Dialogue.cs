using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI CharacterName;

    [SerializeField]
    private TextMeshProUGUI textDialogue;

    [SerializeField]
    private string[] sentences;

    private string ID_Dialogue;

    private int indexText = 0;

    public void showDialogue(string[] sentencesToShow, string name)
    {
        sentences = sentencesToShow;
        CharacterName.text = name;

        textDialogue.text = string.Empty;
        textDialogue.text = sentences[indexText];
    }

    public void nextLine()
    {
        if (indexText < sentences.Length - 1)
        {
            textDialogue.text = string.Empty;
            indexText++;
            textDialogue.text = sentences[indexText];
        }
        else
        {
            gameObject.SetActive(false);
            indexText = 0;
        }
    }
}
