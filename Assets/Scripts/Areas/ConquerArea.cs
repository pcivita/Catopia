using System;
using TMPro;
using UnityEngine;
using System.Linq;

public class ConquerArea : AreaController
{
    [SerializeField]
    private Transform[] catSlots; // Backing field

    private int catCapacity = 3;
    private TMP_Text catTextMesh;

    private void Start()
    {
        areaName = "Conquer";
        TMP_Text[] textMeshes = gameObject.GetComponentsInChildren<TMP_Text>();
        
        if (textMeshes.Length >= 1)
        {
            catTextMesh = textMeshes[0];
        }
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        Debug.Log("UPDATING CAPACITY");
        int nonNullCatCount = _cats.Count(cat => cat != null);
        catTextMesh.text = "Cat: " + nonNullCatCount + "/" + catCapacity;
    }
    public override void AddCat(Cat cat) // This tries first
    {
        int availableSlot = GetFirstAvailableSlot();
        if (availableSlot == -1)
        {
            Debug.Log("No available slot");
        }
        else
        {
            // Ensure the list has enough space for the new cat
            if (availableSlot >= _cats.Count)
            {
                _cats.Add(cat);
            }
            else
            {
                _cats[availableSlot] = cat;
            }
            cat.inArea = true;
            cat.inConquer = true;
            cat.currArea = areaName;
            Debug.Log(availableSlot);
            cat.transform.position = catSlots[availableSlot].position;
        }
        UpdateTexts();

        
    }

    private int GetFirstAvailableSlot()
    {
        for (int i = 0; i < catSlots.Length; i++)
        {
            if (i >= _cats.Count || _cats[i] == null)
            {
                return i;
            }
        }
        return -1; // No available slots
    }
    
    public override void RemoveCat(Cat cat)
    {
        int index = _cats.IndexOf(cat);
        if (index != -1)
        {
            _cats[index] = null; // Clear the slot
            cat.inArea = false;
            cat.inConquer = false;
            cat.currArea = "None";
            Debug.Log($"Cat removed from slot {index}");
        }
        UpdateTexts();
    }
    
    public override void UpdateAreaState(Cat cat, bool addingCat)
    {

        Debug.Log("RUIN THIS IS THE CAT");
        UpdateTexts();
    }

    public override void NewDay()
    {
        Debug.Log("NEW DAY CONQUER");
    }
}