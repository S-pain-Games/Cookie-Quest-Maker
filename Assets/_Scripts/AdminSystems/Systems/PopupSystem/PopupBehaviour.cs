using System.Collections;
using UnityEngine;
using TMPro;

public class PopupBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_textMesh;

    public void Initialize(PopupData popData)
    {
        m_textMesh.text = popData.m_Text;
        StartCoroutine(Lifetime(popData.m_TimeAlive));
    }

    public IEnumerator Lifetime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}