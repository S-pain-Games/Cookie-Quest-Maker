using System.Collections;
using UnityEngine;
using TMPro;

public class MissionPopupBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_textMesh;

    public void Initialize(PopupData_MissionStarted popData)
    {
        m_textMesh.text = popData.m_MissionTitle;
        StartCoroutine(Lifetime(popData.m_TimeAlive));
    }

    public IEnumerator Lifetime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}