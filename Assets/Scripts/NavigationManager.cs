using System.Diagnostics;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------
    private Stopwatch m_stopwatch = new Stopwatch();
    private float m_totalCostInMilliseconds = 0;

    //-----------------------------------------------------------------------------------------
    [SerializeField]
    private Scheduler m_scheduler = null;

    [SerializeField]
    private float m_fakeTaskCostInMilliseconds = 0;

    //-----------------------------------------------------------------------------------------
    public float FakeTaskCostInMilliseconds { get { return m_fakeTaskCostInMilliseconds; } set { m_fakeTaskCostInMilliseconds = value; } }

    public float TotalCostInMilliseconds { get { return m_totalCostInMilliseconds; } }

    //-----------------------------------------------------------------------------------------
    public Scheduler Scheduler { get { return m_scheduler; } }

    //-----------------------------------------------------------------------------------------
    public void Process()
    {
        m_stopwatch.Start();
        m_scheduler.Process();
        m_totalCostInMilliseconds = (float)m_stopwatch.Elapsed.TotalMilliseconds;
        m_stopwatch.Reset();
    }
}
