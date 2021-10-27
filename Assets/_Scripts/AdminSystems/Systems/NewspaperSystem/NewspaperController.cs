using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * NOT COMPLETE
 */
public class NewspaperController : MonoBehaviour
{
    // TODO: Convert this into system
    [SerializeField] private TextMeshProUGUI _mainTitleText;
    [SerializeField] private List<TextMeshPro> _articleTitlesText = new List<TextMeshPro>();

    [SerializeField] private NewspaperData data = new NewspaperData();

    public void UpdateTextWithData()
    {
        _mainTitleText.text = data.MainTitle;

        for (int i = 0; i < data.Articles.Count; i++)
        {
            _articleTitlesText[i].text = data.Articles[i].content;
        }
    }
}
