using UnityEngine;
using TMPro;

public class UIStorageElement : MonoBehaviour
{
    public RectTransform m_RectTransf;
    public string elemName = "Unnamed";

    private TextMeshProUGUI _textComp;

    private void Awake()
    {
        _textComp = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Build()
    {
        _textComp.text = elemName;
    }
}