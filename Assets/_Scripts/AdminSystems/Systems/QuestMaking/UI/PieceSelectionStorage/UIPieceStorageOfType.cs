using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the UI that shows all the available pieces of the selected type
public class UIPieceStorageOfType : MonoBehaviour
{
    [SerializeField]
    private GameObject elementPrefab;

    private RectTransform rectTransf;
    private List<UIStorageElement> m_Elements = new List<UIStorageElement>();

    private void Awake()
    {
        rectTransf = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        Refresh(QuestPiece.PieceType.Cookie);
    }

    public void Refresh(QuestPiece.PieceType pieceType)
    {
        for (int i = m_Elements.Count - 1; i >= 0; i--)
        {
            Destroy(m_Elements[i].gameObject);
        }
        m_Elements.Clear();

        var list = Admin.g_Instance.playerPieceStorage.m_Storage;
        Vector3 pos = rectTransf.position;
        pos.y += 150;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].m_Type == pieceType)
            {
                pos += new Vector3(250, 0, 0);
                var elem = Instantiate(elementPrefab, pos, Quaternion.identity, transform).GetComponent<UIStorageElement>();
                elem.elemName = list[i].m_PieceName;
                elem.Build();
                m_Elements.Add(elem);
            }
        }
    }
}
