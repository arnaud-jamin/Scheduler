using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    //-----------------------------------------------------------------------------------------
    [SerializeField]
    private NavigationManager m_navigationManager = null;

    [SerializeField]
    private SpawnManager m_spawnManager = null;

    [SerializeField]
    private UiManager m_uiManager = null;

    //-----------------------------------------------------------------------------------------
    public NavigationManager NavigationManager { get { return m_navigationManager; } }

    public SpawnManager SpawnManager { get { return m_spawnManager; } }

    //-----------------------------------------------------------------------------------------
    void Update()
    {
        m_navigationManager.Process();
        m_spawnManager.Process();
        m_uiManager.Process();
    }
}
