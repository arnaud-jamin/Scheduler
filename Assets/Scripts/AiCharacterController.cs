using System.Diagnostics;
using UnityEngine;

public class AiCharacterController : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------
    private Stopwatch m_stopwatch = new Stopwatch();
    private float m_changePathfindTime = 0;
    private Vector3 m_pathfindResult = Vector3.zero;

    //---------------------------------------------------------------------------------------------
    [SerializeField]
    private Character m_character = null;

    [SerializeField]
    private float m_changePathfindResultDelay = 0.5f;

    //---------------------------------------------------------------------------------------------
    public void OnEnable()
    {
        GameManager.Instance.NavigationManager.Scheduler.Register(Pathfind);
    }

    //---------------------------------------------------------------------------------------------
    public void OnDisable()
    {
        GameManager.Instance.NavigationManager.Scheduler.Unregister(Pathfind);
    }

    //---------------------------------------------------------------------------------------------
    public void Update()
    {
        m_character.MoveInput = m_pathfindResult;
    }

    //---------------------------------------------------------------------------------------------
    private void Pathfind()
    {
        // Fake the pathfind gave the same or a different result.
        if (Time.time - m_changePathfindTime > m_changePathfindResultDelay)
        {
            var angle = Random.Range(0, 2 * Mathf.PI);
            m_pathfindResult = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            m_changePathfindTime = Time.time;
        }

        m_character.ChangeColor();

        // Fake task taking more time
        m_stopwatch.Start();
        while (m_stopwatch.Elapsed.TotalMilliseconds < GameManager.Instance.NavigationManager.FakeTaskCostInMilliseconds)
        {
        }
        m_stopwatch.Reset();
    }
}
