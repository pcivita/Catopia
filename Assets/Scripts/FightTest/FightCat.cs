using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    [SerializeField] LayerMask slotMask;
    private FightSlot slot;
    private Vector2 previousPosition;
    public CatSO _catSO;
    
    // TODO: Grab Incoming Data
    public int attack;
    public int health;
    private TMP_Text attackText;
    private TMP_Text healthText;
    
    // TODO: setup Friend Logic
    public bool isFriend = true;
    
    public void InitEnemy(CatSO catSO)
    {
        _catSO = catSO;
        attack = _catSO.Strength;
        health = _catSO.Health;
        isFriend = false;
    }
  
    void Start()
    {
        gameObject.AddComponent<CircleCollider2D>().radius = clickRadius;
        // Get our Texts:
        TMP_Text[] textMeshes = gameObject.GetComponentsInChildren<TMP_Text>();
        
        if (textMeshes.Length >= 2)
        {
            attackText = textMeshes[0];
            healthText = textMeshes[1];
        }
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        attackText.text = "Attack: " + attack;
        healthText.text = "Health: " + health;
        
    }

    void OnMouseDrag()
    {
        if (isFriend)
        {
        transform.position = GetMouseWorldPos() + offset;
        }
    }
    
    private void OnMouseDown()
    {
        if (isFriend)
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
        if (isFriend)
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
                else
                {
                    inSlot = true;
                    break;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
