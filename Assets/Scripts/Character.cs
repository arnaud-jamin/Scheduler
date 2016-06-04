using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------
    private Rigidbody m_rigidbody;
    private float m_colorTime;

    //-----------------------------------------------------------------------------------------
    [SerializeField]
    private float m_speed = 5;

    [SerializeField]
    private float m_grip = 0.2f;

    [SerializeField]
    private Vector2 m_moveInput = Vector2.zero;

    [SerializeField]
    private Renderer m_renderer = null;

    [SerializeField]
    private Color m_normalColor = Color.white;

    [SerializeField]
    private Color m_updateColor = Color.white;

    [SerializeField]
    private float m_colorChangeDuration = 1.0f;

    //-----------------------------------------------------------------------------------------
    public Vector2 MoveInput { get { return m_moveInput; } set { m_moveInput = value; } }

    //-----------------------------------------------------------------------------------------
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    //-----------------------------------------------------------------------------------------
    void Update()
    {
        var input = MathHelper.FilterInput(m_moveInput, 0.25f, 2.0f);

        var desiredVelocity = new Vector3(input.x, 0, input.y) * m_speed;
        var force = (desiredVelocity - m_rigidbody.velocity) * m_grip;
        force.y = 0;
        m_rigidbody.AddForce(force, ForceMode.VelocityChange);

        m_renderer.material.color = Color.Lerp(m_normalColor, m_updateColor, m_colorTime);
        m_colorTime = Math.Max(0, m_colorTime - Time.deltaTime);
    }

    //-----------------------------------------------------------------------------------------
    public void ChangeColor()
    {
        m_colorTime = m_colorChangeDuration;
    }
}
