using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class MissionPopupBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMesh;
    [SerializeField] private Image m_Image;

    public void Initialize(PopupData_MissionStarted popData)
    {
        m_TextMesh.text = popData.m_MissionTitle;
        transform.DOScale(1.0f, 0.7f).ChangeStartValue(Vector3.zero).SetEase(Ease.OutBounce);
        StartCoroutine(Lifetime(popData.m_TimeAlive));
    }

    public IEnumerator Lifetime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        m_TextMesh.DOColor(new Color(1, 1, 1, 0f), 0.5f);
        m_Image.DOColor(new Color(1, 1, 1, 0f), 0.5f).OnComplete(() => Destroy(gameObject));
    }
}