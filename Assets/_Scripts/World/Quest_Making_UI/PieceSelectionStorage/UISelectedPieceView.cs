using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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

            if (_image.sprite != sprite)
            {
                _image.transform.DOKill();
                _image.sprite = sprite;
                _image.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                _image.transform.DOScale(1.0f, 1.0f).SetEase(Ease.OutElastic);
            }

            _nameTextComp.text = name;
            _descTextComp.text = description;

            if (_convince.text != convince.ToString())
            {
                _convince.text = convince.ToString();
                _convince.transform.DOScale(2.0f, 0.15f).OnComplete(() => _convince.transform.DOScale(1.0f, 0.15f));
            }
            if (_help.text != help.ToString())
            {
                _help.text = help.ToString();
                _help.transform.DOScale(2.0f, 0.15f).OnComplete(() => _help.transform.DOScale(1.0f, 0.15f));
            }
            if (_harm.text != harm.ToString())
            {
                _harm.text = harm.ToString();
                _harm.transform.DOScale(2.0f, 0.15f).OnComplete(() => _harm.transform.DOScale(1.0f, 0.15f));
            }
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