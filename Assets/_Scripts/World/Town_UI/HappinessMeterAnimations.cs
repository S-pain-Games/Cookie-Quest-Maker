using UnityEngine;
using DG.Tweening;

namespace CQM.UI.Town
{
    public class HappinessMeterAnimations : MonoBehaviour
    {
        public void EnableForBuilding(Vector3 buildingPos)
        {
            Vector3 pos = buildingPos;
            pos.y -= 200.0f;
            transform.position = pos;
            transform.DOScale(1.0F, 0.3F).ChangeStartValue(new Vector3(0.5f, 0.5f, 0.5f)).SetEase(Ease.OutBounce);
        }

        public void Hide()
        {
            transform.position = new Vector3(-2000, -2000, -2000);
        }
    }
}