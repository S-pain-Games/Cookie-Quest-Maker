using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieStatsTriagle : MonoBehaviour
{
    [SerializeField] private RectTransform top;
    [SerializeField] private RectTransform botRight;
    [SerializeField] private RectTransform botLeft;

    private MeshRenderer mr;
    private MeshFilter mf;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mf = GetComponent<MeshFilter>();
    }

    [MethodButton]
    private void UpdateTriangle()
    {
        Mesh m = mf.mesh;
        m.Clear();

        m.vertices = new Vector3[] { botLeft.localPosition, top.localPosition, botRight.localPosition, };
        m.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        m.triangles = new int[] { 0, 1, 2 };
    }
}
