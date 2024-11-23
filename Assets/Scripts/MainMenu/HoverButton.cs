using UnityEngine;
using UnityEngine.EventSystems; // Required for pointer events
using TMPro;
using UnityEngine.SceneManagement; // For TextMeshPro, or UnityEngine.UI for standard UI Text

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText; // Assign your TextMeshPro or UI Text component here

    public Color myColor;
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = Color.black;
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}