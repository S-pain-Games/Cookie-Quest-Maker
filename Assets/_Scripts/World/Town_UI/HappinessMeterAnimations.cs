using UnityEngine;
using DG.Tweening;
using TMPro;

namespace CQM.UI.Town
{
    public class HappinessMeterAnimations : MonoBehaviour
    {
        [SerializeField] private RectTransform _slider;
        [SerializeField] private TextMeshProUGUI _text;

        public void EnableForBuilding(Vector3 buildingPos, int buildingHappiness, string buildingName)
        {
            UpdateSlider(buildingHappiness);
            _text.text = buildingName;

            Vector3 pos = buildingPos;
            pos.y -= 160.0f;
            transform.position = pos;
            transform.DOScale(1.0F, 0.3F).ChangeStartValue(new Vector3(0.5f, 0.5f, 0.5f)).SetEase(Ease.OutBounce);
        }

        public void Hide()
        {
            transform.position = new Vector3(-2000, -2000, -2000);
        }

        public void UpdateSlider(int newValue)
        {
            Vector2 newPos = _slider.anchoredPosition;
            newPos.x = newValue;
            _slider.DOAnchorPos(newPos, 0.3f);
        }
    }
}