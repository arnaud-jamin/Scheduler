using UnityEngine;

public class AiCharacterController : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------
    [SerializeField]
    private Character m_character = null;

    //---------------------------------------------------------------------------------------------
    public void OnEnable()
    {
        GameManager.Instance.NavigationManager.Scheduler.Register(Navigate);
    }

    //---------------------------------------------------------------------------------------------
    public void OnDisable()
    {
        GameManager.Instance.NavigationManager.Scheduler.Unregister(Navigate);
    }

    //---------------------------------------------------------------------------------------------
    public void Navigate()
    {
        var angle = Random.Range(0, 2 * Mathf.PI);
        m_character.MoveInput = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        m_character.ChangeColor();
    }
}
