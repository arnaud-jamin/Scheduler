using UnityEngine;
using UnityEngine.EventSystems;

public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    //-----------------------------------------------------------------------------------------
    private bool m_isHeld = false;
    private bool m_isHovered = false;

    //-----------------------------------------------------------------------------------------
    public bool IsHeld {  get { return m_isHeld; } }
    public bool IsHovered {  get { return m_isHovered; } }

    //-----------------------------------------------------------------------------------------
    public void OnPointerDown(PointerEventData eventData)
    {
        m_isHeld = true;
    }

    //-----------------------------------------------------------------------------------------
    public void OnPointerUp(PointerEventData eventData)
    {
        m_isHeld = false;
    }

    //-----------------------------------------------------------------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_isHovered = true;
    }

    //-----------------------------------------------------------------------------------------
    public void OnPointerExit(PointerEventData eventData)
    {
        m_isHovered = false;
    }
}
