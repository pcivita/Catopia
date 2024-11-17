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
            wanderTarget = new Vector3(UnityEngine.Random.RandomRange(wanderMin.x, wanderMax.x), UnityEngine.Random.Range(wanderMin.y, wanderMax.y), 0);
            yield return new WaitForSeconds(UnityEngine.Random.RandomRange(2f, 5f));
        }
    }

    private void Update()
    {
        //Vector3 dir = Vector3.Normalize(wanderTarget - transform.position) * Time.deltaTime;
        Vector3 dir = (wanderTarget - transform.position) * Time.deltaTime;
        transform.position += dir;
        bool flip = dir.x > 0;
        bodySR.flipX = flip;
        lineSR.flipX = flip;
        patternSR.flipX = flip;
        accessorySR.flipX = flip;
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
        catText.text = "Name:" + _catSO.CatName +
            "\nHealth: " + _catSO.Health +
            "\nHunting: " + _catSO.Hunting;
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

    //TODO: implement. this will be used at the start of a new day.
    public void ResetPosition()
    {
        transform.position = Vector2.zero;
        inArea = false;
    }
}


