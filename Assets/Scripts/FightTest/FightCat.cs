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

    public SpriteRenderer bodySR;
    public SpriteRenderer lineSR;
    public SpriteRenderer patternSR;
    public SpriteRenderer accessorySR;

    public void Init(CatSO catSO, bool friend)
    {
        _catSO = catSO;

        patternSR.sprite = _catSO.Pattern;
        accessorySR.sprite = _catSO.Accessory;
        patternSR.color = _catSO.patternColor;
        bodySR.color = _catSO.bodyColor;

        attack = _catSO.Strength;
        health = _catSO.Health;
        isFriend = friend;
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
        if (isFriend  && !BattleManager.instance.inBattle)
        {
        transform.position = GetMouseWorldPos() + offset;
        }
    }
    
    private void OnMouseDown()
    {
        if (isFriend  && !BattleManager.instance.inBattle)
        {
            previousPosition = transform.position;
            offset = (Vector2)transform.position - GetMouseWorldPos();
            // Remove Cat
            if (inSlot)
            {
                Debug.Log("REMOVE SLOT");
                slot.fightCat = null;
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
        if (isFriend && !BattleManager.instance.inBattle)
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

            BattleManager.instance.CheckButtonCondition();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Shake(Transform target, float duration = 0.5f, float magnitude = 0.1f, float randomness = 1f)
    {
        Vector3 originalPosition = target.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate offsets with Perlin noise using a unique seed (randomness) for smoother, different patterns
            float xOffset = (Mathf.PerlinNoise(Time.time * 2f + randomness, 0f) - 0.5f) * 2f * magnitude;
            float yOffset = (Mathf.PerlinNoise(0f, Time.time * 2f + randomness) - 0.5f) * 2f * magnitude;

            target.localPosition = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        target.localPosition = originalPosition;
    }

}
