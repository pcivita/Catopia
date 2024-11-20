using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCat : MonoBehaviour
{
    
    // Strength 
    // Health
    // Name
    // Ability (?)

    [SerializeField] float clickRadius;
    private Vector2 offset;
    private bool inSlot;
    public bool isFriend;
    [SerializeField] LayerMask slotMask;
    private FightSlot slot;
    private Vector2 previousPosition;
    
    // Mouse Behavior similar to Cat
    
    // Slots are taken care of by BattleManager
    
    // Start is called before the first frame update
    void Start()
    {
        // Get Our Collider
        gameObject.AddComponent<CircleCollider2D>().radius = clickRadius;
    }
    
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }
    
    private void OnMouseDown()
    {
        previousPosition = transform.position;
        offset = (Vector2)transform.position - GetMouseWorldPos();
        // Remove Cat
        if (inSlot)
        {
            Debug.Log("REMOVE SLOT");
            
            slot.isOccupied = false;
            inSlot = false;
        }
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
        var hits = Physics2D.OverlapCircleAll(GetMouseWorldPos(), clickRadius, slotMask);
        if (hits.Length <= 0) return;
        
        foreach (var hit in hits)
        {
            slot = hit.transform.root.GetComponent<FightSlot>();
            if (slot == null) continue;
            if (!slot.TryAddCat(this))
            {
                // Return Cat to OG Position
                transform.position = previousPosition;
                inSlot = false;
                
            }
            else { 
                inSlot = true;
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
