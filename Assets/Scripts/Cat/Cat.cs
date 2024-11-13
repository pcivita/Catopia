using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Investigate bug where a cat seems to be locked in the slot. 
public class Cat : MonoBehaviour
{
    private CatSO _catSO;
    [SerializeField] float clickRadius;
    private Vector2 offset;
    public bool inArea = false;
    private SpriteRenderer spriteRenderer;
    private AreaController area;
    [SerializeField] LayerMask areaMask;
    
    public void Init(CatSO catSO)
    {
        _catSO = catSO;
        gameObject.AddComponent<CircleCollider2D>().radius = clickRadius;
    }

    private void Start()
    {
        // For visual purposes
        gameObject.AddComponent<SpriteRenderer>().sprite = _catSO.Sprite;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 10;
        
    }

    private void Update()
    {
       
    }
    
    // Doesn't deal with overlapping Cats
    private void OnMouseDown()
    { 
        offset = (Vector2)transform.position - GetMouseWorldPos();
        if (inArea)
        {
            area.RemoveCat(this);
        }
        // get OG pos
    }
    
    void OnMouseDrag()
    {
        // Update the object's position to follow the mouse, keeping the offset
        transform.position = GetMouseWorldPos() + offset;
    }
    
    private Vector2 GetMouseWorldPos()
    {
        // Get the mouse position in screen coordinates
        Vector2 mousePoint = Input.mousePosition;

        // Convert screen coordinates to world coordinates
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        var hits = Physics2D.OverlapCircleAll(GetMouseWorldPos(), clickRadius, areaMask);
        if (hits.Length <= 0) return;
        
        foreach (var hit in hits)
        {
            area = hit.transform.root.GetComponent<AreaController>();
            if (area == null) continue;
            if (!area.TryReceiveCat(this))
            {
                // Todo: Return Cat
                SetPosition(Vector2.zero);
            }
            break;
        }
    }

    // Todo: Animation Triggers etc. 
    private void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
}

// Data of cats in a separate class in the shop.

// Buying one of those translates into an initializer for the cat!