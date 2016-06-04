using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------
    private const float m_measurePeriod = 1.0f;

    //-----------------------------------------------------------------------------------------
    private float m_nextMeasureTime;
    private int m_frameRateAccumulator;
    private int m_maxSchedulerUpdates;
    private int m_minSchedulerUpdates;

    //-----------------------------------------------------------------------------------------
    [SerializeField]
    private Text m_frameRateText = null;

    [SerializeField]
    private Text m_instancesText = null;

    [SerializeField]
    private Text m_schedulerUpdatesText = null;

    //-----------------------------------------------------------------------------------------
    public void Process()
    {
        var gameManager = GameManager.Instance;

        m_frameRateAccumulator++;
        m_minSchedulerUpdates = Mathf.Min(m_minSchedulerUpdates, gameManager.NavigationManager.Scheduler.UpdateCount);
        m_maxSchedulerUpdates = Mathf.Max(m_maxSchedulerUpdates, gameManager.NavigationManager.Scheduler.UpdateCount);

        m_nextMeasureTime -= Time.deltaTime;

        m_instancesText.text = string.Format("Instances: {0}", GameManager.Instance.SpawnManager.Instances.Count);

        if (m_nextMeasureTime <= 0)
        {
            m_frameRateText.text = string.Format("FPS: {0}", (int)(m_frameRateAccumulator / m_measurePeriod));
            m_frameRateAccumulator = 0;

            m_schedulerUpdatesText.text = string.Format("Updates Per Frame: [{0}, {1}]", m_minSchedulerUpdates, m_maxSchedulerUpdates);

            m_maxSchedulerUpdates = 0;
            m_minSchedulerUpdates = int.MaxValue;

            m_nextMeasureTime += m_measurePeriod;
        }
    }
}
