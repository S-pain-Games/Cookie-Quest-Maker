using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CQM.UI.QuestMakingTable
{
    [Serializable]
    public class UISelectedPieceView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameTextComp;
        [SerializeField] private TextMeshProUGUI _descTextComp;

        [SerializeField] private TextMeshProUGUI _convince;
        [SerializeField] private TextMeshProUGUI _help;
        [SerializeField] private TextMeshProUGUI _harm;


        public void UpdateUI(Sprite sprite, string name, string description, int convince, int help, int harm)
        {
            if (!_gameObject.activeInHierarchy)
                _gameObject.SetActive(true);

            _image.color = Color.white;
            _image.sprite = sprite;
            _nameTextComp.text = name;
            _descTextComp.text = description;

            _convince.text = convince.ToString();
            _help.text = help.ToString();
            _harm.text = harm.ToString();
        }

        public void Clear()
        {
            _gameObject.SetActive(false);
            _image.sprite = null;
            _image.color = new Color(0, 0, 0, 0);
            _nameTextComp.text = "";
            _descTextComp.text = "";
        }
    }
}