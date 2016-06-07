using System.Collections;
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
    private float m_averageCost;
    private bool m_repeat = true;

    //-----------------------------------------------------------------------------------------
    [SerializeField]
    private Text m_frameRateText = null;

    [SerializeField]
    private Text m_instancesText = null;

    [SerializeField]
    private Text m_minMaxTasksPerFrameText = null;

    [SerializeField]
    private Text m_averageCostPerFrameText = null;

    [SerializeField]
    private Slider m_intervalSlider = null;

    [SerializeField]
    private Text m_intervalSliderText = null;

    [SerializeField]
    private Slider m_taskCostSlider = null;

    [SerializeField]
    private Text m_taskCostSliderText = null;

    [SerializeField]
    private PointerListener m_createInstanceButton = null;

    [SerializeField]
    private PointerListener m_destroyInstanceButton = null;

    //-----------------------------------------------------------------------------------------
    void OnEnable()
    {
        m_intervalSlider.value = GameManager.Instance.NavigationManager.Scheduler.UpdateInterval;
        m_intervalSlider.onValueChanged.AddListener(OnIntervalValueChanged);
        RefreshIntervalText();

        m_taskCostSlider.value = GameManager.Instance.NavigationManager.FakeTaskCostInMilliseconds;
        m_taskCostSlider.onValueChanged.AddListener(OnTaskCostValueChanged);
        RefreshTaskCostText();
    }

    //-----------------------------------------------------------------------------------------
    void OnDisable()
    {
        m_intervalSlider.onValueChanged.RemoveListener(OnIntervalValueChanged);
        m_taskCostSlider.onValueChanged.RemoveListener(OnTaskCostValueChanged);
    }

    //-----------------------------------------------------------------------------------------
    public void Process()
    {
        var gameManager = GameManager.Instance;

        m_frameRateAccumulator++;
        m_minSchedulerUpdates = Mathf.Min(m_minSchedulerUpdates, gameManager.NavigationManager.Scheduler.UpdateCount);
        m_maxSchedulerUpdates = Mathf.Max(m_maxSchedulerUpdates, gameManager.NavigationManager.Scheduler.UpdateCount);
        m_averageCost += gameManager.NavigationManager.TotalCostInMilliseconds;

        m_nextMeasureTime -= Time.deltaTime;

        m_instancesText.text = string.Format("Instances: {0}", GameManager.Instance.SpawnManager.Instances.Count);

        if (m_nextMeasureTime <= 0)
        {
            m_minMaxTasksPerFrameText.text = string.Format("Min Max #Tasks Per Frame: [{0}, {1}]", m_minSchedulerUpdates, m_maxSchedulerUpdates);

            m_maxSchedulerUpdates = 0;
            m_minSchedulerUpdates = int.MaxValue;

            m_averageCostPerFrameText.text = string.Format("Average Cost Per Frame: {0:F2}ms", m_averageCost / m_frameRateAccumulator);
            m_averageCost = 0;

            m_frameRateText.text = string.Format("FPS: {0}", (int)(m_frameRateAccumulator / m_measurePeriod));
            m_frameRateAccumulator = 0;

            m_nextMeasureTime += m_measurePeriod;
        }

        if (m_createInstanceButton.IsHeld && m_repeat)
        {
            gameManager.SpawnManager.Spawn();
            StartCoroutine(WaitAndRepeat());
        }

        if (m_destroyInstanceButton.IsHeld && m_repeat)
        {
            gameManager.SpawnManager.Unspawn();
            StartCoroutine(WaitAndRepeat());
        }
    }

    //-----------------------------------------------------------------------------------------
    private void OnIntervalValueChanged(float value)
    {
        GameManager.Instance.NavigationManager.Scheduler.UpdateInterval = value;
        RefreshIntervalText();
    }

    //-----------------------------------------------------------------------------------------
    private void RefreshIntervalText()
    {
        m_intervalSliderText.text = string.Format("{0:F2}s", GameManager.Instance.NavigationManager.Scheduler.UpdateInterval);;
    }

    //-----------------------------------------------------------------------------------------
    private void OnTaskCostValueChanged(float value)
    {
        GameManager.Instance.NavigationManager.FakeTaskCostInMilliseconds = value;
        RefreshTaskCostText();
    }

    //-----------------------------------------------------------------------------------------
    private void RefreshTaskCostText()
    {
        m_taskCostSliderText.text = string.Format("{0:F2}ms", GameManager.Instance.NavigationManager.FakeTaskCostInMilliseconds);
    }

    //-----------------------------------------------------------------------------------------
    IEnumerator WaitAndRepeat()
    {
        m_repeat = false;
        yield return new WaitForSeconds(0.05f);
        m_repeat = true;
    }
}
