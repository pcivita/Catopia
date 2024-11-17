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

    private void Update()
    {
       
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


