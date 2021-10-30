using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Camera _cam;

    [SerializeField]
    private float m_MovSpeed = 5.0f;
    [SerializeField]
    private float m_MinMovDistance = 0.1f;
    [SerializeField]
    private Vector2 m_MovDir = Vector2.zero;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 movDir = (Vector2)_cam.ScreenToWorldPoint(Input.mousePosition) - _rb.position;
            if (movDir.magnitude >= m_MinMovDistance)
                m_MovDir = movDir.normalized;
            else
                m_MovDir = Vector2.zero;
        }
        else
        {
            m_MovDir = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = m_MovDir * m_MovSpeed;
    }
}
