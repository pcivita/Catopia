using UnityEngine;
using UnityEngine.EventSystems; // Required for IPointerEnterHandler and IPointerExitHandler

public class TooltipUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
    }

    // This method is called when the pointer (mouse) exits the UI element's area
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}