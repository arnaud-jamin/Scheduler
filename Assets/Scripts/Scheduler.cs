using System;
using UnityEngine;
using System.Collections.Generic;

public class Scheduler : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------
    private List<Action> m_items = new List<Action>();
    private int m_index = 0;
    private float m_updateAccumulator = 0;
    private int m_updateCount = 0;

    //-----------------------------------------------------------------------------------------
    [SerializeField]
    [Tooltip("The interval in seconds at which the objects are processed")]
    private float m_updateInterval = 0.5f;

    //-----------------------------------------------------------------------------------------
    public int UpdateCount { get { return m_updateCount; } }

    public float UpdateInterval { get { return m_updateInterval; } set { m_updateInterval = value; } }

    //-----------------------------------------------------------------------------------------
    public void Register(Action action)
    {
        m_items.Add(action);
    }

    //-----------------------------------------------------------------------------------------
    public void Unregister(Action action)
    {
        m_items.Remove(action);
    }

    //-----------------------------------------------------------------------------------------
    public void Process()
    {
        if ((Time.deltaTime == 0) || (Time.timeScale == 0))
            return;

        var updateInterval = Mathf.Max(m_updateInterval, 0.0001f);
        var updatesPerSecond = (m_items.Count / updateInterval);
        var updatesPerFrame = updatesPerSecond * Time.deltaTime;

        m_updateAccumulator += updatesPerFrame;
        m_updateCount = Mathf.Min(Mathf.FloorToInt(m_updateAccumulator), m_items.Count);
        m_updateAccumulator -= m_updateCount;

        for (int i = 0; i < m_updateCount; ++i)
        {
            m_index = (m_index + 1) % m_items.Count;

            var action = m_items[m_index];
            action();
        }
    }
}