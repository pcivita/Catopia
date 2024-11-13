using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems; // Allows to create

public class Draggable : MonoBehaviour
{
    private Vector3 offset;

    void OnMouseDown()
    {
        // Calculate the offset between the mouse position and the object's position
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        // Update the object's position to follow the mouse, keeping the offset
        transform.position = GetMouseWorldPos() + offset;
    }

    private Vector3 GetMouseWorldPos()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePoint = Input.mousePosition;

        // Convert screen coordinates to world coordinates
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
}
