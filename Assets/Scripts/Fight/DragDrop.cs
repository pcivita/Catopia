using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    public void onPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }
}
