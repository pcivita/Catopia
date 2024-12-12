using UnityEngine;

public class TooltipGO : MonoBehaviour
{
    public GameObject tooltip;
    void OnMouseEnter()
    {
        Debug.Log("Mouse entered the object.");
        // Change the object's material color to indicate hover
        tooltip.SetActive(true);
       
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse exited the object.");
        // Revert the material color
        tooltip.SetActive(false);
      
    }
}