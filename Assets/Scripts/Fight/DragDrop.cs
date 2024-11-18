using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform; // For UI objects
    private Vector3 offset; // Offset to maintain the correct dragging position
    private Camera mainCamera; // Reference to the main camera

    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");

        // For world-space objects, calculate the offset
        if (rectTransform == null)
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
            offset = transform.position - mouseWorldPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform != null)
        {
            // For UI objects
            rectTransform.anchoredPosition += eventData.delta;
        }
        else
        {
            // For world-space objects
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
            transform.position = mouseWorldPosition + offset;
        }
    }
}