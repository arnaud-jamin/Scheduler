using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------
    [SerializeField]
    private Scheduler m_scheduler = null;

    //-----------------------------------------------------------------------------------------
    public Scheduler Scheduler { get { return m_scheduler; } }

    //-----------------------------------------------------------------------------------------
    public void Process()
    {
        m_scheduler.Process();
    }
}
