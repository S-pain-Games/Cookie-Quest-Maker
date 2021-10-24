using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI CharacterName;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private string[] sentences;

    private string ID_Dialogue;

    private int indexText;
}
