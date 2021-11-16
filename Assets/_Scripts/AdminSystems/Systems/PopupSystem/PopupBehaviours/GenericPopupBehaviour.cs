using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenericPopupBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_textMesh;

    public void Initialize(PopupData_GenericPopup popData)
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
