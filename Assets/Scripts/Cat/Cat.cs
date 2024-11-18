using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// TODO: Investigate bug where a cat seems to be locked in the slot. 
public class Cat : MonoBehaviour
{
    public CatSO _catSO;
    [SerializeField] float clickRadius;
    private Vector2 offset;
    
    public bool inArea = false;
    private bool dragging = false;
    public bool inConquer = false;
    public string currArea = "None";
    
    public SpriteRenderer bodySR;
    public SpriteRenderer lineSR;
    public SpriteRenderer patternSR;
    public SpriteRenderer accessorySR;
    public TMP_Text catText;
    public Canvas uiCanvas;


    private AreaController area;
    [SerializeField] LayerMask areaMask;

    Vector3 wanderTarget;
    Vector3 wanderMin;
    Vector3 wanderMax;
    
    public void Init(CatSO catSO)
    {
        _catSO = catSO;
        gameObject.AddComponent<CircleCollider2D>().radius = clickRadius;
    }

    private void Start()
    {
        // For visual purposes
        uiCanvas.worldCamera = Camera.main;
        uiCanvas.gameObject.SetActive(false);
        patternSR.sprite = _catSO.Pattern;
        accessorySR.sprite = _catSO.Accessory;
        patternSR.color = _catSO.patternColor;
        bodySR.color = _catSO.bodyColor;
    }

    public void SetWanderBounds(Vector3 boundMin, Vector3 boundMax)
    {
        StopAllCoroutines();
        wanderMin = boundMin;
        wanderMax = boundMax;
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        while (true)
        {
            wanderTarget = new Vector3(UnityEngine.Random.Range(wanderMin.x, wanderMax.x), UnityEngine.Random.Range(wanderMin.y, wanderMax.y), 0);
            yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 6f));
        }
    }

    private void Update()
    {
        //Vector3 dir = Vector3.Normalize(wanderTarget - transform.position) * Time.deltaTime;
        if (!inConquer)
        {
            
        
        Vector3 dir = (wanderTarget - transform.position) * Time.deltaTime;
        transform.position += dir;
        bool flip = dir.x > 0;
        bodySR.flipX = flip;
        lineSR.flipX = flip;
        patternSR.flipX = flip;
        accessorySR.flipX = flip;
        }
     }
        

    private void OnMouseEnter()
    {
        if (!dragging) DisplayStats();
    }

    private void OnMouseExit()
    {
        uiCanvas.gameObject.SetActive(false);
    }


    private void DisplayStats(){
        uiCanvas.gameObject.SetActive(true);

        catText.fontSize = 36;
        catText.enableWordWrapping = true;
        // catText.alignment = TMPro.TextAlignmentOptions.TopLeft;

        string firstLine = "Name:" + _catSO.CatName;
        string fullText = firstLine + 
            "\n\nStrength: " + GetStrength() +
            "\nHealth: " + GetHealth() +
            "\nHunting: " + _catSO.Hunting;
            // "\nAbility: " + _catSO.Ability.abilityName + " - " + _catSO.Ability.description;
        
        // catText.text = firstLine;
        // float newWidth = catText.preferredWidth;

        catText.text = fullText; 

        // float maxWrapWidth = newWidth * 0.8f;
        // catText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWrapWidth);

        // float newHeight = catText.preferredHeight;

        // GameObject panel = uiCanvas.transform.Find("Panel").gameObject;
        // RectTransform panelRect = panel.GetComponent<RectTransform>();
        // panelRect.sizeDelta = new Vector2(newWidth, newHeight);
        // panelRect.anchorMin = new Vector2(0f, 1f);
        // panelRect.anchorMax = new Vector2(0f, 1f);
        // panelRect.pivot = new Vector2(0f, 1f);

        // catText.rectTransform.anchorMin = new Vector2(0f, 1f);
        // catText.rectTransform.anchorMax = new Vector2(0f, 1f);

        // catText.rectTransform.anchoredPosition = new Vector2(newWidth * 0.1f, newWidth * 0.1f);
    }

    private int GetStrength() {
        int baseStrength = _catSO.Strength;
        Debug.Log($"Strength Buff For {_catSO.CatName} Is {_catSO.Ability.GetStrengthBuff(this)}");
        return baseStrength + _catSO.Ability.GetStrengthBuff(this);
    }

    private int GetHealth() {
        int baseHealth = _catSO.Health;
        Debug.Log($"Health Buff For {_catSO.CatName} Is {_catSO.Ability.GetHealthBuff(this)}");
        return baseHealth + _catSO.Ability.GetHealthBuff(this);
    }

    // Doesn't deal with overlapping Cats
    private void OnMouseDown()
    {
        dragging = true;
        uiCanvas.gameObject.SetActive(false);
        Debug.Log(_catSO.CatName);
        offset = (Vector2)transform.position - GetMouseWorldPos();
        // Remove Cat
        if (inArea)
        {
            area.RemoveCat(this);
        }
    }
    
    void OnMouseDrag()
    {
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
        dragging = false;
        var hits = Physics2D.OverlapCircleAll(GetMouseWorldPos(), clickRadius, areaMask);
        if (hits.Length <= 0) return;
        
        foreach (var hit in hits)
        {
            area = hit.transform.root.GetComponent<AreaController>();
            if (area == null) continue;
            area.AddCat(this);
            break;
        }
    }

    // Todo: Animation Triggers etc. 
    private void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
    public void Train(string trainType, int trainAmount) 
    {
     
        if (trainType == "Health")
        {
            _catSO.Health += trainAmount;
        } else if (trainType == "Hunting")
        {
            _catSO.Hunting += trainAmount;
        }
        else if (trainType == "Strength")
        {
            _catSO.Strength += trainAmount;
        }
    }
    
    public void ConsumeFood()
    {
        if (GameManager.instance.gameState.TryConsumeFood(1))
        {
            Debug.Log(_catSO.CatName + " consumed food");
        }
        else
        {
            Debug.Log(_catSO.CatName + " didn't eat");
            // TODO: Should Cat die?
        }
    }
    public void NewDay()
    {
        this.ResetPosition();
        this.ConsumeFood();
        GameManager.instance.CatDefaultWander(this);
    }
    //TODO: implement. this will be used at the start of a new day.
    public void ResetPosition()
    {
        transform.position = Vector2.zero;
        inArea = false;
        inConquer = false;
        currArea = "None";
    }
}


