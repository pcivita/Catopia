using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

// TODO: Investigate bug where a cat seems to be locked in the slot. 
public class Cat : MonoBehaviour
{
    public CatSO _catSO;
    [SerializeField] float clickRadius;
    private Vector2 offset;
    
    public bool inArea = false;
    private bool hovering = false;
    private bool dragging = false;
    public bool inConquer = false;
    public string currArea = "None";
    
    public SpriteRenderer bodySR;
    public SpriteRenderer lineSR;
    public SpriteRenderer patternSR;
    public SpriteRenderer accessorySR;
    
    public GameObject abilityIcon;
    public GameObject visibleAbilityIcon;
    public GameObject hoverAbilityIcon;
    Squishable squishable;
    public TMP_Text catText;
    public Canvas uiCanvas;
    public Canvas statsCanvas;
    public TMP_Text strengthText;
    public TMP_Text healthText;
    public TMP_Text huntingText;
    public TMP_Text abilityText;
    public TMP_Text abilityNameText;
    public TMP_Text nameText;
    public TMP_Text hoverStrengthText;
    public TMP_Text hoverHealthText;
    public TMP_Text hoverHuntingText;


    private AreaController area;
    [SerializeField] LayerMask areaMask;

    Vector3 wanderTarget;
    Vector3 wanderMin;
    Vector3 wanderMax;
    
    public void Init(CatSO catSO)
    {
        squishable = GetComponentInChildren<Squishable>();
        _catSO = catSO;
        gameObject.AddComponent<CircleCollider2D>().radius = clickRadius;
    }

    private void Start()
    {
        // For visual purposes
        uiCanvas.worldCamera = Camera.main;
        uiCanvas.gameObject.SetActive(false);
        statsCanvas.gameObject.SetActive(true);
        patternSR.sprite = _catSO.Pattern;
        accessorySR.sprite = _catSO.Accessory;
        patternSR.color = _catSO.patternColor;
        bodySR.color = _catSO.bodyColor;

        abilityIcon.GetComponent<SpriteRenderer>().sprite = GetAbility().icon;
        visibleAbilityIcon.GetComponent<SpriteRenderer>().sprite = GetAbility().icon;
        hoverAbilityIcon.GetComponent<Image>().sprite = GetAbility().icon;
        abilityNameText.text = GetAbility().GetName();
        abilityText.text = GetAbility().GetDescription();
        nameText.text = _catSO.CatName;
        UpdateStats();
        // DontDestroyOnLoad(gameObject);
    }

    public void SetWanderBounds(Vector3 boundMin, Vector3 boundMax)
    {
        StopAllCoroutines();
        wanderMin = boundMin;
        wanderMax = boundMax;
        StartCoroutine(Wander());
    }

    void WanderClamp()
    {
        wanderTarget.x = Mathf.Clamp(wanderTarget.x, wanderMin.x+0.5f, wanderMax.x-0.5f);
        wanderTarget.y = Mathf.Clamp(wanderTarget.y, wanderMin.y+0.5f, wanderMax.y-0.5f);
    }

    IEnumerator Wander()
    {
        while (true)
        {
            wanderTarget = transform.position + new Vector3(UnityEngine.Random.RandomRange(-2f, 2f), UnityEngine.Random.Range(-2f, 2f), 0);
            WanderClamp();
            yield return new WaitForSeconds(UnityEngine.Random.RandomRange(5f, 10f));
        }
    }

    private void Update()
    {
        //Vector3 dir = Vector3.Normalize(wanderTarget - transform.position) * Time.deltaTime;
        if (!inConquer && !hovering)
        {
            
        
        Vector3 dir = (wanderTarget - transform.position) * Time.deltaTime *0.5f;
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
        hovering = true;
        squishable.OnMouseEnter();
        if (!dragging) DisplayFullStats();
        
    }

    private void OnMouseExit()
    {
        hovering = false;
        uiCanvas.gameObject.SetActive(false);
        statsCanvas.gameObject.SetActive(true);
    }

    public void UpdateStats()
    {
        strengthText.text = GetStrengthPlusBuffs().ToString();
        healthText.text = GetHealthPlusBuffs().ToString();
        huntingText.text = GetHuntingPlusBuffs().ToString();
    }


    private void DisplayFullStats(){
        statsCanvas.gameObject.SetActive(false);
        uiCanvas.gameObject.SetActive(true);

        strengthText.text = GetStrengthPlusBuffs().ToString();
        healthText.text = GetHealthPlusBuffs().ToString();
        huntingText.text = GetHuntingPlusBuffs().ToString();

        hoverStrengthText.text = GetStrengthPlusBuffs().ToString();
        hoverHealthText.text = GetHealthPlusBuffs().ToString();
        hoverHuntingText.text = GetHuntingPlusBuffs().ToString();
    }

    public int GetStrengthPlusBuffs() {
        int baseStrength = _catSO.Strength;
        // Debug.Log($"Strength Buff For {_catSO.CatName} Is {_catSO.Ability.GetStrengthBuff(this)}");
        return baseStrength + _catSO.Ability.GetStrengthBuff(this);
    }

    public int GetHealthPlusBuffs() {
        int baseHealth = _catSO.Health;
        // Debug.Log($"Health Buff For {_catSO.CatName} Is {_catSO.Ability.GetHealthBuff(this)}");
        return baseHealth + _catSO.Ability.GetHealthBuff(this);
    }

    public int GetHuntingPlusBuffs() {
        int baseHunting = _catSO.Hunting;
        return baseHunting + _catSO.Ability.GetHuntingBuff(this);
    }

    // Doesn't deal with overlapping Cats
    private void OnMouseDown()
    {
        squishable.OnMouseDown();
        dragging = true;
        statsCanvas.gameObject.SetActive(true);
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
        wanderTarget = transform.position;
        WanderClamp();

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
        WanderClamp();
    }

    // Todo: Animation Triggers etc. 
    private void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
    public void Train() 
    {
        GameObject trainObject = GameObject.Find("Train");
        TrainArea trainArea = trainObject.GetComponent<TrainArea>();

        IncreaseStat(trainArea.getCurrentTraining(), 4);
    
        GameObject conquerObject = GameObject.Find("Conquer");
        ConquerArea conquerArea = conquerObject.GetComponent<ConquerArea>();
        foreach (var cat in conquerArea.GetCats()) 
        {
            if (cat.GetAbility().IsActive(cat)) 
            {
                if (cat.GetAbility().GetName() == "Pillar Of Strength")
                {
                    IncreaseStat("Health", trainArea.GetNumCats());
                    IncreaseStat("Hunting", trainArea.GetNumCats());
                    IncreaseStat("Strength", trainArea.GetNumCats());
                }
            }
        }
        GameManager.gameState.TryConsumeFood(1);
    }

    public void IncreaseStat(string trainType, int trainAmount) 
    {
        int mult = 1;
        if (GetAbility().GetName() == "I Go To The Gym By Myself" && GetAbility().IsActive(this))
        {
            mult = 3;
        }

        if (trainType == "Health")
        {
            _catSO.Health += trainAmount * mult;
        } else if (trainType == "Hunting")
        {
            _catSO.Hunting += trainAmount * mult;
        }
        else if (trainType == "Strength")
        {
            _catSO.Strength += trainAmount * mult;
        }
    }
    
    public void ConsumeFood()
    {
        if (GameManager.gameState.TryConsumeFood(1))
        {
            Debug.Log(_catSO.CatName + " consumed food");
        }
        else
        {
            Debug.Log(_catSO.CatName + " didn't eat");
            // TODO: Should Cat die?
        }
    }
    public void NewDay(bool fight)
    {
        this.ConsumeFood();

        if (!fight)
        {
            this.ResetPosition();
            GameManager.instance.CatDefaultWander(this);
        }
    }
    //TODO: implement. this will be used at the start of a new day.
    public void ResetPosition()
    {
        transform.position = Vector2.zero;
        inArea = false;
        inConquer = false;
        currArea = "None";
    }

    public Ability GetAbility()
    {
        return _catSO.Ability;
    }

    public string GetName()
    {
        return _catSO.CatName;
    }

    public CatSO GetCatSO()
    {
        return _catSO;
    }

    public string GetCurrArea()
    {
        return currArea;
    }
}


